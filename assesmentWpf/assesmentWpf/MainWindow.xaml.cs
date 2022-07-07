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
        public void ResetBullet(Player player)
        {
            Canvas.SetLeft(this.visual, Canvas.GetLeft(player.middle) - 3);
            Canvas.SetTop(this.visual, Canvas.GetTop(player.middle) - 3);
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
        public string outDir;


        public Portals(SolidColorBrush color)
        {
            this.visualVertical.Fill = color;
            this.visualVertical.Stroke = color;
            this.visualVertical.Width = 5;
            this.visualVertical.Height = 75;
            Canvas.SetLeft(this.visualVertical, 0);
            Canvas.SetTop(this.visualVertical, 0);
            this.visualVertical.Visibility = Visibility.Hidden;

            this.visualSideWays.Fill = color;
            this.visualSideWays.Stroke = color;
            this.visualSideWays.Width = 75;
            this.visualSideWays.Height = 5;
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
        public void spawn(Bullets bullet, int a, Walls w, Player player) //a = 1 = v
        {
            this.SetHitHox();
            if (a == 1)
            {
                this.visualSideWays.Visibility = Visibility.Collapsed;
                this.visualVertical.Visibility = Visibility.Visible;
                bullet.SetHitHox();
                var top = Canvas.GetTop(bullet.visual);//bul top/left
                var left = Canvas.GetLeft(bullet.visual);
                var wleft = Canvas.GetLeft(w.visual);//wall top/left
                var wtop = Canvas.GetTop(w.visual);

                bullet.ResetBullet(player);


                if (left == wleft || left > wleft - bullet.visual.Width && left < wleft + 25) //left of vertical wall
                {
                    Canvas.SetLeft(this.visualVertical, Canvas.GetLeft(w.visual) - this.visualVertical.Width);
                    Canvas.SetTop(this.visualVertical, top - this.visualVertical.Height / 2 + 5);
                    this.outDir = "left";
                }
                else
                {
                    Canvas.SetLeft(this.visualVertical, Canvas.GetLeft(w.visual) + w.visual.Width);//right of vertical wall
                    Canvas.SetTop(this.visualVertical, top - this.visualVertical.Height / 2 + 5);
                    this.outDir = "right";
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

                bullet.ResetBullet(player);

                if (top == wtop || top > wtop - bullet.visual.Width && top < wtop + 25)//top of horizontal wall
                {
                    Canvas.SetLeft(this.visualSideWays, left + bullet.visual.Width - this.visualSideWays.Width/2);
                    Canvas.SetTop(this.visualSideWays, Canvas.GetTop(w.visual) - this.visualSideWays.Height);
                    this.outDir = "up";
                }
                else
                {
                    Canvas.SetLeft(this.visualSideWays, left + bullet.visual.Width - this.visualSideWays.Width / 2);//bottom of horizontral wall
                    Canvas.SetTop(this.visualSideWays, Canvas.GetTop(w.visual) + w.visual.Height);
                    this.outDir = "down";
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
        public double y = 0;
        public double xpos = 0;
        public double ypos = 0;
        public bool shooting = false;
        public int bullspeed = 10;
        public double yinc;
        public double xinc;

        public Player(double x, double y)
        {
            this.visual.Stroke = new SolidColorBrush(Colors.Black);
            this.visual.Fill = new SolidColorBrush(Colors.Red);
            this.visual.Width = 80;
            this.visual.Height = 80;
            this.visual.Stretch = Stretch.Fill;
            Canvas.SetLeft(this.visual, x);
            Canvas.SetTop(this.visual, y);

            this.setGunMiddle();
        }

        public void setGunMiddle()
        {
            this.gun.Fill = new SolidColorBrush(Colors.Purple);
            this.gun.Width = this.visual.Width/2;
            this.gun.Height = 6;
            this.gun.Stretch = Stretch.Fill;
            Canvas.SetLeft(this.gun, Canvas.GetLeft(this.visual) + this.visual.Width / 2);
            Canvas.SetTop(this.gun, Canvas.GetTop(this.visual) + this.visual.Height / 2 - (this.gun.Height / 2));



            this.middle.Width = 1;
            this.middle.Height = 1;
            Canvas.SetLeft(this.middle, Canvas.GetLeft(this.visual) + this.visual.Width/2);
            Canvas.SetTop(this.middle, Canvas.GetTop(this.visual) + this.visual.Height/2);


        }


        public void SetHitHox()
        {
            this.hitbox.X = Canvas.GetLeft(this.visual);
            this.hitbox.Y = Canvas.GetTop(this.visual);
            this.hitbox.Width = this.visual.Width;
            this.hitbox.Height = this.visual.Height;
        }



        public void Shoot(int witchbul, Point direction, double ang, Bullets blue, Bullets orange, Player player)
        {
            var angle = (Math.Atan(Math.Abs(direction.Y) / Math.Abs(direction.X)));
            yinc = Math.Sin(angle) * this.bullspeed;
            xinc = Math.Cos(angle) * this.bullspeed;
            this.x = 1; //x counter for movement 
            this.y = 1; //y counter for movement
            this.shooting = true;






            if (direction.X < 0)
            {
                this.xinc *= -1;
            }
            if (direction.Y < 0)
            {
                this.yinc *= -1;
            }





            if (witchbul == 1) 
            {
                blue.shooting = true;
                Canvas.SetLeft(blue.visual, Canvas.GetLeft(this.middle) - 3);
                Canvas.SetTop(blue.visual, Canvas.GetTop(this.middle) - 3); //reset the bullet
                this.xpos = Canvas.GetLeft(blue.visual);
                this.ypos = Canvas.GetTop(blue.visual);
            }
            else 
            {
                orange.shooting = true;
                Canvas.SetLeft(orange.visual, Canvas.GetLeft(this.middle) - 3);
                Canvas.SetTop(orange.visual, Canvas.GetTop(this.middle) - 3);
                this.xpos = Canvas.GetLeft(orange.visual);
                this.ypos = Canvas.GetTop(orange.visual);

            }


            


            










            DispatcherTimer shootingTimer = new DispatcherTimer();
            shootingTimer.Tick += shootingTimer_Tick;
            shootingTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);


            shootingTimer.Start();
            



            void shootingTimer_Tick(object sender, EventArgs e)
            {
                this.x += xinc;
                this.y += yinc;

                if (this.x > 1920 || this.x < -1920)
                {
                    this.shooting = false;
                    blue.shooting = false;
                    orange.shooting = false;
                    shootingTimer.Stop();
                    blue.ResetBullet(player);
                    orange.ResetBullet(player);
                }


                if (witchbul == 1)
                {
                    Canvas.SetLeft(blue.visual, (this.x + this.xpos));
                    
                    
                    Canvas.SetTop(blue.visual, (this.y + this.ypos));
                    
                    
                    if (this.shooting == false)
                    {
                        shootingTimer.Stop();
                        this.shooting = false;
                        blue.shooting = false;
                    }
                }
                else
                {
                    Canvas.SetLeft(orange.visual, (this.x + this.xpos));


                    Canvas.SetTop(orange.visual, (this.y + this.ypos));
                    
                    
                    
                    if (this.shooting == false)
                    {
                        shootingTimer.Stop();
                        this.shooting = false;
                        orange.shooting = false;
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
        public int canTele = 1;
        public int canColide = 1;

        public int x, y;
        public Ball(int width, int height, int x, int y)
        {
            this.x = x;
            this.y = y;

            this.visual.Width = width;
            this.visual.Height = height;
            //this.visual.Fill = new SolidColorBrush(Colors.Gray);
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



            var xdif = (nleft - mleft);
            var ydif = (ntop - mtop);

            
            if (xdif != 0) {xdif = xdif / Math.Abs(xdif);}
            if (ydif != 0) { ydif = ydif / Math.Abs(ydif); } 

            Canvas.SetLeft(this.visual, vleft + (xdif * 2));
            Canvas.SetTop(this.visual, vtop + (ydif * 2));

            mtop = Canvas.GetTop(this.visual);
            mleft = Canvas.GetLeft(this.visual);
            this.resetNose(mleft, mtop, this.direction);


        
        }

        public void reset(Point rxy, string direc)
        {
            

           

            Canvas.SetLeft(this.middle, rxy.X + this.visual.Width / 2);
            Canvas.SetTop(this.middle, rxy.Y + this.visual.Height / 2);
            Canvas.SetLeft(this.visual, rxy.X);
            Canvas.SetTop(this.visual, rxy.Y);
            this.resetNose(rxy.X, rxy.Y, direc);
            this.direction = direc;



        }



    }
    public class TelePad
    {
        public Ellipse visual = new Ellipse();
        public Ellipse visualDesign_middleCircle = new Ellipse();
        public Rect hitbox = new Rect();



        public TelePad(int x, int y)
        {
            this.visual.Height = 60;
            this.visual.Width = 60;
            this.visual.Fill = new SolidColorBrush(Colors.LimeGreen);
            this.visual.Stroke = new SolidColorBrush(Colors.Black);

            this.visualDesign_middleCircle.Height = this.visual.Height / 2;
            this.visualDesign_middleCircle.Width = this.visual.Width / 2;
            this.visualDesign_middleCircle.Fill = new SolidColorBrush(Colors.Blue);
            this.visualDesign_middleCircle.Stroke = new SolidColorBrush(Colors.Blue);

            Canvas.SetLeft(this.visual, x);
            Canvas.SetTop(this.visual, y);

            Canvas.SetLeft(this.visualDesign_middleCircle, x + this.visual.Width / 4);
            Canvas.SetTop(this.visualDesign_middleCircle, y + this.visual.Height / 4);


        }




        public void SetHitHox()
        {
            this.hitbox.X = Canvas.GetLeft(this.visual);
            this.hitbox.Y = Canvas.GetTop(this.visual);
            this.hitbox.Width = this.visual.Width;
            this.hitbox.Height = this.visual.Height;
        }


    }
    public class Goal
    {
        public Rectangle visual = new Rectangle();
        public Rect hitbox = new Rect();

        public Goal(int x, int y)
        {
            this.visual.Width = 15;
            this.visual.Height = 75;
            this.visual.Fill = new SolidColorBrush(Colors.Purple);
            this.visual.Stroke = new SolidColorBrush(Colors.Purple);

            Canvas.SetLeft(this.visual, x);
            Canvas.SetTop(this.visual, y);
            this.SetHitHox();
        
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
        public static Window1 menu = new Window1();
        public DispatcherTimer ballMoveTimer = new DispatcherTimer();
        public Point pos;
        public double angle;
        public Portals portal_blue = new Portals(new SolidColorBrush(Colors.Blue));
        public Portals portal_orange = new Portals(new SolidColorBrush(Colors.Orange));
        public Bullets blue = new Bullets(new SolidColorBrush(Colors.Blue));
        public Bullets orange = new Bullets(new SolidColorBrush(Colors.Orange));

        public List<Walls> blocksLVL1 = new List<Walls>();
        public List<Walls> blocksLVL2 = new List<Walls>();
        public List<Walls> blocksLVL3 = new List<Walls>();

        public List<TelePad> telePadsLVL1 = new List<TelePad>();
        public List<TelePad> telePadsLVL2 = new List<TelePad>();
        public List<TelePad> telePadsLVL3 = new List<TelePad>();

        public Goal goalLVL1 = new Goal(1920 - 50 - 15, 900);        //1920-50-15,900
        public Goal goalLVL2 = new Goal(1820 + 50 - 15, 15 + 620 + 310 / 2 + 75 / 2); //1820+50-15, 15+620+310/2+75/2
        public Goal goalLVL3 = new Goal(1820 + 50 - 15, 15 + 620 + 310 / 2 + 75 / 2); //1820 + 50 - 15, 15 + 620 + 310 / 2 + 75 / 2



        public List<Walls> activeWalls = new List<Walls>();
        public List<TelePad> activeTeles = new List<TelePad>();
        public Goal activeGoal;

        public Point ballresetpoint; 
        public string ballresetdir;

        public static int score = 0;

        //public Label ballloc = new Label();
        //public Label ballloch = new Label();

        public Player player;
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Ball ball = new Ball(50, 50, 100, 51);
        public Button backToMenu = new Button();

        public int currentLvl;


        public MainWindow(int lvl, SolidColorBrush b)
        {
            currentLvl = lvl;
            makeObjsLists();
            
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ball.visual.Fill = b;



            //Canvas.SetTop(ballloc, 1080 / 2);
            //Canvas.SetLeft(ballloc, 1920 / 2);
            //ballloc.Width = 100;
            //ballloc.Height = 100;
            //ballloc.Background = new SolidColorBrush(Colors.Yellow);

            //Canvas.SetTop(ballloch, 1080 / 2 + 100);
            //Canvas.SetLeft(ballloch, 1920 / 2);
            //ballloch.Width = 100;
            //ballloch.Height = 100;
            //ballloch.Background = new SolidColorBrush(Colors.Yellow);



            player = new Player(1,1);

            switch (lvl)
            {
                case 1:
                    makeWalls(blocksLVL1, goalLVL1);
                    makeTelePads(telePadsLVL1);
                    ballresetpoint = new Point(100,51);
                    ballresetdir = "down";
                    //goalLVL1.SetHitHox();
                    break;
                case 2:
                    makeWalls(blocksLVL2, goalLVL2);
                    makeTelePads(telePadsLVL2);
                    ballresetpoint = new Point(51, 100);
                    ballresetdir = "right";
                    //goalLVL2.SetHitHox();
                    break;
                case 3:
                    makeWalls(blocksLVL3, goalLVL3);
                    makeTelePads(telePadsLVL3);
                    ballresetpoint = new Point(51, 100);
                    ballresetdir = "right";
                    //goalLVL3.SetHitHox();
                    break;
                default:
                    break;
            }
            Canvas.SetLeft(player.visual, Canvas.GetLeft(activeTeles[0].visualDesign_middleCircle) - player.visual.Width / 4);
            Canvas.SetTop(player.visual, Canvas.GetTop(activeTeles[0].visualDesign_middleCircle) - player.visual.Height/4);
            player.setGunMiddle();
            orange.ResetBullet(player);
            blue.ResetBullet(player);

            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(1);

            ballMoveTimer.Tick += BallMoveTimer_Tick;
            ballMoveTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);



            background.Children.Add(player.visual);
            background.Children.Add(player.gun);
            background.Children.Add(player.middle);
            background.Children.Add(blue.visual);
            background.Children.Add(orange.visual);
            background.Children.Add(portal_blue.visualVertical);
            background.Children.Add(portal_orange.visualVertical);
            background.Children.Add(portal_blue.visualSideWays);
            background.Children.Add(portal_orange.visualSideWays); 
            background.Children.Add(ball.visual);
            background.Children.Add(backToMenu);
            //background.Children.Add(ballloc);
            //background.Children.Add(ballloch);


            ball.reset(ballresetpoint, ballresetdir);

            backToMenu.Content = "Menu";
            backToMenu.Width = 60;
            backToMenu.Height = 40;
            backToMenu.Click += gameExit;
            backToMenu.FontFamily = new FontFamily("Arial");
            //backToMenu.VerticalAlignment = VerticalAlignment.Top;
            //backToMenu.HorizontalAlignment = HorizontalAlignment.Center;
            backToMenu.Background = null;
            backToMenu.BorderBrush = new SolidColorBrush(Colors.Red);
            backToMenu.Foreground = new SolidColorBrush(Colors.White);




            Canvas.SetLeft(backToMenu, 1);
            Canvas.SetTop(backToMenu, 1);


            GC.Collect();
        }

        public void makeObjsLists()
        {
            telePadsLVL1.Add(new TelePad(50+573/2-15, 35+980/2-15)); //left

            telePadsLVL1.Add(new TelePad(50+573+50+573/2-30, 35 + 980 / 2 - 30));
            telePadsLVL1.Add(new TelePad(50+573+50+573+50+573/2-30, 35 + 980 / 2 - 30));

            blocksLVL1.Add(new Walls(new SolidColorBrush(Colors.Black), 50, 1080-50, 1920 - 50, 50, "h"));//floor
            blocksLVL1.Add(new Walls(new SolidColorBrush(Colors.Black), 1920 - 50, 0, 50, 1080, "v")); //right wall
            blocksLVL1.Add(new Walls(new SolidColorBrush(Colors.Black), 50, 0, 1820, 50, "h"));//celling                   //boundarys
            blocksLVL1.Add(new Walls(new SolidColorBrush(Colors.Black), 0, 0, 50, 1920, "v"));//left wall

            blocksLVL1.Add(new Walls(new SolidColorBrush(Colors.Black), 50+573, 50, 50, 980, "v"));//middle
            blocksLVL1.Add(new Walls(new SolidColorBrush(Colors.Black), 623+50+573, 50, 50, 980, "v"));//middle

            /////////
            telePadsLVL2.Add(new TelePad(50+100, 50+980-310/2-30)); //bottom
            telePadsLVL2.Add(new TelePad(50+1820-200, 50+310+50+310/2-40)); //middle
            telePadsLVL2.Add(new TelePad(50+100,50+310/2-30)); //top


            blocksLVL2.Add(new Walls(new SolidColorBrush(Colors.Black), 50, 1080-50, 1920 - 50, 50, "h"));//floor
            blocksLVL2.Add(new Walls(new SolidColorBrush(Colors.Black), 1920 - 50, 0, 50, 1080, "v")); //right wall
            blocksLVL2.Add(new Walls(new SolidColorBrush(Colors.Black), 50, 0, 1820, 50, "h"));//celling                   //boundarys
            blocksLVL2.Add(new Walls(new SolidColorBrush(Colors.Black), 0, 0, 50, 1920, "v"));//left wall
            //x,y,w,h
            blocksLVL2.Add(new Walls(new SolidColorBrush(Colors.Black), 50, 50+310, 1820-400, 50, "h"));//top                   //boundarys
            blocksLVL2.Add(new Walls(new SolidColorBrush(Colors.Black), 50+400, 50+310+310, 1820-400, 50, "h"));//bottom                   //boundarys




            ////////
            telePadsLVL3.Add(new TelePad(50 + 100, 50+310+50+100)); //bottom left
            telePadsLVL3.Add(new TelePad(50 + 600, 50 + 980 - 310 / 2 - 30)); //bottom right
            telePadsLVL3.Add(new TelePad(50 + 1820 - 200, 50 + 310 + 50 + 310 / 2 - 40)); //middle
            telePadsLVL3.Add(new TelePad(50 + 100, 50 + 310 / 2 - 30)); //top


            blocksLVL3.Add(new Walls(new SolidColorBrush(Colors.Black), 50, 1080-50, 1920 - 50, 50, "h"));//floor
            blocksLVL3.Add(new Walls(new SolidColorBrush(Colors.Black), 1920 - 50, 0, 50, 1080, "v")); //right wall
            blocksLVL3.Add(new Walls(new SolidColorBrush(Colors.Black), 50, 0, 1820, 50, "h"));//celling                   //boundarys
            blocksLVL3.Add(new Walls(new SolidColorBrush(Colors.Black), 0, 0, 50, 1920, "v"));//left wall
            //x,y,w,h
            blocksLVL3.Add(new Walls(new SolidColorBrush(Colors.Black), 50, 50 + 310, 1820 - 400, 50, "h"));//top                   //boundarys
            blocksLVL3.Add(new Walls(new SolidColorBrush(Colors.Black), 50 + 400, 50 + 310 + 310, 1820 - 400, 50, "h"));//bottom                   //boundarys
            blocksLVL3.Add(new Walls(new SolidColorBrush(Colors.Black),1820-400,50,50,310,"v"));
            blocksLVL3.Add(new Walls(new SolidColorBrush(Colors.Black),50+400,1080-50-310,50,310 , "v"));
            blocksLVL3.Add(new Walls(new SolidColorBrush(Colors.Black), 50+1820/2, 100+310, 50, 310 , "v"));


        }
        public void makeWalls(List<Walls> wallstomake, Goal goal)
        {

            background.Children.Add(goal.visual);
            activeGoal = goal;

            orange.ResetBullet(player);
            blue.ResetBullet(player);
            activeGoal.SetHitHox();


            foreach (Walls item in wallstomake)
            {
                activeWalls.Add(item);
                background.Children.Add(item.visual);
                item.SetHitHox();

            }


        }
        public void makeTelePads(List<TelePad> tpads)
        {
            



            foreach (TelePad item in tpads)
            {
                activeTeles.Add(item);
                background.Children.Add(item.visual);
                background.Children.Add(item.visualDesign_middleCircle);
                item.visualDesign_middleCircle.MouseDown += VisualDesign_middleCircle_MouseDown;
            }
        }
        public void VisualDesign_middleCircle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {


                var x = Canvas.GetLeft(sender as Ellipse);
                var y = Canvas.GetTop(sender as Ellipse);

                Canvas.SetLeft(player.visual, x - player.visual.Width / 4);
                Canvas.SetTop(player.visual, y - player.visual.Height / 4);
                player.setGunMiddle();
                orange.ResetBullet(player);
                blue.ResetBullet(player);
            }
        }
        public void gameExit(object sender, RoutedEventArgs e)
        {
            MainWindow.menu.Show();
            Window1.games[currentLvl-1].Hide();
            this.dispatcherTimer.Stop();
            this.ballMoveTimer.Stop();
        }
        public void BallMoveTimer_Tick(object sender, EventArgs e)
        {
            ball.move();
        }
        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            GC.Collect();
            //foreach (Walls item in blocks)
            //{
            //    item.SetHitHox();
            //}                           //resets all hitboxes
            blue.SetHitHox();
            orange.SetHitHox();
            ball.SetHitHox();
            activeGoal.SetHitHox();


            var col = ColisionDetc(ball.hitbox, activeGoal.hitbox);
            var bx = Canvas.GetLeft(ball.visual);
            var by = Canvas.GetTop(ball.visual);

            //ballloch.Content = $"{ball.hitbox.X}, {ball.hitbox.Y} ";


            //ballloc.Content = $"{bx}, {by}";

            if (ball.canTele == 1) { teleportCheck(); }

            if (ColisionDetc(ball.hitbox, activeGoal.hitbox))
            {
                //Canvas.SetLeft(ball.visual, 1);
                ball.reset(ballresetpoint,ballresetdir);
                ball.SetHitHox();
                nextLevel(currentLvl);
            }

            foreach (Walls x in activeWalls)
            {
                if (ColisionDetc(blue.hitbox, x.hitbox))
                {
                    blue.shooting = false;
                    player.shooting = false;
                    if ((string)x.visual.Tag == "v")
                    {
                        
                        portal_blue.spawn(blue, 1, x, player);
                    }
                    else
                    {

                        portal_blue.spawn(blue, 0, x, player);
                    }
                }
                if (ColisionDetc(orange.hitbox, x.hitbox))
                {
                    orange.shooting = false;
                    player.shooting = false;
                    if ((string)x.visual.Tag == "v")
                    { 
                        portal_orange.spawn(orange, 1, x, player);
                    }
                    else
                    {
                        portal_orange.spawn(orange, 0, x, player);

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
                    player.Shoot(1, pos, angle, blue, orange, player);
                    blue.shooting = true;
                }
                else if (Mouse.RightButton == MouseButtonState.Pressed) 
                {
                    player.shooting = true;
                    player.Shoot(2, pos, angle, blue, orange, player);
                    orange.shooting = true;
                }
            }


            RotateTransform rotateTransform = new RotateTransform(angle,0,player.gun.Height/2);
            player.gun.RenderTransform = rotateTransform;

            if (ball.canColide == 1)
            { 
                foreach (var x in activeWalls)
                {

                    if (ball.hitbox.IntersectsWith(x.hitbox) && (ball.hitbox.IntersectsWith(orange.hitbox) == false || ball.hitbox.IntersectsWith(blue.hitbox) == false)) { ball.reset(ballresetpoint, ballresetdir); score += 1; }
                }
            }

            GC.Collect();
        }
        public void teleportCheck()
        {
            DispatcherTimer wait = new DispatcherTimer();
            wait.Tick += Wait_Tick;
            wait.Interval = new TimeSpan(0, 0, 3);

            if (ball.hitbox.IntersectsWith(portal_blue.hitBoxVertical) || ball.hitbox.IntersectsWith(portal_blue.hitBoxSideWays))//blue portal 
            {
                if (portal_orange.visualVertical.Visibility == Visibility.Visible)//vertical
                {
                    Canvas.SetLeft(ball.visual, Canvas.GetLeft(portal_orange.visualVertical));
                    Canvas.SetTop(ball.visual, Canvas.GetTop(portal_orange.visualVertical));
                }
                else if (portal_orange.visualSideWays.Visibility == Visibility.Visible)//sideways 
                {
                    Canvas.SetLeft(ball.visual, Canvas.GetLeft(portal_orange.visualSideWays));
                    Canvas.SetTop(ball.visual, Canvas.GetTop(portal_orange.visualSideWays));
                }
                ball.direction = portal_orange.outDir;
                ball.canTele = 0;
                ball.canColide = 0;
                wait.Start();
            }


            if (ball.hitbox.IntersectsWith(portal_orange.hitBoxVertical) || ball.hitbox.IntersectsWith(portal_orange.hitBoxSideWays))//blue portal 
            {
                if (portal_blue.visualVertical.Visibility == Visibility.Visible)//vertical
                {
                    Canvas.SetLeft(ball.visual, Canvas.GetLeft(portal_blue.visualVertical));
                    Canvas.SetTop(ball.visual, Canvas.GetTop(portal_blue.visualVertical));
                }
                else if (portal_blue.visualSideWays.Visibility == Visibility.Visible)//sideways 
                {
                    Canvas.SetLeft(ball.visual, Canvas.GetLeft(portal_blue.visualSideWays));
                    Canvas.SetTop(ball.visual, Canvas.GetTop(portal_blue.visualSideWays));
                }
                ball.direction = portal_blue.outDir;
                ball.canTele = 0;
                ball.canColide = 0;
                wait.Start();
            }

            void Wait_Tick(object sender, EventArgs e)
            {
                ball.canTele = 1;
                ball.canColide = 1;
                wait.Stop();
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
        public void nextLevel(int nextLevel)
        {
            this.Close();
            if (currentLvl == 3)
            {
                var end = new End();
                end.Show();
                this.Close();
            }
            else
            {
                this.dispatcherTimer.Stop();
                this.ballMoveTimer.Stop();

                this.Close();
                Window1.games[currentLvl].Show();
                Window1.games[currentLvl].dispatcherTimer.Start();
                Window1.games[currentLvl].ballMoveTimer.Start();
            }










        }



        
    }
}

