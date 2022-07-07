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
    /// Interaction logic for End.xaml
    /// </summary>
    public partial class End : Window
    {
        public Window1 menu = new Window1();

        public End()
        {
            InitializeComponent();
            if (MainWindow.score == 1)
            {

                resets.Content = $"You Had {MainWindow.score} reset";
            }
            else
            {
                resets.Content = $"You Had {MainWindow.score} resets";

            }

        }

        public void btn_back_Click(object sender, RoutedEventArgs e)
        {
            menu.Show();
            this.Hide();
        }






    }
}
