using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CNC_Door : MonoBehaviour
{
    /********************************************************************************************************************************************/
    // Imports and initialisation
    private Vector3 getUp;
    private Vector3 getDown;
    public bool open = false;
    public bool close = true;
    public CNC parent;
    public bool front;

    /********************************************************************************************************************************************/
    // functions

    private void Awake()
    {   // setting up the movement vector
        getUp = new Vector3(transform.position.x, 2.145f, transform.position.z);
        getDown = new Vector3(transform.position.x, 1.245f, transform.position.z);
    }

    public void OpenDoor()
    {    // called by CNC.cs
        open = true;
        close = false;
        if(front)
        {
            parent.hasObject = true;
        }
    }

    public void CloseDoor()
    {   // called by CNC.cs
        open = false;
        close = true;
        if(!front)
        {
            parent.hasObject = false;
        }
    }

    void Update()
    {   // operating logic (smooth opening and closing of the door)
        if (open && transform.position != getUp)
        {
            if(front)
            {
                parent.machineVorne = 0;
                parent.machineHinten = 0;
                parent.MachineVor = 1;
                parent.MachineZurück = 0;
            }

            
            transform.position = Vector3.MoveTowards(transform.position, getUp, Time.deltaTime * 0.5f);

        }else if (close && transform.position != getDown)
        {   
            if(!front)
            {
                parent.machineVorne = 0;
                parent.machineHinten = 0;
                parent.MachineVor = 0;
                parent.MachineZurück = 1;
            }
            transform.position = Vector3.MoveTowards(transform.position, getDown, Time.deltaTime * 0.5f);
        }
        if(transform.position == getUp)
        {
            if(front)
            {
                parent.MachineVor = 0;
                parent.MachineZurück = 0;
                parent.machineVorne = 1;
                parent.machineHinten = 0;
            }

        }
        if(transform.position == getDown)
        {
            if(!front && !parent.hasObject)
            {
                parent.MachineVor = 0;
                parent.MachineZurück = 0;
                parent.machineVorne = 0;
                parent.machineHinten = 1;
            }
        }

    }
}
