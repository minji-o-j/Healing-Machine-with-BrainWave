using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;
using System.IO;

using NeuroSky.ThinkGear;
using NeuroSky.ThinkGear.Algorithms;

namespace ERP_BNT
{
    public partial class Form1 : Form
    {
        static Connector connector;
        static HeartRateAcceleration heartRateAcceleration;
        static HeartRateRecovery heartRateRecovery;

        static byte poorSig;
        public List<string> result = new List<string>();
        public string raw_signal;
        static bool tag = false;
        public static string set_start = "mindwave connection...";
        public static string stimuli = "0";
        string oddball = "";

        Functions fs;
        System.Windows.Forms.Timer Timer1;
        public Form1()
        {
            InitializeComponent();
            fs = new Functions();

            Timer1 = new System.Windows.Forms.Timer();
            Timer1.Interval = 1500;
            Timer1.Tick += new EventHandler(Timer1_Tick);

            Timer1.Enabled = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Stop")
            {
                button1.Text = "Start";
                Timer1.Enabled = false;
                tag = true;
            }
            else
            {
                button1.Text = "Stop";
                Timer1.Enabled = true;
            }
        }

        private void Timer1_Tick(object Sender, EventArgs e)
        {
            // Set the caption to the current time.
            Random rd = new Random();
            int rn = rd.Next(0, 7);
            if (rn.Equals(0))
            {
                fs.playStimulSound();
                stimuli = "1";
            }
            else
            {
                fs.playOrdiSound();
                stimuli = "0";
            }
            richTextBox1.Text += rn;
        }

        Thread eegThread;
        private void EEG_btn_Click(object sender, EventArgs e)
        {
            eegThread = new Thread(new ThreadStart(HelloEEG));
            eegThread.Start();
        }

        private void click_btn_Click(object sender, EventArgs e)
        {
            oddball = stimuli + "\t" + "1";
        }


        public void HelloEEG()
        {

            richTextBox1.Text += "Hello EEG!";

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

        void OnEKGPersonalizationEvent(object sender, EventArgs e)
        {

            EKGPersonalizationEventArgs ekgArgs = (EKGPersonalizationEventArgs)(e);
            int status = ekgArgs.statusMessage;

            switch (status)
            {
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
        void OnDeviceConnected(object sender, EventArgs e)
        {
            Connector.DeviceEventArgs de = (Connector.DeviceEventArgs)e;

            richTextBox1.Text += "Device found on: " + de.Device.PortName;

            de.Device.DataReceived += new EventHandler(OnDataReceived);
        }

        /**
         * Called when scanning fails
         */
        void OnDeviceFail(object sender, EventArgs e)
        {
            richTextBox1.Text += "No devices found! :(\n";
        }

        /**
         * Called when each port is being validated
         */
        void OnDeviceValidating(object sender, EventArgs e)
        {
            heartRateRecovery.enableHeartRateRecovery();
        }

        /**
         * Called when data is received from a device
         */
        void OnDataReceived(object sender, EventArgs e)
        {
            //Device d = (Device)sender;
            Device.DataEventArgs de = (Device.DataEventArgs)e;
            NeuroSky.ThinkGear.DataRow[] tempDataRowArray = de.DataRowArray;

            TGParser tgParser = new TGParser();
            tgParser.Read(de.DataRowArray);

            /* Loop through new parsed data */
            for (int i = 0; i < tgParser.ParsedData.Length; i++)
            {
                if (tgParser.ParsedData[i].ContainsKey("Raw"))
                {
                    richTextBox1.Text += tgParser.ParsedData[i]["Raw"].ToString() + "\t"+ DateTime.Now.ToString("HHmmss.fff") + "\t" + oddball + "\n";
                    result.Add(DateTime.Now.ToString("HHmmss.fff") + "\t" + tgParser.ParsedData[i]["Raw"].ToString() + "\t" + oddball);
                    oddball = stimuli;
                }

                if (tgParser.ParsedData[i].ContainsKey("PoorSignal"))
                {
                    //Console.WriteLine("Time:" + tgParser.ParsedData[i]["Time"]);
                    //Console.WriteLine("PQ Value:" + tgParser.ParsedData[i]["PoorSignal"]);
                    poorSig = (byte)tgParser.ParsedData[i]["PoorSignal"];

                    //this is required because heart rate value is not returned when you have poor signal 200
                    if (poorSig != 200)
                    {
                        int recovery = heartRateRecovery.getHeartRateRecovery(0, poorSig);
                        double[] acceleration = heartRateAcceleration.getAcceleration(0, poorSig);
                    }

                }

                if (tgParser.ParsedData[i].ContainsKey("Attention"))
                {
                    //Console.WriteLine("Att Value:" + tgParser.ParsedData[i]["Attention"]);
                }

                if (tgParser.ParsedData[i].ContainsKey("Meditation"))
                {
                    //Console.WriteLine("Med Value:" + tgParser.ParsedData[i]["Meditation"]);
                }

                if (tgParser.ParsedData[i].ContainsKey("EegPowerDelta"))
                {
                    //Console.WriteLine("Delta: " + tgParser.ParsedData[i]["EegPowerDelta"]);
                }
                if (tgParser.ParsedData[i].ContainsKey("EnergyLevel"))
                {
                    //Console.WriteLine("Energy: " + tgParser.ParsedData[i]["EnergyLevel"]);
                }
                if (tgParser.ParsedData[i].ContainsKey("HeartRate"))
                {
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

                if (tgParser.ParsedData[i].ContainsKey("HeartAge"))
                {
                    //Console.WriteLine("Heart Age: " + tgParser.ParsedData[i]["HeartAge"]);
                }
                if (tgParser.ParsedData[i].ContainsKey("Positivity"))
                {
                    //Console.WriteLine("Positivity: " + tgParser.ParsedData[i]["Positivity"]);
                }
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            StreamWriter writer = File.CreateText("result_test.txt");
            for(int i =0; i < result.Count; i++)
            {
                writer.WriteLine(result[i]);
            }
            writer.Close();
        }

        private void stop_Click(object sender, EventArgs e)
        {
            eegThread.Abort();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
