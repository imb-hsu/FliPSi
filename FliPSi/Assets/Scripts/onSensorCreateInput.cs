using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onSensorCreateInput : MonoBehaviour
{
    BilloSPS sps;
    Sensor sensor;

    // Update is called once per frame
    void Update()
    {
        if (sensor.sensorVal == true)
        {
            sps.AllAddCubes();
        }
    }
}
