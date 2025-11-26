using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VertaSchema2.Characters;
using VertaSchema2.Data;
using VertaSchema2.Gear;
using VertaSchema2.Stats;

namespace VertaSchema2.Battle
{
    public class Battler
    {
        public CharacterDefinition Definition { get; }
        public StatBlock Stats { get; }
        public Loadout Loadout { get; }
        public List<StatusInstance> Statuses { get; } = new();
        public int CurrentHP { get; private set; }
        public int CurrentMP { get; private set; }
        public bool IsDefeated => CurrentHP <= 0;
        public bool IsPlayerControlled { get; }

        public Battler(CharacterDefinition definition, Loadout loadout, bool playerControlled)
        {
            Definition = definition;
            Loadout = loadout;
            IsPlayerControlled = playerControlled;

            Stats = new StatBlock();
            foreach (var entry in definition.BaseStats)
            {
                Stats.SetBase(entry.Stat, entry.Value);
            }

            foreach (var mod in loadout.AllModifiers())
            {
                Stats.AddModifier(mod);
            }

            foreach (var status in loadout.AllGear().SelectMany(g => g.Definition.PassiveStatuses))
            {
                ApplyStatus(new StatusInstance(status));
            }

            Refill();
        }

        public void Refill()
        {
            CurrentHP = Mathf.RoundToInt(Stats.Get(StatType.HP));
            CurrentMP = Mathf.RoundToInt(Stats.Get(StatType.MP));
        }

        public void ApplyDamage(int amount)
        {
            CurrentHP = Mathf.Max(0, CurrentHP - amount);
        }

        public void ApplyHeal(int amount)
        {
            CurrentHP = Mathf.Min(Mathf.RoundToInt(Stats.Get(StatType.HP)), CurrentHP + amount);
        }

        public void SpendMP(int amount)
        {
            CurrentMP = Mathf.Max(0, CurrentMP - amount);
        }

        public void ApplyStatus(StatusInstance instance)
        {
            Statuses.Add(instance);
            if (instance.Definition.Modifiers != null)
            {
                foreach (var mod in instance.Definition.Modifiers)
                {
                    Stats.AddModifier(mod);
                }
            }
        }

        public void TickStatuses()
        {
            for (int i = Statuses.Count - 1; i >= 0; i--)
            {
                var status = Statuses[i];
                var expired = status.Tick();
                if (expired)
                {
                    if (status.Definition.Modifiers != null)
                    {
                        foreach (var mod in status.Definition.Modifiers)
                        {
                            Stats.RemoveModifier(mod);
                        }
                    }

                    Statuses.RemoveAt(i);
                }
            }
        }
    }
}
