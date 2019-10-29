using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace ecg_ppg
{
    public partial class Form1 : Form
    {
        double Min = double.MaxValue;
        double Max = double.MinValue;
        double[] ecg = new double[100000];
        //private int ecgLength = 1;
        public static int count = 0;
        public static double IntervalOffset = 0.617443;

        private int Tick = 0;
        private bool isTimerOn = true;

        public Form1()
        {
            InitializeComponent();

            chart1.Series["기준 이하"].BorderWidth = 1;
            StartProcess();


        }

        private void chart1_Click(object sender, EventArgs e)
        {
            if (isTimerOn)
            {
                timer1.Stop();
                isTimerOn = false;
            }
            else
            {
                timer1.Start();
                isTimerOn = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StartProcess()
        {
            Thread th = new Thread(ReadBWNDraw);
            th.Start();
        }
        private void ReadBWNDraw()
        {
            //Thread.Sleep(10000);
            for (int j = 0; j <= 60; j++)
            {

                string filename = @"C:\Users\정민지\Desktop\changedata\" + count + ".txt";
                string[] lines = System.IO.File.ReadAllLines(filename);
                count++;

                ecg[0] = Convert.ToDouble(lines[0]);
                int i = 0;
                foreach (string line in lines)
                {
                    ecg[i] = Convert.ToDouble(line);
                    if (ecg[i] < Min) Min = Convert.ToInt32(ecg[i]);
                    if (ecg[i] > Max) Max = Convert.ToInt32(ecg[i]);
                    //i++;
                }
                //ecgLength = i;
                Console.WriteLine("Min = {0}, Max ={1}", Min, Max);

                //for (int x = 0; x < ecgLength; x += 1)
                //{
                this.Invoke(new Action(delegate ()
                {
                    if(ecg[0]> IntervalOffset)
                    {
                        chart1.Series["기준 이상"].Color = Color.Red;
                        chart1.Series["기준 이상"].Points.AddXY((double)count, ecg[0]);

                    }
                    else if(ecg[0]<=IntervalOffset)
                    {
                        chart1.Series["기준 이하"].Color = Color.Blue;
                        chart1.Series["기준 이하"].Points.AddXY((double)count, ecg[0]);
                    }
                }));
                Thread.Sleep(1000);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ecgChartSetting(Tick * 10);
            Tick += 1;
        }

        private void ecgChartSetting(int minX)
        {
            for (int k = 0; k < count; k++)
            {
              chart1.ChartAreas["ChartArea1"].AxisX.Minimum = k-5;
              chart1.ChartAreas["ChartArea1"].AxisX.Maximum = k+2;
              chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -3;
              chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 3;
              chart1.ChartAreas["ChartArea1"].AxisY.Crossing = 0.617443;

            }
           // chart1.Series[0].Points.AddXY(1, 2);
            //chart1.Series[0].Points.AddXY(2, 20);
           /* chart1.ChartAreas[0].AxisY.StripLines.Add(
            new StripLine()
            {
                //BackColor=Color.Aqua,
                BorderColor = Color.Red,
                IntervalOffset = 0.617443,*/
            //StripWidth=3,
            // Text = "the..."
           // });
        }
    }

   

}