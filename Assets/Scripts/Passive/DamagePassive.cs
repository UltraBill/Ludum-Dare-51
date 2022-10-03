using UnityEngine;

namespace Assets.Scripts.Passive
{
    public class DamagePassive : Passive
    {
        public DamagePassive()
        {
            Damage = 10;
            sprite = Resources.Load<Sprite>("Passif/p_strength");
        }

        public override void AdditionalEffect()
        {
            return;
        }
    }
}
