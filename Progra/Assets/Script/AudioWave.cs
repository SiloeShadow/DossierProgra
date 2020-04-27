using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWave : MonoBehaviour
{

    public int QSamples = 256;
    public Transform parent;
    public GameObject prefab;
    public float xScale = 0.1f;
    public float yScale = 1.0f;

    private float[] samples;
    private GameObject[] objects;

    void Start()
    {
        samples = new float[QSamples];
        objects = new GameObject[QSamples];

        for (int i = 0; i < QSamples; i++)
        {
            objects[i] = Instantiate(prefab, parent, false);
        }
    }

    void Update()
    {
        GetComponent<AudioSource>().GetOutputData(samples, 0);

        for (int i = 0; i < QSamples; i++)
        {
            Vector3 pos = new Vector3(i * xScale, samples[i] * yScale, 0);
            objects[i].transform.localPosition = pos;
        }
    }
}
