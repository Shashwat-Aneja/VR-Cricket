# Arduino to Unity Serial Data Protocol

This document describes the frame emitted by `arduino/bat_tracker.ino` and the parsing behavior expected in Unity.

## Connection settings

- Baud rate: **115200**
- Output cadence: approximately **100 Hz** (one sample every 10 ms)
- Transport: USB serial or a compatible serial bridge
- Encoding: ASCII text, one newline-terminated frame per sample

## Frame format

A valid orientation frame contains four comma-separated fields:

```text
YPR,<yaw>,<pitch>,<roll>
```

Example:

```text
YPR,12.50,-3.25,8.00
```

The firmware prints each angle to two decimal places. Values represent degrees. Yaw is gyro-integrated and may drift over time; it is not an absolute heading reference.

The firmware also emits a startup status line (`MPU6050 Ready`) before orientation frames. A receiver must ignore non-frame status lines.

## Receiver validation

For every incoming line, Unity should:

1. Trim surrounding whitespace and line terminators.
2. Split on commas and require exactly four fields.
3. Require the first field to equal `YPR` using ordinal comparison.
4. Parse all three angle fields using invariant-culture numeric parsing.
5. Reject the entire frame if any value is missing, malformed, NaN, or infinite.
6. Keep the last valid orientation when a malformed or status line arrives.
7. Avoid blocking the Unity main thread while waiting for serial data.

Do not parse using the current machine's locale. A decimal comma would conflict with the comma-separated protocol.

## Timing and robustness

- Treat the nominal 100 Hz rate as a target, not a guarantee.
- Timestamp received frames in Unity if motion timing matters.
- If the stream stalls, hold the last valid pose briefly and then apply the application's chosen neutral/recenter behavior.
- Do not assume yaw is bounded to a fixed range; normalize it only where the consuming rotation logic requires it.
- The current frame has no sequence number or checksum, so the receiver cannot detect every dropped or corrupted sample.

## Hardware bring-up

1. Confirm the MPU6050 connection succeeds at startup.
2. Open the serial monitor at 115200 baud and verify `YPR,` lines arrive steadily.
3. Move the sensor slowly around each axis and confirm the corresponding output changes.
4. Confirm the Unity parser ignores `MPU6050 Ready` and accepts only complete finite frames.

Test the serial stream with the bat disconnected from active gameplay before tuning swing detection.
