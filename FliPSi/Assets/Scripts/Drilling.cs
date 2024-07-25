using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Derived class from cnc for drilling operations. 
 * implements funktion executeCNC(go) -> will be called on CNC.enterSensorMiddle()
 */

public class Drilling : CNC
{
    //The item which replaces the raw item (implemented in Unity -> Prefab)
    public GameObject processed_target_mill_drill;
    public GameObject processed_target_drill;

    /*
     * (1) check for input (GO) -> tag
     * (2) process input -> destroy input; spawn new GO
     */
    public override void executeCNC(GameObject go)
    {
    ProductList productS = go.GetComponent<ProductList>();
    if(productS.currentProduct == ProductList.Product.Product_raw)
    {
        productS.ChangeType(2);
    }else if (productS.currentProduct == ProductList.Product.Product_cnc)
    {
        productS.ChangeType(3);
    }else{
        Debug.Log("drilling Put");
    }
    }
   
}
