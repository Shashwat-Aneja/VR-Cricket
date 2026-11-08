#include <assert.h>
#include <math.h>
#include "../arduino/GyroCalibration.h"

int main() {
    GyroBiasCalibrator calibrator;
    assert(!calibrator.ready());
    assert(calibrator.sampleCount() == 0);
    assert(calibrator.biasXRaw() == 0.0f);

    for (uint16_t index = 0; index < GyroBiasCalibrator::REQUIRED_SAMPLES - 1; ++index) {
        assert(!calibrator.addSample(100, -50, 25));
    }
    assert(calibrator.sampleCount() == 199);
    assert(!calibrator.ready());

    assert(calibrator.addSample(100, -50, 25));
    assert(calibrator.ready());
    assert(calibrator.sampleCount() == GyroBiasCalibrator::REQUIRED_SAMPLES);
    assert(fabsf(calibrator.biasXRaw() - 100.0f) < 0.0001f);
    assert(fabsf(calibrator.biasYRaw() + 50.0f) < 0.0001f);
    assert(fabsf(calibrator.biasZRaw() - 25.0f) < 0.0001f);

    // Samples after the calibration window must not move the established bias.
    assert(calibrator.addSample(1200, 1200, 1200));
    assert(calibrator.sampleCount() == GyroBiasCalibrator::REQUIRED_SAMPLES);
    assert(fabsf(calibrator.biasXRaw() - 100.0f) < 0.0001f);

    calibrator.reset();
    assert(!calibrator.ready());
    assert(calibrator.sampleCount() == 0);
    assert(calibrator.biasXRaw() == 0.0f);
    return 0;
}
