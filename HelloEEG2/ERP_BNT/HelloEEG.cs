using System;

using System.Collections.Generic;
using System.Text;
using System.Threading;

using System.IO;
using System.IO.Ports;

using NeuroSky.ThinkGear;
using NeuroSky.ThinkGear.Algorithms;

namespace ERP_BNT
{
    class HelloEEG {

        static Connector connector;
        static HeartRateAcceleration heartRateAcceleration;
        static HeartRateRecovery heartRateRecovery;

        static byte poorSig;
        public static string raw_signal;
        public static string set_start = "mindwave connection..."; 

        public HelloEEG() {

            Console.WriteLine("Hello EEG!");

            // Initialize a new Connector and add event handlers
            connector = new Connector();
            connector.DeviceConnected += new EventHandler(OnDeviceConnected);
            connector.DeviceConnectFail += new EventHandler(OnDeviceFail);
            connector.DeviceValidating += new EventHandler(OnDeviceValidating);

            Connector.EKGPersonalizationEvent += new EventHandler(OnEKGPersonalizationEvent);

            heartRateRecovery = new HeartRateRecovery();
            heartRateAcceleration = new HeartRateAcceleration();

            // Scan for devices
            connector.ConnectScan("COM6");

            // Enable Energy level calculation
           // connector.StartEnergyLevel();
            //connector.GetHeartAge(90, "Neraj");

            connector.EKGstartLongTraining("Neraj");
            
            Thread.Sleep(450000);

            //Console.WriteLine(connector.GetHeartRiskAware("Neraj"));
            //Console.WriteLine(connector.GetHeartAgeStatus("Neraj"));
            //Console.WriteLine(connector.ResetHeartAge("Neraj"));

            System.Console.WriteLine("Goodbye.");

            // Close all open connections
            connector.Close();
            Environment.Exit(0);
        }

        static void OnEKGPersonalizationEvent(object sender, EventArgs e) {

            EKGPersonalizationEventArgs ekgArgs = (EKGPersonalizationEventArgs)(e);
            int status = ekgArgs.statusMessage;

            switch(status) {
                case 268:
                    string data = (string)(ekgArgs.dataMessage);
                    Console.WriteLine("Handler Message: status = MSG_EKG_IDENTIFIED " + " and username = " + data);
                    break;

                case 269:
                    Console.WriteLine("Handler Message: status = MSG_EKG_TRAINED");
   
                    connector.EKGstartDetection();
                    break;

                case 270:
                    int trainStep = (int)ekgArgs.dataMessage;
                    Console.WriteLine("Handler Message: status = MSG_EKG_TRAIN_STEP " + " and training step = " + trainStep);
                    break;

                case 271:
                    Console.WriteLine("Handler Message: status = MSG_EKG_TRAIN_TOUCH");
                    break;
            }
        }

       
        /**
         * Called when a device is connected 
         */
        static void OnDeviceConnected(object sender, EventArgs e) {
            Connector.DeviceEventArgs de = (Connector.DeviceEventArgs)e;

            Console.WriteLine("Device found on: " + de.Device.PortName);

            de.Device.DataReceived += new EventHandler(OnDataReceived);
        }

        /**
         * Called when scanning fails
         */
        static void OnDeviceFail(object sender, EventArgs e) {
            Console.WriteLine("No devices found! :(");
        }

        /**
         * Called when each port is being validated
         */ 
        static void OnDeviceValidating(object sender, EventArgs e) {
            Console.WriteLine("Validating: ");

            heartRateRecovery.enableHeartRateRecovery();
        }

        /**
         * Called when data is received from a device
         */
        static void OnDataReceived(object sender, EventArgs e) {
            //Device d = (Device)sender;
            Device.DataEventArgs de = (Device.DataEventArgs)e;
            DataRow[] tempDataRowArray = de.DataRowArray;

            TGParser tgParser = new TGParser();
            tgParser.Read(de.DataRowArray);
            
            /* Loop through new parsed data */
            for (int i = 0; i < tgParser.ParsedData.Length; i++){


                if (tgParser.ParsedData[i].ContainsKey("Raw")){
                    //Console.WriteLine("Raw Value:" + tgParser.ParsedData[i]["Raw"]);
                    raw_signal = tgParser.ParsedData[i]["Raw"].ToString();
                }
                
                if (tgParser.ParsedData[i].ContainsKey("PoorSignal")){
                    //Console.WriteLine("Time:" + tgParser.ParsedData[i]["Time"]);
                    //Console.WriteLine("PQ Value:" + tgParser.ParsedData[i]["PoorSignal"]);
                    poorSig = (byte)tgParser.ParsedData[i]["PoorSignal"];

                    //this is required because heart rate value is not returned when you have poor signal 200
                    if (poorSig != 200) {
                        int recovery = heartRateRecovery.getHeartRateRecovery(0, poorSig);
                        double[] acceleration = heartRateAcceleration.getAcceleration(0, poorSig);
                    }

                }

                if (tgParser.ParsedData[i].ContainsKey("Attention")) {
                    //Console.WriteLine("Att Value:" + tgParser.ParsedData[i]["Attention"]);
                }

                if (tgParser.ParsedData[i].ContainsKey("Meditation")) {
                    //Console.WriteLine("Med Value:" + tgParser.ParsedData[i]["Meditation"]);
                }

                if(tgParser.ParsedData[i].ContainsKey("EegPowerDelta")) {
                    //Console.WriteLine("Delta: " + tgParser.ParsedData[i]["EegPowerDelta"]);
                }
                if(tgParser.ParsedData[i].ContainsKey("EnergyLevel")) {
                   //Console.WriteLine("Energy: " + tgParser.ParsedData[i]["EnergyLevel"]);
                }
                if (tgParser.ParsedData[i].ContainsKey("HeartRate")) {
                    //Console.WriteLine("HeartRate: " + tgParser.ParsedData[i]["HeartRate"]);

                    //int recovery = heartRateRecovery.getHeartRateRecovery((int)tgParser.ParsedData[i]["HeartRate"], poorSig);
                    //double[] acceleration = heartRateAcceleration.getAcceleration((int)tgParser.ParsedData[i]["HeartRate"], poorSig);

                    //if (recovery != -501) {
                    //    Console.WriteLine("heart rate recovery: " + recovery);

                    //    heartRateRecovery.enableHeartRateRecovery();
                    //}

                    //if (acceleration[0] > 0) {
                    //    Console.WriteLine("average heart rate = " + acceleration[0] + " and heart rate acceleration = " + acceleration[1] + " beats/min^2");
                    //}

                }
                
                if (tgParser.ParsedData[i].ContainsKey("HeartAge")) {
                    //Console.WriteLine("Heart Age: " + tgParser.ParsedData[i]["HeartAge"]);
                }
                if (tgParser.ParsedData[i].ContainsKey("Positivity")) {
                    //Console.WriteLine("Positivity: " + tgParser.ParsedData[i]["Positivity"]);
                }
            }
        }
    }
}
