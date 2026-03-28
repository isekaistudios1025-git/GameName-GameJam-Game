# Enemy AI

## Links

**Depends On:**
- [[Enemy_Design]]
- [[Player_Controller]]

**Used By:**
- [[Combat_Implementation]]
- [[Game_Loop]]

---

## Overview

Enemy AI controls how enemies behave in relation to the player.

It is designed to be:

- Simple
- Predictable
- Responsive

---

## Core Behavior Loop

1. Detect player position
2. Move toward player
3. Stop within attack range
4. Perform attack
5. React when hit

---

## Movement Behavior

- Enemy moves toward player on X and Z axes
- Movement should be steady and readable
- Avoid complex pathfinding

---

## Distance Logic

- If player is far → move closer
- If player is within range → stop and attack

---

## Attack Behavior

- Trigger attack when within range
- Respect cooldown between attacks
- Do not spam attacks

---

## Hit Reaction

When enemy is hit:

- Enter stun state
- Stop all movement
- Apply knockback
- Resume behavior after stun

---

## States

- Idle
- Moving
- Attacking
- Stunned
- Defeated

---

## Design Goals

- Easy to understand behavior
- Fair reactions
- No unpredictable movement

---

## Scope Constraints

- No advanced pathfinding
- No complex decision trees
- Focus on one enemy type first

---

## Notes

> “Enemy AI should support combat, not complicate it.”