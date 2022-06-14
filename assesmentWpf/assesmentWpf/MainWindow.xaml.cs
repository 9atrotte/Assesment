using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
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

    public class Bullets
    {
        public bool shooting = false;
        public Ellipse visual = new Ellipse();
        public Rect hitbox = new Rect();
        public Bullets(SolidColorBrush color)
        {
            this.visual.Fill = color;
            this.visual.Stroke = color;
            this.visual.Width = 10;
            this.visual.Height = 10;
            this.visual.KeyDown += Visual_KeyDown;
            this.visual.KeyUp += Visual_KeyUp;
            this.visual.Focusable = true;
            this.ResetBullet();
        }
        public void Visual_KeyUp(object sender, KeyEventArgs e)
        {
        }

        void Visual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                this.shooting = true;
            }
        }


        public void SetHitHox()
        {
            this.hitbox.X = Canvas.GetLeft(this.visual);
            this.hitbox.Y = Canvas.GetTop(this.visual);
            this.hitbox.Width = this.visual.Width;
            this.hitbox.Height = this.visual.Height;
        }
        public void ResetBullet()
        {
            Canvas.SetLeft(this.visual, 50);
            Canvas.SetTop(this.visual, 50);
            Canvas.SetRight(this.visual, 60);
            Canvas.SetBottom(this.visual, 60);
        }
    }
    public class Walls
    {
        public Rectangle visual = new Rectangle();
        public Rect hitbox = new Rect();
        public Walls(SolidColorBrush color)
        {
            this.visual.Fill = color;
            this.visual.Stroke = color;
            this.visual.Width = 50;
            this.visual.Height = 497;
            Canvas.SetLeft(this.visual, 500);
            Canvas.SetTop(this.visual, 0);
        }
        public void SetHitHox()
        {
            this.hitbox.X = Canvas.GetLeft(this.visual);
            this.hitbox.Y = Canvas.GetTop(this.visual);
            this.hitbox.Width = this.visual.Width;
            this.hitbox.Height = this.visual.Height;
        }
    }
    public class Portals
    {

        public Rectangle visual = new Rectangle();
        public Rect hitbox = new Rect();
        public Portals(SolidColorBrush color)
        {
            this.visual.Fill = color;
            this.visual.Stroke = color;
            this.visual.Width = 2;
            this.visual.Height = 50;
            Canvas.SetLeft(this.visual, 0);
            Canvas.SetTop(this.visual, 0);
            this.visual.Visibility = Visibility.Hidden;
        }
        public void SetHitHox()
        {
            this.hitbox.X = Canvas.GetLeft(this.visual);
            this.hitbox.Y = Canvas.GetTop(this.visual);
            this.hitbox.Width = this.visual.Width;
            this.hitbox.Height = this.visual.Height;
        }
        public void spawn(Bullets bullet)
        {
            this.visual.Visibility = Visibility.Visible;
            bullet.SetHitHox();
            var top = Canvas.GetTop(bullet.visual);
            var left = Canvas.GetLeft(bullet.visual);
            Canvas.SetLeft(this.visual, left + bullet.visual.Width - 2);
            Canvas.SetTop(this.visual, top - this.visual.Height / 2 + 5);
            bullet.ResetBullet();
        }

    }
    public class Player
    {
        public Ellipse visual = new Ellipse();
        public Rectangle gun = new Rectangle();
        public Rect hitbox = new Rect();
        public Rectangle middle = new Rectangle();
        public double x = 0;
        public double xpos = 0;

        public Player()
        {
            this.visual.Fill = new SolidColorBrush(Colors.Red);
            this.visual.Width = 50;
            this.visual.Height = 50;
            this.visual.Stretch = Stretch.Fill;
            Canvas.SetLeft(this.visual, 100);
            Canvas.SetTop(this.visual, 100);

            this.gun.Fill = new SolidColorBrush(Colors.Purple);
            this.gun.Width = 25;
            this.gun.Height = 6;
            this.gun.Stretch = Stretch.Fill;
            Canvas.SetLeft(this.gun, 125);
            Canvas.SetTop(this.gun, 122);



            this.middle.Width = 1;
            this.middle.Height = 1;
            Canvas.SetLeft(this.middle, Canvas.GetLeft(this.visual) + 25);
            Canvas.SetTop(this.middle, Canvas.GetTop(this.visual) + 25);
        }


        public void SetHitHox()
        {
            this.hitbox.X = Canvas.GetLeft(this.visual);
            this.hitbox.Y = Canvas.GetTop(this.visual);
            this.hitbox.Width = this.visual.Width;
            this.hitbox.Height = this.visual.Height;
        }



        public bool shooting = false;
        public void Shoot(int witchbul, Point direction, double ang)
        {
            this.x = 0;
            this.shooting = true;
            

            
            if (witchbul == 1) 
            {
                Canvas.SetLeft(MainWindow.bullet, Canvas.GetLeft(this.middle) - 3);
                Canvas.SetTop(MainWindow.bullet, Canvas.GetTop(this.middle) - 3);
                this.xpos = Canvas.GetLeft(MainWindow.bullet);
            }
            else 
            {
                Canvas.SetLeft(MainWindow.bullet2, Canvas.GetLeft(this.middle) - 3);
                Canvas.SetTop(MainWindow.bullet2, Canvas.GetTop(this.middle) - 3);
                this.xpos = Canvas.GetLeft(MainWindow.bullet2);
            }

            DispatcherTimer shootingTimer = new DispatcherTimer();
            shootingTimer.Tick += shootingTimer_Tick;
            shootingTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);


            shootingTimer.Start();
            double Eqauation()
            {
                
                var equat = direction.Y / direction.X;
                return equat * this.x + this.xpos; 
            }



            void shootingTimer_Tick(object sender, EventArgs e)
            {
                this.x += 5;
                if (witchbul == 1)
                {
                    Canvas.SetLeft(MainWindow.bullet, (this.x + this.xpos));
                    Canvas.SetTop(MainWindow.bullet, Eqauation());
                    MainWindow.xx.Content = this.x;
                    if (this.x == 500)
                    {
                        shootingTimer.Stop();
                        this.shooting = false;

                    }
                }
                else
                {
                    Canvas.SetLeft(MainWindow.bullet2, (this.x + this.xpos));
                    Canvas.SetTop(MainWindow.bullet2, Eqauation());
                    MainWindow.xx.Content = this.x;
                    if (this.x == 500)
                    {
                        shootingTimer.Stop();
                        this.shooting = false;

                    }

                }
                

            }
        }

    }


    public partial class MainWindow : Window
    {
        
        public Point pos;
        public double angle;
        public static Label xx = new Label();


        Player player = new Player();
        public MainWindow()
        {
            

            xx.Height = 20;
            xx.Width = 50;
            xx.Background = new SolidColorBrush(Colors.LightBlue);
            xx.FontSize = 10;


            InitializeComponent();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
            background.Children.Add(player.visual);
            background.Children.Add(player.gun);
            background.Children.Add(player.middle);
            background.Children.Add(bullet2);
            background.Children.Add(bullet);
            background.Children.Add(xx);

            Canvas.SetLeft(bullet, Canvas.GetLeft(player.middle) - 3);
            Canvas.SetTop(bullet, Canvas.GetTop(player.middle) - 3);


            Canvas.SetLeft(bullet2, Canvas.GetLeft(player.middle) - 3);
            Canvas.SetTop(bullet2, Canvas.GetTop(player.middle) - 3);

            blocks.Add(wall);
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
            
            background.Children.Add(blue.visual);
            background.Children.Add(wall.visual);
            background.Children.Add(portal_blue.visual);
            background.Children.Add(portal_orange.visual);
       
        }

       


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            


            pos = Mouse.GetPosition(player.middle);
            //pos.Offset(1, 1);
            angle = GetAngle(pos);
            if (player.shooting == false)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed) { player.Shoot(1, pos, angle); }
                else if (Mouse.RightButton == MouseButtonState.Pressed) { player.Shoot(2, pos, angle); }
            }


            lbl_output.Content = angle.ToString();
            lbl_output_pos.Content = pos.ToString();
            RotateTransform rotateTransform = new RotateTransform(angle,0,3);
            player.gun.RenderTransform = rotateTransform;
            //lbl_output.Content = GetMousePos(player); 
        }

        static double GetAngle(Point mouse)
        {
            if (mouse.X > 0 && mouse.Y > 0) //bottom right
            {
                return (Math.Atan(mouse.Y / mouse.X)) * ( 180/Math.PI);
            }
            else if (mouse.X < 0 && mouse.Y > 0) //bottom left
            {
                mouse.X *= -1;
                var ang = Math.Atan(mouse.Y / mouse.X) * ( 180/Math.PI);
                return 90 + (90 - ang);
            }
            else if (mouse.X > 0 && mouse.Y < 0) //top right
            {
                mouse.Y *= -1;
                var ang = Math.Atan(mouse.Y / mouse.X) * ( 180/Math.PI);
                return 360 - ang;
            }
            else if (mouse.X < 0 && mouse.Y < 0) //top left
            {
                mouse.X *= -1;
                mouse.Y *= -1;
                var ang = Math.Atan(mouse.Y / mouse.X) * ( 180/Math.PI);
                return 180 + ang;
            }
            return -1;


            //return (Math.Atan(mouse.Y / mouse.X)) * (180 / Math.PI);

        }


      

        private void background_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            //player.Shoot(1, pos, angle);

            
        }


        Portals portal_blue = new Portals(new SolidColorBrush(Colors.Blue));
        Portals portal_orange = new Portals(new SolidColorBrush(Colors.Orange));
        Bullets blue = new Bullets(new SolidColorBrush(Colors.Blue));
        Bullets orange = new Bullets(new SolidColorBrush(Colors.Orange));
        Walls wall = new Walls(new SolidColorBrush(Colors.Black));
        List<Walls> blocks = new List<Walls>();


        public MainWindow()
        {
            

        }



        public void ResetBullet(Ellipse obj, Rect HitBox)
        {
            Canvas.SetLeft(obj, 50);
            Canvas.SetTop(obj, 50);
            HitBox.X = 10;
            HitBox.Y = 10;
        }

        

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            wall.SetHitHox();
            blue.SetHitHox();
            orange.SetHitHox();
            foreach  (Walls x in blocks)
                {

                if (ColisionDetc(blue.hitbox, x.hitbox)) 
                {
                    blue.shooting = false;
                    portal_blue.spawn(blue);
                }
                else { if (blue.shooting == true) { Canvas.SetLeft(blue.visual, Canvas.GetLeft(blue.visual) + 1); } } 
            }




        }

        public bool ColisionDetc(Rect a, Rect b)
        {
            if (a.IntersectsWith(b))
            {
                return true;
                
            }
            else
            {
                return false;
                
            }
            
            
        }





        
    }
}

