using System;
using System.Windows;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;



namespace test5
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        [DllImport("user32.dll")]
        static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        //중립 40초
        public static int second1 = 40;
        public static ArrayList FTData1 = new ArrayList();  //중립 뇌파 받아오는
        public static double OneSecFFT1 = 0.0;
        public static int count1= 2;

        //자연영상
        public static int second2 = 48;
        public static ArrayList FTData2 = new ArrayList(); //자연 뇌파 받아오는
        public static double OneSecFFT2 = 0.0;
        public static int count2 = 42;
        public static int recount = 0;

        //변화량
        public static ArrayList ChData = new ArrayList(); //변화량
        public static int count3 = 0; //중립뇌파 비교
        public static int count4 = 0; //변화량 저장
        public static ArrayList user_data = new ArrayList();//30초 변화량의 평균


        //선택
        public static int count_choice = 0;
        public static double ct = 0.617443;

        //혐오영상
        public static int count_bad = 0;
        public static int second3 = 48;    
        public static int recount2 = 0;
        public static ArrayList user_data2 = new ArrayList();
        #endregion

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

        #region 30초 중립뇌파측정(+2초)+설명화면8초+자연영상

        private static System.Windows.Forms.Timer timer_n;
        public static void TicTok1()
        {
            timer_n = new System.Windows.Forms.Timer() { Interval = second1* 1000 };
            timer_n.Tick += new EventHandler(neutral_data);
            timer_n.Enabled = true;
        }
        public static void neutral_data(object sender, System.EventArgs e)
        {
            while (FTData1.Count < second1)
            {
                string path = @"C:\Users\정민지\Desktop\DB\" + count1 + ".txt";
                count1++;
                
                FileInfo fi = new FileInfo(path);
                string RData1 = "0.1";
                if (fi.Exists == true)
                {
                    RData1 = System.IO.File.ReadAllText(path);
                    // fi.Delete();

                    OneSecFFT1 = double.Parse(RData1);
                    FTData1.Add(OneSecFFT1);
                }
            }
            timer_n.Enabled = false;
        }
        #endregion


        #region 자연영상
        private static System.Windows.Forms.Timer timer_c;
        public static void TicTok2()
        {
            timer_c = new System.Windows.Forms.Timer() { Interval = (second1+1) * 1000 };
            timer_c.Tick += new EventHandler(choice);
            timer_c.Enabled = true;
        }
        public static void choice(object sender, System.EventArgs e)
        {
            FTData2 = new ArrayList();
            ChData = new ArrayList();
            count3 = 0;
            recount = 0;
            while (FTData2.Count < second2)
            {
                string path = @"C:\Users\정민지\Desktop\DB\" + count2 + ".txt";              

                FileInfo fi = new FileInfo(path);
                string RData2 = "0.1";
                if (fi.Exists)
                {
                    count2++;
                    RData2 = System.IO.File.ReadAllText(path);
                    OneSecFFT2 = double.Parse(RData2);                   

                    //변화량구하기
                    if (FTData2.Count < 8)
                    {
                        FTData2.Add(OneSecFFT2);
                        double data1 = (double)FTData1[count3];
                        count3++;
                        double sum = 0;
                        sum = (OneSecFFT2 - data1) / data1;
                        //ChData.Add(sum);

                        string makeFolder_path = @"C:\Users\정민지\Desktop\";
                        string folderName = makeFolder_path + "changedata";
                        DirectoryInfo f = new DirectoryInfo(folderName);
                        f.Create();
                        StreamWriter wr = new StreamWriter(@"C:\Users\정민지\Desktop\changedata\" + count4 + ".txt");
                        count4++;

                        wr.WriteLine(sum);
                        wr.Close();

                        Thread.Sleep(800);
                    }
                    else if (FTData2.Count >= 8)
                    {
                        FTData2.Add(OneSecFFT2);
                        if (recount == 0)
                        {
                            count3 = 0;
                            recount++;
                        }                    
                        double data1 = (double)FTData1[count3];
                        count3++;
                        double sum = 0;
                        sum = (OneSecFFT2 - data1) / data1;              
                        ChData.Add(sum);

                        string makeFolder_path = @"C:\Users\정민지\Desktop\";
                        string folderName = makeFolder_path + "changedata";
                        DirectoryInfo f = new DirectoryInfo(folderName);
                        f.Create();
                        StreamWriter wr = new StreamWriter(@"C:\Users\정민지\Desktop\changedata\" + count4 + ".txt");
                        count4++;

                        wr.WriteLine(sum);
                        wr.Close();

                        Thread.Sleep(800);
                    }
                }
            }
            user_data.Add(arr_avr(ChData));
            
            //영상유지 or 노래 선택하기
            double udata0 = (double)user_data[0];
            if (count_choice == 0)
            {
                if (udata0 > ct)
                {
                    count_choice++;
                }
                else if (udata0 <= ct)
               {
                    Random rando = new Random();
                    int ran = rando.Next(1, 4);
                    Sound(ran);

                    TicTok3();
                    DateTime startTime = DateTime.Now; //시작 시간
                    DateTime currentTime = DateTime.Now; //현재 시간
                    TimeSpan timeDiff = currentTime - startTime;
                    count3 = 0;
                    while (timeDiff < TimeSpan.FromSeconds(40))
                    {                 
                        string path = @"C:\Users\정민지\Desktop\DB\" + count2 + ".txt";
                        FileInfo fi = new FileInfo(path);
                        string RData2 = "0.1";
                        if (fi.Exists)
                        {
                            count2++;
                            RData2 = System.IO.File.ReadAllText(path);
                            OneSecFFT2 = double.Parse(RData2);
                            double data1 = (double)FTData1[count3];
                            count3++;
                            double sum = 0;
                            sum = (OneSecFFT2 - data1) / data1;
                            string makeFolder_path = @"C:\Users\정민지\Desktop\";
                            string folderName = makeFolder_path + "changedata";
                            DirectoryInfo f = new DirectoryInfo(folderName);
                            f.Create();
                            StreamWriter wr = new StreamWriter(@"C:\Users\정민지\Desktop\changedata\" + count4 + ".txt");
                            count4++;
                            wr.WriteLine(sum);
                            wr.Close();
                            Thread.Sleep(1000);
                        }
                        currentTime = DateTime.Now;
                        timeDiff = currentTime - startTime;
                    }                  
                    timer_c.Enabled = false;
                }
            }
            else if (count_choice != 0)
            {
                for(int i=1; ; i++)
                {
                    double udata1 = (double)user_data[i - 1];
                    double udata2 = (double)user_data[i];

                    if((udata1>ct) && (udata2 > ct))
                    {
                        count_choice++;
                        break;
                    }
                    else if ((udata1 > ct) && (udata2 <= ct))
                    {
                        Random random = new Random();
                        int ran = random.Next(1, 4);
                        Sound(ran);

                        //30~40초 쉬는게 필요함
                        TicTok3();
                        DateTime startTime = DateTime.Now; //시작 시간
                        DateTime currentTime = DateTime.Now; //현재 시간
                        TimeSpan timeDiff = currentTime - startTime;
                        count3 = 0;
                        while (timeDiff < TimeSpan.FromSeconds(40))
                        {
                            string path = @"C:\Users\정민지\Desktop\DB\" + count2 + ".txt";
                            FileInfo fi = new FileInfo(path);
                            string RData2 = "0.1";
                            if (fi.Exists)
                            {
                                count2++;
                                RData2 = System.IO.File.ReadAllText(path);
                                double data1 = (double)FTData1[count3];
                                count3++;
                                double sum = 0;
                                sum = (OneSecFFT2 - data1) / data1;
                                string makeFolder_path = @"C:\Users\정민지\Desktop\";
                                string folderName = makeFolder_path + "changedata";
                                DirectoryInfo f = new DirectoryInfo(folderName);
                                f.Create();
                                StreamWriter wr = new StreamWriter(@"C:\Users\정민지\Desktop\changedata\" + count4 + ".txt");
                                count4++;
                                wr.WriteLine(sum);
                                wr.Close();
                                Thread.Sleep(1000);
                            }
                            currentTime = DateTime.Now;
                            timeDiff = currentTime - startTime;
                        }
                        timer_c.Enabled = false;
                        break;
                    }
                }
                timer_c.Enabled = false;
            }

        }
        #endregion

        #region 혐오영상

        private static System.Windows.Forms.Timer timer_c2;
        public static void TicTok3()
        {
            timer_c2 = new System.Windows.Forms.Timer() { Interval = second1 * 1000 };
            timer_c2.Tick += new EventHandler(choice2);
            timer_c2.Enabled = true;
        }
        public static void choice2(object sender, System.EventArgs e)
        {
            if (count_bad == 0)
            {
                for (int j = 0; j < 5; j++) //혐오영상
                {
                    keybd_event((byte)Keys.Y, 0, 0, 0);
                    keybd_event((byte)Keys.Y, 0, 0x02, 0);
                }
                count_bad++;
            }
            else if (count_bad != 0)
            {
            }

            FTData2 = new ArrayList();
            ChData = new ArrayList();
            count3 = 0;
            recount2 = 0;
            while (FTData2.Count < second3)
            {
                string path = @"C:\Users\정민지\Desktop\DB\" + count2 + ".txt";               

                FileInfo fi = new FileInfo(path);
                string RData = "0.1";
                if (fi.Exists == true)
                {
                    count2++;
                    RData = System.IO.File.ReadAllText(path);
                    //fi.Delete();

                    OneSecFFT2 = double.Parse(RData);
                                                           
                    if (FTData2.Count < 8)
                    {
                        FTData2.Add(OneSecFFT2);
                        double data1 = (double)FTData1[count3];
                        count3++;
                        double sum = 0;
                        sum = (OneSecFFT2 - data1) / data1;
                        //ChData.Add(sum);
                     
                        string makeFolder_path = @"C:\Users\정민지\Desktop\";
                        string folderName = makeFolder_path + "changedata";
                        DirectoryInfo f = new DirectoryInfo(folderName);
                        f.Create();
                        StreamWriter wr = new StreamWriter(@"C:\Users\정민지\Desktop\changedata\" + count4 + ".txt");
                        count4++;
                        double val1 = sum;
                        wr.WriteLine(val1);
                        wr.Close();
                     
                        Thread.Sleep(800);
                    }
                    else if (FTData2.Count >= 8)
                    {
                        FTData2.Add(OneSecFFT2);
                        if (recount2 == 0)
                        {
                            count3 = 0;
                            recount2++;
                        }
                        double data1 = (double)FTData1[count3];
                        count3++;
                        double sum = 0;
                        sum = (OneSecFFT2 - data1) / data1;
                        ChData.Add(sum);

                        string makeFolder_path = @"C:\Users\정민지\Desktop\";
                        string folderName = makeFolder_path + "changedata";
                        DirectoryInfo f = new DirectoryInfo(folderName);
                        f.Create();
                        StreamWriter wr = new StreamWriter(@"C:\Users\정민지\Desktop\changedata\" + count4 + ".txt");
                        count4++;
                        double val1 = sum;
                        wr.WriteLine(val1);
                        wr.Close();

                        Thread.Sleep(800);
                    }
                }
            }
            user_data2.Add(arr_avr(ChData));      

            double udata0 = (double)user_data2[0];
            if (count_choice == 0)
            {
                if (udata0 > ct)
                {
                    count_choice++;
                    Random random = new Random();
                    int ran = random.Next(1, 3);
                    Image2(ran);
                }
                else if (udata0 <= ct)
                {
                    //  for (int j = 0; j < 5; j++)  //힐링되셨네요!+체험종료
                    //  {
                    //      keybd_event((byte)Keys.O, 0, 0, 0);
                    //      keybd_event((byte)Keys.O, 0, 0x02, 0);
                    //  }
                    //  Thread.Sleep(10000);
                    //
                    //  timer_c.Enabled = false;
                    //  System.Windows.Forms.Application.ExitThread();
                    //  Environment.Exit(0);
                    count_choice++;
                }
            }
            else if (count_choice != 0)
            {
                for (int i = 1; ; i++)
                {
                    double udata1 = (double)user_data2[i - 1];
                    double udata2 = (double)user_data2[i];

                    if ((udata1 > ct) && (udata2 > ct))
                    {
                        count_choice++;
                    }
                    else if ((udata1 > ct) && (udata2 <= ct))
                    {
                        for (int j = 0; j < 5; j++)  //힐링되셨네요!+체험종료
                        {
                            keybd_event((byte)Keys.O, 0, 0, 0);
                            keybd_event((byte)Keys.O, 0, 0x02, 0);
                        }
                        Thread.Sleep(10000);

                        timer_c.Enabled = false;
                        System.Windows.Forms.Application.ExitThread();
                        Environment.Exit(0);
                    }
                    else if ((udata1 <= ct) && (udata2<=ct))
                    {
                        count_choice++;
                        Random random = new Random();
                        int ran = random.Next(1, 3);
                        Image2(ran);

                        DateTime startTime = DateTime.Now; //시작 시간
                        DateTime currentTime = DateTime.Now; //현재 시간
                        TimeSpan timeDiff = currentTime - startTime;
                        count3 = 0;
                        while (timeDiff < TimeSpan.FromSeconds(40))
                        {
                            string path = @"C:\Users\정민지\Desktop\DB\" + count2 + ".txt";
                            FileInfo fi = new FileInfo(path);
                            string RData2 = "0.1";
                            if (fi.Exists)
                            {
                                count2++;
                                RData2 = System.IO.File.ReadAllText(path);
                                double data1 = (double)FTData1[count3];
                                count3++;
                                double sum = 0;
                                sum = (OneSecFFT2 - data1) / data1;
                                string makeFolder_path = @"C:\Users\정민지\Desktop\";
                                string folderName = makeFolder_path + "changedata";
                                DirectoryInfo f = new DirectoryInfo(folderName);
                                f.Create();
                                StreamWriter wr = new StreamWriter(@"C:\Users\정민지\Desktop\changedata\" + count4 + ".txt");
                                count4++;
                                wr.WriteLine(sum);
                                wr.Close();
                                Thread.Sleep(1000);
                            }
                            currentTime = DateTime.Now;
                            timeDiff = currentTime - startTime;
                        }

                        for (int j = 0; j < 5; j++)  //힐링되셨네요!+체험종료
                        {
                            keybd_event((byte)Keys.O, 0, 0, 0);
                            keybd_event((byte)Keys.O, 0, 0x02, 0);
                        }
                        Thread.Sleep(10000);

                        timer_c.Enabled = false;
                        System.Windows.Forms.Application.ExitThread();
                        Environment.Exit(0);
                    }
                    else if ((udata1 <= ct) && (udata2 > ct))
                    {
                        count_choice++;
                        Random random = new Random();
                        int ran = random.Next(1, 3);
                        Image2(ran);
                    }
                }
            }

        }
        #endregion


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

        //랜덤 영상 선택(처음)
        public static void Image1(int ran)
        {
            if (ran == 1)
            {
                for (int j = 0; j < 5; j++)
                {
                    keybd_event((byte)Keys.Q, 0, 0, 0);
                    keybd_event((byte)Keys.Q, 0, 0x02, 0);
                }
            }
            else if (ran == 2)
            {
                for (int j = 0; j < 5; j++)
                {
                    keybd_event((byte)Keys.W, 0, 0, 0);
                    keybd_event((byte)Keys.W, 0, 0x02, 0);
                }
            }
        }

        //랜덤 영상 선택(중간)
        public static void Image2(int ran)
        {
            if (ran == 1)
            {
                for (int j = 0; j < 5; j++)
                {
                    keybd_event((byte)Keys.U, 0, 0, 0);
                    keybd_event((byte)Keys.U, 0, 0x02, 0);
                }
            }
            else if (ran == 2)
            {
                for (int j = 0; j < 5; j++)
                {
                    keybd_event((byte)Keys.I, 0, 0, 0);
                    keybd_event((byte)Keys.I, 0, 0x02, 0);
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

            int time = 2000; // ms
            Thread.Sleep(time);

            Random random = new Random();
            int ran = random.Next(1, 3);
            Image1(ran);

            //30초 중립 상태
            TicTok1();

            TicTok2();

        }
    }
}