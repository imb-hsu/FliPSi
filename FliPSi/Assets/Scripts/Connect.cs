using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect : MonoBehaviour
{
    public bool front;
    public InductiveSensor MySensor;
    public LightSensor LS;
    public bool left;
    public bool right;
    public ElementComponent EC;
    public Collider _collider;

    public void ConnectEC(Collider other)
    {
        EC = null;
        if(other.GetComponentInParent<Conveyor>() != null)
        {
            EC = other.GetComponentInParent<Conveyor>() as ElementComponent;
        }else if (other.GetComponentInParent<Turntable>() != null)
        {
            EC = other.GetComponentInParent<Turntable>() as ElementComponent;
        }else if (other.GetComponentInParent<CNC>() != null)
        {
            //Failsafe to push product towards unknown CNC
            EC = other.GetComponentInParent<CNC>() as ElementComponent;
        }
        if (EC != null)
        {
            if(front)
            {
                if(MySensor != null)
                {
                    MySensor.Next = EC;
                }
                else if(LS != null)
                {
                    LS.Next = EC;
                }
            }else if (!front && left)
            {
                MySensor.Next = EC;
                MySensor.left = true;
            }else if (!front && right)
            {
                MySensor.Next = EC;
                MySensor.right = true;
            }
            else if(!front && !left && !right)
            {
                if(MySensor != null)
                {
                    MySensor.Prev = EC;
                }
                else if(LS != null)
                {
                    LS.Prev = EC;
                }
            }
        }
    }
    public void OnTriggerStay(Collider other)
    {
        _collider = other;
        ConnectEC(other);
    }
}
