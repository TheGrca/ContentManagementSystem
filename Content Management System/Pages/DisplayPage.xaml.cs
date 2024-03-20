using System;
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
    public partial class DisplayPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Driver> Drivers { get; set; }
        MainWindow mainWindow;
        
        public DisplayPage(string role)
        {
            InitializeComponent();
            CheckUserRole(role);
            if(Drivers == null)
                DeleteDriverButton.IsEnabled = false;
            else
                DeleteDriverButton.IsEnabled = true;
            
            
            mainWindow = (MainWindow)Application.Current.MainWindow;
            Drivers = mainWindow.Drivers;

            DataContext = this;
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

        private void AddDriverButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDriverPage());
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DeleteDriverButton_Click(object sender, RoutedEventArgs e)
        {
                foreach(Driver driver in Drivers)
                {
                    if(driver.IsSelected == true)
                    {
                        Drivers.Remove(driver);
                    }
                }
                /*  TO DO: Remove Drivers from XML file
                DataIO dataIO = new DataIO();
                dataIO.SerializeObject(driversToKeep, "drivers.xml");

                // Update the Drivers list in your application
                Drivers = driversToKeep;
                */
        }
    }
}
