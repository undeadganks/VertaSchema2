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
1. Install **Unity 2021 LTS or newer** (URP/2D renderer supported out-of-box).
2. Create a new project using the **2D (URP) template** so lighting/shaders are ready. If you choose the 3D template, add the **Universal RP** and **2D Renderer** packages via Package Manager and switch the Pipeline Asset to URP.
3. Copy the repository `Assets` folder into your project’s `Assets` directory (merge if prompted). No additional packages beyond Unity’s built-in 2D/URP modules are required.
4. In the Project window, right-click to create ScriptableObjects for Characters, Enemies, Gear, Skills, Status Effects, Loot Tables, and a `GameDatabase` asset that references all other assets.
5. Create a new scene (e.g., `BattleTest`) and drop the `BattleController` MonoBehaviour on an empty GameObject. Assign player and enemy party definitions plus optional UI hooks (turn order, command menu, damage popups). The prototype will auto-run a basic turn loop if none are provided.
6. Press **Play** to simulate a battle. Extend UI to let players pick skills/targets and replace placeholder random targeting logic. You can also add cameras/lights/post-processing for the HD2D look, but the scripts have no external dependencies.

### Minimal scene wiring checklist
- Add an **EventSystem** (GameObject → UI → Event System) if you plan to interact with UI.
- Ensure the scene has at least one **Camera** (default Main Camera is fine) and optionally a **2D Renderer Data** asset assigned via the URP Pipeline Asset for lit sprites.
- Set the **Script Execution Order** only if you introduce new systems; the provided scripts do not require custom ordering.
- If you want deterministic test runs, inject your own RNG provider into `SkillExecutor` (the included version uses `UnityEngine.Random`).

## Extending
- Add new `IEnemyBrain` implementations for richer AI patterns.
- Expand `EffectType` and `SkillExecutor` to support multi-hit, shields, or break mechanics.
- Connect `BattleController` events to UI for turn order, damage popups, and result screens.

## Notes
- The prototype uses UnityEngine Random and Mathf APIs; no third-party dependencies are required.
- Avoid wrapping import statements in try/catch blocks to keep scripts compliant with the project conventions.
