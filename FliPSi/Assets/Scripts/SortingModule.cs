using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This script initializes the components of the sorting module, and controls their behaviour
 * 
 * It is used to control the underlying functions
 * 
 * 
 */


public class SortingModule : Module
{

    private Color lastSeenColor = Color.white;
    private bool debounce = false;
    //controlls the physics of turntable while sorting (for sortingAction() && turnBack() ) 
    void sortingAction()
    {
        
        lastSeenColor = findColorsensor().objectColor;
        float rotation = 0.0f;

        if(lastSeenColor == Color.red) { rotation = 90.0f; };
        if (lastSeenColor == Color.green) { rotation = -90.0f; };

        findTurntable().Grab(true);
        findTurntable().TurnByDegree(rotation);
        findTurntable().StartCoroutine(findTurntable().releaseAfterDelay(5));
        
    }

    void turnBack()
    {
                
        float rotation = 0.0f;

        if (lastSeenColor == Color.red) { rotation = -90.0f; };
        if (lastSeenColor == Color.green) { rotation = +90.0f; };

        StartCoroutine(findTurntable().callTurnByDegreeWithDelay(rotation, 10));
        
    }


    // Update is called once per frame
    void Update()
    {
        //Check if the sensors were fired
        if (findTurntable().BackSensor && !debounce)
        {
            sortingAction();
            debounce = true;
        }
        else if (findTurntable().BackSensor && debounce)
        {
            ; 
        }
        else if (!findTurntable().BackSensor && debounce)
        {
            turnBack();
            debounce = false;
        }
        else if (!findTurntable().BackSensor && !debounce)
        {
            ;
        }


        //Debounce

    }
}
