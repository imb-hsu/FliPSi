using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *Class to represent the colleted data of one module of the simulation
 *
 *
 */

public class DataCollection 
{
    public string ModuleName { get; set; }
    public List<string> FieldNames { get; set; }
    public MonoBehaviour ObjRef;
    public bool IsAsync;
    

    //The actual data
    public List<double[]> Data;

    //Append only take double arrays, avoid issues while storing
    public void appendData(double[] newData) 
    {
        Data.Add(newData);
    }

    //Getter for Data
    List<double[]> getData()
    {
        return Data;
    }


    //Constructor
    public DataCollection(string Name, List<string> FieldNames, MonoBehaviour ObjRef, bool IsAsync = false) 
    {
        this.ModuleName = Name;
        this.FieldNames = FieldNames;
        this.ObjRef = ObjRef;
        this.IsAsync = IsAsync;

        Data = new List<double[]>();

    }

}
