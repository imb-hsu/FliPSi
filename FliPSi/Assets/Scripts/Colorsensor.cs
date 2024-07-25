using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorsensor : Sensor
{
    
    // the 9 possible colors
    public Color[] color = { Color.white, Color.grey, Color.black, Color.yellow, Color.red, Color.magenta, Color.blue, Color.cyan, Color.green };

    // attributes of object
    public Color objectColor;
    public bool objectPresent = false;

    // change values
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            objectColor = other.gameObject.GetComponent<Renderer>().material.color;
            objectPresent = true;
            return;
        }
        catch (MissingComponentException)
        {
            //Object has no material color
        };
        try
        {
            objectColor = other.gameObject.GetComponentInChildren<Renderer>().material.color;
            objectPresent = true;
            return;
        }
        catch (MissingComponentException e)
        {
            //Children also have no material color
            throw e;
        };
    }

    private void OnTriggerExit(Collider other)
    {
        objectPresent = false;
    }

}
