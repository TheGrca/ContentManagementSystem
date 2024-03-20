﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Content_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private DataIO serializer = new DataIO();
        public ObservableCollection<Driver> Drivers;
        public MainWindow()
        {
            InitializeComponent();
            Drivers = serializer.DeSerializeObject<ObservableCollection<Driver>>("Drivers.xml");
            if(Drivers == null) {
                Drivers = new ObservableCollection<Driver>();
            }
        }
    }
}
