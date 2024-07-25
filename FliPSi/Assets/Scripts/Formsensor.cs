using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formsensor : Sensor
{ 
    /*This is the subscript for the sensor that recognizes the different Product types
     * 
     * It inherits it's basic functions from the class Sensor
     * Basic functions are: Connect with sps, disconnect from sps
     * 
     * Its' specific function is to recognize, which type of Product passes the Sensor
     * and to save this information in the var "product".
     */
    public ProductList.Product product = ProductList.Product.Unknown;

    //checking which Product goes through
    public void OnTriggerEnter(Collider other)
    {
        
        switch (product)
        {
            case ProductList.Product.Product_raw:
                product = ProductList.Product.Product_raw;
                break;
            case ProductList.Product.Product_cnc:
                product = ProductList.Product.Product_cnc;
                break;
            case ProductList.Product.Product_drill:
                product = ProductList.Product.Product_drill;
                break;
            case ProductList.Product.Product_cnc_drill:
                product = ProductList.Product.Product_cnc_drill;
                break;
            default:
                product = ProductList.Product.Unknown;
                break;
        }
    }
}
