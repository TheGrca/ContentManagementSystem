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
    /// Interaction logic for DisplayPage.xaml
    /// </summary>
    public partial class DisplayPage : Page
    {
        public DisplayPage(string role)
        {
            InitializeComponent();
            CheckUserRole(role);
        }

        private void CheckUserRole(string role)
        {
            if(role == "Visitor")
            {
                AddDriverButton.Visibility = Visibility.Hidden;
                DeleteDriverButton.Visibility = Visibility.Hidden;
            }
            else
            {
                AddDriverButton.Visibility = Visibility.Visible;
                DeleteDriverButton.Visibility = Visibility.Visible;
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
