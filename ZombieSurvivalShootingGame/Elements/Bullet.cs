using System;
using System.Drawing;
using System.Windows.Forms;
using ZombieSurvivalShootingGame.Constants;

namespace ZombieSurvivalShootingGame.Elements
{
    public class Bullet : BaseElements
    {
        private readonly int speed = 20;
        private string direction;
        private int index = 0;
        Timer timer = new Timer();

        public Bullet(Random random, int index, int left, int top, string direction) : base(random)
        {
            this.direction = direction;
            Tag = ElementTags.Bullet;
            Size = new Size(5, 5);
            BackColor = Color.White;
            Left = left;
            Top = top;
            this.index = index;
            timer.Interval = speed;
            timer.Tick += new EventHandler(Tm_Tick);
            timer.Start();
        }

        public void Tm_Tick(object sender, EventArgs e)
        {
            HandleDirection();

            Clean();
        }

        private void HandleDirection()
        {
            if(index == 0)
            {
                if (direction == Directions.Left)
                {
                    Left -= speed;
                }

                if (direction == Directions.Right)
                {
                    Left += speed;
                }

                if (direction == Directions.Up)
                {
                    Top -= speed;
                }

                if (direction == Directions.Down)
                {
                    Top += speed;
                }
            }
            else if (index == 1)
            {
                if (direction == Directions.Left)
                {
                    Left -= (int)(speed * (80.00 / 100));
                    Top -= (int)(speed * (20.00 / 100));
                }

                if (direction == Directions.Right)
                {
                    Left += (int)(speed * (80.00 / 100));
                    Top -= (int)(speed * (20.00 / 100));
                }

                if (direction == Directions.Up)
                {
                    Top -= (int)(speed * (80.00 / 100));
                    Left -= (int)(speed * (20.00 / 100));
                }

                if (direction == Directions.Down)
                {
                    Top += (int)(speed * (80.00 / 100));
                    Left -= (int)(speed * (20.00 / 100));
                }
            }
            else
            {
                if (direction == Directions.Left)
                {
                    Left -= (int)(speed * (80.00 / 100));
                    Top += (int)(speed * (20.00 / 100));
                }

                if (direction == Directions.Right)
                {
                    Left += (int)(speed * (80.00 / 100));
                    Top += (int)(speed * (20.00 / 100));
                }

                if (direction == Directions.Up)
                {
                    Top -= (int)(speed * (80.00 / 100));
                    Left += (int)(speed * (20.00 / 100));
                }

                if (direction == Directions.Down)
                {
                    Top += (int)(speed * (80.00 / 100));
                    Left += (int)(speed * (20.00 / 100));
                }
            }
            
        }

        private void Clean()
        {
            if (Left < GameConstants.BoardLeft
                || Left > GameConstants.BoardRight
                || Top < GameConstants.BoardTop
                || Top > GameConstants.BoardDown)
            {
                Dispose();

                timer.Stop();
                timer.Dispose();
                timer = null;
            }
        }
    }
}