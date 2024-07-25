	﻿using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System;
	using System.Diagnostics;


	public class Turntable : ElementComponent
	{	


		//To make this a sorting module (sort by color) remove the ISTT script from InductiveSensor and add SortByColor_Sensor.cs (remember to add dependencies)



	    /********************************************************************************************************************************************/
	    // Imports and initialisation
	    public float targetVelocity = 200.0f;
	    public float force = 250.0f;
	    public bool freeSpin = false;
	    public int turningSpeed = 20;
	    public bool FrontSensor = false;
	    public bool BackSensor = false;

	    public bool isRightTurn = false;
	    public bool isLeftTurn = false;

		//export for Ftech (BA Thesis)
		public InductiveSensor ID;

		[Exportable]
		public int SensorVal = 0;
		[Exportable]
		public int motorVal = 0;
		[Exportable]
		public int pos0 = 1;
		[Exportable]
		public int pos90 = 0;
		[Exportable]
		public int turnRight;
		[Exportable]
		public int turnLeft;



	/********************************************************************************************************************************************/
	// functions


	public override void AddItems()
    {
		sps = GameObject.FindGameObjectWithTag("BilloSPS").GetComponent<BilloSPS>();
        sps.Add(this);
    }
	public override void Start()
	{
		sps = GameObject.FindGameObjectWithTag("BilloSPS").GetComponent<BilloSPS>();
		AddItems();
		MotorOff();
		turnExecuted = false;
	}

	public void ContinouusRightTurn(bool active)
	    {
		this.gameObject.GetComponentInChildren<Turnplate>().continouusRightTurn = active;
	    }

	public void ContinouusLeftTurn(bool active)
	    {
		this.gameObject.GetComponentInChildren<Turnplate>().continouusLeftTurn = active;
	    }

	public void TurnRight()
	    {
		this.gameObject.GetComponentInChildren<Turnplate>().turnByDegree(90);
		turnRight = 1;
	    }

	public void TurnLeft()
	    {
		this.gameObject.GetComponentInChildren<Turnplate>().turnByDegree(-90);
		turnLeft = 1;
	    }

	public void TurnByDegree(float degree)
	    {
		this.gameObject.GetComponentInChildren<Turnplate>().turnByDegree(degree);
	    }



	    public void Grab(bool active)
	    {
		if (active)
		{
		    this.gameObject.GetComponentInChildren<Turnplate>().Connect();
		}
		else
		{
		    this.gameObject.GetComponentInChildren<Turnplate>().Disconnect();
		}
	    }
	   
	    public override void MotorForward()
	    { 
			/* 
				---legacy code---
			----	Find the calling script for debugging	----
			motorVal = 1;
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
        	}        else
        	{
            	//UnityEngine.Debug.Log("No calling script found.");
        	}
			*/

			// conveyor forward
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
	    {
			// conveyor backward
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

	    public override void MotorOff()
	    {   
			motorVal = 0;
			// conveyor off
			HingeJoint[] hingeJoints;
			Transform root = gameObject.transform.root;
			hingeJoints = root.gameObject.GetComponentsInChildren<HingeJoint>();

			if (hingeJoints != null)
			{
		    	foreach (HingeJoint joint in hingeJoints)
		        	joint.useMotor = false;
			}
			turnExecuted = true;
			if(turnExecuted)
			{
				BackSensor = false;
			}
			Grab(false);
	    }

	    public void enterSensorFront ()
	    {
		//Start the motors whenever something comes in.
		//empty statement, fix later
	    }


	    public IEnumerator callTurnByDegreeWithDelay(float degree, float delayTime)
	    {
		yield return new WaitForSeconds(delayTime);
		TurnByDegree(degree);
	    }

	    public IEnumerator releaseAfterDelay (float delayTime)
	    {
		yield return new WaitForSeconds(delayTime);
		Grab(false);
	    }


	    public bool turnExecuted = false;
	   void Update()
	    {
			Turnplate TP = this.gameObject.GetComponentInChildren<Turnplate>();
			if(!TP.currentlyRotating)
			{
				turnRight = 0;
				turnLeft = 0;
			}






			SensorVal = ID.value ? 1 : 0;
			//Check if the sensors were fired
			if (isRightTurn || isLeftTurn)
			{
		    	if (this.BackSensor && !turnExecuted)
		    	{
					//UnityEngine.Debug.Log("Turning");
		        	Grab(true);
		        	if (isRightTurn) { TurnRight(); }
		        	if (isLeftTurn) { TurnLeft(); }
		        	turnExecuted = true;
					pos0 = 0;
					//StartCoroutine(DelayFunction(0.3f));
					pos90 = 1;

		    	}
		    else if (this.BackSensor && turnExecuted)
		    	{
		        	;//empty
		    	}
		    else if (!this.BackSensor && turnExecuted)
		    	{
					
		        	Grab(false);
		        	if (isRightTurn) {
		            	//StartCoroutine(callTurnByDegreeWithDelay( -90, 2));
						TurnLeft();
						
		        	};
		        	if (isLeftTurn) {
		            	//StartCoroutine(callTurnByDegreeWithDelay( 90, 2));
						TurnRight();
						
		        	};
		        	turnExecuted = false;
					//UnityEngine.Debug.Log("turning Back");
					//pos90 = 0;
					//pos0 = 1;
					
		    	}
		    else if (!this.BackSensor && !turnExecuted)
		    {
		        ;//empty
		    }
		}

		//Debounce

	    }
	public IEnumerator  DelayFunction(float delay)
    {
        // Yield for x seconds
        yield return new WaitForSeconds(delay);
    }
	    /********************************************************************************************************************************************/
	    #region Settings
	    // is the settings window displayed? 
	    private bool optionWindow = false;

	    private void OnGUI()
	    {   // operates single settings windows 
		if (optionWindow)
		{
		    RemoveItems();
		    GUI.Window(0, new Rect(0, 0, 350, 210), OptionWindow, "Options");
		}
	    }

	    void OptionWindow(int id)
	    {   // settings window for manipulating all settings of the GameObject

		// name of GameObject
		this.gameObject.name = GUI.TextField(new Rect(20, 30, 200, 20), this.gameObject.name);

		// GameObject variables
		targetVelocity = GUI.HorizontalSlider(new Rect(20, 60, 100, 20), Convert.ToInt16(targetVelocity), 0, 500);
		GUI.Label(new Rect(150, 60, 50, 20), Convert.ToString(targetVelocity));
		GUI.Label(new Rect(220, 60, 100, 20), "target Velocity");

		force = GUI.HorizontalSlider(new Rect(20, 90, 100, 20), Convert.ToInt16(force), 0, 500);
		GUI.Label(new Rect(150, 90, 50, 20), Convert.ToString(force));
		GUI.Label(new Rect(220, 90, 100, 20), "force");

		freeSpin = GUI.Toggle(new Rect(20, 120, 100, 20), freeSpin, " free Spin");
		isLeftTurn = GUI.Toggle(new Rect(140, 120, 100, 20), isLeftTurn, " turn left ");
		isRightTurn = GUI.Toggle(new Rect(260, 120, 100, 20), isRightTurn, " turn right");

		turningSpeed = Convert.ToInt32(GUI.HorizontalSlider(new Rect(20, 150, 100, 20), turningSpeed, 0, 180));
		GUI.Label(new Rect(150, 150, 50, 20), Convert.ToString(turningSpeed));
		GUI.Label(new Rect(220, 150, 100, 20), "turning Speed");

		// delete gameObject (<- turning table in this case) 
		if (GUI.Button(new Rect(20, 180, 70, 20), "Delete"))
		{
		    Destroy(this.gameObject);
		}

		// close window 
		if (GUI.Button(new Rect(100, 180, 50, 20), "OK"))
		{
		    this.gameObject.GetComponentInChildren<Turnplate>().speed = turningSpeed;
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
