using System.Collections.Generic;
using System.Linq;
using VertaSchema2.Data;

namespace VertaSchema2.Battle
{
    public class TurnManager
    {
        private readonly List<Battler> _turnOrder = new();
        private int _index;

        public void BuildRound(IEnumerable<Battler> battlers)
        {
            _turnOrder.Clear();
            _turnOrder.AddRange(battlers.Where(b => !b.IsDefeated).OrderByDescending(b => b.Stats.Get(StatType.SPD)));
            _index = 0;
        }

        public Battler Current => _turnOrder.Count > 0 ? _turnOrder[_index] : null;

        public bool Next()
        {
            _index++;
            return _index < _turnOrder.Count;
        }
    }
}
