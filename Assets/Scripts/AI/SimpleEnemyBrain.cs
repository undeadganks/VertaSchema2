using System.Linq;
using UnityEngine;
using VertaSchema2.Battle;
using VertaSchema2.Skills;

namespace VertaSchema2.AI
{
    [CreateAssetMenu(menuName = "VertaSchema2/AI/Simple Brain", fileName = "SimpleEnemyBrain")]
    public class SimpleEnemyBrain : EnemyBrainBase
    {
        public SkillDefinition PreferredSkill;

        public EnemyDecision ChooseAction(Battler self, System.Collections.Generic.IReadOnlyList<Battler> allies, System.Collections.Generic.IReadOnlyList<Battler> foes)
        {
            var livingFoes = foes.Where(f => !f.IsDefeated).ToArray();
            if (PreferredSkill != null && livingFoes.Length > 0)
            {
                var target = livingFoes[Random.Range(0, livingFoes.Length)];
                return new EnemyDecision(PreferredSkill, new[] { target });
            }

            return default;
        }
    }
}
