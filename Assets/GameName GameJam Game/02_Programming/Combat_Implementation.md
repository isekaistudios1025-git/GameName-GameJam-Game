# Combat Implementation

## Links

**Depends On:**
- [[Combat_System]]
- [[Player_Controller]]
- [[Enemy_AI]]

**Used By:**
- [[Game_Loop]]  
- [[Feedback_System]]

---

## Overview

The combat implementation defines how attacks are executed, detected, and resolved.

It translates the design of the combat system into gameplay behavior.

---

## Core Combat Flow

1. Player presses attack input
2. Player enters attack state
3. Attack animation plays
4. Hitbox becomes active
5. Enemy is detected within hitbox
6. Enemy takes hit
7. Enemy enters stun state
8. Knockback is applied

---

## Player Attack Execution

- Attack is triggered by input
- Player enters attack state
- Movement may be limited during attack
- Attack has a short duration

---

## Hitbox System

- Hitboxes are trigger colliders
- Enabled only during active frames of attack
- Positioned in front of player

---

## Hit Detection

- When hitbox overlaps enemy:
  - Register hit
  - Prevent multiple hits per attack (optional)
- Can hit multiple enemies at once

---

## Enemy Hit Handling

When enemy is hit:

- Receive hit event
- Enter stun state
- Apply knockback force
- Interrupt current action

---

## Knockback

- Push enemy away from player
- Direction based on player facing
- Keep values small and readable

---

## Attack Cooldown

- Prevent immediate re-triggering of attack
- Short delay between attacks
- Supports simple combo timing (optional)

---

## State Interaction

### Player States

- Idle
- Moving
- Attacking

---

### Enemy States

- Moving
- Attacking
- Stunned
- Defeated

---

## Design Alignment

Matches rules from [[Combat_System]]:

- Simple attacks
- Immediate response
- Clear hit feedback

---

## Scope Constraints

- No complex combo system initially
- No weapon systems
- No damage types

---

## Notes

> “Combat should feel good before it becomes complex.”