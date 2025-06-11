## Overview

This project is a game developed in Unity. It utilizes an Entity Component System (ECS) architecture, using Unity's DOTS. The project is organized into two main C# assemblies: `Game.Logic` and `Game.Visuals`.

## Core Architecture

The project separates game logic and visual representation into distinct assemblies.

## Key Features

*   **ECS Architecture (Unity DOTS)**: Leverages data-oriented design for performance and scalability.
*   **Dependency Injection**: Promotes modularity and testability.
*   **Save/Load System**: Allows players to save and resume their progress. This system handles both entity state and general game state.
*   **AI System**: Includes systems for target acquisition, movement, and attack behavior for enemies.
*   **Player Input Handling**: Dedicated system for processing player controls.
*   **Character Movement and Animation**: Systems for controlling character movement and representing it through animations.
*   **Combat System**: Features attack requests and damage handling and effects. 
*   **Scene Management**: System for loading and managing game scenes.
*   **Camera Control**: Includes character tracking for the game camera.
*   **UI System**: Displays game information like health, abilities, and provides menus for main navigation, pause/resume, and save/load functionality.
*   **Visual Proxies**: A system for linking ECS entities to GameObject representations for rendering and other Unity-specific interactions.

## Project Structure Details

*   **`Assets/Game/Scripts/Logic`**: Contains all core gameplay logic, ECS systems, and components.
*   **`Assets/Game/Scripts/Visuals`**: Contains scripts related to visual representation, UI, and animations.
*   **`Assets/Game/Prefabs`**: Stores GameObject prefabs used for characters, enemies, items, UI elements, etc.
*   **`Assets/Game/Configs`**: Stores ScriptableObjects or other configuration files for game balance, AI parameters, etc.
*   **`Assets/Game/Scene`**: Contains the Unity scenes for the game.
