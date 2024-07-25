using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script defines the Sensor, that detects Products entering and exiting the Storage Module
 */

public class StorageSensor : MonoBehaviour
{
    public bool front;
    public bool sensorVal = false;

    public GameObject spottedProduct;


    public bool SensorVal
    {
        get { return sensorVal; }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Detects object when it enters 
        //print("Sensor Enter");
        sensorVal = true;
        if (front)
        {
            if (other.tag == "Component" || other.tag == "Product_raw" || other.tag == "Product_cnc" || other.tag == "Product_cncdrill" || other.tag == "Product_drill")
            { spottedProduct = other.gameObject; }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //sensorVal = false;
        //print("Sensor Exit");

    }

    void Update()
    {
        sensorVal = false;
        //print("Sensor Exit");
    }
}
