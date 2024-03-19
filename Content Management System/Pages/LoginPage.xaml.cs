using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using Content_Management_System.Class;
using FontAwesome5;

namespace Content_Management_System.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        bool isPasswordHidden = true;
        public LoginPage()
        {
            InitializeComponent();
            UsernameTextBox.Text = "Username";
            UsernameTextBox.Foreground = Brushes.LightSlateGray;
            PasswordTextBox.Text = "Password";
            PasswordTextBox.Foreground = Brushes.LightSlateGray;
            

            this.DataContext = this;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEmptyLoginData())
            {
                string username = UsernameTextBox.Text;
                string password = PasswordTextBox.Text;
                try
                {
                    string[] lines = File.ReadAllLines("Users.txt");

                    foreach(string line in lines)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 3)
                        {
                            string storedUsername = parts[0];
                            string storedPassword = parts[1];
                            UserRole storedRole = (UserRole)Enum.Parse(typeof(UserRole), parts[2]);

                            if(username == storedUsername && password == storedPassword)
                            {
                                User user = new User(storedUsername, storedPassword, storedRole);
                                NavigationService.Navigate(new DisplayPage(storedRole.ToString()));
                                return;
                            }
                        }
                    }
                    PasswordErrorLabel.Content = "Invalid username or password";
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(messageBoxResult == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private bool ValidateEmptyLoginData()
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

        private void PasswordToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButtonIconOff.Visibility = Visibility.Hidden;
            ToggleButtonIconOn.Visibility = Visibility.Visible;
            isPasswordHidden = false;
        }

        private void PasswordToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButtonIconOff.Visibility = Visibility.Visible;
            ToggleButtonIconOn.Visibility = Visibility.Hidden;
            isPasswordHidden = true;
        }

        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string password = PasswordTextBox.Text;
            if (!PasswordTextBox.Text.Trim().Equals("Password") && isPasswordHidden)
            {
                int passwordCharacterCount = PasswordTextBox.Text.Length;
                string passwordText = new string('*', passwordCharacterCount);
                PasswordTextBox.Text = passwordText;
                PasswordTextBox.CaretIndex = passwordCharacterCount;
            }
            else
            {
                PasswordTextBox.Text = password;
            }

        }
    }
}
