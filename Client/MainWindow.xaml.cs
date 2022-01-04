using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TalMiniProject___Chatty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Socket socket;
        public MainWindow()
        {
            InitializeComponent();
            if (!Vars.OnceConnecting)
            {
                Media.Play();
                this.IsEnabled = false;
                Thread t = new Thread(ConnectToServer);
                t.Start();
                Vars.OnceConnecting = true;
            }
        }
        public void ConnectToServer()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6969);
            while (true)
            {
                try
                {
                    socket.Connect(ipPoint);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        this.IsEnabled = true;
                        Media.Stop();
                        Media.Visibility = Visibility.Hidden;
                    })); 
                    break;
                }
                catch (Exception)
                { }
            }

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Environment.Exit(0);
                Application.Current.Shutdown();
            }
        }





        private void PassBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PassBox.Password == "")
                PassBack.Visibility = Visibility.Hidden;

        }

        private void UserBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UserBox.Text == "")
                UserBack.Visibility = Visibility.Hidden;
        }

        private void PassBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PassBack.Visibility = Visibility.Visible;

        }


        private void UserBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            UserBack.Visibility = Visibility.Visible;

        }

        private void Button_Click(object sender, RoutedEventArgs e) 
        {

        }
        int bytes;
        byte[] data;
        string recv;
        private void ChatBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            socket.Send(Encoding.ASCII.GetBytes("Login" + ";" + UserBox.Text + ";" + PassBox.Password));

            data = new byte[1024];
            bytes = socket.Receive(data, data.Length, 0);
            recv = Encoding.ASCII.GetString(data, 0, bytes);
            MessageBox.Show(recv);
            if (recv == "Sign In Failed")    // Sign In Failed
            {

            }
            if (recv == "Signed In Successfully") // Signed In Successfully
            {
                Chat chat = new Chat();
                chat.Show();
                this.Close();
            }

        }

        private void SignUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SignUp s = new SignUp();
            s.Show();
            this.Close();
        }

        private void Media_MediaEnded(object sender, RoutedEventArgs e)
        {
            Media.Stop();
            Media.Play();
        }
    }
}
