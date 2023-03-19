using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZombieSurvivalShootingGame.Classes.StrategyPattern;
using ZombieSurvivalShootingGame.Constants;

namespace ZombieSurvivalShootingGame.Elements
{
    public class BulletBuilder
    {

    }

    public class Bullet : BaseElements
    {
        private readonly int speed = 20;
        private string direction;
        private int bulletLeft;
        private int bulletTop;
        private int index = 0;
        Timer timer = new Timer();

        public void MakeBullet(Form form, GunType gunType, int left, int top, string direction)
        {
            this.direction = direction;
            bulletLeft = left;
            bulletTop = top;
            Tag = ElementTags.Bullet;
            Size = new Size(5, 5);
            BackColor = Color.White;
            Left = left;
            Top = top;

            form.Controls.Add(this);

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
            }

            timer.Stop();
            timer.Dispose();
            timer = null;
        }
    }
}