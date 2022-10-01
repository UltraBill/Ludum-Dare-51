using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Passive
{
    public class MaxHealthPassive : Passive
    {
        public MaxHealthPassive()
        {
            MaxLifePoint = 10;
        }

        public override void AdditionalEffect()
        {
            return;
        }
    }
}
