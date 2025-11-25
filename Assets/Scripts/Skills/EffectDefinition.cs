using UnityEngine;
using VertaSchema2.Data;

namespace VertaSchema2.Skills
{
    [System.Serializable]
    public class EffectDefinition
    {
        public EffectType Type;
        public DamageKind DamageKind;
        public TargetScope Scope;
        public StatType Stat;
        public ModifierOp Operation;
        public float Power = 1f;
        public float Variance = 0.1f;
        public float Chance = 1f;
        public int Priority;
        public Battle.StatusEffectDefinition Status;
    }
}
