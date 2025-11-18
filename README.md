# 🏏 VR Cricket Simulator  
### A Hardware–Integrated Virtual Reality Cricket Experience

The **VR Cricket Simulator** combines **Unity**, **Arduino**, **MPU6050 motion tracking**, **AI-driven gameplay**, and **mobile VR head tracking** to create a realistic, immersive cricket experience.  
This project blends **VR interaction**, **sensor fusion**, **physics-based batting**, and **gameplay AI** in a single integrated system.

---

# 📌 Features

### 🎮 **1. VR Gameplay**
- Phone-based VR (Google Cardboard style)
- 360° head tracking using gyroscope
- Gaze-based UI interaction (no controllers needed)
- Stadium-based immersive environment

### 🏏 **2. Real Bat Motion Tracking (Arduino + MPU6050)**
- IMU attached to cricket bat
- Complementary filter for smooth orientation
- Real-time yaw/pitch/roll → Unity rotation
- USB/Bluetooth communication protocol
- 100 Hz update rate for accurate swings

### 🔥 **3. Physics-Based Batting System**
- Swing-speed detection  
- Shot direction based on bat orientation  
- Lofted shots, ground shots, edges  
- Fully simulation-driven, no animation cheats  

### 🤖 **4. AI Fielders**
- Reaction delay to simulate human reflexes  
- Ball chasing with smooth rotation  
- Catching logic  
- Intelligent ball return to bowler/keeper  
- Home-position return behaviour  

### 🎯 **5. Scripted Pacer Bowling System**
- Code-driven bowling animation (no Animator)
- Run-up, arm rotation, release point, follow-through
- Physics-based ball delivery

### 📊 **6. Match Logic & VR HUD**
- Runs, boundaries, overs, wickets
- Ball reset and flow control
- VR floating scoreboard using TextMeshPro

---
