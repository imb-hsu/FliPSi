using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is operating the assembly module && extends CNC; 
 * implements funktion executeCNC(go) -> will be called on CNC.enterSensorMiddle()
*/
public class AssemblyModule : CNC 
{
    //The item which replaces the raw item 
    public GameObject processed_target_raw_cnc_assembly;
    //to check if two correct Products passed the sensor
    public bool Object1=false;
    public bool Object2=false;

    /*
     * (1) takes in Gameobject (later Target) (one "Product_raw" and one "Product_cnc" -> spits out assembled Gameobject)
     * (2) checks for case (how is the product handled eg. error, process product, 
     * (3) process product (here delete old GO and create new one) OR Error
     * (4) reset to original state
    */
    public override void executeCNC(GameObject go)
    {
        
        target = go;

        switch (go.tag)
        {
            case "Product_raw":
                if (Object1 == true)
                {
                    //Error if more than one raw product passes the sensor
                    print("Error: product_raw detected twice");
                }
                // first required product
                else if (Object1 == false) 
                {
                    Object1 = true;
                    Destroy(target);
                }

                break;
            case "Product_cnc":
                if (Object2 == true)
                {
                    //Error if more than one milled product passes the sensor
                    print("Error: product_cnc detected twice");
                }
                //second needed product
                else if (Object2 == false)
                {
                    Object2 = true;
                    Destroy(target);
                }
                break;

            //no output if newly created product touches the sensor
            case "Product_raw_cnc_assembly":
                break;

            case "Product_drill":
                print("Error: wrong product!");
                // Error if wrong product enters the sensor
                break;                              

            case "Product_cnc_drill":
                print("Error: wrong product!");
                break;
        }

        //spawning new processed product
        if (Object1==true && Object2==true)
        {
            Instantiate(processed_target_raw_cnc_assembly, new Vector3(transform.position.x - 0.15f, transform.position.y + 1.5f, transform.position.z -0.03f), transform.rotation);
            //set module back to original state
            Object1 = false;
            Object2 = false; 
        }

    }
}
