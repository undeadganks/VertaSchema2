using UnityEngine;
using VertaSchema2.Data;
using VertaSchema2.Stats;

namespace VertaSchema2.Battle
{
    [CreateAssetMenu(menuName = "VertaSchema2/Status Effect", fileName = "StatusEffect")]
    public class StatusEffectDefinition : ScriptableObject
    {
        public string StatusId;
        public string DisplayName;
        [TextArea] public string Description;
        public int DurationTurns = 3;
        public bool IsPositive;
        public StatModifier[] Modifiers;
    }

    public class StatusInstance
    {
        public StatusEffectDefinition Definition { get; private set; }
        public int RemainingTurns { get; private set; }

        public StatusInstance(StatusEffectDefinition definition)
        {
            Definition = definition;
            RemainingTurns = definition.DurationTurns;
        }

        public bool Tick()
        {
            RemainingTurns--;
            return RemainingTurns <= 0;
        }
    }
}
