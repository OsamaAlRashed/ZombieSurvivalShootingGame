
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;
using System;
using ZombieSurvivalShootingGame.Classes.StrategyPattern;

namespace zombieShooter
{
    class Bullet
    {

        // start the variable

        public string direction; // creating a public string called direction
        public int speed = 20; // creating a integer called speed and assigning a value of 20
        PictureBox BulletPic = new PictureBox(); // create a picture box 
        PictureBox BulletPic2 = new PictureBox(); // create a picture box 
        PictureBox BulletPic3 = new PictureBox(); // create a picture box 
        Timer tm = new Timer(); // create a new timer called tm. 

        public int bulletLeft; // create a new public integer
        public int bulletTop; // create a new public integer

        // end of the variables
        GunType gun = GunType.HandGun;
        public void MkBullet(Form form, GunType gunType)
        {
            // this function will add the bullet to the game play
            // it is required to be called from the main class

            gun = gunType;

            BulletPic.BackColor = System.Drawing.Color.White; // set the colour white for the bullet
            BulletPic.Size = new Size(5, 5); // set the size to the bullet to 5 pixel by 5 pixel
            BulletPic.Tag = "bullet"; // set the tag to bullet
            BulletPic.Left = bulletLeft; // set bullet left 
            BulletPic.Top = bulletTop; // set bullet right
            BulletPic.BringToFront(); // bring the bullet to front of other objects
            form.Controls.Add(BulletPic); // add the bullet to the screen


            if (gunType == GunType.ShotGun)
            {
                BulletPic2.BackColor = Color.White; // set the colour white for the bullet
                BulletPic2.Size = new Size(5, 5); // set the size to the bullet to 5 pixel by 5 pixel
                BulletPic2.Tag = "bullet"; // set the tag to bullet
                BulletPic2.Left = bulletLeft; // set bullet left 
                BulletPic2.Top = bulletTop; // set bullet right
                BulletPic2.BringToFront(); // bring the bullet to front of other objects
                form.Controls.Add(BulletPic2); // add the bullet to the screen

                BulletPic3.BackColor = Color.White; // set the colour white for the bullet
                BulletPic3.Size = new Size(5, 5); // set the size to the bullet to 5 pixel by 5 pixel
                BulletPic3.Tag = "bullet"; // set the tag to bullet
                BulletPic3.Left = bulletLeft; // set bullet left 
                BulletPic3.Top = bulletTop; // set bullet right
                BulletPic3.BringToFront(); // bring the bullet to front of other objects
                form.Controls.Add(BulletPic3); // add the bullet to the screen
            }


            tm.Interval = speed; // set the timer interval to speed
            tm.Tick += new EventHandler(Tm_Tick); // assignment the timer with an event
            tm.Start(); // start the timer
        }

        public void Tm_Tick(object sender, EventArgs e)
        {

            // if direction equals to left
            if (direction == "left")
            {
                BulletPic.Left -= speed; // move bullet towards the left of the screen
            }
            // if direction equals right
            if (direction == "right")
            {
                BulletPic.Left += speed; // move bullet towards the right of the screen
            }
            // if direction is up
            if (direction == "up")
            {
                BulletPic.Top -= speed; // move the bullet towards top of the screen
            }
            // if direction is down
            if (direction == "down")
            {
                BulletPic.Top += speed; // move the bullet bottom of the screen
            }

            if(gun == GunType.ShotGun)
            {
                if (direction == "left")
                {
                    BulletPic2.Left -= (int)(speed * (80.00 / 100)); 
                    BulletPic2.Top -= (int)(speed * (20.00 / 100));
                    BulletPic3.Left -= (int)(speed * (80.00 / 100));
                    BulletPic3.Top += (int)(speed * (20.00 / 100));
                }
                if (direction == "right")
                {
                    BulletPic2.Left += (int)(speed * (80.00 / 100));
                    BulletPic2.Top -= (int)(speed * (20.00 / 100));
                    BulletPic3.Left += (int)(speed * (80.00 / 100));
                    BulletPic3.Top += (int)(speed * (20.00 / 100));
                }
                if (direction == "up")
                {
                    BulletPic2.Top -= (int)(speed * (80.00 / 100));
                    BulletPic2.Left -= (int)(speed * (20.00 / 100));
                    BulletPic3.Top -= (int)(speed * (80.00 / 100));
                    BulletPic3.Left += (int)(speed * (20.00 / 100));
                }
                if (direction == "down")
                {
                    BulletPic2.Top += (int)(speed * (80.00 / 100));
                    BulletPic2.Left -= (int)(speed * (20.00 / 100));
                    BulletPic3.Top += (int)(speed * (80.00 / 100));
                    BulletPic3.Left += (int)(speed * (20.00 / 100));
                }
            }


            // if the bullet is less the 16 pixel to the left OR
            // if the bullet is more than 860 pixels to the right OR
            // if the bullet is 10 pixels from the top OR
            // if the bullet is 616 pixels to the bottom OR
            // IF ANY ONE OF THE CONDITIONS ARE MET THEN THE FOLLOWING CODE WILL BE EXECUTED

            if (BulletPic.Left < 16 || BulletPic.Left > 860 || BulletPic.Top < 10 || BulletPic.Top > 616)
            {
                tm.Stop(); // stop the timer
                tm.Dispose(); // dispose the timer event and component from the program
                BulletPic.Dispose(); // dispose the bullet
                BulletPic2.Dispose(); // dispose the bullet
                BulletPic3.Dispose(); // dispose the bullet
                tm = null; // nullify the timer object
                BulletPic = null; // nullify the bullet object
                BulletPic2 = null; // nullify the bullet object
                BulletPic3 = null; // nullify the bullet object
            }
        }
    }
}
