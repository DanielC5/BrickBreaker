# BrickBreaker

A classic Brick Breaker game built in Unity with multiple levels, powerups, and local two-player support. Built during CMU's NHSGA program in the summer of 2025.

**Play it here:** [https://danielc87.itch.io/brick-breaker](https://danielc87.itch.io/brick-breaker)

## Features

- **3 Levels:** Each level has a unique procedurally generated brick pattern (rainbow diagonals, eyes, T-shape).
- **Powerups:** Extra ball spawns, paddle speed boosts, and a double points multiplier.
- **1 or 2 Player Mode:** Player 1 uses A/D, Player 2 uses Left/Right arrow keys.
- **High Score Tracking:** Persists top 3 scores per level and player count.
- **Audio & Visual Feedback:** Sound effects for all interactions, background music, and color-matched particle effects on brick destruction.

## Project Structure

```
Assets/
├── Scripts/
│   ├── GameManager.cs        # Central game state machine and score tracking
│   ├── BallMovement.cs       # Ball physics, collisions, and speed adjustments
│   ├── PaddleController.cs   # Paddle input handling and powerup effects
│   ├── BrickController.cs    # Brick health, destruction, and powerup spawning
│   ├── BrickSpawner.cs       # Procedural brick layout generation per level
│   ├── SoundManager.cs       # Audio playback for SFX and music
│   ├── ButtonController.cs   # Scene navigation and UI button handlers
│   ├── WinLoseUI.cs          # Victory/defeat overlay display
│   ├── EndSceneUI.cs         # End screen scores and leaderboard
│   └── ParticleSpawner.cs    # Particle effects on brick break
├── Scenes/
│   ├── StartScene             # Main menu
│   ├── PlayerSelectScene      # 1 or 2 player selection
│   ├── LevelSelectScene       # Level selection with previews
│   ├── GameScene1-3           # Gameplay levels
│   └── EndScene               # Results and high scores
├── Prefabs/                   # Ball, paddle, 7 brick variants, particles
├── Materials/                 # Brick colors, ball, paddle, environment
├── Audio/                     # SFX (bounce, break, death) and music tracks
└── Level Images/              # Level preview thumbnails
```

## How It Works

1. **Menu Flow:** Start screen → player count selection → level selection → gameplay → end screen with scores.
2. **Ball Launch:** Ball starts attached to the paddle. Press W (Player 1) or Up Arrow (Player 2) to launch.
3. **Ball Physics:** Ball reflects off surfaces. Speed increases on brick/wall hits and decreases on paddle hits.
4. **Brick Destruction:** Bricks have configurable health. On destruction, they flash white, fade out, spawn particles, and may drop a powerup.
5. **Win/Lose:** Destroy all bricks to win. Lose all balls to lose. High scores are saved automatically.

## Controls

| Action | Player 1 | Player 2 |
|--------|----------|----------|
| Move Paddle | A / D | Left / Right Arrow |
| Launch Ball | W | Up Arrow |

## Powerups

| Powerup | Effect | Duration |
|---------|--------|----------|
| Extra Ball | Spawns an additional ball | Permanent |
| Speed Boost | Increases paddle speed by 1.2x | 5 seconds |
| Double Points | Score multiplier x2 | 30 seconds |

## Requirements

- Unity 6000.0.51f1 or compatible
- Universal Render Pipeline (URP)
- TextMesh Pro
