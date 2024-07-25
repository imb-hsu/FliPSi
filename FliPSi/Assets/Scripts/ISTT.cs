using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISTT : InductiveSensor
{
    //Inductive-Sensor-Turntable
    public Turntable MyTT;
    public Turnplate TP;



    public void push()
    {
        //Debug.Log("pushing TT");
        MyTT.MotorForward();
        Next.MotorForward();
    }
    public new void OnTriggerEnter(Collider other)
    {   if(other.gameObject.GetComponent<ProductList>() != null)
        {
            TP.target = other.gameObject;
            value = true;
        }
        //Debug.Log("EnterCollider");
        //MyTT.MotorOff();
        Prev.MotorOff();
        
        StartCoroutine(DelayFunction(0.2f));
        if(straight)
        {
            push();
        }else if(left)
        {
            MyTT.Grab(true);
            MyTT.TurnLeft();
            MyTT.isLeftTurn = true;
            //MyTT.turnExecuted = true;
        }else  if (right)
        {
            MyTT.Grab(true);
            MyTT.TurnRight();
            MyTT.isRightTurn = true;
            //MyTT.turnExecuted = true;
        }

    }
    public new void OnTriggerExit(Collider other)
    {
        //Debug.Log("ExitCollider");
        if(Next == null)
        {
            MyTT.MotorOff();
            MyTT.Grab(false);
            MyTT.BackSensor  = true;
            StartCoroutine(DelayFunction(0.5f));
            MyTT.BackSensor = false;

        }
        value = false;
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<ProductList>() != null)
        {
            if(!TP.currentlyRotating)
            {
                push();
            }
        }
    }
}
