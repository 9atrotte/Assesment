using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

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
            Canvas.SetLeft(this.visual, Canvas.GetLeft(Window1.game.player.middle) - 3);
            Canvas.SetTop(this.visual, Canvas.GetTop(Window1.game.player.middle) - 3);
            Canvas.SetRight(this.visual, 60);
            Canvas.SetBottom(this.visual, 60);
        }
    }
    public class Walls
    {
        public Rectangle visual = new Rectangle();
        public Rect hitbox = new Rect();
        
        /// <summary>
        /// wall builder
        /// </summary>
        /// <param name="color">color</param>
        /// <param name="x">x pos</param>
        /// <param name="y">y pos</param>
        /// <param name="w">width</param>
        /// <param name="h">height</param>
        public Walls(SolidColorBrush color, int x, int y, int w, int h, string tag)
        {
            this.visual.Fill = color;
            this.visual.Stroke = color;
            this.visual.Width = w;
            this.visual.Height = h;
            Canvas.SetLeft(this.visual, x);
            Canvas.SetTop(this.visual, y);
            this.visual.Tag = tag;
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

        public Rectangle visualVertical = new Rectangle();
        public Rectangle visualSideWays = new Rectangle();
        public Rect hitBoxVertical = new Rect();
        public Rect hitBoxSideWays = new Rect();
        public Portals(SolidColorBrush color)
        {
            this.visualVertical.Fill = color;
            this.visualVertical.Stroke = color;
            this.visualVertical.Width = 2;
            this.visualVertical.Height = 50;
            Canvas.SetLeft(this.visualVertical, 0);
            Canvas.SetTop(this.visualVertical, 0);
            this.visualVertical.Visibility = Visibility.Hidden;

            this.visualSideWays.Fill = color;
            this.visualSideWays.Stroke = color;
            this.visualSideWays.Width = 50;
            this.visualSideWays.Height = 2;
            Canvas.SetLeft(this.visualSideWays, 0);
            Canvas.SetTop(this.visualSideWays, 0);
            this.visualSideWays.Visibility = Visibility.Hidden;



        }
        public void SetHitHox()
        {
            this.hitBoxVertical.X = Canvas.GetLeft(this.visualVertical);
            this.hitBoxVertical.Y = Canvas.GetTop(this.visualVertical);
            this.hitBoxVertical.Width = this.visualVertical.Width;
            this.hitBoxVertical.Height = this.visualVertical.Height;

            this.hitBoxSideWays.X = Canvas.GetLeft(this.visualSideWays);
            this.hitBoxSideWays.Y = Canvas.GetTop(this.visualSideWays);
            this.hitBoxSideWays.Width = this.visualSideWays.Width;
            this.hitBoxSideWays.Height = this.visualSideWays.Height;
        }
        public void spawn(Bullets bullet, int a, Walls w) //a = 1 = v
        {
            if (a == 1)
            {
                this.visualSideWays.Visibility = Visibility.Collapsed;
                this.visualVertical.Visibility = Visibility.Visible;
                bullet.SetHitHox();
                var top = Canvas.GetTop(bullet.visual);
                var left = Canvas.GetLeft(bullet.visual);
                var wleft = Canvas.GetLeft(w.visual);
                var wtop = Canvas.GetTop(w.visual);

                bullet.ResetBullet();


                if (left == wleft || left > wleft && left < wleft + 10) 
                {
                    Canvas.SetLeft(this.visualVertical, Canvas.GetLeft(w.visual) - 2);
                    Canvas.SetTop(this.visualVertical, top - this.visualVertical.Height / 2 + 5);
                }
                else
                {
                    Canvas.SetLeft(this.visualVertical, Canvas.GetLeft(w.visual) + w.visual.Width);
                    Canvas.SetTop(this.visualVertical, top - this.visualVertical.Height / 2 + 5);
                }

                this.SetHitHox();

            }
            else
            {
                this.visualVertical.Visibility = Visibility.Collapsed;
                this.visualSideWays.Visibility = Visibility.Visible;
                bullet.SetHitHox();
                var top = Canvas.GetTop(bullet.visual);
                var left = Canvas.GetLeft(bullet.visual);
                var wleft = Canvas.GetLeft(w.visual);
                var wtop = Canvas.GetTop(w.visual);

                bullet.ResetBullet();

                if (top == wtop || top > wtop && top < wtop + 10)
                {
                    Canvas.SetLeft(this.visualSideWays, left + bullet.visual.Width - 2);
                    Canvas.SetTop(this.visualSideWays, Canvas.GetTop(w.visual) - 2);
                }
                else
                {
                    Canvas.SetLeft(this.visualSideWays, left + bullet.visual.Width - 2);
                    Canvas.SetTop(this.visualSideWays, Canvas.GetTop(w.visual) + w.visual.Height);
                }

                this.SetHitHox();
            }







               
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
            this.visual.Stroke = new SolidColorBrush(Colors.Black);
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
                Window1.game.blue.shooting = true;
                Canvas.SetLeft(Window1.game.blue.visual, Canvas.GetLeft(this.middle) - 3);
                Canvas.SetTop(Window1.game.blue.visual, Canvas.GetTop(this.middle) - 3); //reset the bullet
                this.xpos = Canvas.GetLeft(Window1.game.blue.visual);
                this.ypos = Canvas.GetTop(Window1.game.blue.visual);
            }
            else 
            {
                Window1.game.orange.shooting = true;
                Canvas.SetLeft(Window1.game.orange.visual, Canvas.GetLeft(this.middle) - 3);
                Canvas.SetTop(Window1.game.orange.visual, Canvas.GetTop(this.middle) - 3);
                this.xpos = Canvas.GetLeft(Window1.game.orange.visual);
                this.ypos = Canvas.GetTop(Window1.game.orange.visual);

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

                if (this.x > 1920 || this.x < -1920)
                {
                    this.shooting = false;
                    Window1.game.blue.shooting = false;
                    Window1.game.orange.shooting = false;
                    shootingTimer.Stop();
                    Window1.game.blue.ResetBullet();
                    Window1.game.orange.ResetBullet();
                }


                if (witchbul == 1)
                {
                    Canvas.SetLeft(Window1.game.blue.visual, (this.x + this.xpos));
                    Canvas.SetTop(Window1.game.blue.visual, Eqauation());
                    Window1.game.xx.Content = this.x;
                    if (this.shooting == false)
                    {
                        shootingTimer.Stop();
                        this.shooting = false;
                        Window1.game.blue.shooting = false;
                    }
                }
                else
                {
                    Canvas.SetLeft(Window1.game.orange.visual, (this.x + this.xpos));
                    Canvas.SetTop(Window1.game.orange.visual, Eqauation());
                    Window1.game.xx.Content = this.x;
                    if (this.shooting == false)
                    {
                        shootingTimer.Stop();
                        this.shooting = false;
                        Window1.game.orange.shooting = false;
                    }

                }
                

            }
        }

    }

    public class Ball
    {
        public Ellipse visual = new Ellipse();
        public Rect hitbox = new Rect();
        public string direction;
        public Ellipse nose = new Ellipse();
        public Ellipse middle = new Ellipse();

        public Ball(int width, int height, int x, int y)
        { 

            this.visual.Width = width;
            this.visual.Height = height;
            this.visual.Fill = new SolidColorBrush(Colors.Gray);
            this.visual.Stroke = new SolidColorBrush(Colors.Gray);

            this.nose.Width = 2;
            this.nose.Height = 2;
            this.nose.Fill = new SolidColorBrush(Colors.Black);
            this.nose.Stroke = new SolidColorBrush(Colors.Black);

            this.middle.Width = 1;
            this.middle.Height = 1;
            Canvas.SetLeft(this.middle, x + this.visual.Width / 2);
            Canvas.SetTop(this.middle, y + this.visual.Height / 2);
            this.middle.Fill = new SolidColorBrush(Colors.Red);

            Canvas.SetLeft(this.visual, x);
            Canvas.SetTop(this.visual, y);

            this.hitbox.Width = width;
            this.hitbox.Height = height;

            this.resetNose(x, y, "down");
        }

        public void SetHitHox()
        {
            this.hitbox.X = Canvas.GetLeft(this.visual);
            this.hitbox.Y = Canvas.GetTop(this.visual);
            this.hitbox.Width = this.visual.Width;
            this.hitbox.Height = this.visual.Height;
        }

        public void resetNose(double x, double y, string dir)
        {
            Canvas.SetLeft(this.middle, x + this.visual.Width / 2);
            Canvas.SetTop(this.middle, y + this.visual.Height / 2);

            switch (dir)
            {
                case "up":
                    Canvas.SetLeft(this.nose, x + this.visual.Width / 2);
                    Canvas.SetTop(this.nose, y + this.visual.Height * 0.25);
                    break;
                case "right":
                    Canvas.SetLeft(this.nose, x + this.visual.Width * 0.75);
                    Canvas.SetTop(this.nose, y + this.visual.Height / 2);
                    break;
                case "down":
                    Canvas.SetLeft(this.nose, x + this.visual.Width / 2);
                    Canvas.SetTop(this.nose, y + this.visual.Height * 0.75);
                    break;
                case "left":
                    Canvas.SetLeft(this.nose, x + this.visual.Width * 0.25);
                    Canvas.SetTop(this.nose, y + this.visual.Height / 2);
                    break;
                default:
                    break;
            }

           
        }
        public void move()
        {

            var vtop = Canvas.GetTop(this.visual);
            var vleft = Canvas.GetLeft(this.visual);
            var mtop = Canvas.GetTop(this.middle);
            var mleft = Canvas.GetLeft(this.middle);
            var ntop = Canvas.GetTop(this.nose);
            var nleft = Canvas.GetLeft(this.nose);



            var xdif = nleft - mleft;
            var ydif = ntop - mtop;

            
            if (xdif != 0) {xdif = xdif / Math.Abs(xdif);}
            if (ydif != 0) { ydif = ydif / Math.Abs(ydif); } 

            Canvas.SetLeft(this.visual, vleft + xdif);
            Canvas.SetTop(this.visual, vtop + ydif);

            mtop = Canvas.GetTop(this.visual);
            mleft = Canvas.GetLeft(this.visual);
            this.resetNose(mleft, mtop, this.direction);


        
        }
    
    
    
    
    
    }

    public partial class MainWindow : Window
    {
        public DispatcherTimer ballMoveTimer = new DispatcherTimer();
        public Point pos;
        public double angle;
        public Label xx = new Label();
        public Portals portal_blue = new Portals(new SolidColorBrush(Colors.Blue));
        public Portals portal_orange = new Portals(new SolidColorBrush(Colors.Orange));
        public Bullets blue = new Bullets(new SolidColorBrush(Colors.Blue));
        public Bullets orange = new Bullets(new SolidColorBrush(Colors.Orange));
        public List<Walls> blocks = new List<Walls>();
        public Player player = new Player();
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Ball ball = new Ball(40, 40, 100, 1);


        public MainWindow()
        {
            
            
            

            



            xx.Height = 30;
            xx.Width = 50;
            xx.Background = new SolidColorBrush(Colors.LightBlue);
            xx.FontSize = 10;


            InitializeComponent();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(1);

            ballMoveTimer.Tick += BallMoveTimer_Tick;
            ballMoveTimer.Interval = new TimeSpan(0,0,0,0,1);

            makeWalls();
            background.Children.Add(player.visual);
            background.Children.Add(player.gun);
            background.Children.Add(player.middle);
            background.Children.Add(blue.visual);
            background.Children.Add(orange.visual);
            background.Children.Add(portal_blue.visualVertical);
            background.Children.Add(portal_orange.visualVertical);
            background.Children.Add(portal_blue.visualSideWays);
            background.Children.Add(portal_orange.visualSideWays); 
            background.Children.Add(xx);
            background.Children.Add(ball.visual);
            background.Children.Add(ball.nose);
            background.Children.Add(ball.middle);


            foreach (Walls item in blocks)
            {
                item.SetHitHox();
            }

            ball.direction = "down";

        }



        public void makeWalls()
        {
            
            blocks.Add(new Walls(new SolidColorBrush(Colors.Black), 51, 1080-70, 1920-150, 50, "h"));//floor
            blocks.Add(new Walls(new SolidColorBrush(Colors.Black), 1920-50, 0, 50, 1080, "v")); //right wall
            blocks.Add(new Walls(new SolidColorBrush(Colors.Black), 100, 0, 1870, 50, "h"));//celling
            blocks.Add(new Walls(new SolidColorBrush(Colors.Black), 0, 0, 50, 1920, "v"));//left wall
            blocks.Add(new Walls(new SolidColorBrush(Colors.Black), 1920/2, 0, 50, 1920, "v"));//middle




            foreach (Walls item in blocks)
            {
                background.Children.Add(item.visual);
            }



        }

        private void BallMoveTimer_Tick(object sender, EventArgs e)
        {
            ball.move();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //foreach (Walls item in blocks)
            //{
            //    item.SetHitHox();
            //}                           //resets all hitboxes
            blue.SetHitHox();
            orange.SetHitHox();
            ball.SetHitHox();



            

            teleportCheck();
            


            foreach (Walls x in blocks)
            {

                if (ColisionDetc(blue.hitbox, x.hitbox))
                {
                    blue.shooting = false;
                    player.shooting = false;
                    if ((string)x.visual.Tag == "v")
                    {
                        
                        portal_blue.spawn(blue, 1, x);
                    }
                    else
                    {

                        portal_blue.spawn(blue, 0, x);
                    }
                }
                if (ColisionDetc(orange.hitbox, x.hitbox))
                {
                    orange.shooting = false;
                    player.shooting = false;
                    if ((string)x.visual.Tag == "v")
                    { 
                        portal_orange.spawn(orange, 1, x);
                    }
                    else
                    {
                        portal_orange.spawn(orange, 0, x);

                    }
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

        public void teleportCheck()
        {
            if (ball.hitbox.IntersectsWith(portal_blue.hitBoxVertical) || ball.hitbox.IntersectsWith(portal_blue.hitBoxSideWays))//blue portal 
            {
                if (portal_orange.visualVertical.Visibility == Visibility.Visible)//vertical
                {
                    Canvas.SetLeft(ball.visual, Canvas.GetLeft(portal_orange.visualVertical) + 3);
                    Canvas.SetTop(ball.visual, Canvas.GetTop(portal_orange.visualVertical));
                    ball.direction = "right";
                }
                else if (portal_orange.visualSideWays.Visibility == Visibility.Visible)//sideways 
                {
                    Canvas.SetLeft(ball.visual, Canvas.GetLeft(portal_orange.visualSideWays));
                    Canvas.SetTop(ball.visual, Canvas.GetTop(portal_orange.visualSideWays) + 3);
                    ball.direction = "down";
                }
            }
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

