using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
 *This class is responsible for collecting all required data from the modules of the simulation.
 *
 *The data can then be stored in an external file.
 *
 *
 */

public class DataCollector : MonoBehaviour
{

    //Data points per second
    public const double DataPointsPerSecond = 10;

    //Collection of all the data
    public List<DataCollection> AllData;

    //Counter for FixedUpdate
    private int countCallsWithoutCollection = 0;

    //Running
    private bool dataCollectionRunning = false;

    //Interval for data collection, int for simplicity
    int collectionInterval = 0;

    //BilloSPS
    protected BilloSPS sps;


    #region functions
    public void StartDataCollection()
    /*
     * This function initializes the List of DataCollections and sets the boolean dataCollectionRunning.
     * 
     * In order to initialize the DataCollections it goes through the SPS Member Variables and searches 
     * for ModuleLists with the ExportableModule Attribute. It then looks inside those ModuleLists and 
     * looks up each module. It creates a DataCollection for each module. The Fieldnames of the 
     * DataCollection are the properties of the modules that were marked with the Exportable attribute.
     * 
     * 
     */
    {
        //Initialize DataCollection List

        AllData = new List<DataCollection>();

        //Find Modules in the SPS that should be exportable
        Type spsType = typeof(BilloSPS);
        System.Reflection.FieldInfo[] SpsFields = spsType.GetFields();
        List<String> ExpModules = new List<string>();
        for (int i= 0; i < SpsFields.Length; i++)
        {
            var hasExportableModule = Attribute.IsDefined(SpsFields[i], typeof(ExportableModule));
            //Check if the variable has the attribute exportable
            if (!hasExportableModule) { continue; };
            ExpModules.Add(SpsFields[i].Name);
        }

        //Go Through all Exportable Modules
        foreach (String ModuleName in ExpModules) 
        {
            //Create a List of the Type of the Module
            Type type = sps.GetType().GetField(ModuleName).GetValue(sps).GetType();
            IList ModuleList = (IList)Activator.CreateInstance(type);
            ModuleList = (System.Collections.IList)sps.GetType().GetField(ModuleName).GetValue(sps);

            //Go Through all Modules
            foreach (MonoBehaviour Module in ModuleList) 
            {
                Type mdType = Module.GetType() ;
                System.Reflection.FieldInfo[] mdFields = mdType.GetFields();
                List<String> moduleExpVariables = new List<string>();

                //Get all exportable Properties of the Module
                for (int i = 0; i < mdFields.Length; i++)
                {
                    
                    var hasExportable = Attribute.IsDefined(mdFields[i], typeof(Exportable));

                    //Check if the variable has the attribute exportable
                    if (!hasExportable) { continue; };
                    moduleExpVariables.Add(mdFields[i].Name);
                }

                //Add new DataCollection
                AllData.Add(new DataCollection(Module.name, moduleExpVariables, Module));
            }
        }
        dataCollectionRunning = true;

    }

    // Stops Data Collection and saves Data
    public void StopDataCollection()
    {
        dataCollectionRunning = false;

        //Get the path of the Game data folder
        string m_Path = Application.dataPath;

        //Output the Game data path to the console
        Debug.Log("dataPath : " + m_Path);

        foreach (DataCollection ds in AllData)
        {
            string Path = Application.dataPath + "/Daten/"+ Convert.ToString(ds.ModuleName) +".csv";
            StreamWriter writer = new StreamWriter(Path);
            string header ="Time;";
            foreach (string s in ds.FieldNames)
            {
                header = header + s + ";";
            }
            Debug.Log(header);
            writer.WriteLine(header);
            
            foreach (double[] dd in ds.Data)
             {
                String record = ""; 
               for(int i =0; i<dd.Length; i++)
                {
                    //Debug.Log("Data " + i + "  " + dd[i]);
                    record = record + Convert.ToString(dd[i]) + ";";
                }
                writer.WriteLine(record);
            }
            writer.Flush();
            //This closes the file
            writer.Close();
        }
        
        }

    // Function to Write Data, called by external Modules. Used for Events and non Module Objects
    // 
    public void WriteEventData(String NameOfModule, double[] values, MonoBehaviour myObj = default(MonoBehaviour), List<String> fieldNames = default (List<String>)) 
    {
        if (!dataCollectionRunning) { return; }

        int iModule = -1; //Initialize to impossible Index
        //Get Index of Module, by Name
        for (int i = 0; i < AllData.Count; i++)
        {
            if (NameOfModule == AllData[i].ModuleName)
            {
                iModule = i;
            }
        }

        //If it does not exist, create it
        if (iModule == -1) 
        {
            if (myObj == default(MonoBehaviour) || fieldNames == default(List<String>))
            {
                throw new ArgumentException(message: "You have to specify Field Names and ObjectRef if the Module is not already known!");
            }

            iModule = AllData.Count;
            //Module gets added. It gets added with Async true, so that new data Points will only be added by WriteEventData .
            AllData.Add(new DataCollection(NameOfModule, fieldNames, myObj, true) );
        }

        //Append double values, plus time
        double[] valuesForCollection = new double[values.Length + 1];
        valuesForCollection[0] = Time.time;
        System.Array.Copy(values, 0, valuesForCollection, 1, values.Length);
        AllData[iModule].appendData(valuesForCollection);
    }

    //Function to store the data to AllData Object
    private void CollectData()
    {
        //Go through the previously initialized AllData List
        foreach (DataCollection ds in AllData) 
        {
            //Do not collect for Async entries
            if (ds.IsAsync) { continue; }

            MonoBehaviour myObj = ds.ObjRef;
            double[] values = new double[ds.FieldNames.Count +1];

            //first value is always the time
            values[0] = Convert.ToDouble(Time.time);
            int i = 1;
            foreach (string fn in ds.FieldNames) 
            {
                //Find indiviual values and convert them to double
                values[i] = Convert.ToDouble(myObj.GetType().GetField(fn).GetValue(myObj));
                i++;
            }
            ds.appendData(values);
        }

    }

    #endregion
    #region UnityFunctions
    private void Start()
    {
        //Set interval
        collectionInterval = Convert.ToInt32( 1 / (DataPointsPerSecond * Time.fixedDeltaTime) );

        //Fugly Workaround Cause Unity
        sps = GameObject.FindGameObjectWithTag("BilloSPS").GetComponent<BilloSPS>();
    }

    //Using fixed update to collect data since its independent of the rendering speed
    private void FixedUpdate()
    {
        //Check if data Collection is running
        if (!dataCollectionRunning) return;

        //Check for Collection interval
        //if( countCallsWithoutCollection % collectionInterval == 0)
        //{
            countCallsWithoutCollection = 0;

            CollectData();
        //}
        countCallsWithoutCollection++;
    }
    #endregion
}
