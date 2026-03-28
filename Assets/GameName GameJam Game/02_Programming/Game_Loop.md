# Game Loop

## Links

**Depends On:**
- [[Level_Design]]
- [[Enemy_AI]]
- [[Combat_Implementation]]

**Used By:**  
- [[HUD_Design]]

---

## Overview

The game loop controls how the player progresses through a level.

It manages:

- Combat zones
- Enemy spawning
- Progression gating
- Level completion

---

## Core Loop Flow

1. Player moves forward
2. Player enters combat zone
3. Camera locks
4. Enemies spawn
5. Player defeats enemies
6. Combat zone completes
7. Camera unlocks
8. Player progresses
9. Repeat until level end

---

## Combat Zone Flow

### On Enter Zone

- Trigger zone activation
- Lock camera
- Begin enemy spawning

---

### During Zone

- Enemies spawn in waves or groups
- Track active enemies

---

### On All Enemies Defeated

- Mark zone as complete
- Unlock camera
- Allow player to move forward

---

## Enemy Tracking

- Maintain count of active enemies
- Decrease count when enemies are defeated
- Trigger completion when count reaches zero

---

## Level Completion

At the end of the level:

- Trigger final encounter (optional boss)
- When boss is defeated:
  - Level ends
  - Player wins

---

## System Responsibilities

### Game Loop System

- Manage combat zones
- Control progression flow
- Handle win condition

---

### Other Systems

- [[Enemy_AI]] handles enemy behavior
- [[Combat_Implementation]] handles combat interactions
- [[Level_Design]] defines structure

---

## Design Goals

- Clear progression
- No confusion about next objective
- Smooth transitions between combat and movement

---

## Scope Constraints

- One level minimum
- Simple zone triggers
- No complex event systems

---

## Notes

> “The game loop should always clearly tell the player what to do next.”