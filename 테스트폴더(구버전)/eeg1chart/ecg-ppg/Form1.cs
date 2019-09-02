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
        double[] ecg = new double[100000];
        private int ecgLength = 1;
        public static int count = 0;

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

            chart1.Series["High Beta"].BorderWidth = 1;
            for (int i = 0; i < 10; i++)
            {
                ReadBWNDraw();
            }
        }

        /*private void ReadBrainwave()
        {

            string filename = @"C:\Users\정민지\Desktop\mentalstrainsample.txt";
            string[] lines = System.IO.File.ReadAllLines(filename);

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
        }*/

        private void ReadBWNDraw()
        {
            string filename = @"C:\Users\정민지\Desktop\DB\" + count + ".txt";
            string[] lines = System.IO.File.ReadAllLines(filename);

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

            chart1.Series["High Beta"].BorderWidth = 1;

            for (int x = 0; x < ecgLength; x += 1)
            {
                chart1.Series["High Beta"].Points.AddXY((double)x, ecg[x]);
            }
            Controls.Add(chart1);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ecgChartSetting(Tick * 10);
            Tick += 1;
        }

        private void ecgChartSetting(int minX)
        {
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisX.Maximum = 10;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0.2558;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 0.9;
        }
    }
}