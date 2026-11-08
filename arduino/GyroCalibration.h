#ifndef VR_CRICKET_GYRO_CALIBRATION_H
#define VR_CRICKET_GYRO_CALIBRATION_H

#include <stdint.h>

// A stationary startup window estimates the sensor's zero-rate offset before
// gyro values are integrated. Raw MPU6050 counts are retained to avoid
// introducing a second rounding step during calibration.
class GyroBiasCalibrator {
public:
    enum { REQUIRED_SAMPLES = 200 };

    GyroBiasCalibrator()
        : sampleCount_(0), sumX_(0), sumY_(0), sumZ_(0) {}

    bool addSample(int16_t gyroX, int16_t gyroY, int16_t gyroZ) {
        if (ready()) {
            return true;
        }
        sumX_ += gyroX;
        sumY_ += gyroY;
        sumZ_ += gyroZ;
        ++sampleCount_;
        return ready();
    }

    bool ready() const {
        return sampleCount_ >= REQUIRED_SAMPLES;
    }

    uint16_t sampleCount() const {
        return sampleCount_;
    }

    float biasXRaw() const {
        return sampleCount_ == 0 ? 0.0f : static_cast<float>(sumX_) / sampleCount_;
    }

    float biasYRaw() const {
        return sampleCount_ == 0 ? 0.0f : static_cast<float>(sumY_) / sampleCount_;
    }

    float biasZRaw() const {
        return sampleCount_ == 0 ? 0.0f : static_cast<float>(sumZ_) / sampleCount_;
    }

    void reset() {
        sampleCount_ = 0;
        sumX_ = 0;
        sumY_ = 0;
        sumZ_ = 0;
    }

private:
    uint16_t sampleCount_;
    int32_t sumX_;
    int32_t sumY_;
    int32_t sumZ_;
};

#endif
