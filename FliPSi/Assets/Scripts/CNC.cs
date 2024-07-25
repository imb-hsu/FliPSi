using UnityEngine;
using System;
using System.Diagnostics;

/* This is the main operating script for the cnc-machine elementary module
 * 
 * It is changed to an abstract class so that specific operations can inherit this basic functionallity and specify the actual function. 
 * 
 * The milling operation deletes the incoming product [target] and replaces it by a variant [processed_targetX]
 * Door opening and closing events are operated by events in front and back sensor areas and external scripts CNC_FrontDoor & CNC_BackDoor.
 * 
 * The script operates:
 * (1) Import, save and load [AddItems(), RemoveItems()]
 * (2) conveyor motor status and speed [MotorOn(), MotorOff()]
 * (3) front & backdoor behavior [enterSensorFront(), exitSensorFront(), enterSensorBack(), exitSensorBack()]
 * (4) the specific operation [enterSensorMiddle()]
 * 
 */

public abstract class CNC : Module
{
    //[Exportable]
    public bool hasObject = false;
    /********************************************************************************************************************************************/
    public CNC_Door frontdoor;
    public CNC_Door backdoor;
    // conveyor speed
    //[Exportable]
    public float targetVelocity = 500f;

    // conveyor torque
    //[Exportable]
    public float force = 500f;

    public float var;

    // conveyor free Spin
    public bool freeSpin = false;

    // processed product variant(s) 
    
    // GameObject
    protected GameObject target;


    [Exportable]
    public int MachineVor = 0;
    [Exportable]
    public int MachineZurück = 0;
    [Exportable]
    public int machineVorne = 0;
    [Exportable]
    public int machineHinten = 1;
    [Exportable]
	public int motorValue = 0;
	[Exportable]
	public int Sensorvalue = 0;

    /********************************************************************************************************************************************/
    // functions

    //will be called on enter enterSensorMiddle(GameObject go)
    public abstract void executeCNC(GameObject go);


    public virtual void Update()
    {
        var = UnityEngine.Random.Range(-1.5f, 1.5f);
		targetVelocity = targetVelocity + var;
		var = UnityEngine.Random.Range(-1.5f, 1.5f);
		force = force + var;
		if(!(force > 490f && force < 510f))
		{
			force = 500f;
		}
		if(!(targetVelocity > 490f && targetVelocity < 510f))
		{
			targetVelocity = 500f;
		}
    }
    public void MotorOn()
    {   // conveyor motor on 
        // calling all hinge joints of cylinders and turn motor on
        motorValue = 1;
        HingeJoint[] hingeJoints;
        Transform root = gameObject.transform.root;
        hingeJoints = gameObject.GetComponentsInChildren<HingeJoint>();

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
        motorValue = 0;
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
        motorValue = 1;

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

    public void enterSensorFront(GameObject go)
    { 
        //opens front door when GO is approaching CNC && turns motor on
        //is setting the target important? is never used within funktion!
        if(go.GetComponent<ProductList>() != null)
        {
            target = go;
            frontdoor.OpenDoor();
            //MotorOn();
        }
    }

    public void exitSensorFront()
    {
        //closes door after GO passes
        target = null;
        frontdoor.CloseDoor();
    }

    public void enterSensorMiddle(GameObject go)
    {
        //Execute the respective action of the actual CNC machine (eg milling, drilling...)
        executeCNC(go);   
    }

    public void enterSensorBack(GameObject go)
    {
        //opens back door when GO is approaching
        target = go;
        backdoor.OpenDoor();
    }

    public void exitSensorBack()
    {
        //closes back door after GO passes
        target = null;
        backdoor.CloseDoor();
    }
    
    /********************************************************************************************************************************************/
    #region Menu
    // is the settings window displayed? 
    private bool optionWindow = false;

    private void OnGUI()
    {   // operates single settings windows 
        if (optionWindow)
        {
            RemoveItems();
            GUI.Window(0, new Rect(0, 0, 350, 180), OptionWindow, "Options");
        }
    }

    void OptionWindow(int id)
    {   // settings window for manipulating all settings of the GameObject

        // name of GameObject
        this.gameObject.name = GUI.TextField(new Rect(20, 30, 200, 20), this.gameObject.name);

        // object variables 
        targetVelocity = GUI.HorizontalSlider(new Rect(20, 60, 100, 20), Convert.ToInt16(targetVelocity), 0, 500);
        GUI.Label(new Rect(150, 60, 50, 20), Convert.ToString(targetVelocity));
        GUI.Label(new Rect(220, 60, 100, 20), "target Velocity");

        force = GUI.HorizontalSlider(new Rect(20, 90, 100, 20), Convert.ToInt16(force), 0, 500);
        GUI.Label(new Rect(150, 90, 50, 20), Convert.ToString(force));
        GUI.Label(new Rect(220, 90, 100, 20), "force");

        freeSpin = GUI.Toggle(new Rect(20, 120, 100, 20), freeSpin, " free Spin");
        
        // delete GameObject
        if (GUI.Button(new Rect(20, 150, 70, 20), "Destroy"))
        {
            Destroy(this.gameObject);
        }

        // close window
        if (GUI.Button(new Rect(100, 150, 50, 20), "OK"))
        {
            AddItems();
            optionWindow = false;
        }
    }

    private void OnMouseOver()
    {   // open settings window, if right-click on GameObject
        if (Input.GetMouseButton(1))
        {
            optionWindow = true;
        }
    }
    
    #endregion
}
