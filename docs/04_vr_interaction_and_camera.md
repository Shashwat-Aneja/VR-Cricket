# VR Interaction & Camera System  
### VR Cricket Simulator — Documentation

This document explains how the VR camera and interaction system works inside the VR Cricket Simulator. The system is designed for **phone-based VR headsets** (Google Cardboard style) without external controllers.

---

## 🎯 1. Overview

The VR Cricket interaction model uses:

- **Smartphone gyroscope** → for head tracking  
- **Gaze-based raycasting** → for UI interaction  
- **Central camera point** → acts as VR headset view  
- **No controllers required** → fully hands-free  

This approach is lightweight and ideal for mobile VR.

---

## 🧭 2. Head Tracking System

### 🔹 How It Works  
The phone's gyroscope provides orientation in quaternion form.  
Unity reads this using:

