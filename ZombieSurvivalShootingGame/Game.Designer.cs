
using System.Drawing;
using ZombieSurvivalShootingGame.Elements;
using ZombieSurvivalShootingGame.Properties;

namespace zombieShooter
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Ammo = new System.Windows.Forms.Label();
            this.Kills = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.healthBar = new System.Windows.Forms.ProgressBar();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.player = new ZombieSurvivalShootingGame.Elements.Player();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // Ammo
            // 
            this.Ammo.AutoSize = true;
            this.Ammo.BackColor = System.Drawing.Color.Transparent;
            this.Ammo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ammo.ForeColor = System.Drawing.Color.Transparent;
            this.Ammo.Location = new System.Drawing.Point(22, 18);
            this.Ammo.Name = "Ammo";
            this.Ammo.Size = new System.Drawing.Size(83, 20);
            this.Ammo.TabIndex = 0;
            this.Ammo.Text = "Ammo: 0";
            // 
            // Kills
            // 
            this.Kills.AutoSize = true;
            this.Kills.BackColor = System.Drawing.Color.Transparent;
            this.Kills.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Kills.ForeColor = System.Drawing.Color.Transparent;
            this.Kills.Location = new System.Drawing.Point(405, 18);
            this.Kills.Name = "Kills";
            this.Kills.Size = new System.Drawing.Size(68, 20);
            this.Kills.TabIndex = 1;
            this.Kills.Text = "Kills: 0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(698, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Health:";
            // 
            // healthBar
            // 
            this.healthBar.Location = new System.Drawing.Point(774, 18);
            this.healthBar.Name = "healthBar";
            this.healthBar.Size = new System.Drawing.Size(136, 23);
            this.healthBar.TabIndex = 3;
            this.healthBar.Value = 100;
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 20;
            this.GameTimer.Tick += new System.EventHandler(this.GameEngine);
            // 
            // player
            // 
            this.player.BackColor = System.Drawing.Color.Transparent;
            this.player.Location = new System.Drawing.Point(456, 492);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(75, 64);
            this.player.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.player.TabIndex = 4;
            this.player.TabStop = false;
            // 
            // Game
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::ZombieSurvivalShootingGame.Properties.Resources.Background2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(919, 653);
            this.Controls.Add(this.player);
            this.Controls.Add(this.healthBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Kills);
            this.Controls.Add(this.Ammo);
            this.DoubleBuffered = true;
            this.Name = "Game";
            this.Text = "Zombie Survival Shooting Game";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Ammo;
        private System.Windows.Forms.Label Kills;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar healthBar;
        private Player player;
        private System.Windows.Forms.Timer GameTimer;
    }
}

