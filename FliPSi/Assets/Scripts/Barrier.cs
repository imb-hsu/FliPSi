using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : ElementComponent
{
    public Transform[] Wings = new Transform[2];
    public Conveyor[] conveyors = new Conveyor[2];
    public LightSensor front;
    public LightSensor back;
    public Vector3 minLeft;
    public Vector3 maxLeft;
    public Vector3 minRight;
    public Vector3 maxRight;
    public bool open = false;

    [Exportable]
    public int LSfront = 1;
    [Exportable]
    public int LSBack = 1;
    [Exportable]
    public int LeftWingOpen = 0;
    [Exportable]
    public int LeftWingClosed = 1;
    [Exportable]
    public int RightWingOpen = 0;
    [Exportable]
    public int RightWingClosed = 1;
    [Exportable]
    public int openLW = 0;
    [Exportable]
    public int openRW = 0;
    [Exportable]
    public int closeLW = 0;
    [Exportable]
    public int closeRW = 0;

    public Vector3 offset = new Vector3(0.3f, 0f , 0f);

    public override void Start()
    {
        //global positions for wings
        maxLeft = Wings[0].position;
        maxRight = Wings[1].position;
        minLeft = Wings[0].position;
        minRight = Wings[1].position;
        // Convert global positions back to local positions
        maxLeft = Wings[0].parent.InverseTransformPoint(maxLeft)  - offset;
        maxRight = Wings[1].parent.InverseTransformPoint(maxRight)  + offset;
        minLeft = Wings[0].parent.InverseTransformPoint(minLeft);
        minRight = Wings[1].parent.InverseTransformPoint(minRight);
        //change back to global
        maxLeft = Wings[0].parent.TransformPoint(maxLeft);
        maxRight = Wings[1].parent.TransformPoint(maxRight);
        minLeft = Wings[0].parent.TransformPoint(minLeft);
        minRight = Wings[1].parent.TransformPoint(minRight);
        // this enshures correct behaviour for different orientation of the module

    }
    public override void AddItems()
    {
        sps.Add(this);
    }
    // activates Motor in conveyor (forward)
    public override void MotorForward()
    {

    }

    // activates Motor in conveyor (backward)
    public override void MotorBackward()
    {

    }

    // turns motor in conveyor off
    public override void MotorOff()
    {

    }
    public void Open()
    {
        //Debug.Log("opening");
        open = true;
        openLW = 1;
        openRW = 1;
        LeftWingClosed = 0;
        RightWingClosed = 0;
    }
    public void Close()
    {
        open = false;
        closeLW = 1;
        closeRW = 1;
        LeftWingOpen = 0;
        RightWingOpen = 0;
    }
    public void Update()
    {
        LSfront = front.value ? 1 : 0;
        LSBack = back.value ? 1 : 0;
        if(open)
        {
            if(Wings[0].position == maxLeft)
            {
                openLW = 0;
                LeftWingOpen = 1;
            }
            if(Wings[1].position == maxRight)
            {
                openRW = 0;
                RightWingOpen = 1;
            }
            Wings[0].position = Vector3.MoveTowards(Wings[0].position, maxLeft, Time.deltaTime * 0.2f);
            Wings[1].position = Vector3.MoveTowards(Wings[1].position, maxRight, Time.deltaTime * 0.2f);
        }
        if(!open)
        {
            if(Wings[0].position == minLeft)
            {
                closeLW = 0;
                LeftWingClosed = 1;
            }
            if(Wings[1].position == minRight)
            {
                closeRW = 0;
                RightWingClosed = 1;
            }
            Wings[0].position = Vector3.MoveTowards(Wings[0].position, minLeft, Time.deltaTime * 0.2f);
            Wings[1].position = Vector3.MoveTowards(Wings[1].position, minRight, Time.deltaTime * 0.2f);
        }
        if(LeftWingOpen == 1 && RightWingOpen == 1)
        {
            foreach (Conveyor c in conveyors)
            {
                c.MotorForward();
            }
        }

    }


}
