using UnityEngine;

namespace Assets.Scripts.Passive
{
    public class DoubleJumpPassive : Passive
    {
        public DoubleJumpPassive()
        {
            CanDoubleJump = true;
            sprite = Resources.Load<Sprite>("Passif/p_jump");
        }

        public override void AdditionalEffect()
        {
            return;
        }
    }
}
