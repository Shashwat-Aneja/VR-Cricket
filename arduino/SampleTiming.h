#ifndef VR_CRICKET_SAMPLE_TIMING_H
#define VR_CRICKET_SAMPLE_TIMING_H

#include <stdint.h>

enum { SAMPLE_INTERVAL_MICROS = 10000U };

inline uint32_t elapsedSampleMicros(uint32_t now, uint32_t previous) {
    return static_cast<uint32_t>(now - previous);
}

inline bool sampleIntervalElapsed(uint32_t now, uint32_t previous) {
    return elapsedSampleMicros(now, previous) >= SAMPLE_INTERVAL_MICROS;
}

inline float sampleDeltaSeconds(uint32_t now, uint32_t previous) {
    return static_cast<float>(elapsedSampleMicros(now, previous)) / 1000000.0f;
}

#endif
