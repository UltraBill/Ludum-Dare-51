using UnityEngine;

namespace Assets.Scripts.Passive
{
    public class DamagePassive : Passive
    {
        public DamagePassive()
        {
            Damage = 2;
            sprite = Resources.Load<Sprite>("Passif/p_strength");

            name = "Strength";
            description = "Double your damages";
        }

        public override void AdditionalEffect()
        {
            return;
        }
    }
}
