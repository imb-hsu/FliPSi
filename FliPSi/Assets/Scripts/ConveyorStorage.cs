using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script controlls the modified version of the conveyor.
 * 
 * Its' basic functions are declared in Conveyor.cs from which it inherits
 * 
 * Its' further modification constists of a added sensor, that stops the motor when an object is detected
 */

//I want this to inherit from Conveyor (for essential funktions) AND Module (for sorting porpuses) -> Try Workaround with interface



public class ConveyorStorage : Module
{
    public StorageSensor sensorFront;
    public StorageSensor sensorBack;

 

    public bool sensorTriggered = false;




    public bool useLimits = false;

    public void enterSensorFront()
    {
            // Stop motor if object 

            HingeJoint[] hingeJoints;
            hingeJoints = gameObject.GetComponentsInChildren<HingeJoint>();

            if (hingeJoints != null)
            {
                foreach (HingeJoint joint in hingeJoints)
                {
                    joint.useLimits = true;
                    joint.useMotor = false;
                }
            }

    }

    public void exitSensorFront()
    {
    //sets object back to movement, when detected -- Use limits auf false setzen

        HingeJoint[] hingeJoints;
        hingeJoints = gameObject.GetComponentsInChildren<HingeJoint>();

        if (hingeJoints != null)
        {
            foreach (HingeJoint joint in hingeJoints)
            {
                joint.useLimits = false;
                joint.useMotor = true;
            }
        }
    }

}

