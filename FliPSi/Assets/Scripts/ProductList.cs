using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ProductList : MonoBehaviour
{
    // List of possible Products
    public enum Product
    {
        Product_raw,
        Product_cnc,
        Product_drill,
        Product_cnc_drill,
        Product_raw_cnc_assembly,
        Unknown,
    }
    public GameObject[] Body = new GameObject[4];
/*
position of form X/Y/Z
0 = raw 0/0/0
1 = CNC 0/-0.155/-0.15
*/
    //[Exportable]
    public float posX;
    //[Exportable]
    public float posY;
    //[Exportable]
    public float posZ;
    //public BilloSPS sps;
    
    public Product currentProduct;

    public void Start()
    {
        //BilloSPS sps = GameObject.FindGameObjectWithTag("BilloSPS").GetComponent<BilloSPS>();
        //sps.myDataCollector.WriteEventData(this.name, new double[] { this.transform.position.x, this.transform.position.y }, this, new List<string> { "XPos" , "YPos" } );
        }
    public void Update()
    {
        //if(sps.myDataCollector != null)
        //{
        BilloSPS sps = GameObject.FindGameObjectWithTag("BilloSPS").GetComponent<BilloSPS>();

        sps.myDataCollector.WriteEventData(this.name, new double[] { this.transform.position.x, this.transform.position.y, this.transform.position.z },
         this, new List<string> { "XPos" , "YPos" , "ZPos" } );
        //}
        /******************************************************************************
        //Test
        if(Input.GetKeyDown(KeyCode.Z))
        {
            ChangeType(0);
        }
        */
    }
    public void ChangeType(int newP)
    {
        Destroy(this.gameObject.transform.GetChild(0).gameObject);
        if(newP == 0)
        {
            Vector3 MyVector;
            MyVector = new Vector3(0.0f, 0.0f, 0.0f);
            currentProduct = Product.Product_raw;
            GameObject newchild = Instantiate(Body[0], MyVector, this.gameObject.transform.rotation, this.gameObject.transform );
            newchild.transform.localPosition = MyVector;
        }else if(newP == 1)
        {
            Vector3 MyVector;
            MyVector = new Vector3(0.2f, -0.155f, -0.15f);
            currentProduct = Product.Product_cnc;
            GameObject newchild = Instantiate(Body[1], MyVector, this.gameObject.transform.rotation, this.gameObject.transform );
            newchild.transform.localPosition = MyVector;
        }else if(newP == 2)
        {
            Vector3 MyVector;
            MyVector = new Vector3(0.2f, -0.15f, -0.15f);
            currentProduct = Product.Product_drill;
            GameObject newchild = Instantiate(Body[2], MyVector, this.gameObject.transform.rotation, this.gameObject.transform );
            newchild.transform.localPosition = MyVector;
        }else if(newP == 3)
        {
            Vector3 MyVector;
            MyVector = new Vector3(0.2f, -0.155f, -0.15f);
            currentProduct = Product.Product_cnc_drill;
            GameObject newchild = Instantiate(Body[3], MyVector, this.gameObject.transform.rotation, this.gameObject.transform );
            newchild.transform.localPosition = MyVector;
        }
        {
            Debug.Log("Wrong imput for <changetype()>");
        }
    }

}
