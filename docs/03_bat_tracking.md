# Bat Tracking System — Technical Breakdown

The bat tracking system converts real bat motion into in-game VR motion using an MPU6050 IMU attached to the bat.

---

## Hardware Flow
1. MPU6050 continuously reads accelerometer + gyroscope.
2. Arduino applies complementary filtering:
   - Gyro for fast changes
   - Accelerometer for reference stability
3. Orientation computed → yaw, pitch, roll
4. Sent to Unity over USB/Bluetooth
5. Unity rotates the virtual bat in real-time

---

## Orientation Output Format
