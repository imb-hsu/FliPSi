using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : ElementComponent
{

    public List<ElementComponent> MyComponents = new List<ElementComponent>();
    public override void Start() 
    {
        //Debug.Log(this);
        //Find BilloSPS, Add Module itself AND all components of the Module
        sps = GameObject.FindGameObjectWithTag("BilloSPS").GetComponent<BilloSPS>();
        sps.Add(this);
        this.AddItems();
    }
    public override void AddItems() 
    {
        //Debug.Log("Module Adds Items" + this);
        //connect to BilliSPS
        sps = GameObject.FindGameObjectWithTag("BilloSPS").GetComponent<BilloSPS>();
        //sps.Add(this);
        foreach (ElementComponent EC in MyComponents)
        {   if(!(EC is Module))
            {
                EC.AddItems();
            } else if(EC is Module)
            {
                Module temp = EC as Module;
                temp.AddItems();
            }else{
                Debug.Log("we are fucked"); 
            }
        }

    }
    public override void RemoveItems()
    {
        sps.Remove(this);
        foreach (ElementComponent EC in MyComponents)
        {
            EC.RemoveItems();
        }
    }
    #region find stuff in list
    public Conveyor findConveyor()
    {
        foreach (ElementComponent EC in MyComponents)
        {
            if(EC is Conveyor)
            {
                return EC as Conveyor;
            }
        }
        return null;
    }
    public Turntable findTurntable()
    {
        foreach (ElementComponent EC in MyComponents)
        {
            if (EC is Turntable)
            {
                return EC as Turntable;
            }
        }
        return null;
    }
    public Sensor findSensor()
    {
        foreach (ElementComponent EC in MyComponents)
        {
            if (EC is Sensor)
            {
                return EC as Sensor;
            }
        }
        return null;
    }
    public Colorsensor findColorsensor()
    {
        foreach (ElementComponent EC in MyComponents)
        {
            if (EC is Colorsensor)
            {
                return EC as Colorsensor;
            }
        }
        return null;
    }
    public Barrier findBarrier()
    {
        foreach (ElementComponent EC in MyComponents)
        {
            if (EC is Barrier)
            {
                return EC as Barrier;
            }
        }
        return null;
    }
    #endregion
    #region override from ElementComponent
    public override void MotorForward()
    {
        //
    }
    public override void MotorBackward()
    {
        //
    }
    public override void MotorOff()
    {
        //
    }
    #endregion


}
