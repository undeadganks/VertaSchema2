using System.Collections.Generic;
using UnityEngine;
using VertaSchema2.Characters;
using VertaSchema2.Gear;
using VertaSchema2.Skills;

namespace VertaSchema2.Data
{
    [CreateAssetMenu(menuName = "VertaSchema2/Game Database", fileName = "GameDatabase")]
    public class GameDatabase : ScriptableObject
    {
        public List<CharacterDefinition> PlayableCharacters = new();
        public List<EnemyDefinition> Enemies = new();
        public List<GearDefinition> GearItems = new();
        public List<SkillDefinition> Skills = new();
        public List<Battle.StatusEffectDefinition> StatusEffects = new();
    }
}
