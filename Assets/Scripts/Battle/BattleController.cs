using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VertaSchema2.AI;
using VertaSchema2.Characters;
using VertaSchema2.Gear;

namespace VertaSchema2.Battle
{
    public class BattleController : MonoBehaviour
    {
        [Header("Database & Prefabs")]
        public Data.GameDatabase Database;

        [Header("Party Setup")]
        public CharacterDefinition[] PlayerParty;
        public CharacterDefinition[] EnemyParty;

        [Header("Runtime State")]
        public List<Battler> AllBattlers = new();
        public TurnManager TurnManager = new();
        public SkillExecutor Executor = new();

        public System.Action<Battler> OnTurnStart;
        public System.Action<Battler, Skills.SkillDefinition, IEnumerable<Battler>> OnSkillExecuted;
        public System.Action<bool> OnBattleEnd;

        private void Start()
        {
            BuildBattlers();
            StartCoroutine(RunBattle());
        }

        private void BuildBattlers()
        {
            foreach (var def in PlayerParty.Where(d => d != null))
            {
                AllBattlers.Add(new Battler(def, new Loadout(), true));
            }

            foreach (var def in EnemyParty.Where(d => d != null))
            {
                AllBattlers.Add(new Battler(def, new Loadout(), false));
            }
        }

        private IEnumerator RunBattle()
        {
            while (true)
            {
                TurnManager.BuildRound(AllBattlers);
                while (TurnManager.Current != null)
                {
                    var actor = TurnManager.Current;
                    OnTurnStart?.Invoke(actor);

                    if (actor.IsPlayerControlled)
                    {
                        yield return PlayerTurn(actor);
                    }
                    else
                    {
                        yield return EnemyTurn(actor);
                    }

                    actor.TickStatuses();

                    if (CheckEnd())
                    {
                        OnBattleEnd?.Invoke(AllBattlers.Any(b => b.IsPlayerControlled && !b.IsDefeated));
                        yield break;
                    }

                    if (!TurnManager.Next())
                    {
                        break;
                    }
                }
            }
        }

        private IEnumerator PlayerTurn(Battler actor)
        {
            // Hook this up to UI; for now pick first skill and random target
            var skill = actor.Definition.Skills.FirstOrDefault();
            if (skill != null)
            {
                var foes = AllBattlers.Where(b => !b.IsPlayerControlled && !b.IsDefeated).ToArray();
                if (foes.Length > 0)
                {
                    var target = foes[Random.Range(0, foes.Length)];
                    Executor.Execute(skill, actor, new[] { target });
                    OnSkillExecuted?.Invoke(actor, skill, new[] { target });
                }
            }

            yield return null;
        }

        private IEnumerator EnemyTurn(Battler actor)
        {
            var enemyDef = actor.Definition as EnemyDefinition;
            var brain = enemyDef != null ? enemyDef.Brain : null;
            var allies = AllBattlers.Where(b => !b.IsPlayerControlled).ToArray();
            var foes = AllBattlers.Where(b => b.IsPlayerControlled).ToArray();

            var decision = brain?.ChooseAction(actor, allies, foes) ?? default;
            if (decision.Skill == null && actor.Definition.Skills.Count > 0)
            {
                decision = new EnemyDecision(actor.Definition.Skills[0], foes.Where(f => !f.IsDefeated).Take(1).ToArray());
            }

            if (decision.Skill != null && decision.Targets != null && decision.Targets.Length > 0)
            {
                Executor.Execute(decision.Skill, actor, decision.Targets);
                OnSkillExecuted?.Invoke(actor, decision.Skill, decision.Targets);
            }

            yield return null;
        }

        private bool CheckEnd()
        {
            var playersAlive = AllBattlers.Any(b => b.IsPlayerControlled && !b.IsDefeated);
            var enemiesAlive = AllBattlers.Any(b => !b.IsPlayerControlled && !b.IsDefeated);
            return !(playersAlive && enemiesAlive);
        }
    }
}
