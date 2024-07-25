using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortByColor_Sensor : InductiveSensor
{
    //Inductive-Sensor-Turntable
    public Turntable MyTT;
    public Turnplate TP;
    public Color ColorLeft;
    public Color ColorRight;
    public Color ColorStraight;
    public Connect StraightConnect;
    public Connect LeftConnect;
    public Connect RightConnect;
    public Connect BackConnect;

    void Start()
    {
        BackConnect.ConnectEC(BackConnect._collider);
    }



    public void StopAll()
    {
        Next.MotorOff();
        Prev.MotorOff();
        MyTT.MotorOff();
    }
    public void push()
    {
        //Debug.Log("pushing TT");
        MyTT.MotorForward();
        Next.MotorForward();
    }
    public new void OnTriggerEnter(Collider other)
    {   
        //Check if a valid product is in the TT
        if(other.gameObject.GetComponent<ProductList>() != null)
        {
            TP.target = other.gameObject;
            value = true;
        }
        //find the sorting direction
        Renderer renderer = other.GetComponent<Renderer>();
        Material material = renderer.material;
        if (material.HasProperty("_Color"))
        {
            if(material.color == ColorLeft)
            {
                left = true;
                right = false;
                straight = false;
                LeftConnect.ConnectEC(LeftConnect._collider);
            }else if(material.color == ColorRight)
            {
                left = false;
                right = true;
                straight = false;
                RightConnect.ConnectEC(RightConnect._collider);
            }else if (material.color == ColorStraight)
            {
                left = false;
                right = false;
                straight = true;
                StraightConnect.ConnectEC(StraightConnect._collider);
            }else{
                Debug.Log("Invalid Object");
            }
        }



        //functionality
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
