using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor_Menu : ElementComponent
{
        public override void AddItems()
    {
        sps.Add(this);
    }
    #region useless override
    public override void MotorForward()
    {
        //empty
    }
    public override void MotorBackward()
    {
        //empty
    }
    public override void MotorOff()
    {
        //empty
    }
    #endregion

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
    public void OptionWindow(int id)
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
    //open options menu if mouse is over GO && right mouse click
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            if (transform.root.gameObject.tag == "Sensor")
                this.optionWindow = true;

            if (transform.root.gameObject.tag == "Colorsensor")
                this.optionWindow = true;
        }
    }
}
