using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZombieSurvivalShootingGame.Constants;
using ZombieSurvivalShootingGame.Properties;

namespace ZombieSurvivalShootingGame.Elements
{
    public class Health : BaseElements
    {
        public Health(Random random) : base(random)
        {
            Image = Resources.Heart;
            Tag = ElementTags.Health;
        }
    }
}
