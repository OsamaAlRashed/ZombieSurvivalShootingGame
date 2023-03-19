using System;
using System.Windows.Forms;
using ZombieSurvivalShootingGame.Constants;
using ZombieSurvivalShootingGame.Properties;

namespace ZombieSurvivalShootingGame.Elements
{
    public class Gun : BaseElements
    {
        public Gun(Random random): base(random)
        {
            Image = Resources.Gun;
            Tag = ElementTags.Gun;
        }
    }
}
