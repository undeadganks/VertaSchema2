using VertaSchema2.Data;

namespace VertaSchema2.Stats
{
    public readonly struct StatModifier
    {
        public StatType Stat { get; }
        public ModifierOp Operation { get; }
        public float Value { get; }
        public int Priority { get; }

        public StatModifier(StatType stat, ModifierOp op, float value, int priority = 0)
        {
            Stat = stat;
            Operation = op;
            Value = value;
            Priority = priority;
        }
    }
}
