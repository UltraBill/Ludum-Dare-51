using UnityEngine;

namespace Assets.Scripts.Passive
{
    public abstract class Passive
    {
        public Sprite sprite;

        public string name;
        public string description;

        public float? MovementSpeed { get; internal set; }
        public int? MaxDashNumber { get; internal set; }
        public bool? CanDoubleJump { get; internal set; }

        public int? Damage { get; internal set; }
        public int? HeavyDamageMultiplicator { get; internal set; }
        public float? Range { get; internal set; }
        public float? AreaOfEffectSize { get; internal set; }
        public float? CriticalChance { get; internal set; }
        public int? ArmorPoint { get; internal set; }

        public abstract void AdditionalEffect();

        public Sprite GetSprite()
        {
            return sprite;
        }
    }
}
