using System;
using System.Media;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using System.IO;
using System.IO.Ports;
using System.Timers;

using NeuroSky.ThinkGear;
using NeuroSky.ThinkGear.Algorithms;
using System.Runtime.InteropServices;

using System.Collections;


//using System.Wieeeeendows.Forms;

namespace HelloEEG
{
    class Program
    {
        [DllImport("coclib", CallingConvention = CallingConvention.Cdecl)]
        extern public static IntPtr detect_peaks(double[] data, int length, int sampling_rate);

        [DllImport("coclib", CallingConvention = CallingConvention.Cdecl)]
        extern public static IntPtr fft(double[] data, int length, int mag_alpha);

        [DllImport("coclib", CallingConvention = CallingConvention.Cdecl)]
        extern public static IntPtr band_pass_filter(double[] data, int length, int sampling_rate, double low_cut, double high_cut);

       // [DllImport("user32.dll")]
       // static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        static Connector connector;
        static HeartRateAcceleration heartRateAcceleration;
        static HeartRateRecovery heartRateRecovery;

        /* Loop through new parsed data */
        static ArrayList OneBuffer = new ArrayList();
        static ArrayList FiveBuffer = new ArrayList();
        static ArrayList Result = new ArrayList();

        static int time = 5;
        static double dt = 1 / 512;
        static double df = 1 / (double)time;
        static byte poorSig;

        static int count = 0;

        static double rp = 0;  // smr wave의 relative power
        static double total = 0;  // 1초 동안 나온 256개의 주파수 값을 더하여 저장하는 변수
        static double hbeta = 0;  //hbeta wave 범위에 해당하는 65~150의 주파수를 더하여 저장하는 변수

        public static void Main(string[] args)
        {
            //time = 5;
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
            connector.ConnectScan("COM5");

            // Enable Energy level calculation
            // connector.StartEnergyLevel();
            //connector.GetHeartAge(90, "Neraj");
            connector.EKGstartLongTraining("Neraj");

            Thread.Sleep(4500000);
            //Console.WriteLine(connector.GetHeartRiskAware("Neraj"));
            //Console.WriteLine(connector.GetHeartAgeStatus("Neraj"));
            //Console.WriteLine(connector.ResetHeartAge("Neraj"));

            System.Console.WriteLine("Goodbye.");

            // Close all open connections
            connector.Close();
            Environment.Exit(0);
        }

        static void OnEKGPersonalizationEvent(object sender, EventArgs e)
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
        static void OnDeviceConnected(object sender, EventArgs e)
        {
            Connector.DeviceEventArgs de = (Connector.DeviceEventArgs)e;

            Console.WriteLine("Device found on: " + de.Device.PortName);

            de.Device.DataReceived += new EventHandler(OnDataReceived);
        }

        /**
         * Called when scanning fails
         */
        static void OnDeviceFail(object sender, EventArgs e)
        {
            Console.WriteLine("No devices found! :(");
        }

        /**
         * Called when each port is being validated
         */
        static void OnDeviceValidating(object sender, EventArgs e)
        {
            Console.WriteLine("Validating: ");

            heartRateRecovery.enableHeartRateRecovery();
        }
        /**
         * Called when data is received from a device
         */




        static void OnDataReceived(object sender, EventArgs e)
        {
            //Device d = (Device)sender;
            Device.DataEventArgs de = (Device.DataEventArgs)e;
            DataRow[] tempDataRowArray = de.DataRowArray;

            TGParser tgParser = new TGParser();
            tgParser.Read(de.DataRowArray);
            
            for (int i = 0; i < tgParser.ParsedData.Length; i++)
            {
                if (tgParser.ParsedData[i].ContainsKey("Raw"))
                {
                    OneBuffer.Add(tgParser.ParsedData[i]["Raw"]);
                    FiveBuffer.Insert(0, tgParser.ParsedData[i]["Raw"]);
                    if (FiveBuffer.Count > 512)
                        FiveBuffer.RemoveAt(512);
                    if (OneBuffer.Count >= 512)
                    {
                        double[] _FiveBuffer = new double[5 * 512];
                        _FiveBuffer = FiveBuffer.ToArray(typeof(double)) as double[];
                        double[] pf = FFT(_FiveBuffer, 1000);

                        for (int j = 0; j < 256; j++)
                        {
                            if ((double)(df * j) >= 1.0 && (double)(df * j) < 58.0)  //
                            {
                                total += pf[j];
                            }
                            if ((double)(df * j) >= 20.0 && (double)(df * j) <= 30.0)   // 미간 사이에 둬야 인식이 잘 됨
                                hbeta += pf[j];
                        }
                        Console.WriteLine("High-Beta : " + hbeta);
                        Console.WriteLine("total : " + total);

                        rp = hbeta / total;  // smr wave의 relative power
                        Console.WriteLine(rp);
                        //    Result.Add(new KeyValuePair<string, double>("name", rp));  // name에는 각각의 test 이름을 넣어줄 것. 아니면 순서는 고정 값으로 정해놓고 값만 넣든지
                        hbeta = 0;
                        total = 0;
                        /*
                        string path = @"C:\Users\user\Desktop\BrainWave items\HelloEEG\HelloEEG\bin\Debug\text.txt";
                        FileInfo fi = new FileInfo(path);
                        if(fi.Exists==false)
                        {
                            System.IO.File.WriteAllText(path, rp.ToString());
                        }*/

                        // string source = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                        //string source = DateTime.Now.ToString("dd  HH-mm-ss");

                        //string makeFolder_path= @"C:\Users\김나혜\Desktop\";
                        // DirectoryInfo dir = new DirectoryInfo(@"C:\Users\김나혜\Desktop\");
                        // dir.Name = source;
                        // dir.Create();

                        /*  string makeFolder_path = @"C:\Users\김나혜\Desktop\";
                          string source = DateTime.Now.ToString("dd  HH-mm-ss");
                          string folderName = makeFolder_path + source;
                          DirectoryInfo f = new DirectoryInfo(folderName);
                          f.Create();*/

                        /*  DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Users\김나혜\Desktop\DB");
                          FileInfo fileInfo = null;
                          try
                              {
                                  if (directoryInfo.Exists)
                                  {
                                      fileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, "DB"));
                                      try
                                      {
                                          if (fileInfo.Exists)
                                          {
                                          //StreamWriter wr = new StreamWriter(fileInfo, FileMode.Append);
                                          StreamWriter wr = new StreamWriter(@"C:\Users\김나혜\Desktop\DB" + count + ".txt");
                                          count++;
                                          }
                                          else
                                          {
                                              fileInfo.Create();
                                          }
                                      }
                                  }
                                  else
                                  {
                                      directoryInfo.Create();
                                  }
                              }*/
                        //폴더 생성
                        string makeFolder_path = @"C:\Users\정민지\Desktop\";
                        string folderName = makeFolder_path + "DB";
                        DirectoryInfo f = new DirectoryInfo(folderName);
                        f.Create();

                        StreamWriter wr = new StreamWriter(@"C:\Users\정민지\Desktop\DB\" + count + ".txt");
                        count++;

                        double val = rp;
                        wr.WriteLine(val);
                        wr.Close();

                        OneBuffer.Clear();
                        break;
                    }
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

                    //if (recovery != -501)
                    //{
                    //    Console.WriteLine("heart rate recovery: " + recovery);

                    //    heartRateRecovery.enableHeartRateRecovery();
                    //}

                    //if (acceleration[0] > 0)
                    //{
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
        public static int[] DetectPeaks(double[] data, int sampling_rate)
        {
            IntPtr peakPointer = detect_peaks(data, data.Length, sampling_rate);
            int[] peakCount = new int[1];
            Marshal.Copy(peakPointer, peakCount, 0, 1);
            int[] peakCopy = new int[peakCount[0] + 1];
            int[] peakIndex = new int[peakCount[0]];
            Marshal.Copy(peakPointer, peakCopy, 0, peakCount[0] + 1);
            Array.Copy(peakCopy, 1, peakIndex, 0, peakCount[0]);
            return peakIndex;
        }

        public static double[] FFT(double[] data, int mag_alpha)
        {
            IntPtr psPointer = fft(data, data.Length, mag_alpha);
            int[] ps = new int[data.Length / 2];
            double[] outPs = new double[data.Length / 2];
            Marshal.Copy(psPointer, ps, 0, data.Length / 2);
            for (int i = 0; i < data.Length / 2; i++)
            {
                outPs[i] = ps[i] / (double)mag_alpha;
            }
            return outPs;
        }

        public static double CalculateDF(double sampling_rate, int length)
        {
            double df = sampling_rate / length;
            return df;
        }

        public static double[] BandPass(double[] data, int sampling_rate, double low_cut, double high_cut)
        {
            IntPtr bandPointer = band_pass_filter(data, data.Length, sampling_rate, low_cut, high_cut);
            int[] bandData = new int[data.Length];
            double[] outData = new double[data.Length];
            Marshal.Copy(bandPointer, bandData, 0, data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                outData[i] = bandData[i] / 1000000.0;
            }
            return outData;
        }
    }
}