using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* The elementary module sensor turn true as long a nearby object is detected.
 * 
 * 
 * 
 * 
 */

public class Sensor : ElementComponent
{   
    //bool value to determine if a Collider is in the Sensor area
    public Conveyor MyParent;
    public bool sensorVal = true;
    public Conveyor Next;
    #region useless
    //override abstract Methods from Parent; no application for Sensor
    public override void AddItems()
    {
        sps = GameObject.FindGameObjectWithTag("BilloSPS").GetComponent<BilloSPS>();
        sps.Add(this);
    }
    public override void MotorForward()
    {
        //emptyStatement
    }
    public override void MotorBackward()
    {
        //emptyStatement
    }
    public override void MotorOff()
    {
        //emptyStatement
    }
    #endregion
    //detect collision, reference in InputModule.cs
    private void OnTriggerEnter(Collider other)
    {
        sensorVal = false;
        MyParent.MotorForward();
        Next.MotorForward();
    }

    private void OnTriggerExit(Collider other)
    {
        sensorVal = true;
    }


    #region Menü
    /*

    // is options menu displayed 
    public bool optionWindow = false;
    // controlls options menu
    private void OnGUI()
    {
        if (optionWindow)
        {
            RemoveItems();
            GUI.Window(0, new Rect(0, 0, 250, 90), OptionWindow, "Options");
        }
    }

    // options menu
    void OptionWindow(int id)
    {
        // name of GO
        this.gameObject.name = GUI.TextField(new Rect(20, 30, 200, 20), this.gameObject.name);

        // destroy GO
        if (GUI.Button(new Rect(20, 60, 70, 20), "Destroy"))
        {
            Destroy(this.gameObject);
        }

        // close window
        if (GUI.Button(new Rect(100, 60, 50, 20), "OK"))
        {
            AddItems();
            optionWindow = false;
        }
    }
    */
    #endregion
}
