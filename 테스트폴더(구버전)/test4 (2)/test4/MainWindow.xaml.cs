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


namespace test4
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        //중립영상
        public static int second1 = 30;
        public static ArrayList FTData1 = new ArrayList();
        public static double OneSecFFT1 = 0.0;

        public static int count = 2; //받아오는 파일

        //혐오영상
        public static int second2 = 30;
        public static ArrayList FTData2 = new ArrayList();
        public static double OneSecFFT2 = 0.0;
        public static int count1 = 47;

        //변화량
        public static ArrayList ChData = new ArrayList();
        public static ArrayList user_data = new ArrayList();

        //기준
        //public static double ct = 0.617443;
        public static double ct = 0.0;
        public static double ct_data = 0.0;
        public static int count3 = 1;
        public static ArrayList arr_ct = new ArrayList();

        public static int count2 = 0;
        public static int count_choice = 0;
        public static int count4 = 0;

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
                string path = @"C:\Users\정민지\Desktop\DB\" + count + ".txt";
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
            ChData = new ArrayList();
            while (FTData2.Count < second2)
            {
                string path = @"C:\Users\정민지\Desktop\DB\" + count1 + ".txt";
                count1++;

                FileInfo fi = new FileInfo(path);
                string RData = "0.1";
                if (fi.Exists)
                {
                    RData = System.IO.File.ReadAllText(path);
                    //fi.Delete();
                    OneSecFFT2 = double.Parse(RData);
                    FTData2.Add(OneSecFFT2);

                    //변화량
                    double data1 = (double)FTData1[count4];
                    count4++;
                    double sum = 0;
                    sum = (OneSecFFT2 - data1) / data1;
                    ChData.Add(sum);

                    string makeFolder_path = @"C:\Users\정민지\Desktop\";
                    string folderName = makeFolder_path + "changedata";
                    DirectoryInfo f = new DirectoryInfo(folderName);
                    f.Create();
                    StreamWriter wr = new StreamWriter(@"C:\Users\정민지\Desktop\changedata\" + count2 + ".txt");
                    count2++;
                    double val1 = sum;
                    wr.WriteLine(val1);
                    wr.Close();

                    Thread.Sleep(800);

                  //  Form1 chart = new Form1();
                  //  chart.Show();
                }
            }
            user_data.Add(arr_avr(ChData));
            count4 = 0;

           //Form1 chart = new Form1();
         // chart.Show();

            double udata0 = (double)user_data[0];
            if (count_choice == 0)
            {
                if (udata0 > ct)
                {
                    count_choice++;
                    for (int j = 0; j < 5; j++)
                    {
                        keybd_event((byte)Keys.Q, 0, 0, 0);
                        keybd_event((byte)Keys.Q, 0, 0x02, 0);
                    }
                }
                else if (udata0 <= ct)
                {
                    Random rando = new Random();
                    int ran = rando.Next(1, 4);
                    Sound(ran);
                    Thread.Sleep(3000);

                    //새로 기준정하기
                    StreamWriter wr1 = new StreamWriter(@"C:\Users\정민지\Desktop\ct\" + count3 + ".txt");
                    double new_ct = (ct + udata0) / count3;
                    wr1.WriteLine(new_ct);
                    wr1.Close();

                    timer_c.Enabled = false;
                }
            }
            else if (count_choice != 0)
            {
                for (int i = count_choice; ; i++)
                {
                    double udata1 = (double)user_data[i - 1];
                    double udata2 = (double)user_data[i];

                    if ((udata1 > ct) && (udata2 > ct))
                    {
                        count_choice++;
                        break;
                    }
                    else if ((udata1 > ct) && (udata2 <= ct))
                    {
                        Random random = new Random();
                        int ran = random.Next(1, 4);
                        Sound(ran);
                        Thread.Sleep(3000);

                        //새로 기준정하기
                        StreamWriter wr1 = new StreamWriter(@"C:\Users\정민지\Desktop\ct\" + count3 + ".txt");
                        double new_ct = (ct + udata0) / count3;
                        wr1.WriteLine(new_ct);
                        wr1.Close();

                        timer_c.Enabled = false;
                        
                        break;
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
            WindowState = WindowState.Minimized;

            int time = 2000; // ms
            Thread.Sleep(time);
            Form1 chart = new Form1();
            chart.Show();

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

            //기준 불러오기
            for (;;)
            {
                string path_ct = @"C:\Users\정민지\Desktop\ct\" + count3 + ".txt";
                FileInfo f_ct = new FileInfo(path_ct);
                string RData_ct = "0.1";
                if (f_ct.Exists == true)
                {
                    RData_ct = System.IO.File.ReadAllText(path_ct);
                    ct_data = double.Parse(RData_ct);
                    arr_ct.Add(ct_data);
                    count3++;
                }
                else
                {
                    break;
                }
            }
            ct = arr_avr(arr_ct);
            //30초 혐오 영상
            TicTok2();

           // Form1 chart = new Form1();
           // chart.Show();
        }
    }
}