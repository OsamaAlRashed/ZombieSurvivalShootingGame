using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZombieSurvivalShootingGame.Classes.StrategyPattern;
using ZombieSurvivalShootingGame.Constants;
using ZombieSurvivalShootingGame.Properties;
using ZombieSurvivalShootingGame.Settings;

namespace zombieShooter
{

    public partial class Game : Form
    {
        private readonly GameSettings gameSettings = new GameSettings();
        private string facing = Directions.Up;
        BitArray facingState = new BitArray(4);

        double playerHealth = 100; // this double vaiable is called player health
        int score = 0; // this integer will hold the score the player achieved through the game
        bool gameOver = false; // this boolean is false in the beginning and it will be used when the game is finished
        Random rnd = new Random(); // this is an instance of the random class we will use this to create a random number for this game

        GunType gunType = GunType.HandGun;
        bool flagGun = true;

        List<PictureBox> zombiesList = new List<PictureBox>();

        // end of listing variables

        int preScore = 0;
        
        public Game()
        {
            InitializeComponent();
            RestartGame();
        }
        
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameOver) return; // if game over is true then do nothing in this event

            // if the left key is pressed then do the following
            if (e.KeyCode == Keys.Left)
            {
                facingState.GoLeft();
                facing = Directions.Left; //change facing to left
                if(gunType == GunType.HandGun)
                {
                    player.Image = Resources.survivor_idle_handgun_Left;
                }
                else
                {
                    player.Image = Resources.survivor_idle_shotgun_Left;
                }
            }

            // end of left key selection

            // if the right key is pressed then do the following
            if (e.KeyCode == Keys.Right)
            {
                facingState.GoRight();
                facing = Directions.Right; // change facing to right
                if (gunType == GunType.HandGun)
                {
                    player.Image = Resources.survivor_idle_handgun_Right;
                }
                else
                {
                    player.Image = Resources.survivor_idle_shotgun_Right;
                }

            }
            // end of right key selection

            // if the up key is pressed then do the following
            if (e.KeyCode == Keys.Up)
            {
                facing = Directions.Up; // change facing to up
                goUp = true; // change go up to true
                if (gunType == GunType.HandGun)
                {
                    player.Image = Resources.survivor_idle_handgun_Up;
                }
                else
                {
                    player.Image = Resources.survivor_idle_shotgun_Up;
                }
                
            }

            // end of up key selection

            // if the down key is pressed then do the following
            if (e.KeyCode == Keys.Down)
            {
                facing = Directions.Down; // change facing to down
                goDown = true; // change go down to true
                if (gunType == GunType.HandGun)
                {
                    player.Image = Resources.survivor_idle_handgun_Down;
                }
                else
                {
                    player.Image = Resources.survivor_idle_shotgun_Down;
                }
            }
            // end of the down key selection

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (gameOver) return; // if game is over then do nothing in this event

            // below is the key up selection for the left key
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false; // change the go left boolean to false
            }

            // below is the key up selection for the right key
            if (e.KeyCode == Keys.Right)
            {
                goRight = false; // change the go right boolean to false
            }

            // below is the key up selection for the up key
            if (e.KeyCode == Keys.Up)
            {
                goUp = false; // change the go up boolean to false
            }

            // below is the key up selection for the down key
            if (e.KeyCode == Keys.Down)
            {
                goDown = false; // change the go down boolean to false
            }

            //below is the key up selection for the space key
            if (e.KeyCode == Keys.Space && gameSettings.Ammo > 0) // in this if statement we are checking if the space bar is up and ammo is more than 0
            {
                gameSettings.Ammo--; // reduce ammo by 1 from the total number
                Shoot(facing); // invoke the shoot function with the facing string inside it
                               //facing will transfer up, down, left or right to the function and that will shoot the bullet that way. 

                if (gameSettings.Ammo < 1) // if ammo is less than 1
                {
                    DropAmmo(); // invoke the drop ammo function
                }
            }

        }

        private void gameEngine(object sender, EventArgs e)
        {


            if (playerHealth > 1) // if player health is greater than 1
            {
                healthBar.Value = Convert.ToInt32(playerHealth); // assign the progress bar to the player health integer
            }
            else
            {
                // if the player health is below 1
                player.Image = Resources.dead; // show the player dead image
                GameTimer.Stop(); // stop the timer
                gameOver = true; // change game over to true
            }
            
            if(score % 10 == 0 && score != preScore)
            {
                preScore = score;
                Health();
            }

            if (score == 5 && flagGun)
            {
                MakeNewGun();
                flagGun = false;
            }

            label1.Text = "   Ammo:  " + gameSettings.Ammo; // show the ammo amount on label 1
            label2.Text = "Kills: " + score; // show the total kills on the score

            // if the player health is less than 20
            if (playerHealth < 20)
            {
                healthBar.ForeColor = Color.Red; // change the progress bar colour to red. 
            }

            if (goLeft && player.Left > 0)
            {
                player.Left -= gameSettings.Speed;
                // if moving left is true AND pacman is 1 pixel more from the left 
                // then move the player to the LEFT
            }
            if (goRight && player.Left + player.Width < 930)
            {
                player.Left += gameSettings.Speed;
                // if moving RIGHT is true AND player left + player width is less than 930 pixels
                // then move the player to the RIGHT
            }
            if (goUp && player.Top > 60)
            {
                player.Top -= gameSettings.Speed;
                // if moving TOP is true AND player is 60 pixel more from the top 
                // then move the player to the UP
            }
            if (goDown && player.Top + player.Height < 700)
            {
                player.Top += gameSettings.Speed;
                // if moving DOWN is true AND player top + player height is less than 700 pixels
                // then move the player to the DOWN
            }

            // run the first for each loop below
            // X is a control and we will search for all controls in this loop
            foreach (Control x in Controls)
            {
                // if the X is a picture box and X has a tag AMMO

                if (x is PictureBox && x.Tag == "ammo")
                {
                    // check is X in hitting the player picture box

                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        // once the player picks up the ammo

                        this.Controls.Remove(((PictureBox)x)); // remove the ammo picture box

                        ((PictureBox)x).Dispose(); // dispose the picture box completely from the program
                        gameSettings.Ammo += 5; // add 5 ammo to the integer
                    }
                }


                if (x is PictureBox && x.Tag == "health")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        this.Controls.Remove(((PictureBox)x)); // remove the ammo picture box

                        ((PictureBox)x).Dispose();
                        healthBar.Value += Math.Min(100 - healthBar.Value, 20);
                    }
                }

                if (x is PictureBox && x.Tag == "gun")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        this.Controls.Remove(((PictureBox)x)); // remove the ammo picture box

                        ((PictureBox)x).Dispose();
                        gunType = GunType.ShotGun;
                    }
                }

                // if the bullets hits the 4 borders of the game
                // if x is a picture box and x has the tag of bullet

                if (x is PictureBox && x.Tag == "bullet")
                {
                    // if the bullet is less the 1 pixel to the left
                    // if the bullet is more then 930 pixels to the right
                    // if the bullet is 10 pixels from the top
                    // if the bullet is 700 pixels to the buttom

                    if (((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 930 || ((PictureBox)x).Top < 10 || ((PictureBox)x).Top > 700)
                    {
                        this.Controls.Remove(((PictureBox)x)); // remove the bullet from the display
                        ((PictureBox)x).Dispose(); // dispose the bullet from the program
                    }
                }

                // below is the if statement which will be checking if the player hits a zombie

                if (x is PictureBox && x.Tag == "zombie")
                {

                    // below is the if statament thats checking the bounds of the player and the zombie

                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealth -= 1; // if the zombie hits the player then we decrease the health by 1
                    }

                    //move zombie towards the player picture box

                    if (((PictureBox)x).Left > player.Left)
                    {
                        ((PictureBox)x).Left -= gameSettings.ZombieSpeed; // move zombie towards the left of the player
                        ((PictureBox)x).Image = Resources.skeleton_idle_Left; // change the zombie image to the left
                    }

                    if (((PictureBox)x).Top > player.Top)
                    {
                        ((PictureBox)x).Top -= gameSettings.ZombieSpeed; // move zombie upwards towards the players top
                        ((PictureBox)x).Image = Resources.skeleton_idle_Top; // change the zombie picture to the top pointing image
                    }
                    if (((PictureBox)x).Left < player.Left)
                    {
                        ((PictureBox)x).Left += gameSettings.ZombieSpeed; // move zombie towards the right of the player
                        ((PictureBox)x).Image = Resources.skeleton_idle_Right; // change the image to the right image
                    }
                    if (((PictureBox)x).Top < player.Top)
                    {
                        ((PictureBox)x).Top += gameSettings.ZombieSpeed; // move the zombie towards the bottom of the player
                        ((PictureBox)x).Image = Resources.skeleton_idle_Down;// change the image to the down zombie
                    }
                }

                // below is the second for loop, this is nexted inside the first one
                // the bullet and zombie needs to be different than each other
                // then we can use that to determine if the hit each other

                foreach (Control j in this.Controls)
                {
                    // below is the selection thats identifying the bullet and zombie

                    if ((j is PictureBox && j.Tag == "bullet") && (x is PictureBox && x.Tag == "zombie"))
                    {
                        // below is the if statement thats checking if bullet hits the zombie
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++; // increase the kill score by 1 
                            this.Controls.Remove(j); // this will remove the bullet from the screen
                            j.Dispose(); // this will dispose the bullet all together from the program
                            this.Controls.Remove(x); // this will remove the zombie from the screen
                            x.Dispose(); // this will dispose the zombie from the program
                            MakeZombies(); // this function will invoke the make zombies function to add another zombie to the game
                        }
                    }
                }
            }
        }

        private void DropAmmo()
        {
            // this function will make a ammo image for this game

            PictureBox ammo = new PictureBox(); // create a new instance of the picture box
            ammo.Image = Resources.OsamaGG; // assignment the ammo image to the picture box
            ammo.SizeMode = PictureBoxSizeMode.AutoSize; // set the size to auto size
            ammo.Left = rnd.Next(10, 890); // set the location to a random left
            ammo.Top = rnd.Next(50, 600); // set the location to a random top
            ammo.Tag = "ammo"; // set the tag to ammo
            ammo.BackColor = Color.Transparent;
            this.Controls.Add(ammo); // add the ammo picture box to the screen
            ammo.BringToFront(); // bring it to front
            player.BringToFront(); // bring the player to front
        }

        private void Health()
        {
            // this function will make a ammo image for this game
            PictureBox health = new PictureBox(); // create a new instance of the picture box
            health.Image = Resources.Heart; // assignment the ammo image to the picture box
            health.SizeMode = PictureBoxSizeMode.AutoSize; // set the size to auto size
            health.Left = rnd.Next(10, 890); // set the location to a random left
            health.Top = rnd.Next(50, 600); // set the location to a random top
            health.Tag = "health"; // set the tag to ammo
            health.BackColor = Color.Transparent;
            Controls.Add(health); // add the ammo picture box to the screen
            health.BringToFront(); // bring it to front
            player.BringToFront(); // bring the player to front
        }

        private void MakeNewGun()
        {
            PictureBox gun = new PictureBox(); 
            gun.Image = Resources.Gun;
            gun.SizeMode = PictureBoxSizeMode.AutoSize;
            gun.Left = rnd.Next(10, 890); 
            gun.Top = rnd.Next(50, 600); 
            gun.Tag = "gun"; 
            gun.BackColor = Color.Transparent;
            Controls.Add(gun); 
            gun.BringToFront();
            player.BringToFront();
        }

        private void Shoot(string direct)
        {
            // this is the function thats makes the new bullets in this game
            Bullet shoot = new Bullet(); // create a new instance of the bullet class
            shoot.direction = direct; // assignment the direction to the bullet
            shoot.bulletLeft = player.Left + (player.Width / 2); // place the bullet to left half of the player
            shoot.bulletTop = player.Top + (player.Height / 2); // place the bullet on top half of the player
            shoot.MkBullet(this, gunType); // run the function mkBullet from the bullet class. 
        }

        private void MakeZombies()
        {
            // when this function is called it will make zombies in the game
            PictureBox zombie = new PictureBox(); // create a new picture box called zombie
            zombie.Tag = "zombie"; // add a tag to it called zombie
            zombie.Image = Resources.skeleton_idle_Down; // the default picture for the zombie is zdown
            zombie.Left = rnd.Next(0, 900); // generate a number between 0 and 900 and assignment that to the new zombies left 
            zombie.Top = rnd.Next(0, 800); // generate a number between 0 and 800 and assignment that to the new zombies top
            zombie.SizeMode = PictureBoxSizeMode.AutoSize; // set auto size for the new picture box
            zombie.BackColor = Color.Transparent;
            this.Controls.Add(zombie); // add the picture box to the screen
            player.BringToFront(); // bring the player to the front
        }

        private void RestartGame()
        {
            player.Image = Resources.survivor_idle_handgun_Up;

            foreach (PictureBox i in zombiesList)
            {
                Controls.Remove(i);
            }

            zombiesList.Clear();

            for (int i = 0; i < 3; i++)
            {
                MakeZombies();
            }

            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;

            playerHealth = 100;
            score = 0;
            gameSettings.Ammo = 10;

            GameTimer.Start();
        }

    }
}