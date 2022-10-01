using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Passive
{
    public class SpeedPassive : Passive
    {
        public SpeedPassive()
        {
            MovementSpeed = 10f;
        }

        public override void AdditionalEffect()
        {
            return;
        }
    }
}
