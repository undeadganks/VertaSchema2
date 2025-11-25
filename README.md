# VertaSchema2

This repository contains a Unity-ready prototype for an HD2D-style, turn-based JRPG battle loop with a gear system similar to FFBE. All scripts live under `Assets/Scripts` and are organized by concern so you can drop them into a Unity project and wire up data via ScriptableObjects.

## Features
- Data-driven characters, skills, gear, statuses, and loot via ScriptableObjects.
- Stat system with flat, additive percent, and multiplicative modifiers.
- Gear loadouts that aggregate modifiers and passive statuses.
- Turn-order manager that sorts by Speed every round.
- Skill executor for damage, healing, buffs, and status application.
- Simple AI brain that selects targets and uses a preferred skill.
- Battle controller MonoBehaviour that orchestrates turns for player and enemy parties.

## Folder Overview
- `Assets/Scripts/Data`: enums and a `GameDatabase` ScriptableObject to reference all content.
- `Assets/Scripts/Stats`: stat block and modifier plumbing.
- `Assets/Scripts/Gear`: gear definitions, instances, and loadout aggregation.
- `Assets/Scripts/Skills`: skill and effect definitions.
- `Assets/Scripts/Characters`: character and enemy definitions plus loot tables.
- `Assets/Scripts/Battle`: runtime battle logic, battlers, status effects, and turn execution.
- `Assets/Scripts/AI`: `IEnemyBrain` contract and a `SimpleEnemyBrain` implementation.

## Getting Started in Unity
1. Create a new Unity project (URP 2D template recommended) and copy the `Assets` folder contents into your project.
2. Create ScriptableObjects (Right-click in Project window):
   - Characters, Enemies, Gear, Skills, Status Effects, Loot Tables, and a `GameDatabase` asset that references them all.
3. Add the `BattleController` MonoBehaviour to a scene. Assign player and enemy party definitions and hook optional delegates/events to UI.
4. Press Play to simulate a basic battle loop. Extend UI to let players pick skills/targets and replace the placeholder random targeting logic.

## Extending
- Add new `IEnemyBrain` implementations for richer AI patterns.
- Expand `EffectType` and `SkillExecutor` to support multi-hit, shields, or break mechanics.
- Connect `BattleController` events to UI for turn order, damage popups, and result screens.

## Notes
- The prototype uses UnityEngine Random and Mathf APIs; no third-party dependencies are required.
- Avoid wrapping import statements in try/catch blocks to keep scripts compliant with the project conventions.
