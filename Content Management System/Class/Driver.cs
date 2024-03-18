using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace Content_Management_System.Class
{
    [Serializable]
    public class Driver
    {
        public int Number {  get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string RtfPath { get; set; }
        public DateTime Date { get; set; }

        public Driver(int number, string name, string picture, string RtfPath, DateTime Date)
        {
            this.Number = number;
            this.Name = name;  
            this.Picture = picture;
            this.RtfPath = RtfPath;
            this.Date = Date;
        }

        public Driver() { }

    }
}
