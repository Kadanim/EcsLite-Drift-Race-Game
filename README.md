# Drift Racer

A small 3D racing game focused on drifting, written using ECSLite.

## Overview

Drift Racer is a simple yet engaging 3D racing game that emphasizes the art of drifting. It is built using the ECSLite framework, ensuring high performance and modularity. The project is structured to keep components, data, and systems well-organized and maintainable.

## Project Structure

The project is organized into the following main directories:

- **Components**: Contains all the components used by entities in the game.
  - **Drift**:
    - `DriftComponent.cs`: Manages drifting mechanics.
  - **GameManager**:
    - `GameManagerComponent.cs`: Manages game states and transitions.
  - **Material**:
    - `MaterialComponent.cs`: Manages materials and visual effects.
  - **Player**:
    - `PlayerCameraComponent.cs`: Manages the player camera.
    - `PlayerCarComponent.cs`: Manages the player car's properties.
    - `PlayerInputData.cs`: Handles player input data.
  - **UI**:
    - `DriftScoreUIComponent.cs`: Manages the drift score UI.
  - **Wheel**:
    - `GroundCheckComponent.cs`: Checks if the car is on the ground.
    - `WheelComponent.cs`: Manages wheel properties.
    - `WheelSmokeComponent.cs`: Handles wheel smoke effects.
    - `WheelTrailComponent.cs`: Manages wheel trail effects.

- **Data**: Contains runtime, scene, and static data.
  - `RuntimeData.cs`: Manages runtime-specific data.
  - `SceneData.cs`: Manages scene-specific data.
  - `StaticData.cs`: Manages static, unchanging data.

- **Systems**: Contains all the systems that perform operations on entities and their components.
  - **Drift**:
    - `DriftScoreSystem.cs`: Manages drift scoring.
  - **EcsCore**:
    - `EcsStartup.cs`: Initializes the ECS framework.
  - **Material**:
    - `MaterialUpdateSystem.cs`: Updates materials and visual effects.
  - **Player**:
    - `PlayerCameraInitSystem.cs`: Initializes the player camera.
    - `PlayerCameraUpdateSystem.cs`: Updates the player camera.
    - `PlayerCarMoveSystem.cs`: Handles player car movement.
    - `PlayerInputSystem.cs`: Processes player input.
  - **UI**:
    - `DriftScoreUISystem.cs`: Updates the drift score UI.
    - `UlInitSystem.cs`: Initializes other UI systems.
  - **Wheel**:
    - `WheelEffectsUpdateSystem.cs`: Updates wheel effects.
    - `GroundCheckSystem.cs`: Checks if the wheels are on the ground.
