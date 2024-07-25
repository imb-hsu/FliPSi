using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;



public class Conveyor : ElementComponent
{	
	// motorvalues: speed, spin / freespin
    public float targetVelocity = 500.0f;
    public float force = 500.0f; 
    public bool freeSpin = false;
	public float var;
	public float reference = 500;
	public float brokenReference = 150;
	public Sensor SensorFront;
	public Sensor SensorBack;
	public bool active;
	// stuff to increase speed
	public float increaseby = 10;
	public float timer = 0;
	public float intervall = 5;
	private Rigidbody  RB;


	//exportable values FTech
	[Exportable]
	public int motorValue = 0;
	[Exportable]
	public int Sensorvalue = 0;

	public InductiveSensor ID;

	public void Update()
    {
		if(ID != null)
		{
			Sensorvalue = ID.value ? 1 : 0;
		}
		increaseSpeed();
		var = UnityEngine.Random.Range(-1.5f, 1.5f);
		targetVelocity = targetVelocity + var;
		var = UnityEngine.Random.Range(-1.5f, 1.5f);
		force = force + var;
		if(!(force > reference - 10f && force < reference + 10f))
		{
			force = reference;
		}
		if(!(targetVelocity > reference - 10f && targetVelocity < reference + 10f))
		{
			targetVelocity = reference;
		}

    }

	public void increaseSpeed()
	{
		if(active)
		{
			//UnityEngine.Debug.Log(Time.deltaTime);
			timer = timer + Time.deltaTime;
			if(timer >= intervall)
			{
				timer = 0.0f;
				reference = reference + increaseby;
			}
		}
	}
	// Motor forward (called by Barrier.cs) -> find all HingeJoints && turn motor on
	public override void MotorForward()
    {
		/*
		// Get the call stack
        StackTrace stackTrace = new StackTrace();
		// Check if there's at least one frame in the stack
        if (stackTrace.FrameCount > 1)
        {
            // Get the calling method (frame at index 1)
            StackFrame callingFrame = stackTrace.GetFrame(1);
            
            // Get the method's declaring type (script)
            System.Type declaringType = callingFrame.GetMethod().DeclaringType;

            // Print the script's name to the console
            //UnityEngine.Debug.Log("Calling Script: " + (declaringType != null ? declaringType.Name : "Unknown"));
        }
        else
        {
            //UnityEngine.Debug.Log("No calling script found.");
        }
		*/

		//UnityEngine.Debug.Log("MotorOn" + gameObject.name);
		motorValue = 1;
		active = true;
		HingeJoint[] hingeJoints;
		hingeJoints = gameObject.GetComponentsInChildren<HingeJoint>();

		if (hingeJoints != null)
		{
	    	foreach (HingeJoint joint in hingeJoints)
	 	    	{
					joint.useLimits = false;
					var motor = joint.motor;
					motor.targetVelocity = targetVelocity;
					motor.force = force;
					motor.freeSpin = freeSpin;
					joint.motor = motor;
					joint.useMotor = true;
		    }
		}
    }

	// Motor backward (called by Barrier.cs) -> find all HingeJoints && turn motor on
	public override void MotorBackward()
    {
	HingeJoint[] hingeJoints;
	hingeJoints = gameObject.GetComponentsInChildren<HingeJoint>();

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

    // turn motor off
    public override void MotorOff()
    {
		motorValue = 0;
		//UnityEngine.Debug.Log("MotorOff" + gameObject.name);
		active = false;
		HingeJoint[] HingeJoints;
		//Transform root = gameObject.transform.root;
		HingeJoints = gameObject.GetComponentsInChildren<HingeJoint>();

		if (HingeJoints != null)
		{
	    	foreach (HingeJoint joint in HingeJoints)
			{
				joint.useLimits = true;
				joint.useMotor = false;
			}
		}
    }

	public void SensorAction()
    { /*
		HingeJoint[] hingeJoints;
		Transform root = gameObject.transform.root;
		hingeJoints = root.gameObject.GetComponentsInChildren<HingeJoint>();
		joint.useMotor = false;
		*/
    }
    public override void AddItems()
    {	
		sps = GameObject.FindGameObjectWithTag("BilloSPS").GetComponent<BilloSPS>();
        sps.Add(this);
    }


}
