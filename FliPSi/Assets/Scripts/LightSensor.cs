using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSensor : MonoBehaviour
{
    public bool value = true;
    public ElementComponent MyParent;
    public ElementComponent Prev;
    public ElementComponent Next;
    public Barrier barrier;
    public bool front;

    public IEnumerator  DelayFunction(float delay)
    {
        // Yield for x seconds
        yield return new WaitForSeconds(delay);
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entering LS");
        StartCoroutine(DelayFunction(0.3f));
        MyParent.MotorOff();
        Prev.MotorOff();
        value = false;
        StartCoroutine(DelayFunction(0.2f));
        //MyParent.MotorForward();
        //Next.MotorForward();
        if(front)
        {
            barrier.Open();
        }
        if(!front)
        {
            barrier.Close();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        value = true;
    }
    public void OnTriggerStay(Collider other)
    {
        if(barrier.LeftWingClosed == 1 && barrier.RightWingClosed == 1 && !front)
        {
            MyParent.MotorForward();
            Next.MotorForward();
        }
    }
}
