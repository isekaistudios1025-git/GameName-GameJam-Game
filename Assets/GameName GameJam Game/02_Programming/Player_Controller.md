# Player Controller

## Links

**Depends On:**
- [[Overview]]
- [[Combat_System]]

**Used By:**
- [[Combat_Implementation]]  
- [[Art_Pipeline]]
- [[Game_Loop]]  
- [[Art_Pipeline]]

---

## Overview

The Player Controller handles movement and input.

It is responsible for:

- Movement in 2.5D space
- Handling player input
- Triggering combat actions

---

## Movement System

### Axes

- X → Forward / backward (progression)
- Z → Up / down (depth movement)
- Y → Jump (optional)

---

### Constraints

- Player is limited to a fixed Z range
- Movement should feel responsive and tight

---

## Input

- Movement input (WASD / Arrow Keys)
- Attack input
- Optional:
  - Jump
  - Special

---

## Responsibilities

- Move player character
- Face correct direction
- Trigger attack actions
- Communicate with combat system

---

## Design Goals

- Movement should feel immediate
- Player should always feel in control
- No input delay

---

## Scope Constraints

- Keep movement simple
- Avoid complex physics early
- Focus on responsiveness

---

## Notes

> “If movement feels bad, the whole game feels bad.”