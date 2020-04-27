using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPitch : MonoBehaviour
{

    public AudioAnalysis analysis;
    public float shift = 0f;
    public float factor = 0.01f;

    // Update is called once per frame
    void Update()
    {
        if (analysis.PitchValue != 0)
        {
            float value = factor * (shift + analysis.PitchValue);

            transform.localPosition = new Vector3(value, transform.localPosition.y, transform.localPosition.z);
        }
    }
}

