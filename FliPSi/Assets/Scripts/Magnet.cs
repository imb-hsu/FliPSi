using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// no correction bc not used





public class Magnet : MonoBehaviour
{
    /*
    //Definition of Grabbing Force
    public float forceFactor = 100.0f;
    public List<Rigidbody> products = new List<Rigidbody>();
    public Transform magnetPoint;

    // Start is called before the first frame update
    void Start()
    {
        magnetPoint = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(Rigidbody product in products)
        {
            product.AddForce((magnetPoint.position - GetComponent<Rigidbody>().position) * forceFactor * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BilloSPS.Product product = BilloSPS.Product.Unknown;

        switch (product)
        {
            case BilloSPS.Product.Product_raw:
                product.Add(other.GetComponent<Rigidbody>());
                break;
            case BilloSPS.Product.Product_cnc:
                product.Add(other.GetComponent<Rigidbody>());
                break;
            case BilloSPS.Product.Product_drill:
                product.Add(other.GetComponent<Rigidbody>());
                break;
            case BilloSPS.Product.Product_cnc_drill:
                product.Add(other.GetComponent<Rigidbody>());
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        BilloSPS.Product product = BilloSPS.Product.Unknown;

        switch (product)
        {
            case BilloSPS.Product.Product_raw:
                BilloSPS.RemoveProduct(other.GetComponent<Rigidbody>());
                break;
            case BilloSPS.Product.Product_cnc:
                product.Remove(other.GetComponent<Rigidbody>());
                break;
            case BilloSPS.Product.Product_drill:
                product.Remove(other.GetComponent<Rigidbody>());
                break;
            case BilloSPS.Product.Product_cnc_drill:
                product.Remove(other.GetComponent<Rigidbody>());
                break;
            default:
                break;
        }
    }
    */


}
