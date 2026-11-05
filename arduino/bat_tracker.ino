/*
  Bat Tracker (MPU6050 + Arduino Nano)
  -----------------------------------
  Purpose:
    Sends smoothed bat orientation (yaw, pitch, roll) to Unity
    for VR cricket simulation.
*/

#include <Wire.h>
#include "MPU6050.h"
#include "SampleTiming.h"

MPU6050 mpu;

uint32_t lastSampleMicros = 0;
float yaw = 0, pitch = 0, roll = 0;

// Complementary filter constant
const float alpha = 0.96f;

void setup() {
  Serial.begin(115200);
  Wire.begin();

  mpu.initialize();

  if (!mpu.testConnection()) {
    Serial.println("MPU6050 connection failed!");
    while (1) {
    }
  }

  Serial.println("MPU6050 Ready");
  delay(1000);
  lastSampleMicros = micros();
}

void loop() {
  const uint32_t now = micros();
  if (sampleIntervalElapsed(now, lastSampleMicros)) {
    // Use the actual interval between sensor samples instead of assuming
    // every loop iteration completes at exactly 100 Hz.
    const float dt = sampleDeltaSeconds(now, lastSampleMicros);
    lastSampleMicros = now;

    int16_t ax, ay, az, gx, gy, gz;
    mpu.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);

    const float accelX = ax / 16384.0f;
    const float accelY = ay / 16384.0f;
    const float accelZ = az / 16384.0f;

    const float gyroX = gx / 131.0f;
    const float gyroY = gy / 131.0f;
    const float gyroZ = gz / 131.0f;

    const float accelPitch = atan2(accelY, sqrt(accelX * accelX + accelZ * accelZ)) * 180.0f / PI;
    const float accelRoll = atan2(-accelX, accelZ) * 180.0f / PI;

    pitch += gyroX * dt;
    roll += gyroY * dt;
    yaw += gyroZ * dt;

    pitch = alpha * pitch + (1.0f - alpha) * accelPitch;
    roll = alpha * roll + (1.0f - alpha) * accelRoll;

    Serial.print("YPR,");
    Serial.print(yaw, 2);
    Serial.print(",");
    Serial.print(pitch, 2);
    Serial.print(",");
    Serial.println(roll, 2);
  }
}
