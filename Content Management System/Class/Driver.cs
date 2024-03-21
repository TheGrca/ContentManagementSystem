using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace Content_Management_System.Class
{
    [Serializable]
    public class Driver : INotifyPropertyChanged
    {
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Number {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string RtfPath { get; set; }
        public DateTime Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Driver(int number, string name, string description ,string picture, string RtfPath, DateTime Date)
        {
            this.Number = number;
            this.Name = name;  
            this.Description = description;
            this.Picture = picture;
            this.RtfPath = RtfPath;
            this.Date = Date;
        }

        public Driver() { }

    }
}
