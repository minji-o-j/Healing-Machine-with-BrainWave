using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ecg_ppg
{
    public partial class Form1 : Form
    {
        double Min = double.MaxValue;
        double Max = double.MinValue;
        double Min2 = double.MaxValue;
        double Max2 = double.MinValue;
        double[] ecg = new double[100000];
        double[] ppg = new double[100000];
        private int ecgLength;
        private int ppgLength;

        private int Tick = 0;
        private bool isTimerOn = true;

        public Form1()
        {
            InitializeComponent();
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
            ecgppgRead();
            chart1.Series["ecg"].BorderWidth = 1;
            chart1.Series["ppg"].Color = Color.Red;

            for (int x = 0; x < ecgLength; x += 1)
            {
                chart1.Series["ecg"].Points.AddXY((double)x, ecg[x]);
            }
            for (int x = 0; x < ppgLength; x += 1)
            {
                chart1.Series["ppg"].Points.AddXY((double)x, ppg[x]);
            }
            Controls.Add(chart1);
        }

        private void ecgppgRead()
        {
            string filename = @"C:\Users\정민지\Desktop\neutralsample.txt";
            string[] lines = System.IO.File.ReadAllLines(filename);


            string filename2 = @"C:\Users\정민지\Desktop\mentalstrainsample.txt";
            string[] lines2 = System.IO.File.ReadAllLines(filename2);

            int i = 0;
            foreach (string line in lines)
            {
                ecg[i] = Convert.ToDouble(line);
                if (ecg[i] < Min) Min = Convert.ToInt32(ecg[i]);
                if (ecg[i] > Max) Max = Convert.ToInt32(ecg[i]);
                i++;
            }
            ecgLength = i;
            Console.WriteLine("Min = {0}, Max ={1}", Min, Max);

            i = 0;
            foreach (string line in lines2)
            {
                ppg[i] = Convert.ToDouble(line);
                if (ppg[i] < Min2) Min2 = Convert.ToInt32(ppg[i]);
                if (ppg[i] > Max2) Max2 = Convert.ToInt32(ppg[i]);
                i++;
            }

            ppgLength = i;
            Console.WriteLine("Min2 = {0}, Max2 = {1}", Min2, Max2);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ecgChartSetting(Tick * 10);
            Tick += 1;
        }

        private void ecgChartSetting(int minX)
        {
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisX.Maximum = 60;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -2.0;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 2.0;

            chart1.ChartAreas["ChartArea2"].AxisX.Minimum = 0;
            chart1.ChartAreas["ChartArea2"].AxisX.Maximum = 60;
            chart1.ChartAreas["ChartArea2"].AxisY.Minimum = -2.0;
            chart1.ChartAreas["ChartArea2"].AxisY.Maximum = 2.0; ;
        }
    }
}