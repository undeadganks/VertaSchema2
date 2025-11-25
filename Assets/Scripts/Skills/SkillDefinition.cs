using System.Collections.Generic;
using UnityEngine;
using VertaSchema2.Data;

namespace VertaSchema2.Skills
{
    [CreateAssetMenu(menuName = "VertaSchema2/Skill", fileName = "SkillDefinition")]
    public class SkillDefinition : ScriptableObject
    {
        public string SkillId;
        public string DisplayName;
        [TextArea] public string Description;
        public int MPCost = 0;
        public TargetScope Scope = TargetScope.EnemySingle;
        public List<EffectDefinition> Effects = new();
    }
}
