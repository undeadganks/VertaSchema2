using System.Collections.Generic;
using UnityEngine;
using VertaSchema2.Battle;
using VertaSchema2.Skills;

namespace VertaSchema2.AI
{
    public interface IEnemyBrain
    {
        EnemyDecision ChooseAction(Battler self, IReadOnlyList<Battler> allies, IReadOnlyList<Battler> foes);
    }

    public abstract class EnemyBrainBase : ScriptableObject, IEnemyBrain
    {
        public abstract EnemyDecision ChooseAction(Battler self, IReadOnlyList<Battler> allies, IReadOnlyList<Battler> foes);
    }

    public struct EnemyDecision
    {
        public SkillDefinition Skill;
        public Battler[] Targets;

        public EnemyDecision(SkillDefinition skill, Battler[] targets)
        {
            Skill = skill;
            Targets = targets;
        }
    }
}
