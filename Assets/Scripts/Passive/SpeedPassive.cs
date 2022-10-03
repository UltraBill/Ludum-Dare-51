using UnityEngine;

namespace Assets.Scripts.Passive
{
    public class SpeedPassive : Passive
    {
        public SpeedPassive()
        {
            MovementSpeed = 10f;
            sprite = Resources.Load<Sprite>("Passif/p_speed");
        }

        public override void AdditionalEffect()
        {
            return;
        }
    }
}
