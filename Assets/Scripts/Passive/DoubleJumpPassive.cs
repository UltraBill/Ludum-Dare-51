using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Passive
{
    public class DoubleJumpPassive : Passive
    {
        public DoubleJumpPassive()
        {
            CanDoubleJump = true;
        }

        public override void AdditionalEffect()
        {
            return;
        }
    }
}
