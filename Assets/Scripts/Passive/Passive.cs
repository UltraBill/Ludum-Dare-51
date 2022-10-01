using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Passive
{
    public abstract class Passive
    {
        public uint?  MaxLifePoint { get; set; }
        public float? MovementSpeed { get; set; }
        public uint?  MaxDashNumber { get; set; }

        public uint?  Damage { get; set; }
        public float? Range { get; set; }
        public float? AreaOfEffectSize { get; set; }
        public float? CriticalChance { get; set; }

        public abstract void AdditionalEffect();

    }
}
