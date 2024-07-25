using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script initializes the components of the input module, and controls their behaviour
 * 
 * It is used to control the underlying functions. Elemental function should not go in here!
 * 
 * 
 */

public class InputModule : Module
{
    public InputCubes inputCubes;
    [Exportable]
    public int lightsensorVal = 1;

    public Sensor LS;

    public override void AddItems()
    {
        sps = GameObject.FindGameObjectWithTag("BilloSPS").GetComponent<BilloSPS>();
        base.AddItems();
        sps.AddInput(inputCubes);
    }

    

    void Update()
    {
        lightsensorVal = LS.sensorVal ? 1 : 0;
    }
    public void AddCube()
    {
        inputCubes.AddCube();
    }

}
