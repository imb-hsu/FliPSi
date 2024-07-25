using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : Module
{
    public bool FrontSensor = false;
    public bool BackSensor = true;
    public Transform rotatingPart;
    public Transform magnetPoint;
    
    protected StorageModule storageModule;

    public enum movingState {
        stationary,
        toPickup,
        toCenterWhileLoaded,
        toStorageCell,
        toCenterWhileEmpty
    }
    public movingState ms = movingState.stationary;
    public float targetCellXCood;
    public float targetCellZCood;
    public GameObject spottedProduct;



    // incomming product
    private GameObject target = null;

    private void OnTriggerStay(Collider other)
    {    // detect product 
        if (other.tag == "Component" || other.tag == "Product_raw" || other.tag == "Produt_cnc" || other.tag == "Product_cncdrill" || other.tag == "Product_drill") { target = other.gameObject; }
    }



    // Start movement to Product-Entrance and lift the object
    public void lift(GameObject spottedProduct)
    {


    }

    public void release(GameObject spottedProduct )
    {
        if (spottedProduct != null)
        {
            spottedProduct.transform.SetParent(null, true);
            print($"Disconnect {spottedProduct.name}");
            spottedProduct = null;
        }
    }


    public void moveMagnetPoint(float x, float z)
    {
        Vector3 newPosition = magnetPoint.localPosition;
        newPosition.x += x;
        newPosition.z += z;
        magnetPoint.localPosition = newPosition;
    }

    //counting up current movement, static variable defined outside the loop
    float totalRotation = 0;
    public void Update()
    {
        //Small state machine to encode the different movement stages
        switch (ms) {
            case movingState.stationary:
                break;
            case movingState.toPickup:
                totalRotation += Time.deltaTime * 180f;
                rotatingPart.Rotate(0f, Time.deltaTime * 180f, 0f);

                if (spottedProduct != null && totalRotation >= 90f)
                {
                    spottedProduct.transform.SetParent(magnetPoint.transform, true);
                    print($"Connect This {this.name} with {spottedProduct.name}");
                    ms = movingState.toCenterWhileLoaded;
                    totalRotation = 0;
                }
                break;
            case movingState.toCenterWhileLoaded:
                totalRotation -= Time.deltaTime * 180f;
                rotatingPart.Rotate(0f, -Time.deltaTime * 180f, 0f);
                if (totalRotation <= -90f) {
                    print("ToStorageCell");
                    ms = movingState.toStorageCell;
                    totalRotation = 0;
                }
                break;
            case movingState.toStorageCell:
                moveMagnetPoint(targetCellXCood, targetCellZCood);
                this.release(spottedProduct);
                ms = movingState.toCenterWhileEmpty;
                break;
            case movingState.toCenterWhileEmpty:
                moveMagnetPoint(-targetCellXCood, -targetCellZCood);
                ms = movingState.stationary;
                break;

        }
    }
   

}