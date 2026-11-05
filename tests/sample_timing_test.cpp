#include <assert.h>
#include <math.h>
#include <stdint.h>
#include "../arduino/SampleTiming.h"

int main() {
    assert(!sampleIntervalElapsed(9999U, 0U));
    assert(sampleIntervalElapsed(10000U, 0U));
    assert(sampleIntervalElapsed(15000U, 0U));
    assert(elapsedSampleMicros(15000U, 0U) == 15000U);
    assert(fabsf(sampleDeltaSeconds(10000U, 0U) - 0.01f) < 0.000001f);
    assert(fabsf(sampleDeltaSeconds(15000U, 0U) - 0.015f) < 0.000001f);

    // A 32-bit micros() counter wraps after roughly 71 minutes.
    assert(!sampleIntervalElapsed(0x000026FFU, 0xFFFFFFF0U));
    assert(sampleIntervalElapsed(0x00002700U, 0xFFFFFFF0U));
    assert(elapsedSampleMicros(0x00002700U, 0xFFFFFFF0U) == 10000U);
    assert(fabsf(sampleDeltaSeconds(0x00002700U, 0xFFFFFFF0U) - 0.01f) < 0.000001f);

    return 0;
}
