using System.Collections.Generic;
using System.Linq;
using VertaSchema2.Data;
using VertaSchema2.Stats;

namespace VertaSchema2.Gear
{
    public class GearInstance
    {
        public GearDefinition Definition { get; private set; }
        public int Level { get; private set; }

        public GearInstance(GearDefinition definition, int level = 1)
        {
            Definition = definition;
            Level = level;
        }

        public IEnumerable<StatModifier> GetModifiers() => Definition?.StatModifiers ?? Enumerable.Empty<StatModifier>();
    }

    public class Loadout
    {
        private readonly Dictionary<GearSlot, GearInstance> _slots = new();

        public bool Equip(GearSlot slot, GearInstance gear)
        {
            _slots[slot] = gear;
            return true;
        }

        public GearInstance Get(GearSlot slot)
        {
            return _slots.TryGetValue(slot, out var value) ? value : null;
        }

        public IEnumerable<GearInstance> AllGear() => _slots.Values.Where(v => v != null);

        public IEnumerable<StatModifier> AllModifiers() => AllGear().SelectMany(g => g.GetModifiers());
    }
}
