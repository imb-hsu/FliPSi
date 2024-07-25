using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSensor : Sensor
{
    public int objectId = 0;
    public Turntable actionItem;

    private void OnTriggerEnter(Collider other)
    {
        sensorVal = true;
        if (actionItem != null) return;
    }

    private void OnTriggerExit(Collider other)
    {
        sensorVal = false;
        objectId = 0;
    }


}