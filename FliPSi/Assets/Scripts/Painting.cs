using UnityEngine;
using System;


/* This is the main operating script for the painting-machine elementary module
 * The painting operation RANDOMLY changes the color of the incoming product [target] into one of the options defined in Color[]
 * 
 * All other functionalities are inherited from CNC
 * 
 */

public class Painting : CNC
{   
    /********************************************************************************************************************************************/
    // color list
    public Color[] color = { Color.red, Color.green };

    public override void Update()
    {
        base.Update();
    }
    public override void executeCNC(GameObject go)
    {   
        // random value for random color selection from list
        System.Random random = new System.Random();
        int rndIndex = random.Next(color.Length);

        // paint part randomly, including children
        target = go;
        
        target.GetComponentInChildren<Renderer>().material.color = color[rndIndex];
        foreach (Renderer childrenRenderer in target.GetComponentsInChildren<Renderer>())
        {
            childrenRenderer.material.color = color[rndIndex];
        }
        target.GetComponent<Renderer>().material.color = color[rndIndex];
        //delete for F3
        //Destroy(go);
    }
    
}
