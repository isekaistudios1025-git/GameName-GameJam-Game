
## Links

**Used By:**
- [[Combat_System]]
- [[Enemy_Design]]
- [[Level_Design]]
# Game Jam GDD – [GameName GameJam Game]

---

## 🎯 Core Vision

### High Concept

> A fast-paced 2.5D beat 'em up where players fight through waves of enemies using simple, satisfying combat in locked combat arenas.

---

### Player Fantasy

- Feel powerful
    
- Chain attacks into enemies
    
- Clear waves and push forward
    
- Classic arcade brawler energy
    

---

### Design Pillars

1. **Simple, Responsive Combat**
    
2. **Satisfying Hit Feedback**
    
3. **Clear Combat Flow (Fight → Move → Fight)**
    
4. **Low Scope, High Polish**
    

---

## 🔁 Core Gameplay Loop

1. Move through level (left → right)
    
2. Enter combat zone (camera locks)
    
3. Enemies spawn in waves
    
4. Defeat all enemies
    
5. Camera unlocks → player progresses
    
6. Repeat until level complete
    
7. Fight boss at end
    

---

## 🧱 Core Game Structure

### 🎮 2.5D Movement System

- X Axis → Level progression (left to right)
    
- Z Axis → Depth movement (up/down on screen)
    
- Y Axis → Jumping
    

Player is constrained to a limited Z range (lane-based movement feel).

---

### 🎥 Camera System

- Follows player during movement
    
- Locks when entering combat zones
    
- Unlocks when all enemies are defeated
    

---

### ⚔️ Combat Zones (CORE SYSTEM)

Levels are divided into invisible combat zones.

Each zone:

- Triggers enemy spawns
    
- Locks player progression
    
- Unlocks when all enemies are defeated
    

---

## ⚔️ Core Mechanics

### Player

- Movement (2.5D plane)
    
- Basic attack (combo or single hit)
    
- Optional:
    
    - Jump
        
    - Special attack
        

---

### Combat

- Hit detection
    
- Enemy hit reactions (stun / knockback)
    
- Basic combo chaining
    

---

### Enemies

- Move toward player
    
- Attack player
    
- React to hits
    

---

## 🗺️ Level Structure

- Linear progression
    
- Combat zones gate progression
    
- Final boss at end of level
    

---

## 🧱 Scope (CRITICAL)

### MUST HAVE

- Player movement (2.5D)
    
- Basic attack
    
- 1 enemy type
    
- Combat zone system
    
- Enemy spawning
    
- 1 level
    
- Win condition
    

---

### NICE TO HAVE

- Combo system
    
- Multiple enemy types
    
- Boss fight
    
- Sound effects
    
- Hit effects / screen shake
    

---

### CUT IF NEEDED

- Story
    
- Complex UI
    
- Advanced AI
    
- Multiple levels
    

---

## 👥 Team Roles

- Programmer(s)
	- Player Controller
		- Peacebaby
	- UI UX
		- Keon Fryson
	- Enemy AI
		- DevBunny
	- Level / Game Mechanics
		- DaJagerBomb
- Artist(s)
	- The-Pickle-Man		
- Designer
	- DaJagerBomb
- Audio (optional)
	- AgentGhostJr
	    
- Producer(s)
	- GeoBoshi
	- Kcobra
---

## 🎨 Art Pipeline (IMPORTANT)

Each character requires:

- Idle
    
- Movement
    
- Attack
    
- Hit reaction
    

👉 Keep character count LOW

---

## 🛠️ Tech

- Engine: Unity 6.3
    
- Platform: Windows / Web
    
- Input: Keyboard
    

---

## 🚧 Development Plan (Broad)

### Phase 1

- Player movement
    
- Basic attack
    

### Phase 2

- Enemy behavior
    
- Combat feel
    

### Phase 3

- Combat zones + level flow
    

### Phase 4

- Polish + juice + build
