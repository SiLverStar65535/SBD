﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SBD
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesignTime = true;
        public App()
        {
            InitializeComponent();
            IsDesignTime = false;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new MainWindowBootstrapper().Run();
        }
    }
}
