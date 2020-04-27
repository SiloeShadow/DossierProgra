using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based on https://answers.unity.com/questions/157940/getoutputdata-and-getspectrumdata-they-represent-t.html

public class AudioAnalysis : MonoBehaviour
{

    [Header("Use this to enable/disable parts of the sound analysis")]
    public bool doVolumeAnalysis = true;
    public bool doPitchAnalysis = false;

    [Header("Analysis results: volume")]
    [Tooltip("Root Mean Square")]
    public float RmsValue;
    [Tooltip("Amplitude in Decibels (total silence = -160Db)")]
    public float DbValue;

    [Header("Analysis results: pitch")]
    public float PitchValue;

    private const int QSamples = 1024;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.01f;

    float[] _samples;
    private float[] _spectrum;
    private float _fSample;

    void Start()
    {
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;
    }

    void Update()
    {
        if (doVolumeAnalysis) AnalyseVolume();
        if (doPitchAnalysis) AnalysePitch();
    }

    void AnalyseVolume()
    {
        GetComponent<AudioSource>().GetOutputData(_samples, 0); // fill array with samples
        float sum = 0;
        for (int i = 0; i < QSamples; i++)
        {
            sum += _samples[i] * _samples[i]; // sum squared samples
        }
        RmsValue = Mathf.Sqrt(sum / QSamples); // rms = square root of average
        DbValue = 20 * Mathf.Log10(RmsValue / RefValue); // calculate dB
        if (DbValue < -160) DbValue = -160; // clamp it to -160dB min
    }

    void AnalysePitch()
    {
        // get sound spectrum
        GetComponent<AudioSource>().GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        var maxN = 0;
        for (int i = 0; i < QSamples; i++)
        { // find max 
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                continue;

            maxV = _spectrum[i];
            maxN = i; // maxN is the index of max
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < QSamples - 1)
        { // interpolate index using neighbours
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        PitchValue = freqN * (_fSample / 2) / QSamples; // convert index to frequency
    }
}
