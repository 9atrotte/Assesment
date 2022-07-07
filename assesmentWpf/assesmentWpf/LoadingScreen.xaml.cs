
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace assesmentWpf
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : Window
    {
        public DispatcherTimer tmrLoading = new DispatcherTimer();

        readonly Random rand = new Random();
        int factNumber;
        readonly string[] facts = new string[]
        {
            "Despite Space Game's cool soundtrack, space in reality is completely silent! As space is a vacuum, transversal sound waves have no medium to transfer!",
            "Space Game's soundtrack composer, saopy, is pretty cool.",
            "The Sun's mass takes up 99.86% of the solar system's total mass!",
            "An asteroid the size of a small car enter's earth's atmosphere once a year, but burns up before it reaches us!",
            "Did you know the universe is expanding faster than the speed of light? That means that the edge of the universe we see is actually just old light, and you can never actually see the universe's edge!",
            "To observe black holes, we observe their infrared light output.",
            "The sunset on mars appears blue!",
            "In Neptune, Saturn and Uranus, it rains diamonds!",
            "A day on Venus is nearly 8 months on Earth!",
            "Jupiter's iconic Red Spot is actually shrinking!",
            "You wouldn't want to get caught in a rain storm on Venus! If you did, you'd be getting bathed in concentrated sulfuric acid D:",
            "If there was a medium for sound to travel in space, a black hole would be the loudest thing in the universe!",
            "The moon gets very hot during the day (107°C), but plummets at night (-153°C)!",
            "Venus is the only planet in our solar system to spin clockwise!",
            "It takes 8 minutes and 19 seconds for light to travel from the sun to the earth.",
            "Europa, one of Jupiter's moons, has saltwater geysers that are 20x taller than Mt. Everest.",
            "Saturn's rings are made from trillions of chunks of orbiting ice.",
            "Your funny bone is actually a nerve.",
            "The most requested funeral song in England is by Monty Python.",
            "Research shows that all blue-eyed people may be related.",
            "The average person will spend six months of their life waiting for red lights to turn green.",
            "A bolt of lightning contains enough energy to toast 100,000 slices of bread."
        };

        public Key UpControl, DownControl, LeftControl, RightControl;
        public string ShootControl, RecallControl, Difficulty;
        public DispatcherTimer tmrFacts = new DispatcherTimer();
        public DispatcherTimer tmrText = new DispatcherTimer();

        public LoadingScreen()
        {
            InitializeComponent();
            InitializeAnimation();


            cnvLoading.Height = SystemParameters.PrimaryScreenHeight;
            cnvLoading.Width = SystemParameters.PrimaryScreenWidth;

            factNumber = rand.Next(0, facts.Length);
            txtFacts.Text = Convert.ToString(facts[factNumber]);

            tmrLoading.Tick += Loading;
            tmrLoading.Interval = new TimeSpan(0, 0, 0, 0, 25);

            tmrText.Tick += Text;
            tmrText.Interval = new TimeSpan(0, 0, 0, 0, 500);

            tmrFacts.Tick += FactGenerate;
            tmrFacts.Interval = new TimeSpan(0, 0, 0, 5);
            

            rectLoading.Width = 0;
            txtLoading.Text = "Loading";
        }

        private void InitializeAnimation()
        {
            var menuScroll = new DoubleAnimation
            {
                From = -0,
                To = -1920,
                Duration = TimeSpan.FromSeconds(80 / 3),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
        }

        public void Text(object sender, EventArgs e)
        {
            txtLoading.Text += ".";
            if (Convert.ToString(txtLoading.Text) == "Loading....")
            {
                txtLoading.Text = "Loading";
            }
        }

        public void Loading(object sender, EventArgs e)
        {
            rectLoading.Width += 3;
            if (rectLoading.Width > 1095)
            {
                Window1.games[0].Show();
                Window1.games[0].dispatcherTimer.Start();
                Window1.games[0].ballMoveTimer.Start();
                this.Close();
                tmrLoading.Stop();
            }
        }

        public void FactGenerate(object sender, EventArgs e)
        {
            factNumber = rand.Next(0, facts.Length);

            txtFacts.Text = Convert.ToString(facts[factNumber]);
        }
    }
}
