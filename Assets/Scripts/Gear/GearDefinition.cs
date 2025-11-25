using System.Collections.Generic;
using UnityEngine;
using VertaSchema2.Battle;
using VertaSchema2.Data;
using VertaSchema2.Stats;

namespace VertaSchema2.Gear
{
    [CreateAssetMenu(menuName = "VertaSchema2/Gear", fileName = "GearDefinition")]
    public class GearDefinition : ScriptableObject
    {
        public string GearId;
        public string DisplayName;
        public GearSlot Slot;
        public Sprite Icon;
        [TextArea] public string Description;
        public List<StatModifier> StatModifiers = new();
        public List<StatusEffectDefinition> PassiveStatuses = new();
    }
}
