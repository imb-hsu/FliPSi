using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script initializes the componets of the Storage Module and controlls their behaviour
 * 
 * Their specific actions are defined in their subscripts
 */

public class StorageModule : Module
{
    public ConveyorStorage conveyorOne;
    public ConveyorStorage conveyorTwo;
    public Conveyor conveyorThree;
    public Conveyor conveyorFour;
    public Crane crane;
    public Storage storage;
    public Colorsensor colorsensor;
    public Formsensor formsensor;

    public Transform input;
    public Transform store;
    public Transform output;

    bool debounceStorageSensor = false;

    // Imports and initialisation
    // conveyor speed
    public float targetVelocity = 200.0f;

    // conveyor torque
    public float force = 250.0f;

    // conveyor free Spin
    public bool freeSpin = false;

    
    public void MotorOn()
    {   // conveyor motor on 
        // calling all hinge joints of cylinders and turn motor on
        HingeJoint[] hingeJoints;
        Transform root = gameObject.transform.root;
        hingeJoints = root.gameObject.GetComponentsInChildren<HingeJoint>();

        if (hingeJoints != null)
        {
            foreach (HingeJoint joint in hingeJoints)
            {
                var motor = joint.motor;
                motor.targetVelocity = targetVelocity;
                motor.force = force;
                motor.freeSpin = freeSpin;
                joint.motor = motor;
                joint.useMotor = true;
            }
        }
    }

    public override void MotorOff()
    {   // conveyor motor off 
        // calling all hinge joints of cylinders and turn motor off
        HingeJoint[] hingeJoints;
        Transform root = gameObject.transform.root;
        hingeJoints = root.gameObject.GetComponentsInChildren<HingeJoint>();

        if (hingeJoints != null)
        {
            foreach (HingeJoint joint in hingeJoints)
            {
                joint.useMotor = false;
            }
        }
    }
    public override void MotorForward()
    //conveyor motor forward
    //sets direction to forward
    {
        HingeJoint[] hingeJoints;
        Transform root = gameObject.transform.root;
        hingeJoints = root.gameObject.GetComponentsInChildren<HingeJoint>();

        if (hingeJoints != null)
        {
            foreach (HingeJoint joint in hingeJoints)
            {
                var motor = joint.motor;
                motor.targetVelocity = targetVelocity;
                motor.force = force;
                motor.freeSpin = freeSpin;
                joint.motor = motor;
                joint.useMotor = true;
            }
        }
    }

    public override void MotorBackward()
    //conveyor motor backward
    //sets direction to backward
    {
        HingeJoint[] hingeJoints;
        Transform root = gameObject.transform.root;
        hingeJoints = root.gameObject.GetComponentsInChildren<HingeJoint>();

        if (hingeJoints != null)
        {
            foreach (HingeJoint joint in hingeJoints)
            {
                var motor = joint.motor;
                motor.targetVelocity = -targetVelocity;
                motor.force = force;
                motor.freeSpin = freeSpin;
                joint.motor = motor;
                joint.useMotor = true;
            }
        }
    }
    // Storing Mechanism for free slots
    public void storeInSlot() 
    {
        
        if (crane.ms != Crane.movingState.stationary) { return; };
        GameObject spottedProduct = conveyorOne.sensorFront.spottedProduct;
        crane.spottedProduct = spottedProduct;
        
        Transform[] allChildTr = this.GetComponentsInChildren<Transform>();
        Transform[] storageSlots = new Transform[9];
        foreach (Transform tr in allChildTr){
            if (tr.tag == "StorageSlot")
            {
                switch (tr.name){
                    case "Slot11":
                        storageSlots[0] = tr;
                        break;
                    case "Slot12":
                        storageSlots[1] = tr;
                        break;
                    case "Slot13":
                        storageSlots[2] = tr;
                        break;
                    case "Slot21":
                        storageSlots[3] = tr;
                        break;
                    case "Slot22":
                        storageSlots[4] = tr;
                        break;
                    case "Slot23":
                        storageSlots[5] = tr;
                        break;
                    case "Slot31":
                        storageSlots[6] = tr;
                        break;
                    case "Slot32":
                        storageSlots[7] = tr;
                        break;
                    case "Slot33":
                        storageSlots[8] = tr;
                        break;
                }
            }

        }

        if (storage.StoredObjects[0, 0].occupied == false)
        {
            crane.targetCellXCood = -0.3f;
            crane.targetCellZCood = -1.4f;
            storage.StoredObjects[0, 0].occupied = true;
            storage.StoredObjects[0, 0].color = colorsensor.objectColor;
            storage.StoredObjects[0, 0].productType = formsensor.product;
            print(storage.StoredObjects[0, 0]);
        }
        else if (storage.StoredObjects[0, 1].occupied == false)
        {
            crane.targetCellXCood = -0.3f;
            crane.targetCellZCood = 0f;
            storage.StoredObjects[0, 1].occupied = true;
            storage.StoredObjects[0, 1].color = colorsensor.objectColor;
            storage.StoredObjects[0, 1].productType = formsensor.product;

        }
        else if (storage.StoredObjects[0, 2].occupied == false)
        {
            crane.targetCellXCood = -0.3f;
            crane.targetCellZCood = 1.4f;
            storage.StoredObjects[0, 2].occupied = true;
            storage.StoredObjects[0, 2].color = colorsensor.objectColor;
            storage.StoredObjects[0, 2].productType = formsensor.product;
        }
        else if (storage.StoredObjects[1, 0].occupied == false)
        {
            crane.targetCellXCood = 1.1f;
            crane.targetCellZCood = -1.4f;
            storage.StoredObjects[1, 0].occupied = true;
            storage.StoredObjects[1, 0].color = colorsensor.objectColor;
            storage.StoredObjects[1, 0].productType = formsensor.product;
        }
        else if (storage.StoredObjects[1, 1].occupied == false)
        {
            crane.targetCellXCood = 1.1f;
            crane.targetCellZCood = 0f;
            storage.StoredObjects[1, 1].occupied = true;
            storage.StoredObjects[1, 1].color = colorsensor.objectColor;
            storage.StoredObjects[1, 1].productType = formsensor.product;
        }
        else if (storage.StoredObjects[1, 2].occupied == false)
        {
            crane.targetCellXCood = 1.1f;
            crane.targetCellZCood = 1.4f;
            storage.StoredObjects[1, 2].occupied = true;
            storage.StoredObjects[1, 2].color = colorsensor.objectColor;
            storage.StoredObjects[1, 2].productType = formsensor.product;
        }
        else if (storage.StoredObjects[2, 0].occupied == false)
        {
            crane.targetCellXCood = 2.5f;
            crane.targetCellZCood = -1.4f;
            storage.StoredObjects[2, 0].occupied = true;
            storage.StoredObjects[2, 0].color = colorsensor.objectColor;
            storage.StoredObjects[2, 0].productType = formsensor.product;
        }
        else if (storage.StoredObjects[2, 1].occupied == false)
        {
            crane.targetCellXCood = 2.5f;
            crane.targetCellZCood = 0f;
            storage.StoredObjects[2, 1].occupied = true;
            storage.StoredObjects[2, 1].color = colorsensor.objectColor;
            storage.StoredObjects[2, 1].productType = formsensor.product;
        }
        else if (storage.StoredObjects[2, 2].occupied == false)
        {
            crane.targetCellXCood = 2.5f;
            crane.targetCellZCood = 1.4f;
            storage.StoredObjects[2, 2].occupied = true;
            storage.StoredObjects[2, 2].color = colorsensor.objectColor;
            storage.StoredObjects[2, 2].productType = formsensor.product;
        }
        crane.ms = Crane.movingState.toPickup;
    }

    private void Update()
    {
        if (conveyorOne.sensorFront.sensorVal && !debounceStorageSensor)
        {
            storeInSlot();
            debounceStorageSensor = true;
        }

        else if (!conveyorOne.sensorFront.sensorVal )
        {
            //print("debounce");
            debounceStorageSensor = false;
            return;
        }
    }

}

