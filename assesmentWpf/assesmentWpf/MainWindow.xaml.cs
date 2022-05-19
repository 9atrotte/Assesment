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

namespace assesmentWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double wHeight, wWidth;
        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
        }

        private void btn_look_Click(object sender, RoutedEventArgs e)
        {
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight;
            wHeight = windowMain.Height;
            wWidth = windowMain.Width;
            var pos = GetMousePos(windowMain, wWidth, wHeight);
            var angle = GetAngle(pos);
            lbl_output.Content = angle.ToString();
            lbl_output_pos.Content = pos.ToString();
            RotateTransform rotateTransform = new RotateTransform(angle, 50, 50);
            player.RenderTransform = rotateTransform;
            //lbl_output.Content = GetMousePos(player); 
        }

        static double GetAngle(Point mouse)
        {
            if (mouse.X > 0 && mouse.Y > 0) //bottom right
            {
                return (Math.Atan(mouse.Y / mouse.X)) * (180 / Math.PI);
            }
            else if (mouse.X < 0 && mouse.Y > 0) //bottom left
            {
                mouse.X *= -1;
                var ang = Math.Atan(mouse.Y / mouse.X) * (180 / Math.PI);
                return 90 + (90 - ang);
            }
            else if (mouse.X > 0 && mouse.Y < 0) //top right
            {
                mouse.Y *= -1;
                var ang = Math.Atan(mouse.Y / mouse.X) * (180 / Math.PI);
                return 360 - ang;
            }
            else if (mouse.X < 0 && mouse.Y < 0) //top left
            {
                mouse.X *= -1;
                mouse.Y *= -1;
                var ang = Math.Atan(mouse.Y / mouse.X) * (180 / Math.PI);
                return 180 + ang;
            }
            return -1;


            //return (Math.Atan(mouse.Y / mouse.X)) * (180 / Math.PI);

        }

        static Point GetMousePos(Window back, double w, double h)
        {

            Point mousePos = Mouse.GetPosition(back);
            var ofbackX =  w / 2 * -1;
            var ofbackY = h / 2 * -1;

            mousePos.Offset(ofbackX, ofbackY);
            return mousePos;
        }


        private void btn_look_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                //var pos = GetMousePos(windowMain);
                //var angle = GetAngle(pos);
                //lbl_output.Content = angle.ToString();
                //lbl_output_pos.Content = pos.ToString();
                //RotateTransform rotateTransform = new RotateTransform(angle, 50, 50);
                //player.RenderTransform = rotateTransform;
                //lbl_output.Content = GetMousePos(player); 
            }

        }
    }
}

