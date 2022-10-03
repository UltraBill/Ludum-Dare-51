using UnityEngine;

namespace Assets.Scripts.Passive
{
    public class DoubleJumpPassive : Passive
    {
        public DoubleJumpPassive()
        {
            CanDoubleJump = true;
            sprite = Resources.Load<Sprite>("Passif/p_jump");

            name = "Double Jump";
            description = "Give a second jump mid air";
        }

        public override void AdditionalEffect()
        {
            return;
        }
    }
}
