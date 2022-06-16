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
            this.visual.Width = 6;
            this.visual.Height = 6;
            
            //this.ResetBullet();
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
            Canvas.SetLeft(this.visual, Canvas.GetLeft(MainWindow.player.middle) - 3);
            Canvas.SetTop(this.visual, Canvas.GetTop(MainWindow.player.middle) - 3);
            Canvas.SetRight(this.visual, 60);
            Canvas.SetBottom(this.visual, 60);
        }
    }
    public class Walls
    {
        public Rectangle visual = new Rectangle();
        public Rect hitbox = new Rect();
        public Walls(SolidColorBrush color, int x, int y)
        {
            this.visual.Fill = color;
            this.visual.Stroke = color;
            this.visual.Width = 50;
            this.visual.Height = 497;
            Canvas.SetLeft(this.visual, x);
            Canvas.SetTop(this.visual, y);
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
        public double ypos = 0;
        public bool shooting = false;

        public Player()
        {
            this.visual.Fill = new SolidColorBrush(Colors.Red);
            this.visual.Width = 50;
            this.visual.Height = 50;
            this.visual.Stretch = Stretch.Fill;
            Canvas.SetLeft(this.visual, 500);
            Canvas.SetTop(this.visual, 200);

            this.gun.Fill = new SolidColorBrush(Colors.Purple);
            this.gun.Width = 25;
            this.gun.Height = 6;
            this.gun.Stretch = Stretch.Fill;
            Canvas.SetLeft(this.gun, Canvas.GetLeft(this.visual) + this.visual.Width/2 );
            Canvas.SetTop(this.gun, Canvas.GetTop(this.visual) + this.visual.Height / 2 - (this.gun.Height / 2));



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



        public void Shoot(int witchbul, Point direction, double ang)
        {
            this.x = 1;
            this.shooting = true;
            if (direction.X < 0)
            {
                this.x *= -1;
            }

            
            if (witchbul == 1) 
            {
                MainWindow.blue.shooting = true;
                Canvas.SetLeft(MainWindow.blue.visual, Canvas.GetLeft(this.middle) - 3);
                Canvas.SetTop(MainWindow.blue.visual, Canvas.GetTop(this.middle) - 3); //reset the bullet
                this.xpos = Canvas.GetLeft(MainWindow.blue.visual);
                this.ypos = Canvas.GetTop(MainWindow.blue.visual);
            }
            else 
            {
                MainWindow.orange.shooting = true;
                Canvas.SetLeft(MainWindow.orange.visual, Canvas.GetLeft(this.middle) - 3);
                Canvas.SetTop(MainWindow.orange.visual, Canvas.GetTop(this.middle) - 3);
                this.xpos = Canvas.GetLeft(MainWindow.orange.visual);
                this.ypos = Canvas.GetTop(MainWindow.orange.visual);

            }

            DispatcherTimer shootingTimer = new DispatcherTimer();
            shootingTimer.Tick += shootingTimer_Tick;
            shootingTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);


            shootingTimer.Start();
            double Eqauation()
            {
                
                var equat = direction.Y / direction.X;
                return equat * this.x + this.ypos; 
            }



            void shootingTimer_Tick(object sender, EventArgs e)
            {
                if (x < 0) { x -= 5; }
                else if (x > 0) { x += 5; }
                

                if (witchbul == 1)
                {
                    Canvas.SetLeft(MainWindow.blue.visual, (this.x + this.xpos));
                    Canvas.SetTop(MainWindow.blue.visual, Eqauation());
                    MainWindow.xx.Content = this.x;
                    if (this.shooting == false)
                    {
                        shootingTimer.Stop();
                        this.shooting = false;
                        MainWindow.blue.shooting = false;
                    }
                }
                else
                {
                    Canvas.SetLeft(MainWindow.orange.visual, (this.x + this.xpos));
                    Canvas.SetTop(MainWindow.orange.visual, Eqauation());
                    MainWindow.xx.Content = this.x;
                    if (this.shooting == false)
                    {
                        shootingTimer.Stop();
                        this.shooting = false;
                        MainWindow.orange.shooting = false;
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
        public static Portals portal_blue = new Portals(new SolidColorBrush(Colors.Blue));
        public static Portals portal_orange = new Portals(new SolidColorBrush(Colors.Orange));
        public static Bullets blue = new Bullets(new SolidColorBrush(Colors.Blue));
        public static Bullets orange = new Bullets(new SolidColorBrush(Colors.Orange));
        public static List<Walls> blocks = new List<Walls>();
        public static Player player = new Player();


        public MainWindow()
        {
            

            xx.Height = 30;
            xx.Width = 50;
            xx.Background = new SolidColorBrush(Colors.LightBlue);
            xx.FontSize = 10;


            InitializeComponent();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();

            makeWalls();

            background.Children.Add(player.visual);
            background.Children.Add(player.gun);
            background.Children.Add(player.middle);
            background.Children.Add(blue.visual);
            background.Children.Add(orange.visual);
            background.Children.Add(portal_blue.visual);
            background.Children.Add(portal_orange.visual);
            background.Children.Add(xx);
            
            

            
        }

        public void makeWalls()
        {

            blocks.Add(new Walls(new SolidColorBrush(Colors.Black), 750, 10)); ;
            blocks.Add(new Walls(new SolidColorBrush(Colors.Black), 250, 10));

            foreach (Walls item in blocks)
            {
                background.Children.Add(item.visual);
            }



        }
       


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            foreach (Walls item in blocks)
            {
                item.SetHitHox();
            }                           //resets all hitboxes
            blue.SetHitHox();
            orange.SetHitHox();

            foreach (Walls x in blocks)
            {

                if (ColisionDetc(blue.hitbox, x.hitbox))
                {
                    portal_blue.spawn(blue);
                    blue.shooting = false;
                    player.shooting = false;
                }
                if (ColisionDetc(orange.hitbox, x.hitbox))
                {
                    portal_orange.spawn(orange);
                    orange.shooting = false;
                    player.shooting = false;
                }

            }

            pos = Mouse.GetPosition(player.middle);
            angle = GetAngle(pos);
            if (player.shooting == false)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed) 
                {
                    player.shooting = true;
                    player.Shoot(1, pos, angle);
                    blue.shooting = true;
                }
                else if (Mouse.RightButton == MouseButtonState.Pressed) 
                {
                    player.shooting = true;
                    player.Shoot(2, pos, angle);
                    orange.shooting = true;
                }
            }


            lbl_output.Content = angle.ToString();
            lbl_output_pos.Content = pos.ToString();
            RotateTransform rotateTransform = new RotateTransform(angle,0,player.gun.Height/2);
            player.gun.RenderTransform = rotateTransform;
       
        
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

