# DinoPark

A free-roaming dinosaur island built in **Unity 6 (HDRP)**, focused on believable, needs-driven creature AI.

Each dinosaur gets hungry, thirsty and tired, decides which need matters most, and finds food, water or rest on its own. Herbivores move together as a herd. Every behaviour is driven by a per-species config asset, so new species reuse the same code.

> **Status:** in active development. The current goal is a playable vertical slice (living island, herbivore herd, predator vs. prey) as a Windows build.
---

## Background

DinoPark began as my 2018 university project, a dinosaur island combining finite state machines with A* pathfinding. This repo is a ground-up rebuild in a modern engine. It isn't a port: none of the original code is reused. The same idea is rebuilt with a cleaner, data-driven architecture.

---

## Features

### Working now
- **Needs-driven AI.** Hunger, thirst and tiredness decay over time, and each dinosaur prioritises the most pressing need.
- **Finite state machine.** Each state is its own class (`Idle`, `SeekFood`, `Eat`, `SeekWater`, `Drink`, `Sleep`) with `Enter`/`Tick`/`Exit` methods.
- **Hysteresis on needs.** Separate "start seeking" and "fully satisfied" thresholds stop the AI flipping rapidly between states.
- **Search behaviour.** When no food or water is within sensing range, the dinosaur explores until it finds some instead of freezing.
- **Individual variation.** Each dinosaur rolls its own starting needs and decay rates (±20%), so a herd doesn't eat and sleep in lockstep.
- **Herd cohesion.** Herbivores drift back toward the centre of nearby herd members. This can be turned on or off per species.
- **NavMesh navigation.** Built on the AI Navigation package, with avoidance tuned to each species' body size.
- **Data-driven species.** Speeds, decay rates, thresholds, sensing range and herd settings all live in a ScriptableObject per species.
- **Debug overlay.** A floating label above each dinosaur shows its current state and need levels. Press F1 to toggle it.
- **Environment.** HDRP terrain island with HDRI sky, Adaptive Probe Volumes, volumetric fog and GPU rain (VFX Graph).

### Planned for the vertical slice
- [ ] Velociraptor predator: Patrol → Stalk → Chase → Eat
- [ ] Prey response: Alert → Flee
- [ ] Free-fly camera with click-to-follow
- [ ] Toggleable debug panel (sense radius, current path, target)
- [ ] Main menu and a Windows build on itch.io

### Stretch goals
- Weather system that drives the AI (sheltering, faster tiring in rain)
- Pachycephalosaurus with rival head-butting behaviour
- Dinosaurs remembering where they last found food and water

---

## Architecture
- **`DinoController`** is the only entry point. It hands the species config to its components in `Awake`, which keeps initialisation order predictable, and ticks the state machine each frame.
- **States** hold the decision logic. Each `Tick` returns the next state (or itself), and the state machine handles `Exit` → `Enter` on a change.
- **Components stay small.** Stats only knows about needs, Movement only knows about navigation, and neither knows what state the dinosaur is in.



---
## Getting started

### Requirements
- **Unity 6.5** (6000.5.x) with the **High Definition Render Pipeline**
- Packages: **AI Navigation**, **Visual Effect Graph**, **Input System**

### Third-party assets (not included)
Large third-party assets are excluded from the repo by `.gitignore` to keep it small. Before opening the scene, import them into `Assets/`:

| Asset | Used for | Source |
| --- | --- | --- |
| <!-- TODO --> Dinosaur models pack | Stegosaurus, Pachycephalosaurus, Velociraptor | <!-- TODO: link --> |
| <!-- TODO --> HDRI sky | Sky and lighting | [Poly Haven](https://polyhaven.com) |
| <!-- TODO --> Terrain textures | Terrain layers | [Poly Haven](https://polyhaven.com) |
| <!-- TODO --> Heightmaps / tree assets | Island terrain and foliage | <!-- TODO: link --> |

### Running
1. Clone the repo and open the folder in Unity Hub with Unity 6.5.
2. Import the third-party assets listed above.
3. Open `Assets/Scenes/OutdoorsScene` <!-- TODO: confirm path -->.
4. Press Play.

### Controls
| Key | Action |
| --- | --- |
| F1 | Toggle debug labels |
| <!-- TODO --> | Camera controls (Week 4) |

---

## Adding a new species

1. Right-click in the Project window and choose **Create → DinoConfig** <!-- TODO: confirm menu name -->. Tune speeds, decay rates, thresholds, sensing range and herd settings.
2. Set up the model's prefab with `DinoController`, `DinoStats`, `DinoMovement`, a `NavMeshAgent` and `DinoDebugLabel`.
3. Assign the new config to `DinoController`, and size the NavMeshAgent's radius to the body.

No code changes are needed for a new herbivore. Predators will add their own states.

---

## Roadmap

| Week | Focus | Status |
| --- | --- | --- |
| 1 | Foliage, NavMesh, Wander/Graze/Rest FSM, debug labels | Done (tree performance pass outstanding) |
| 2 | Herd of 4–6 stegosaurs, needs-driven | In progress |
| 3 | Raptor predator and flee response | Planned |
| 4 | Free-fly camera, click-to-follow, debug panel, main menu | Planned |
| 5 | Windows build, video, README polish | Planned |
| 6 | Buffer | Planned |

---
