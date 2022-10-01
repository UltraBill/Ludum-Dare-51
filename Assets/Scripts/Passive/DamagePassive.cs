using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Passive
{
    public class DamagePassive : Passive
    {
        public DamagePassive()
        {
            Damage = 10;
        }

        public override void AdditionalEffect()
        {
            return;
        }
    }
}
