using System.Drawing;
using System;
using System.Windows.Forms;

namespace ZombieSurvivalShootingGame.Elements
{
    public class BaseElements : PictureBox
    {
        public BaseElements(Random random = default)
        {
            Left = random.Next(0, 900);
            Top = random.Next(0, 800);
            SizeMode = PictureBoxSizeMode.AutoSize;
            BackColor = Color.Transparent;

            BringToFront();
        }
    }
}
