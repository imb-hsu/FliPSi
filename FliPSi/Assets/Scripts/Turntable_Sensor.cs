using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turntable_Sensor : MonoBehaviour
{   
    // bool for configuring whether to use front or back sensor
    public bool front;

    //private bool sensorEnterAlreadyFired = false;
    //private bool sensorExitAlreadyFired = false;

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (sensorEnterAlreadyFired)
        {
            return;
        }
        sensorEnterAlreadyFired = true;
        sensorExitAlreadyFired = false;
        */
        if (front)
        {
            this.gameObject.GetComponentInParent<Turntable>().FrontSensor = true;
        }
        else
        {
            this.gameObject.GetComponentInParent<Turntable>().BackSensor = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        /*
        if (sensorExitAlreadyFired)
        {
            return;
        }
        sensorExitAlreadyFired = true;
        sensorEnterAlreadyFired = false;
        */
        if (front)
        {
            this.gameObject.GetComponentInParent<Turntable>().FrontSensor = false;
        }
        else
        {
            this.gameObject.GetComponentInParent<Turntable>().BackSensor = false;
        }
        
    }
}
