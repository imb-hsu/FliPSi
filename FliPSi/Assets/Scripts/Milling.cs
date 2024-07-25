using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/* Derived class from cnc for milling operations. 
 * implements funktion executeCNC(go) -> will be called on CNC.enterSensorMiddle()
 */

public class Milling : CNC
{
    //The item which replaces the raw item (implemented in Unity -> Prefab)
    public GameObject processed_target_mill_drill;
    public GameObject processed_target_mill;
    //exportable values FTech


	public InductiveSensor ID;

  /*
   * (1) check for input (GO) -> tag
   * (2) process input -> destroy input; spawn new GO
   */
    public override void executeCNC(GameObject go)
    {
        /*
        old code, I try something new
        target = go;

        //First check if the entered object actually is the raw product.
        if (go.tag == "Product_cnc" || go.tag == "Product_cnc+drill")
        {
            return;
        }
        string tag = go.tag;
        
        Destroy(target);
        if (tag == "Product_drill")
        {
            Debug.Log("I reached if 1");
            Instantiate(processed_target_mill_drill, new Vector3(transform.position.x , transform.position.y + 1.5f, transform.position.z), transform.rotation);
            
        }
        if (tag == "Product_raw")
        {
            Debug.Log("I reached if 2");
            Instantiate(processed_target_mill_drill, new Vector3(transform.position.x , transform.position.y + 1.5f, transform.position.z), transform.rotation);
            Debug.Log("I reached if 2");
            
        }
    
    new code worked :)
    */
    ProductList productS = go.GetComponent<ProductList>();
    productS.ChangeType(1);
    //delete for F3
    //Destroy(go);
    }
}
