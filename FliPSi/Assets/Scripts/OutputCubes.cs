using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This scripts operates the output elemtary-modul.
 * on collision the colliding gameObject is destroyed.
 * 
 */

public class OutputCubes : MonoBehaviour
{
    public Conveyor MyC;
    /********************************************************************************************************************************************/
    //Destroys whatever is leaving the trigger
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        MyC.MotorOff();
    }

    /********************************************************************************************************************************************/
    #region Menu
    // is the settings window displayed?  
    private bool optionWindow = false;

    private void OnGUI()
    {   // operates single settings windows 
        if (optionWindow)
        {
            GUI.Window(0, new Rect(0, 0, 230, 90), OptionWindow, "Options");
        }
    }

    void OptionWindow(int id)
    {   // settings window for manipulating all settings of the GameObject

        // name of GameObject
        this.gameObject.name = GUI.TextField(new Rect(20, 30, 200, 20), this.gameObject.name);

        // delete GameObject (<- the output module object) 
        if (GUI.Button(new Rect(20, 60, 70, 20), "Delete"))
        {
            Destroy(this.gameObject);
        }

        // close window
        if (GUI.Button(new Rect(100, 60, 50, 20), "OK"))
        {
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
