# Combat System

## Links

**Depends On:**
- [[Overview]]

**Used By:**  
- [[Combat_Implementation]]
- [[Enemy_Design]]
- [[Level_Design]] 

---

## Overview

The combat system is the core gameplay experience.

It is designed to be:

- Simple
- Fast
- Responsive
- Satisfying

---

## Player Combat

### Basic Attack

- Single button input
- Player performs a forward attack
- Hits enemies in front of them

---

### Optional (If Time Allows)

- 2–3 hit combo chain
- Special attack

---

## Attack Behavior

- Attacks have a short duration
- Hit occurs during active frames
- Player can move between attacks

---

## Enemy Interaction

When hit:

- Enemy is stunned briefly
- Enemy is pushed back slightly
- Enemy cannot attack during stun

---

## Combat Rules

- One attack hits one or more enemies in range
- Enemies do not interrupt player unless attacking
- Player should always feel in control

---

## Combat Feel Goals

- Attacks should feel immediate
- Hits should feel impactful
- Feedback should be clear

---

## Scope Constraints

- Keep attack system simple
- Avoid complex combo logic early
- Focus on responsiveness over depth

---

## Notes

> “Make hitting one enemy feel great before adding more complexity.”[[Design Linkage]]