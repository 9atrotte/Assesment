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
    /// 
    public class Player
    {
        public Image visual = new Image();
        public Rect hitbox = new Rect();

        public Player()
        {
            this.visual.Source = new BitmapImage(new Uri("C:/Users/YelloElefant/OneDrive/Coding/visual studio/Assesment/Sprites/player.gif"));
            this.visual.Width = 50;
            this.visual.Height = 50;
            this.visual.Stretch = Stretch.Fill;
            Canvas.SetLeft(this.visual, 100);
            Canvas.SetTop(this.visual, 100);
        }


        public void SetHitHox()
        {
            this.hitbox.X = Canvas.GetLeft(this.visual);
            this.hitbox.Y = Canvas.GetTop(this.visual);
            this.hitbox.Width = this.visual.Width;
            this.hitbox.Height = this.visual.Height;
        }


    }


    public partial class MainWindow : Window
    {
        Player player = new Player();
        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
            background.Children.Add(player.visual);
        }

        private void btn_look_Click(object sender, RoutedEventArgs e)
        {
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {




            var pos = Mouse.GetPosition(player.visual);
            pos.Offset(player.visual.Width / 2, player.visual.Height / 2);
            var angle = GetAngle(pos);

            lbl_output.Content = angle.ToString();
            lbl_output_pos.Content = pos.ToString();
            RotateTransform rotateTransform = new RotateTransform(angle, 25, 25);
            player.visual.RenderTransform = rotateTransform;
            //lbl_output.Content = GetMousePos(player); 
        }

        static double GetAngle(Point mouse)
        {
            if (mouse.X > 0 && mouse.Y > 0) //bottom right
            {
                return (Math.Atan(mouse.Y / mouse.X)) * ( Math.PI);
            }
            else if (mouse.X < 0 && mouse.Y > 0) //bottom left
            {
                mouse.X *= -1;
                var ang = Math.Atan(mouse.Y / mouse.X) * ( Math.PI);
                return 90 + (90 - ang);
            }
            else if (mouse.X > 0 && mouse.Y < 0) //top right
            {
                mouse.Y *= -1;
                var ang = Math.Atan(mouse.Y / mouse.X) * ( Math.PI);
                return 360 - ang;
            }
            else if (mouse.X < 0 && mouse.Y < 0) //top left
            {
                mouse.X *= -1;
                mouse.Y *= -1;
                var ang = Math.Atan(mouse.Y / mouse.X) * ( Math.PI);
                return 180 + ang;
            }
            return -1;


            //return (Math.Atan(mouse.Y / mouse.X)) * (180 / Math.PI);

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

