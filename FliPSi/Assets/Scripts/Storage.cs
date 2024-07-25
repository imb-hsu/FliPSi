using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This class controlls the actual store, saving information about color, type and position of the stored assets
 * 
 */
public class Storage : Module
{

    public struct StoredInformation
    {
        public Color color;
        public ProductList.Product productType;
        public bool occupied;
    }


    public StoredInformation[,] StoredObjects = new StoredInformation[3, 3];


}
