using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using zombieShooter;

namespace ZombieSurvivalShootingGame
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            var m = new Form1();
            m.Show();
        }
    }
}
