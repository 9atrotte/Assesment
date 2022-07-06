﻿using System;
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
using System.Windows.Shapes;

namespace assesmentWpf
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {








        public static MainWindow game = new MainWindow(1);
        //public static MainWindow game2 = new MainWindow(2);
        //public static MainWindow game3 = new MainWindow(3);
        public static Credits credits = new Credits();
        public static HowToPlay howToPlay = new HowToPlay();
        public static LoadingScreen LoadingScreen = new LoadingScreen();

        public Window1()
        {
            InitializeComponent();
        }

        
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            LoadingScreen.Show();



            this.Hide();
        }

        private void btn_howtoplay_Click(object sender, RoutedEventArgs e)
        {
            howToPlay.Show();
            //this.Close();
        }

        private void btn_credits_Click(object sender, RoutedEventArgs e)
        {
            credits.Show();
            //this.Close();

           
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
