using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_V0._1
{
    public partial class Form1 : Form
    {
        bool left, right, up, down;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (left == true) { pic_character.Left -= 5; }
            if (right == true) { pic_character.Left += 5; }
            if (down == true) { pic_character.Top += 5; }
            if (up == true) { pic_character.Top -= 5; }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }





        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { up = false; }
            if (e.KeyCode == Keys.Down) { down = false; }
            if (e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.Right) { right = false; }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up){ up = true; }
            if (e.KeyCode == Keys.Down){ down = true; }
            if (e.KeyCode == Keys.Left){ left = true; }
            if (e.KeyCode == Keys.Right){ right = true; }
        }
    }
}
