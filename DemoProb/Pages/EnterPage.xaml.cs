﻿using DemoProb.Controls;
using System;
using System.Collections.Generic;
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

namespace DemoProb.Pages
{
    /// <summary>
    /// Логика взаимодействия для EnterPage.xaml
    /// </summary>
    public partial class EnterPage : Page
    {
        public Action OnItemRemoved { get; set; }
        public EnterPage()
        {
            InitializeComponent();
            OnItemRemoved = () =>
            {
                UpdatePage();
            };
            UpdatePage();
        }
        public void UpdatePage()
        {
            ServiceWpar.Children.Clear();
            foreach (var item in App.db.Service)
            {
                ServiceWpar.Children.Add(new UserControl1(item, OnItemRemoved));
            }
        }
    }
    }

