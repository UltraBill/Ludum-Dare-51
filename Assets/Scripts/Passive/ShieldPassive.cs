using UnityEngine;

namespace Assets.Scripts.Passive
{
    public class ShieldPassive : Passive
    {
        public ShieldPassive()
        {
            ArmorPoint  = 4;
            MovementSpeed = 4f;
            sprite = Resources.Load<Sprite>("Passif/p_shield");

            name = "Armor";
            description = "Make you more resistent to incoming attack, but slow you down";
        }

        public override void AdditionalEffect()
        {
            return;
        }
    }
}
