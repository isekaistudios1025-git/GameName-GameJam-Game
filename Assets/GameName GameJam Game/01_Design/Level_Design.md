# Level Design

## Links

**Depends On:**
- [[Overview]]
- [[Combat_System]]
- [[Enemy_Design]]

**Used By:**
- [[Game_Loop]]
- [[Combat_Implementation]]

---

## Overview

Levels are linear and structured around combat zones.

The player progresses from left to right, encountering enemy waves that gate progression.

---

## Core Structure

Each level consists of:

1. Movement section
2. Combat zone
3. Enemy wave
4. Clear condition
5. Progression unlock

This loop repeats until the end of the level.

---

## Combat Zones

Combat zones are invisible trigger areas.

When the player enters a zone:

- Camera locks
- Enemies begin spawning
- Player cannot progress forward

When all enemies are defeated:

- Camera unlocks
- Player can move forward

---

## Enemy Waves

- Enemies spawn over time or in groups
- Number of enemies should be limited
- Keep encounters short and readable

---

## Boss Encounter (Optional)

At the end of the level:

- Unique enemy with more health
- Slight variation in behavior
- Acts as final challenge

---

## Design Goals

- Keep pacing fast
- Avoid long downtime
- Make each combat zone feel meaningful
- Keep encounters simple and readable

---

## Scope Constraints

- One level minimum
- Reuse environment where possible
- Avoid complex level mechanics

---

## Notes

> “The level exists to deliver combat in a controlled, repeatable way.”