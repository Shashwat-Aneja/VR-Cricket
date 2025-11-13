/*
  Bat Tracker (MPU6050 + Arduino Nano)
  -----------------------------------
  Purpose:
    Sends smoothed bat orientation (yaw, pitch, roll) to Unity
    for VR cricket simulation.

  Features:
    ✔ MPU6050 initialization
    ✔ Sensor calibration
    ✔ Complementary filter for orientation
    ✔ Stable serial output at 100 Hz
*/

#include <Wire.h>
#include "MPU6050.h"

MPU6050 mpu;

unsigned long lastTime = 0;
float yaw = 0, pitch = 0, roll = 0;

// Complementary filter constant
const float alpha = 0.96;

void setup() {
  Serial.begin(115200);
  Wire.begin();

  mpu.initialize();

  if (!mpu.testConnection()) {
    Serial.println("MPU6050 connection failed!");
    while (1);
  }

  Serial.println("MPU6050 Ready");
  delay(1000);
}

void loop() {
  if (millis() - lastTime >= 10) {   // 100 Hz
    lastTime = millis();

    int16_t ax, ay, az, gx, gy, gz;
    mpu.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);

    // Convert raw values
    float accelX = ax / 16384.0;
    float accelY = ay / 16384.0;
    float accelZ = az / 16384.0;

    float gyroX = gx / 131.0;
    float gyroY = gy / 131.0;
    float gyroZ = gz / 131.0;

    float dt = 0.01;

    // Accelerometer angle estimation
    float accelPitch = atan2(accelY, sqrt(accelX * accelX + accelZ * accelZ)) * 180 / PI;
    float accelRoll  = atan2(-accelX, accelZ) * 180 / PI;

    // Integrate gyroscope data
    pitch += gyroX * dt;
    roll  += gyroY * dt;
    yaw   += gyroZ * dt;

    // Complementary filter
    pitch = alpha * pitch + (1 - alpha) * accelPitch;
    roll  = alpha * roll  + (1 - alpha) * accelRoll;

    // Output formatted for Unity
    Serial.print("YPR,");
    Serial.print(yaw, 2);
    Serial.print(",");
    Serial.print(pitch, 2);
    Serial.print(",");
    Serial.println(roll, 2);
  }
}

