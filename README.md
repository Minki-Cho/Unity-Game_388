# 3D Cross-Platform Platformer Game  
**Unity · Windows · Nintendo Switch · Android (Mobile)**

A fast-paced **3D platformer** developed by a **3-programmer team**, now supporting **Windows**, **Nintendo Switch**, and **Android Mobile**.  
This public repository includes all **platform-agnostic Unity gameplay code**, editor tools, UI systems, and **Windows/Android builds**.

> **Nintendo Switch–specific code and build files are NOT included** due to SDK NDA restrictions.  
> **Android builds include only platform-safe, OS-agnostic scenes and scripts.**

---

## Jummpers  
![Happy](Jumppers/Assets/screenshot.jpg)

---

## Project Overview

This game is a **vertical progression 3D platformer** where players climb upwards across dynamic platforms, avoid enemies, collect items, and survive without falling out of the camera’s view.

Key focuses include:

- Clean, modular Unity C# architecture  
- Cross-platform structure (Windows / Android / Switch)  
- Responsive movement & physics  
- Procedural platform generation  
- Mobile touch input support  
- Clear camera readability  
- Polished UI, VFX, SFX, and feedback systems  

---

# Team Roles

## 1. Core Gameplay Programmer  
**Player movement, physics, collisions, and overall game feel**

- PlayerCharacter architecture  
- Movement (Enhanced Input + Mobile touch controls)  
- Jumping, bounce, fall acceleration  
- Landing & collision detection  
- Platform-based jump modifiers  
- Death conditions (falling, enemy hit)  
- Respawn / Restart logic  

---

## 2. World & Interaction Programmer  
**Platforms, enemies, items, and environmental interactions**

- Platform types: Normal / SuperJump / Random  
- Player landing → jump power transfer  
- EnemyBase (Patrol / Chase / Damage / Knockback)  
- Item pickups (score, buffs, temporary boosts)  
- Procedural PlatformSpawner  
- Object pooling for performance  

---

## 3. Camera, UI & Feedback Programmer  
**Camera systems, UI design, mobile controls, VFX/SFX**

- CameraFollow with soft zones & vertical lead  
- Out-of-view warning system  
- Game Over → Retry flow  
- HUD (Score, Combo, Life)  
- GameStateManager  
- Mobile UI buttons (move, jump)  
- VFX/SFX feedback triggers  
- Debug overlay (bounds, jump force, FPS)  

---

# Core Gameplay Features

- ✔️ Responsive movement & mobile touch controls  
- ✔️ Dynamic platforms with special effects  
- ✔️ Score, combo, life, and state systems  
- ✔️ Out-of-view warning indicators  
- ✔️ Full Game Over → Retry loop  
- ✔️ Polished VFX & SFX feedback  

---

# Nintendo Switch Policy

Nintendo Switch development requires private SDK tools and restricted documentation.

### Allowed in this repository:
- General Unity gameplay code  
- Windows & Android builds  

### Not included:
- **Nintendo Switch–specific code, assets, or build files**  
  (Protected under Nintendo SDK NDA)

---

