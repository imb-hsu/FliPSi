using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPoint : MonoBehaviour
{
    private GameObject target = null;

    private void OnTriggerStay(Collider other)
    {    // detect product 
        if (other.tag == "Component" || other.tag == "Product_raw" || other.tag == "Produt_cnc" || other.tag == "Product_cncdrill" || other.tag == "Product_drill") { target = other.gameObject; }
    }

    public void Connect()
    {    // connect to product
        if (target != null)
        {
            target.transform.parent = this.transform;
        }
    }

    public void Disconnect()
    {   // disconnect from product
        if (target != null)
        {
            target.transform.parent = null;
            target = null;
        }
    }
}
