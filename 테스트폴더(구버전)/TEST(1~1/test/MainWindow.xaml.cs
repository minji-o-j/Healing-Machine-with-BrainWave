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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace test
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
           // keybd_event((byte)Keys.A, 0, 0, 0);
           // keybd_event((byte)Keys.A, 0, 0x02, 0);

        }



        [DllImport("user32.dll")]
        static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SendKeys.Send("Q");
            SendKeys.SendWait("Q");
        }



        /*private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
          {
              if (e.KeyChar == (char)Keys.Enter)
              {
                  MessageBox.Show("Enter key pressed");
              }
              if (e.KeyChar == 13)
              {
                  MessageBox.Show("Enter key pressed");
              }
          }*/


        /*   private void button_Click(object sender, RoutedEventArgs e)
           {
               mu.Stop();
               mu.Open(new Uri(@"C:\Users\김나혜\Desktop\jujima.mp3"));
               mu.Play();

               //label.Text = "HELLO";
           }

           private void button_Click_1(object sender, RoutedEventArgs e)
           {
               textBox.Text = "hello world";
               button.Visibility = Visibility.Hidden;
           }
           */
    }
}
