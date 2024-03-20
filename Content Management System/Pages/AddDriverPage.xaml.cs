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
using System.Collections.ObjectModel;

namespace Content_Management_System.Pages
{
    /// <summary>
    /// Interaction logic for AddDriverPage.xaml
    /// </summary>
    public partial class AddDriverPage : Page
    {
        string selectedImageName;
        DataIO serializer = new DataIO();
        public AddDriverPage()
        {
            InitializeComponent();
            CharacterCounterLabel.Content = "0";

            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
           
            for(int i = 8; i<=24; i += 2)
            {
                FontSizeComboBox.Items.Add(i);
            }

            typeof(Colors).GetProperties().ToList().ForEach(f => { FontColorComboBox.Items.Add(f.Name); });

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DriverNameRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object fontWeight = DriverNameRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            BoldToggleButton.IsChecked = (fontWeight != DependencyProperty.UnsetValue) && (fontWeight.Equals(FontWeights.Bold));

            object fontItalic = DriverNameRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            ItalicToggleButton.IsChecked = ((fontItalic != DependencyProperty.UnsetValue)) && (fontItalic.Equals(FontStyles.Italic));

            object fontUnderline = DriverNameRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            UnderlineToggleButton.IsChecked = (fontUnderline != DependencyProperty.UnsetValue) && (fontUnderline.Equals(TextDecorations.Underline));

            object fontFamily = DriverNameRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            FontFamilyComboBox.SelectedItem = fontFamily;

            object fontSize = DriverNameRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty);
            FontSizeComboBox.SelectedItem = fontSize;

            object fontColor = DriverNameRichTextBox.Selection.GetPropertyValue(Inline.ForegroundProperty);
            FontColorComboBox.SelectedItem = fontColor;

            int charCount = new TextRange(DriverNameRichTextBox.Document.ContentStart, DriverNameRichTextBox.Document.ContentEnd).Text.Length;
            CharacterCounterLabel.Content = (charCount-2).ToString();
        }

        private bool ValidateEmptyFormData()
        {
            bool isValid = true;

            if (DriverNumberTextBox.Text.Trim().Equals(string.Empty))
            {
                isValid = false;
                DriverNumberErrorLabel.Content = "Number field cannot be empty!";
                DriverNumberTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                DriverNumberErrorLabel.Content = string.Empty;
                DriverNumberTextBox.BorderBrush = Brushes.Transparent;
            }

            if (CharacterCounterLabel.Content.ToString() == "0")
            {
                isValid = false;
                DriverNameErrorLabel.Content = "Name field cannot be empty!";
                DriverNameRichTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                DriverNameErrorLabel.Content = string.Empty;
                DriverNameRichTextBox.BorderBrush = Brushes.Transparent;
            }

            if(PreviewPictureBrush.ImageSource == null)
            {
                isValid = false;
                DriverPictureErrorLabel.Content = "Driver picture must be uploaded!";
                SelectPictureButton.BorderBrush = Brushes.Red;
            }
            else
            {
                DriverPictureErrorLabel.Content = string.Empty;
                DriverNameRichTextBox.BorderBrush = Brushes.Transparent;
            }

            return isValid;
        }

        private void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedImageName= System.IO.Path.GetFileName(openFileDialog.FileName);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.DecodePixelWidth = 50;
                bitmap.DecodePixelHeight = 50;
                bitmap.EndInit();
                PreviewPictureBrush.ImageSource = bitmap;
            }
        }

        private void AddDriverButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEmptyFormData())
            {
                string driverNumber = DriverNumberTextBox.Text.Trim();
                string driverName = new TextRange(DriverNameRichTextBox.Document.ContentStart, DriverNameRichTextBox.Document.ContentEnd).Text.Trim();
                string picturePath = selectedImageName;

                // Generate a unique filename for RTF document
                string rtfFileName = $"Driver_{driverNumber}_{DateTime.Now:yyyyMMddHHmmss}.rtf";

                try
                {
                    // Create and save RTF document
                    using (FileStream fileStream = new FileStream(rtfFileName, FileMode.Create))
                    {
                        TextRange textRange = new TextRange(DriverNameRichTextBox.Document.ContentStart, DriverNameRichTextBox.Document.ContentEnd);
                        textRange.Save(fileStream, DataFormats.Rtf);
                    }

                    Driver driver = new Driver(int.Parse(driverNumber), driverName, picturePath, rtfFileName, DateTime.Now);
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    // Save driver information to XML file
                    mainWindow.Drivers.Add(driver);
                    serializer.SerializeObject<ObservableCollection<Driver>>(mainWindow.Drivers, "Drivers.xml");
                    MessageBox.Show("Driver added successfully."); //TO DO: Toaster notifikacija
                    //this.NavigationService.Navigate(new Uri("Pages/DisplayPage.xaml", UriKind.RelativeOrAbsolute));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding driver: " + ex.Message); //TO DO: Toaster notifikacija
                }
            }
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem != null && !DriverNameRichTextBox.Selection.IsEmpty)
            {
                DriverNameRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FontFamilyComboBox.SelectedItem);
            }
        }

        private void FontColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedColor = FontColorComboBox.SelectedItem as string;
            if(selectedColor != null)
            {
                Color color = (Color)ColorConverter.ConvertFromString(selectedColor);
                ColorPreviewRectangle.Fill = new SolidColorBrush(color);
            }

            if (FontColorComboBox.SelectedItem != null && !DriverNameRichTextBox.Selection.IsEmpty)
            {
                Color color = (Color)ColorConverter.ConvertFromString(selectedColor);
                DriverNameRichTextBox.Selection.ApplyPropertyValue(Inline.ForegroundProperty, new SolidColorBrush(color));
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (double.TryParse(FontSizeComboBox.SelectedItem.ToString(), out double fontSize))
            {
                DriverNameRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSize);
            }
        }
    }
}
