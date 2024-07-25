using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attached to the planning squares of the modules and steers the movement of the planning squares. 
 * Additionally it creates the objects in on the squares' position.
 * 
 * 
 * 
 */ 

public class CreateObject : MonoBehaviour
{
    public GameObject Objekt;
    private float range = 0.1f;
    private float angle = 90f;
    private float amp = 10;
    int counter = 0;

    private void Update()
    {

        // move with up/down/left/right ; increase movement by 10 with left shift
        if (Input.GetKeyDown("up") & !(Input.GetKey("left ctrl") | Input.GetKey("right ctrl")))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                transform.position = transform.position + new Vector3(0, 0, amp * range);
            }else
            {
                transform.position = transform.position + new Vector3(0, 0, range);
            }
        }
        


        if (Input.GetKeyDown("down") & !(Input.GetKey("left ctrl") | Input.GetKey("right ctrl")))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                transform.position = transform.position + new Vector3(0, 0, -amp * range);
            }else
            {
                transform.position = transform.position + new Vector3(0, 0, -range);
            }
        }

        if (Input.GetKeyDown("right") & !(Input.GetKey("left ctrl") | Input.GetKey("right ctrl")))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                transform.position = transform.position + new Vector3(amp * range, 0, 0);
            }else
            {
                transform.position = transform.position + new Vector3(range, 0, 0);
            }
        }

        if (Input.GetKeyDown("left") & !(Input.GetKey("left ctrl") | Input.GetKey("right ctrl")))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                transform.position = transform.position + new Vector3(-amp * range, 0, 0);
            }else
            {
                transform.position = transform.position + new Vector3(-range, 0, 0);
            }
        }

        // turn with ctr + up/right and ctr + down/left
        if ((Input.GetKey("left ctrl") | Input.GetKey("right ctrl")) & (Input.GetKeyDown("right") | Input.GetKeyDown("up")))
        {
            transform.Rotate(0, angle, 0);
        }

        if ((Input.GetKey("left ctrl") | Input.GetKey("right ctrl")) & (Input.GetKeyDown("left") | Input.GetKeyDown("down")))
        {
            transform.Rotate(0, -angle, 0);
        }

        // create module on pressing "enter" 
        if (Input.GetKeyDown("return") | Input.GetKeyDown("enter"))
        {
            GameObject go = Instantiate(Objekt, transform.position - new Vector3(0, 0.001f, 0), this.transform.rotation);
            go.name = go.tag + counter;
            counter++;
            gameObject.SetActive(false);
        }

        // "escape" for killing the process
        if (Input.GetKey("escape"))
        {
            gameObject.SetActive(false);
        }
    }
}
