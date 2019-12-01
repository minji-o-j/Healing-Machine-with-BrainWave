using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;


namespace test
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        //중립영상
        public static int second1 = 10;
        public static ArrayList FTData1 = new ArrayList();
        public static double OneSecFFT1 = 0.0;

        public static int count = 2; //받아오는 파일

        //혐오영상
        public static int second2 = 10;
        public static ArrayList FTData2 = new ArrayList();
        public static double OneSecFFT2 = 0.0;
        public static int count1 = 47;

        //변화량
        public static ArrayList ChData = new ArrayList();
        public static ArrayList user_data = new ArrayList();

        //기준
        public static double ct = 0.617443;


        public static int count2 = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        //평균구하기
        public static double arr_avr(ArrayList myarr)
        {
            double total = 0;
            int count = myarr.Count;
            for (int i = 0; i < count; i++)
            {
                double arr1 = (double)myarr[i];
                total += arr1;
            }
            return total / count;
        }

        //1.30초 동안 중립 영상+노이즈 2초
        private static System.Windows.Forms.Timer timer_n;
        public static void TicTok1()
        {
            timer_n = new System.Windows.Forms.Timer() { Interval = second1 * 1000 };
            timer_n.Tick += new EventHandler(natual_data);
            timer_n.Enabled = true;
        }

        public static void natual_data(object sender, System.EventArgs e)
        {
            while (FTData1.Count < second1)
            {
                // int imsy = int.Parse(DateTime.Now.ToString("HH-mm-ss dd"));
                string path = @"C:\Users\김나혜\Desktop\DB\" + count + ".txt";
                count++;

                FileInfo fi = new FileInfo(path);
                string RData = "0.1";
                if (fi.Exists == true)
                {
                    RData = System.IO.File.ReadAllText(path);
                    // fi.Delete();

                    OneSecFFT1 = double.Parse(RData);
                    FTData1.Add(OneSecFFT1);
                }
            }
            timer_n.Enabled = false;
        }

        //2. 혐오영상 + 영상 or 음악
        private static System.Windows.Forms.Timer timer_c;
        public static void TicTok2()
        {
            timer_c = new System.Windows.Forms.Timer() { Interval = second2 * 1000 };
            timer_c.Tick += new EventHandler(choice);
            timer_c.Enabled = true;
        }

        public static void choice(object sender, System.EventArgs e)
        {
            FTData2 = new ArrayList();
            while (FTData2.Count < second2)
            {
                string path = @"C:\Users\김나혜\Desktop\DB\" + count1 + ".txt";
                count1++;

                FileInfo fi = new FileInfo(path);
                string RData = "0.1";
                if (fi.Exists == true)
                {
                    RData = System.IO.File.ReadAllText(path);
                    fi.Delete();

                    OneSecFFT2 = double.Parse(RData);
                    FTData2.Add(OneSecFFT2);
                }
            }

            //(혐오-중립)/중립 ->변화량
            for (int i = 0; i < second2; i++)
            {
                double data1 = (double)FTData1[i];
                double data2 = (double)FTData2[i];
                double sum = 0;
                sum = (data2 - data1) / data1;
                ChData.Add(sum);
            }
            user_data.Add(arr_avr(ChData)); //변화량의 평균

            //변화량 평균 텍스트 파일로
            string makeFolder_path = @"C:\Users\김나혜\Desktop\";
            string folderName = makeFolder_path + "changedata";
            DirectoryInfo f = new DirectoryInfo(folderName);
            f.Create();
            StreamWriter wr = new StreamWriter(@"C:\Users\김나혜\Desktop\changedata\" + count2 + ".txt");
            count2++;
            double val1 = arr_avr(ChData);
            wr.WriteLine(val1);
            wr.Close();

            for (int i = 0; ; i++)
            {
                if (i == 0 && (double)user_data[i] > ct)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        keybd_event((byte)Keys.Q, 0, 0, 0);
                        keybd_event((byte)Keys.Q, 0, 0x02, 0);
                    }
                }
                else if (i == 0 && (double)user_data[i] <= ct)
                {
                    Random rando = new Random();
                    int ran = rando.Next(1, 3);
                    Sound(ran);
                    Thread.Sleep(3000);
                    timer_c.Enabled = false;
                }
                else if (i > 0)
                {
                  //  double udata1 = (double)user_data[i - 1];
                    //double udata2 = (double)user_data[i];
                    if (((double)user_data[i - 1] > ct) && ((double)user_data[i] > ct))
                     {
                     }
                     else if (((double)user_data[i - 1] > ct) && ((double)user_data[i] <= ct))
                     {
                        Random rando = new Random();
                        int ran = rando.Next(1, 3);
                        Sound(ran);
                        Thread.Sleep(3000);
                        timer_c.Enabled = false;
                      }
                }

 
             }                     
        }

        //랜덤 노래 선택
        public static void Sound(int ran)
        {
            if (ran == 1)
            {
                for (int j = 0; j < 5; j++)
                {
                    keybd_event((byte)Keys.E, 0, 0, 0);
                    keybd_event((byte)Keys.E, 0, 0x02, 0);
                }

            }
            else if (ran == 2)
            {
                for (int j = 0; j < 5; j++)
                {
                    keybd_event((byte)Keys.R, 0, 0, 0);
                    keybd_event((byte)Keys.R, 0, 0x02, 0);
                }
            }
            else if (ran == 3)
            {
                for (int j = 0; j < 5; j++)
                {
                    keybd_event((byte)Keys.T, 0, 0, 0);
                    keybd_event((byte)Keys.T, 0, 0x02, 0);
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int time = 2000; // ms
            Thread.Sleep(time);

            for (int j = 0; j < 5; j++)
            {
                //뇌파 측정 시작
                keybd_event((byte)Keys.U, 0, 0, 0);
                keybd_event((byte)Keys.U, 0, 0x02, 0);
            }

            //30초 중립 상태
            TicTok1();

            int time2 = 10000;
            Thread.Sleep(time2);

            //30초 혐오 영상
            TicTok2();
        }
    }
}
