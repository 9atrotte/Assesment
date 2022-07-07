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





        public static MainWindow[] games = new MainWindow[3];


        
        public static Credits credits = new Credits();
        public static HowToPlay howToPlay = new HowToPlay();
        public static LoadingScreen LoadingScreen = new LoadingScreen();

        public Window1()
        {
            InitializeComponent();
        }

        
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.score = 0;
            for (int i = 0; i < games.Length; i++)
            {
                
                games[i] = new MainWindow(i + 1, new SolidColorBrush(Colors.Gray));
                
            }


            games[0].Show();
            games[0].dispatcherTimer.Start();
            games[0].ballMoveTimer.Start();
            
            //End end = new End();
            //end.Show();

            //LoadingScreen.Show();
            //LoadingScreen.tmrFacts.Start();
            //LoadingScreen.tmrText.Start();
            //LoadingScreen.tmrLoading.Start();

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

        private void btn_start_MouseEnter(object sender, MouseEventArgs e)
        {
            var s = sender as Button;
            s.Background = new SolidColorBrush(Colors.Lime);
        }

        private void btn_start_MouseLeave(object sender, MouseEventArgs e)
        {
            var s = sender as Button;
            s.Background = null;
        }
    }
}
