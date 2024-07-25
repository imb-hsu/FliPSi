using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementComponent : MonoBehaviour
{
    //public int MyType; 
    /*
     * 1 = conveyor
     * 2 = turntable
     * 3 = sensor
     * 4 = colorsensor
     * 5 = barrier
     * 6 = conveyorstorage
     * we use int to determine the Type of ElementComponent because enum somehow didnt work :( 
     * Is used in BilloSPS.Add() to sort the ElementComponents into corresponding Lists
     */
    public BilloSPS sps;
    public virtual void Start()
    {
        sps = GameObject.FindGameObjectWithTag("BilloSPS").GetComponent<BilloSPS>();
        //this.AddItems();
    }
    public abstract void AddItems();

    public virtual void RemoveItems()
    {
        sps.Remove(this);
    }
    public virtual void MotorForward()
    {

    }
    public virtual void MotorBackward()
    {

    }
    public virtual void MotorOff()
    {

    }
}
