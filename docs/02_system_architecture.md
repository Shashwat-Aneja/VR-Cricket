# System Architecture — VR Cricket Simulator

The VR Cricket architecture is divided into four major subsystems:

---

## 1. Hardware Motion Tracking (Arduino + MPU6050)
- MPU6050 IMU measures bat orientation.
- Arduino processes orientation using complementary filtering.
- Data transmitted as `YPR,yaw,pitch,roll`.
- Unity receives data and rotates the bat accordingly.

**Files involved:**
- `arduino/bat_tracker.ino`
- `arduino/data_protocol.md`

---

## 2. VR Interaction + Camera System
- Player's head tracked using smartphone gyroscope.
- Gaze-based UI using raycasting.
- Camera rotation updated every frame.

**Files:**
- `VRHeadTracker.cs`
- `GazeInteractor.cs`

---

## 3. Gameplay Logic (Unity)
Includes:
- Batting physics  
- Ball motion  
- Scoring system  
- Ball reset logic  
- Boundary detection  

**Files:**
- `PlayerBattingController.cs`
- `VRHUDManager.cs`

---

## 4. NPC AI Systems
### A. Pacer Bowling Animation
- Scripted run-up
- Arm rotation
- Ball release
- Follow-through

### B. AI Fielders
- Chase ball
- Catch ball
- Throw back
- Return to home position

**Files:**
- `BowlerAnimation.cs`
- `AIFieldingController.cs`

---

## Architecture Flow Diagram (Text Format)

