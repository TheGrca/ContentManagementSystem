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
using System.Xml.Linq;
using Microsoft.Win32;
using Content_Management_System.Class;

namespace Content_Management_System.Pages
{
    /// <summary>
    /// Interaction logic for AddDriverPage.xaml
    /// </summary>
    public partial class AddDriverPage : Page
    {
        string selectedImagePath;
        public AddDriverPage()
        {
            InitializeComponent();

            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
           
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(FontFamilyComboBox.SelectedItem != null && !DriverNameRichTextBox.Selection.IsEmpty)
            {
                DriverNameRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FontFamilyComboBox.SelectedItem);
            }
        }

        private void DriverNameRichTextBox_SelectionChanged_1(object sender, RoutedEventArgs e)
        {
            object fontWeight = DriverNameRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            BoldToggleButton.IsChecked = (fontWeight != DependencyProperty.UnsetValue) && (fontWeight.Equals(FontWeights.Bold));

            object fontItalic = DriverNameRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            ItalicToggleButton.IsChecked = ((fontItalic != DependencyProperty.UnsetValue)) && (fontItalic.Equals(FontStyles.Italic));

            object fontUnderline = DriverNameRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            UnderlineToggleButton.IsChecked = (fontUnderline != DependencyProperty.UnsetValue) && (fontUnderline.Equals(TextDecorations.Underline));

            object fontFamily = DriverNameRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            FontFamilyComboBox.SelectedItem = fontFamily;

            object fontColor = DriverNameRichTextBox.Selection.GetPropertyValue(Inline.ForegroundProperty);
            FontColorComboBox.SelectedItem = fontColor;
        }

        private bool ValidateEmptyFormData()
        {
            bool isValid = true;

            if (DriverNumberTextBox.Text.Trim().Equals(string.Empty))
            {
                isValid = false;
            }

            return isValid;
        }

        private void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedImagePath);
                bitmap.DecodePixelWidth = 50;
                bitmap.DecodePixelHeight = 50;
                bitmap.EndInit();
                PreviewImageBrush.ImageSource = bitmap;
            }
        }

        private void AddDriverButton_Click(object sender, RoutedEventArgs e)
        {
            // TO DO: Uradi validaciju
            int driverNumber = int.Parse(DriverNumberTextBox.Text.Trim());
            string driverName = new TextRange(DriverNameRichTextBox.Document.ContentStart, DriverNameRichTextBox.Document.ContentEnd).Text.Trim();
            string picturePath = selectedImagePath;

            // Generate a unique filename for RTF document
            string rtfFileName = $"Driver_{driverNumber}_{DateTime.Now:yyyyMMddHHmmss}.rtf";
            string rtfFilePath = System.IO.Path.GetFileName(rtfFileName);

            try
            {
                // Create and save RTF document
                using (FileStream fileStream = new FileStream(rtfFilePath, FileMode.Create))
                {
                    TextRange textRange = new TextRange(DriverNameRichTextBox.Document.ContentStart, DriverNameRichTextBox.Document.ContentEnd);
                    textRange.Save(fileStream, DataFormats.Rtf);
                }

                Driver driver = new Driver(driverNumber, driverName, picturePath, rtfFilePath, DateTime.Now);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.Drivers.Add(driver);
                // Save driver information to XML file
                SaveDriverToXml(driverNumber, driverName, picturePath, rtfFilePath);
                MessageBox.Show("Driver added successfully.");
                this.NavigationService.Navigate(new Uri("Pages/DisplayPage.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding driver: " + ex.Message);
            }
        }

        private void SaveDriverToXml(int driverNumber, string driverName, string picturePath, string rtfFilePath)
        {
            string xmlFilePath = "Drivers.xml";
            if (!File.Exists(xmlFilePath))
            {
                // Create a new XML document if it doesn't exist
                XDocument xmlDoc = new XDocument(new XElement("Drivers"));
                xmlDoc.Save(xmlFilePath);
            }

            // Load existing XML document
            XDocument doc = XDocument.Load(xmlFilePath);

            // Add new driver element
            XElement newDriver = new XElement("Driver",
                new XElement("Number", driverNumber),
                new XElement("Name", driverName),
                new XElement("Picture", picturePath),
                new XElement("RtfPath", rtfFilePath),
                new XElement("Date", DateTime.Now)
            );

            // Add the new driver to the XML document
            doc.Element("Drivers").Add(newDriver);

            // Save the updated XML document
            doc.Save(xmlFilePath);
        }

    }
}
