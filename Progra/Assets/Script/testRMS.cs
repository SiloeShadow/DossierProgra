using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRMS : MonoBehaviour
{

    public AudioAnalysis analysis;
    public float shift = 0f;
    public float factor = 1f;

    // Update is called once per frame
    void Update()
    {
        float value = factor * (shift + analysis.RmsValue);
        if (value < 0) value = 0;

        transform.localScale = new Vector3(1f, value, 1f);
    }
}