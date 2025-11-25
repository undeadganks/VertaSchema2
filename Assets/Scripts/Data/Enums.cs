using UnityEngine;

namespace VertaSchema2.Data
{
    public enum StatType { HP, MP, ATK, DEF, MAG, SPR, SPD, CRT, ACC, EVA }

    public enum ModifierOp { Flat, PercentAdd, PercentMult }

    public enum GearSlot { Weapon, Armor, Accessory1, Accessory2 }

    public enum TargetScope { Self, AllySingle, AllyAll, EnemySingle, EnemyAll }

    public enum EffectType { Damage, Heal, Buff, Status }

    public enum DamageKind { Physical, Magical }
}
