using System;
using System.Drawing;
using System.Windows.Forms;
using ZombieSurvivalShootingGame.Constants;
using ZombieSurvivalShootingGame.Properties;

namespace ZombieSurvivalShootingGame.Elements
{
    public class Ammo : BaseElements
    {
        public Ammo(Random random) : base(random)
        {
            Image = Resources.OsamaGG;
            Tag = ElementTags.Ammo;
        }
    }
}
