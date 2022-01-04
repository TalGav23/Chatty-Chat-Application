using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TalMiniProject___Chatty
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        Thread t;
        public Chat()
        {
            InitializeComponent();
            t = new Thread(Waiting);
            t.Start();
        }
        private void Waiting()
        {
            byte[] data = new byte[1024];
            int bytes = MainWindow.socket.Receive(data, data.Length, 0);
            string recv = Encoding.ASCII.GetString(data, 0, bytes);
            user.Content = recv;
            user.Visibility = Visibility.Visible;
            user.Background = (Brush)new BrushConverter().ConvertFrom("#FF0E192A");
        }
        //Background="#FF0E192A"
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                Environment.Exit(0);
                Application.Current.Shutdown();
            }
        }
    }
}
