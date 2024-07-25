using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script operates the turning part of the turning table
 * 
 * 
 * 
 */
public class Turnplate : ElementComponent
{
    /********************************************************************************************************************************************/
    // Imports and initialisation
    public bool continouusRightTurn;
    public bool continouusLeftTurn;
    public int speed = 20;
    public float rotateTime = 1.0f; // former 5
    public float rotation;
    public bool currentlyRotating = false;

    // incomig product
    public GameObject target = null;

	private int P0 = 1;
	private int P90 = 0;

	/********************************************************************************************************************************************/
	// functions
	public override void AddItems()
    {
        sps.Add(this);
    }
	public override void MotorForward()
    {
		//empty statement
    }
	public override void MotorBackward()
    {
		//empty statement
	}
	public override void MotorOff()
    {
		//empty statement
	}

	private void OnTriggerStay(Collider other)
    {    // detect product 
	if (other.tag == "Component" || other.tag == "Product_raw" || other.tag == "Product_cnc" || other.tag == "Product_cncdrill" || other.tag == "Product_drill")
	{ target = other.gameObject; }
    }

    public void Connect()
    {    // connect to product
		if (target != null)
		{
			target.transform.parent = this.transform;
		}
    }

    public void Disconnect()
    {   // disconnect from product
	if (target != null)
	{
	    target.transform.parent = null;
	    target = null;
	}
    }

    IEnumerator RotateMe(Vector3 byDegree, float inTime)
    {   // Rotate disk "byDegree" in "inTime" seconds
		currentlyRotating = true;

		var fromDegree = transform.rotation;
		var toDegree = Quaternion.Euler(transform.eulerAngles + byDegree);
		for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
		{
	    	transform.rotation = Quaternion.Lerp(fromDegree, toDegree, t);
	    	yield return null;
		}

		currentlyRotating = false;
		if(P0 == 0)
		{
			GetComponentInParent<Turntable>().pos0 = 1;
			P0 = 1;
		}else
		{
			GetComponentInParent<Turntable>().pos0 = 0;
			P0 = 0;
		}
		if(P90 == 0)
		{
			GetComponentInParent<Turntable>().pos90 = 1;
			P90 = 1;
		}else
		{
			GetComponentInParent<Turntable>().pos90 = 0;
			P90 = 0;
		}
    }

    public void turnByDegree(float degree)
    {
	if (!currentlyRotating)
	{
	    StartCoroutine(RotateMe(Vector3.up * degree, rotateTime));

	}
    }

    private void Update()
    { 
	
		if(currentlyRotating)
		{
			GetComponentInParent<Turntable>().pos0 = 0;
			GetComponentInParent<Turntable>().pos90 = 0;
		}	
	
    }

}



