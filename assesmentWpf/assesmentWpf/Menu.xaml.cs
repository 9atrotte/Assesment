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
using System.Windows.Shapes;

namespace assesmentWpf
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {


        public static MainWindow game = new MainWindow();


        public Window1()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            game.Show();
            game.dispatcherTimer.Start();
            this.Close();
        }

        private void btn_howtoplay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_credits_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
