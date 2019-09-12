using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioPower
{
    public static float ComputeRMS(float[] buffer, int offset, ref int length)
    {
        // sum of squares
        float sos = 0f;
        float val;

        if (offset + length > buffer.Length)
        {
            length = buffer.Length - offset;
        }

        for (int i = 0; i < length; i++)
        {
            val = buffer[offset];
            sos += val * val;
            offset++;
        }

        // return sqrt of average
        return Mathf.Sqrt(sos / length);
    }

    public static float ComputeDB(float[] buffer, int offset, ref int length)
    {
        float rms;

        rms = ComputeRMS(buffer, offset, ref length);

        // could divide rms by reference power, simplified version here with ref power of 1f.
        // will return negative values: 0db is the maximum.
        return 10 * Mathf.Log10(rms);
    }
}
