using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circus : MonoBehaviour
{
    public Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angle = transform.localEulerAngles;
        //angle = angle + rotation;
        angle.z = angle.z + rotation.y;
        angle.x = angle.x + rotation.z;
        transform.localEulerAngles = angle;
    }
}
