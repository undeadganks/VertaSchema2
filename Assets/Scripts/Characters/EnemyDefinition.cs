using System.Collections.Generic;
using UnityEngine;
using VertaSchema2.Data;

namespace VertaSchema2.Characters
{
    [CreateAssetMenu(menuName = "VertaSchema2/Enemy", fileName = "EnemyDefinition")]
    public class EnemyDefinition : ScriptableObject
    {
        public string EnemyId;
        public string DisplayName;
        public Sprite Portrait;
        public RuntimeAnimatorController AnimatorController;
        public List<StatEntry> BaseStats = new();
        public List<Skills.SkillDefinition> Skills = new();
        public AI.EnemyBrainBase Brain;
        public LootTable LootTable;
    }

    [CreateAssetMenu(menuName = "VertaSchema2/Loot Table", fileName = "LootTable")]
    public class LootTable : ScriptableObject
    {
        public List<LootEntry> Entries = new();
    }

    [System.Serializable]
    public struct LootEntry
    {
        public Gear.GearDefinition Gear;
        public float Weight;
    }
}
