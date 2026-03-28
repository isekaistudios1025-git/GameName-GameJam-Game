# Enemy Design

## Links

**Depends On:**
- [[Combat_System]]

**Used By:**
- [[Enemy_AI]]
- [[Level_Design]]  
- [[Art_Pipeline]]

---

## Overview

Enemies provide the core challenge in the game.

They are designed to be:

- Simple
- Readable
- Reactive to player actions

---

## Basic Enemy Behavior

- Move toward player
- Stop within attack range
- Perform attack
- React when hit

---

## Enemy States

- Idle
- Moving
- Attacking
- Hit (stunned)
- Defeated

---

## Combat Interaction

When hit by player:

- Enemy is stunned briefly
- Enemy is pushed backward
- Enemy cannot act during stun

---

## Design Goals

- Enemies should be easy to understand
- Player should feel in control
- Combat should feel fair

---

## Scope Constraints

- Start with ONE enemy type
- Keep AI simple
- Focus on responsiveness over complexity

---

## Notes

> “Enemies exist to make the player feel powerful, not frustrated.”