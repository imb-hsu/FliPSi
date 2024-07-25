using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * This scripts operates the input elemtary-modul.
 * On call the GameObject InstantiateObject is instantiated at the parent's position [FixedUpdate()]
 * 
 * The GameObject is linked within the module
 */

public class InputCubes : MonoBehaviour
{
    /********************************************************************************************************************************************/

    
    // variable for game object, which will be instantiated
    public GameObject InstantiateObject;

    public int counter = 0;
    public ProductList cubescript;

    /********************************************************************************************************************************************/
    // functions
    #region connectSPS
    // get SPS 
    BilloSPS sps;

    public void AddItems()
    {   // Connect with SPS and create variables
        sps = GameObject.FindGameObjectWithTag("BilloSPS").GetComponent<BilloSPS>();
        sps.AddInput(this);
    }

    public void RemoveItems()
    {   // Remove all items from network variable list
        sps.RemoveInput(this);
    }
    #endregion

    public ProductList AddCube()
    {
        GameObject go = Instantiate(InstantiateObject, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        go.name = go.name + counter;
        counter++;
        cubescript = go.GetComponent<ProductList>();
        return cubescript;
    }

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
