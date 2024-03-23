using System;
using System.Collections.Generic;
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

namespace Content_Management_System.Pages
{
    /// <summary>
    /// Interaction logic for DriverDetailsPage.xaml
    /// </summary>
    public partial class DriverDetailsPage : Page
    {
        public DriverDetailsPage(Driver selectedDriver)
        {
            InitializeComponent();
            DataContext = selectedDriver;

            if (!string.IsNullOrEmpty(selectedDriver.RtfPath))
            {
                LoadRTFContent(selectedDriver.RtfPath);
            }
        }

        private void LoadRTFContent(string rtfFilePath)
        {
            try
            {
                using (FileStream fs = new FileStream(rtfFilePath, FileMode.Open))
                {
                    TextRange range = new TextRange(DriverDescriptionRichTextBox.Document.ContentStart, DriverDescriptionRichTextBox.Document.ContentEnd);
                    range.Load(fs, DataFormats.Rtf);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading RTF content: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
             NavigationService.GoBack();
        }
    }
}
