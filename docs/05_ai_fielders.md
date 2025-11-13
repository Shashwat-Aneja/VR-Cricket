# AI Fielders — Technical Documentation

The VR Cricket Simulator contains AI fielders that dynamically react to the ball, chase it, catch it, and throw it back.

This system is fully code-driven (no navmesh).

---

## 🎯 Core Behaviors

### 1. Reaction Delay
To simulate human reflexes:
- Fielder waits 0.3–0.5 seconds after ball is hit
- Then begins chase

### 2. Ball Detection
The fielder activates chase mode when:
