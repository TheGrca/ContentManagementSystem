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

namespace Content_Management_System.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            UsernameTextBox.Text = "Username";
            UsernameTextBox.Foreground = Brushes.LightSlateGray;
            PasswordTextBox.Text = "Password";
            PasswordTextBox.Foreground = Brushes.LightSlateGray;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateLoginData())
            {

            }

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(messageBoxResult == MessageBoxResult.Yes)
            {

            }
            else
            {
               
            }

        }

        private bool ValidateLoginData()
        {
            bool isValid = true;

            if(UsernameTextBox.Text.Trim().Equals(string.Empty) || UsernameTextBox.Text.Trim().Equals("Username")){
                isValid = false;
                UsernameErrorLabel.Content = "Username field cannot be empty!";
                UsernameTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                UsernameErrorLabel.Content = string.Empty;
                UsernameTextBox.BorderBrush = Brushes.Gray;
            }

            if (PasswordTextBox.Text.Trim().Equals(string.Empty) || PasswordTextBox.Text.Trim().Equals("Password")){
                isValid = false;
                PasswordErrorLabel.Content = "Password field cannot be empty!";
                PasswordTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                PasswordErrorLabel.Content = string.Empty;
                PasswordTextBox.BorderBrush = Brushes.Gray;
            }



            return isValid;
        }

        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text.Trim().Equals("Username"))
            {
                UsernameTextBox.Text = string.Empty;
                UsernameTextBox.Foreground = Brushes.Black;
            }
        }

        private void PasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Text.Trim().Equals("Password"))
            {
                PasswordTextBox.Text = string.Empty;
                PasswordTextBox.Foreground = Brushes.Black;
            }
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text.Trim().Equals(string.Empty))
            {
                UsernameTextBox.Text = "Username";
                UsernameTextBox.Foreground = Brushes.LightSlateGray;
            }
        }

        private void PasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Text.Trim().Equals(string.Empty))
            {
                PasswordTextBox.Text = "Password";
                PasswordTextBox.Foreground = Brushes.LightSlateGray;
            }
        }
    }
}
