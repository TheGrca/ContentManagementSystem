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
using Content_Management_System.Class;
using Microsoft.Win32;

namespace Content_Management_System.Pages
{
    public partial class EditDriverPage : Page
    {
        string selectedImageName;
        DataIO serializer = new DataIO();
        public EditDriverPage(Driver driver)
        {
            InitializeComponent();

            DriverDescriptionRichTextBox.Loaded += DriverDescriptionRichTextBox_Loaded;

            FontSizeComboBox.SelectedIndex = 2;
            FontColorComboBox.SelectedValue = "Black";

            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            for (int i = 8; i <= 24; i += 2)
            {
                FontSizeComboBox.Items.Add(i);
            }
            typeof(Colors).GetProperties().ToList().ForEach(f => { FontColorComboBox.Items.Add(f.Name); });
            DataContext = driver;
            if (!string.IsNullOrEmpty(driver.RtfPath))
            {
                LoadRTFContent(driver.RtfPath);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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
            else if (!int.TryParse(DriverNumberTextBox.Text, out _))
            {
                isValid = false;
                DriverNumberErrorLabel.Content = "Number field must be a number!";
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
                DescriptionRichTextBoxError.Content = "Description field cannot be empty!";
                DriverDescriptionRichTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                DescriptionRichTextBoxError.Content = string.Empty;
                DriverDescriptionRichTextBox.BorderBrush = Brushes.Transparent;
            }

            if (DriverPicture.DataContext == null)
            {
                isValid = false;
                DriverPictureErrorLabel.Content = "Driver picture must be uploaded!";
                SelectPictureButton.BorderBrush = Brushes.Red;
            }
            else
            {
                DriverPictureErrorLabel.Content = string.Empty;
                SelectPictureButton.BorderBrush = Brushes.Transparent;
            }

            if (DriverNameTextBox.Text.Trim().Equals(string.Empty))
            {
                isValid = false;
                DriverNameErrorLabel.Content = "Name field cannot be empty!";
                DriverNameTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                DriverNameErrorLabel.Content = string.Empty;
                DriverNameTextBox.BorderBrush = Brushes.Transparent;
            }

            return isValid;
        }

        private void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedImageName = System.IO.Path.GetFileName(openFileDialog.FileName);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.DecodePixelWidth = 50;
                bitmap.DecodePixelHeight = 50;
                bitmap.EndInit();
                DriverPicture.Source = bitmap;
            }
        }

        private void EditDriverButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (ValidateEmptyFormData())
            {
                string driverNumber = DriverNumberTextBox.Text.Trim();
                string driverName = DriverNameTextBox.Text.Trim();
                string driverPicture = DriverPicture.Source.ToString();
                string driverDescription = new TextRange(DriverDescriptionRichTextBox.Document.ContentStart, DriverDescriptionRichTextBox.Document.ContentEnd).Text.Trim();

                try
                {
                    {
                        Driver driverToEdit = mainWindow.Drivers.FirstOrDefault(d => d.Number == int.Parse(driverNumber));
                        if (driverToEdit != null)
                        {
                            driverToEdit.Name = driverName;
                            driverToEdit.Description = driverDescription;
                            driverToEdit.Picture = driverPicture;
                            using (FileStream fileStream = new FileStream(driverToEdit.RtfPath, FileMode.Open))
                            {
                                TextRange textRange = new TextRange(DriverDescriptionRichTextBox.Document.ContentStart, DriverDescriptionRichTextBox.Document.ContentEnd);
                                textRange.Save(fileStream, DataFormats.Rtf);
                            }

                            serializer.SerializeObject<ObservableCollection<Driver>>(mainWindow.Drivers, "Drivers.xml");
                            mainWindow.ShowToastNotification(new ToastNotification("Success", "Driver edited successfully!", Notification.Wpf.NotificationType.Success));
                            NavigationService.GoBack();
                        }
                        else
                        {
                            mainWindow.ShowToastNotification(new ToastNotification("Error", "Driver not found!", Notification.Wpf.NotificationType.Error));
                        }
                    }
                }
                catch (Exception ex)
                {
                    mainWindow.ShowToastNotification(new ToastNotification("Error", "Error while adding a driver", Notification.Wpf.NotificationType.Error));
                }
            }
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem != null && !DriverDescriptionRichTextBox.Selection.IsEmpty)
            {
                DriverDescriptionRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FontFamilyComboBox.SelectedItem);
            }
        }

        private void FontColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedColor = FontColorComboBox.SelectedItem as string;
            if (selectedColor != null)
            {
                Color color = (Color)ColorConverter.ConvertFromString(selectedColor);
                ColorPreviewRectangle.Fill = new SolidColorBrush(color);
            }

            if (FontColorComboBox.SelectedItem != null && !DriverDescriptionRichTextBox.Selection.IsEmpty)
            {
                Color color = (Color)ColorConverter.ConvertFromString(selectedColor);
                DriverDescriptionRichTextBox.Selection.ApplyPropertyValue(Inline.ForegroundProperty, new SolidColorBrush(color));
            }
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeComboBox.SelectedItem != null && !DriverDescriptionRichTextBox.Selection.IsEmpty)
            {
                DriverDescriptionRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, double.Parse(FontSizeComboBox.SelectedItem.ToString()));
            }
        }

        private void DriverDescriptionRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object fontWeight = DriverDescriptionRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            BoldToggleButton.IsChecked = (fontWeight != DependencyProperty.UnsetValue) && (fontWeight.Equals(FontWeights.Bold));

            object fontItalic = DriverDescriptionRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            ItalicToggleButton.IsChecked = ((fontItalic != DependencyProperty.UnsetValue)) && (fontItalic.Equals(FontStyles.Italic));

            object fontUnderline = DriverDescriptionRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            UnderlineToggleButton.IsChecked = (fontUnderline != DependencyProperty.UnsetValue) && (fontUnderline.Equals(TextDecorations.Underline));

            object fontFamily = DriverDescriptionRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            FontFamilyComboBox.SelectedItem = fontFamily;

            object fontSize = DriverDescriptionRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty);
            if (fontSize != DependencyProperty.UnsetValue)
            {
                FontSizeComboBox.SelectedItem = (int)(double)fontSize;
            }
            object fontColorObject = DriverDescriptionRichTextBox.Selection.GetPropertyValue(Inline.ForegroundProperty);
            if (fontColorObject != DependencyProperty.UnsetValue && fontColorObject is SolidColorBrush)
            {
                SolidColorBrush fontColorBrush = (SolidColorBrush)fontColorObject;
                Color color = fontColorBrush.Color;

                string colorName = typeof(Colors).GetProperties()
                                                 .FirstOrDefault(p => ((Color)p.GetValue(null, null)).ToString() == color.ToString())?.Name;
                if (!string.IsNullOrEmpty(colorName))
                {
                    FontColorComboBox.SelectedItem = colorName;
                }
            }

            GetCharacterCount();

        }

        private void GetCharacterCount()
        {
            int charCount = new TextRange(DriverDescriptionRichTextBox.Document.ContentStart, DriverDescriptionRichTextBox.Document.ContentEnd).Text.Length;
            CharacterCounterLabel.Content = (charCount - 2).ToString();
        }

        private void DriverDescriptionRichTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            GetCharacterCount();
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
    }
}

