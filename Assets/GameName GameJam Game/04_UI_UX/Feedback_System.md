# Feedback System

## Links

**Depends On:**
- [[Combat_System]]
- [[Combat_Implementation]]

**Used By:**
- [[HUD_Design]]

---

## Overview

The feedback system communicates the results of player actions.

It is critical for making combat feel:

- Responsive
- Impactful
- Satisfying

---

## Core Feedback Types

### ⚔️ Hit Feedback

When an attack connects:

- Enemy briefly flashes
- Enemy enters hit animation
- Small knockback applied

---

### 🎯 Player Feedback

- Attack animations feel immediate
- Inputs respond instantly
- No noticeable delay

---

### 💥 Visual Effects (Optional)

- Hit sparks / particles
- Screen shake (small)
- Impact flashes

---

### 🔊 Audio Feedback (Optional)

- Attack sound
- Hit sound
- Enemy reaction sound

---

## Timing (VERY IMPORTANT)

- Feedback must occur immediately on hit
- No delay between input and reaction

---

## Design Goals

- Make hits feel powerful
- Clearly communicate success
- Reinforce player control

---

## Scope Constraints

- Keep effects simple
- Avoid overloading screen with effects
- Prioritize clarity over style

---

## Notes

> “If the player can’t feel the hit, the game doesn’t work.”