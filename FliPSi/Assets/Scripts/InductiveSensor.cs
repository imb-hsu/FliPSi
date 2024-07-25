using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InductiveSensor : MonoBehaviour
{   public bool value = false;
    public ElementComponent MyParent;
    public ElementComponent Prev;
    public ElementComponent Next;
    public bool left;
    public bool right;
    public bool straight;

    public IEnumerator  DelayFunction(float delay)
    {
        // Yield for x seconds
        yield return new WaitForSeconds(delay);
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EnterCollider");
        MyParent.MotorOff();
        Prev.MotorOff();
        value = true;
        StartCoroutine(DelayFunction(0.2f));
        MyParent.MotorForward();
        if(Next != null)
        {Next.MotorForward();}else{
            Debug.Log("Failure to continue; Open-end in factory");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        //Debug.Log("ExitCollider");
        if(Next == null)
        {
            MyParent.MotorOff();
        }
        value = false;
    }
}
