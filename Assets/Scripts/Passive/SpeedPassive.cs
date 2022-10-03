using UnityEngine;

namespace Assets.Scripts.Passive
{
    public class SpeedPassive : Passive
    {
        public SpeedPassive()
        {
            MovementSpeed = 10f;
            sprite = Resources.Load<Sprite>("Passif/p_speed");

            name = "Speed Boost";
            description = "Augment your Speed";
        }

        public override void AdditionalEffect()
        {
            return;
        }
    }
}
