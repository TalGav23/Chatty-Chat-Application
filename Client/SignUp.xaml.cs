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
using System.Windows.Shapes;

namespace TalMiniProject___Chatty
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
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
        StringBuilder builder;
        byte[] data;
        string recv;
        private void ChatBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


        }

        private void ConfirmPassBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ConfirmPassBox.Password == "")
                ConfirmPassBack.Visibility = Visibility.Hidden;

        }

        private void ConfirmPassBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ConfirmPassBack.Visibility = Visibility.Visible;

        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ChatBtn_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (PassBox.Password == ConfirmPassBox.Password)
            {
                MainWindow.socket.Send(Encoding.ASCII.GetBytes("SignUp" + ";" + UserBox.Text + ";" + PassBox.Password));

                data = new byte[1024];
                bytes = MainWindow.socket.Receive(data, data.Length, 0);
                recv = Encoding.ASCII.GetString(data, 0, bytes);
                MessageBox.Show(recv);
            }
            else
                MessageBox.Show("The Passwords Needs Be The Same !"); 
            
            if(recv == "Account Created Successfullly") // Account Created Successfullly
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }

        private void Login_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
