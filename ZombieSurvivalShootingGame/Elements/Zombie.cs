using System;
using System.Drawing;
using System.Windows.Forms;
using ZombieSurvivalShootingGame.Constants;
using ZombieSurvivalShootingGame.Properties;

namespace ZombieSurvivalShootingGame.Elements
{
    public class Zombie : BaseElements
    {
        public Zombie(Random random): base(random)
        {
            Image = Resources.skeleton_idle_Down;
            Tag = ElementTags.Zombie;
        }
    }
}
