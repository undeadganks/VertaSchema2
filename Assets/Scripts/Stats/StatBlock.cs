using System.Collections.Generic;
using System.Linq;
using VertaSchema2.Data;

namespace VertaSchema2.Stats
{
    public class StatBlock
    {
        private readonly Dictionary<StatType, float> _baseValues = new();
        private readonly List<StatModifier> _modifiers = new();

        public void SetBase(StatType stat, float value)
        {
            _baseValues[stat] = value;
        }

        public float GetBase(StatType stat)
        {
            return _baseValues.TryGetValue(stat, out var value) ? value : 0f;
        }

        public float Get(StatType stat)
        {
            var flat = 0f;
            var percentAdd = 0f;
            var percentMult = 1f;

            foreach (var mod in _modifiers.Where(m => m.Stat == stat).OrderBy(m => m.Priority))
            {
                switch (mod.Operation)
                {
                    case ModifierOp.Flat:
                        flat += mod.Value;
                        break;
                    case ModifierOp.PercentAdd:
                        percentAdd += mod.Value;
                        break;
                    case ModifierOp.PercentMult:
                        percentMult *= 1f + mod.Value;
                        break;
                }
            }

            return (GetBase(stat) + flat) * (1f + percentAdd) * percentMult;
        }

        public void AddModifier(StatModifier mod) => _modifiers.Add(mod);

        public void RemoveModifier(StatModifier mod) => _modifiers.Remove(mod);

        public void ClearModifiers() => _modifiers.Clear();
    }
}
