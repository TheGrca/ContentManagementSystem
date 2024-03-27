using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Serialization;
using Content_Management_System.Class;


namespace Content_Management_System.Pages
{
    /// <summary>
    /// Interaction logic for DisplayPage.xaml
    /// </summary>
    public partial class DisplayPage : Page
    {

        public ObservableCollection<Driver> Drivers { get; set; }
        public MainWindow mainWindow;
        private string role;
        
        public DisplayPage(string role)
        {
            InitializeComponent();
            CheckUserRole(role);
            
            
            mainWindow = (MainWindow)Application.Current.MainWindow;
            Drivers = mainWindow.Drivers;
            this.role = role;   
            DataContext = this;
        }
        private void CheckUserRole(string role)
        {
            if(role == "Visitor")
            {
                AddDriverButton.Visibility = Visibility.Hidden;
                DeleteDriverButton.Visibility = Visibility.Hidden;
                CheckBoxColumn.IsReadOnly = true;
            }
            else
            {
                AddDriverButton.Visibility = Visibility.Visible;
                DeleteDriverButton.Visibility = Visibility.Visible;
                CheckBoxColumn.IsReadOnly = false;
            }
        }
        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new LoginPage());
            }
        }
        private void AddDriverButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDriverPage());
        }
        private void DeleteDriverButton_Click(object sender, RoutedEventArgs e)
        {
            if (Drivers.Count(d => d.IsSelected) == 0)
            {
                mainWindow.ShowToastNotification(new ToastNotification("Error", "Must pick a driver/s before deleting!", Notification.Wpf.NotificationType.Error));
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        foreach (var driver in Drivers.ToList())
                        {
                            if (driver.IsSelected)
                            {
                                if (File.Exists(driver.RtfPath))
                                {
                                    File.Delete(driver.RtfPath);
                                }
                                Drivers.Remove(driver);
                                DataIO dataIO = new DataIO();
                                dataIO.SerializeObject(Drivers.ToList(), "Drivers.xml");
                            }
                        }
                        mainWindow.ShowToastNotification(new ToastNotification("Success", "Driver/s deleted successfully!", Notification.Wpf.NotificationType.Success));
                    }
                    catch (Exception)
                    {
                        mainWindow.ShowToastNotification(new ToastNotification("Error", "Error while deleting driver/s", Notification.Wpf.NotificationType.Error));
                    }
                }
            }
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Hyperlink hyperlink && hyperlink.DataContext is Driver selectedDriver)
            {
                if (role == "Visitor")
                {
                    DriverDetailsPage driverDetailsPage = new DriverDetailsPage(selectedDriver);
                    NavigationService.Navigate(driverDetailsPage);
                }
                else
                {
                    EditDriverPage editDriverPage = new EditDriverPage(selectedDriver, role);
                    NavigationService.Navigate(editDriverPage);
                }
            }

        }
    }
}
