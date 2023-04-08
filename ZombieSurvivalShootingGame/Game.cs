using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using ZombieSurvivalShootingGame.Classes.StrategyPattern;
using ZombieSurvivalShootingGame.Constants;
using ZombieSurvivalShootingGame.Elements;
using ZombieSurvivalShootingGame.Properties;
using ZombieSurvivalShootingGame.Settings;

namespace zombieShooter
{

    public partial class Game : Form
    {
        private readonly GameSettings gameSettings = new GameSettings();
        private string facing = Directions.Up;
        private BitArray facingState = new BitArray(4);
        private double playerHealth = 100;
        private int score = 0;
        private bool gameOver = false;
        private readonly Random random = new Random();
        private GunType gunType = GunType.HandGun;


        bool flagShotGun = true;
        List<PictureBox> zombiesList = new List<PictureBox>();
        int preScore = 0;
        
        public Game()
        {
            InitializeComponent();
            RestartGame();
        }
        
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameOver) return;

            if (e.KeyCode == Keys.Left)
            {
                facingState.GoLeft();
                facing = Directions.Left;
                if (gunType == GunType.HandGun)
                    player.Image = Resources.survivor_idle_handgun_Left;
                else
                    player.Image = Resources.survivor_idle_shotgun_Left;
            }

            if (e.KeyCode == Keys.Right)
            {
                facingState.GoRight();
                facing = Directions.Right;
                if (gunType == GunType.HandGun)
                    player.Image = Resources.survivor_idle_handgun_Right;
                else
                    player.Image = Resources.survivor_idle_shotgun_Right;
            }

            if (e.KeyCode == Keys.Up)
            {
                facing = Directions.Up;
                facingState.GoUp();

                if (gunType == GunType.HandGun)
                    player.Image = Resources.survivor_idle_handgun_Up;
                else
                    player.Image = Resources.survivor_idle_shotgun_Up;
            }

            if (e.KeyCode == Keys.Down)
            {
                facing = Directions.Down;
                facingState.GoDown();

                if (gunType == GunType.HandGun)
                    player.Image = Resources.survivor_idle_handgun_Down;
                else
                    player.Image = Resources.survivor_idle_shotgun_Down;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (gameOver) return;

            facingState.Stop(e.KeyCode);

            if (e.KeyCode == Keys.Space && gameSettings.Ammo > 0)
                Shoot(facing);    
        }

        private void GameEngine(object sender, EventArgs e)
        {
            if (playerHealth > 0)
            {
                healthBar.Value = Convert.ToInt32(playerHealth);
            }
            else
            {
                player.Image = Resources.dead;
                GameTimer.Stop();
                gameOver = true;
                return;
            }
            
            if(score % 10 == 0 && score != preScore)
            {
                preScore = score;
                Controls.Add(new Health(random));
                player.BringToFront();
            }

            if (score == 5 && flagShotGun)
            {
                Controls.Add(new Gun(random));
                player.BringToFront();
                flagShotGun = false;
            }

            Ammo.Text = GameConstants.AmmoLabel + gameSettings.Ammo;
            Kills.Text = GameConstants.KillsLabel + score;

            if (playerHealth < GameConstants.HealthBarRedWhenScore)
            {
                healthBar.ForeColor = Color.Red;
            }

            if (facingState.GetLeft() && player.Left > GameConstants.BoardLeft)
            {
                player.Left -= gameSettings.PlayerSpeed;
            }
            if (facingState.GetRight() && player.Left + player.Width < GameConstants.BoardRight)
            {
                player.Left += gameSettings.PlayerSpeed;
            }
            if (facingState.GetUp() && player.Top > GameConstants.BoardTop)
            {
                player.Top -= gameSettings.PlayerSpeed;
            }
            if (facingState.GetDown() && player.Top + player.Height < GameConstants.BoardDown)
            {
                player.Top += gameSettings.PlayerSpeed;
            }

            foreach (Control control in Controls)
            {
                HandleAmmo(control);
                HandleHealth(control);
                HandleGun(control);
                HandleBullet(control);
                HandleZombie(control);

                foreach (Control bullet in Controls)
                {
                    HandleZombieWithBullet(control, bullet);
                }
            }
        }

        #region Handle Controls

        private void HandleZombieWithBullet(Control control, Control bullet)
        {
            if (bullet is Bullet && control is Zombie zombie)
            {
                if (zombie.Bounds.IntersectsWith(bullet.Bounds))
                {
                    score++;
                    Controls.Remove(bullet);
                    bullet.Dispose();

                    Controls.Remove(zombie);
                    zombie.Dispose();

                    Controls.Add(new Zombie(random));
                }
            }
        }

        private void HandleZombie(Control control)
        {
            if (control is Zombie zombie)
            {
                if (zombie.Bounds.IntersectsWith(player.Bounds))
                {
                    playerHealth -= 1;
                }

                if (zombie.Left > player.Left)
                {
                    zombie.Left -= gameSettings.ZombieSpeed;
                    zombie.Image = Resources.skeleton_idle_Left;
                }

                if (zombie.Top > player.Top)
                {
                    zombie.Top -= gameSettings.ZombieSpeed;
                    zombie.Image = Resources.skeleton_idle_Top;
                }
                if (zombie.Left < player.Left)
                {
                    zombie.Left += gameSettings.ZombieSpeed;
                    zombie.Image = Resources.skeleton_idle_Right;
                }
                if (zombie.Top < player.Top)
                {
                    zombie.Top += gameSettings.ZombieSpeed;
                    zombie.Image = Resources.skeleton_idle_Down;
                }
            }
        }

        private void HandleBullet(Control control)
        {
            if (control is Bullet bullet)
            {
                if (bullet.Left < GameConstants.BoardLeft 
                 || bullet.Left > GameConstants.BoardRight
                 || bullet.Top < GameConstants.BoardTop
                 || bullet.Top > GameConstants.BoardDown)
                {
                    Controls.Remove(bullet);
                    bullet.Dispose();
                }
            }
        }

        private void HandleGun(Control control)
        {
            if (control is Gun gun)
            {
                if (gun.Bounds.IntersectsWith(player.Bounds))
                {
                    Controls.Remove(gun);

                    gun.Dispose();
                    gunType = GunType.ShotGun;
                }
            }
        }

        private void HandleHealth(Control control)
        {
            if (control is Health health)
            {
                if (health.Bounds.IntersectsWith(player.Bounds))
                {
                    Controls.Remove(health);

                    health.Dispose();
                    healthBar.Value += Math.Min(100 - healthBar.Value, 20);
                }
            }
        }

        private void HandleAmmo(Control control)
        {
            if (control is Ammo ammo)
            {
                if (ammo.Bounds.IntersectsWith(player.Bounds))
                {
                    Controls.Remove(ammo);

                    ammo.Dispose();
                    gameSettings.Ammo += 5;
                }
            }
        }

        #endregion

        private void Shoot(string direct)
        {
            gameSettings.Ammo--;

            var bulletLeft = player.Left + (player.Width / 2);
            var bulletTop = player.Top + (player.Height / 2);

            var bullet1 = new Bullet(random, 0, bulletLeft, bulletTop, direct);
            Controls.Add(bullet1);

            if(gunType == GunType.ShotGun)
            {
                var bullet2 = new Bullet(random, 1, bulletLeft, bulletTop, direct);
                Controls.Add(bullet2);
                var bullet3 = new Bullet(random, 2, bulletLeft, bulletTop, direct);
                Controls.Add(bullet3);
            }

            if (gameSettings.Ammo < 1)
            {
                Controls.Add(new Ammo(random));
                player.BringToFront();
            }
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
                Controls.Add(new Zombie(random));
            }
            playerHealth = 100;
            score = 0;
            gameSettings.Ammo = 10;

            GameTimer.Start();
        }
    }
}