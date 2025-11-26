using System.Collections.Generic;
using UnityEngine;
using VertaSchema2.Data;

namespace VertaSchema2.Characters
{
    [CreateAssetMenu(menuName = "VertaSchema2/Character", fileName = "CharacterDefinition")]
    public class CharacterDefinition : ScriptableObject
    {
        public string CharacterId;
        public string DisplayName;
        public Sprite Portrait;
        public RuntimeAnimatorController AnimatorController;
        public List<StatEntry> BaseStats = new();
        public List<Skills.SkillDefinition> Skills = new();
    }

    [System.Serializable]
    public struct StatEntry
    {
        public StatType Stat;
        public float Value;
    }
}
