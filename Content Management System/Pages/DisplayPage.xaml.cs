using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public DisplayPage(string role)
        {
            InitializeComponent();
            CheckUserRole(role);
            DataContext = this;
            LoadData();
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

        private void LoadData()
        {
            // Deserialize XML file into a List<Driver>
            List<Driver> drivers = DeserializeDrivers("Drivers.xml");

            // Set the ItemsSource of the DataGrid to the list of drivers
            DriversDataGrid.Items.Clear();
            DriversDataGrid.ItemsSource = drivers;
        }

        private List<Driver> DeserializeDrivers(string fileName)
        {
            List<Driver> drivers = new List<Driver>();
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Driver>));
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                {
                    drivers = (List<Driver>)serializer.Deserialize(fileStream);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("XML file not found.");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while deserializing XML: " + ex.Message);
            }
            return drivers;
        }
    }
}
