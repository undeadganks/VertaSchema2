using System.Collections.Generic;
using UnityEngine;
using VertaSchema2.Data;
using VertaSchema2.Skills;

namespace VertaSchema2.Battle
{
    public class SkillExecutor
    {
        public void Execute(SkillDefinition skill, Battler user, IReadOnlyList<Battler> targets)
        {
            if (user.CurrentMP < skill.MPCost)
            {
                Debug.LogWarning($"{user.Definition.DisplayName} lacks MP for {skill.DisplayName}");
                return;
            }

            user.SpendMP(skill.MPCost);

            foreach (var target in targets)
            {
                foreach (var effect in skill.Effects)
                {
                    ResolveEffect(effect, user, target);
                }
            }
        }

        private void ResolveEffect(EffectDefinition effect, Battler user, Battler target)
        {
            switch (effect.Type)
            {
                case EffectType.Damage:
                    ResolveDamage(effect, user, target);
                    break;
                case EffectType.Heal:
                    ResolveHeal(effect, user, target);
                    break;
                case EffectType.Buff:
                    ResolveBuff(effect, target);
                    break;
                case EffectType.Status:
                    TryApplyStatus(effect, target);
                    break;
            }
        }

        private void ResolveDamage(EffectDefinition effect, Battler user, Battler target)
        {
            var atkStat = effect.DamageKind == DamageKind.Magical ? StatType.MAG : StatType.ATK;
            var defStat = effect.DamageKind == DamageKind.Magical ? StatType.SPR : StatType.DEF;
            var attack = user.Stats.Get(atkStat);
            var defense = target.Stats.Get(defStat);
            var variance = Random.Range(1f - effect.Variance, 1f + effect.Variance);
            var raw = (attack * effect.Power - defense) * variance;
            var finalDamage = Mathf.Max(1, Mathf.RoundToInt(raw));
            target.ApplyDamage(finalDamage);
        }

        private void ResolveHeal(EffectDefinition effect, Battler user, Battler target)
        {
            var healAmount = user.Stats.Get(StatType.MAG) * effect.Power;
            target.ApplyHeal(Mathf.RoundToInt(healAmount));
        }

        private void ResolveBuff(EffectDefinition effect, Battler target)
        {
            var modifier = new Stats.StatModifier(effect.Stat, effect.Operation, effect.Power, effect.Priority);
            target.Stats.AddModifier(modifier);
        }

        private void TryApplyStatus(EffectDefinition effect, Battler target)
        {
            if (Random.value <= effect.Chance)
            {
                target.ApplyStatus(new StatusInstance(effect.Status));
            }
        }
    }
}
