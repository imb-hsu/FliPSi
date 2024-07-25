using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Globalization;

/*
 * This script is for saving and loading factory configurations in and from xml-files
 * 
 * 
 */ 

public class SaveAndLoad : MonoBehaviour
{
    // all savable GameObjects
    public GameObject CNC;
    public GameObject Turntable;
    public GameObject Conveyor;
    public GameObject Conveyor_verticalMerge;   
    public GameObject Conveyor_horizontalMerge; 
    public GameObject Kuka;
    public GameObject Sensor;
    public GameObject Colorsensor;
    public GameObject Input;
    public GameObject Output;
    public GameObject Barrier;
    public GameObject Painting;
    public GameObject InputModule;
    public GameObject SortingModule;
    public GameObject MillingModule;
    public GameObject DrillingModule;
    public GameObject PaintingModule;
    public GameObject OutputModule;
    public GameObject StorageModule;
    public GameObject AssemblyModule;



    public void SaveAsXML(string filename)
    {
        // Saves factory to XML

        CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
        XmlDocument xmlDocument = new XmlDocument();
        XmlNode xmlRoot, xmlNode, xmlNode2;
        XmlAttribute xmlAttribute;

        xmlRoot = xmlDocument.CreateElement("FactorySimulator");
        xmlAttribute = xmlDocument.CreateAttribute("Name");
        xmlAttribute.Value = filename;
        xmlRoot.Attributes.Append(xmlAttribute);
        xmlAttribute = xmlDocument.CreateAttribute("Date");
        xmlAttribute.Value = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        xmlRoot.Attributes.Append(xmlAttribute);
        xmlDocument.AppendChild(xmlRoot);

        GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
        Camera cam = camObj.GetComponent<Camera>();
        xmlNode = xmlDocument.CreateElement("Camera");
        xmlAttribute = xmlDocument.CreateAttribute("Size");
        xmlAttribute.Value = cam.orthographicSize.ToString();
        xmlNode.Attributes.Append(xmlAttribute);
        xmlRoot.AppendChild(xmlNode);

        GameObject floor = GameObject.FindGameObjectWithTag("Floor"); 
        xmlNode = xmlDocument.CreateElement("Floor");
        xmlAttribute = xmlDocument.CreateAttribute("SizeX");
        xmlAttribute.Value = floor.transform.localScale.x.ToString();
        xmlNode.Attributes.Append(xmlAttribute);
        xmlAttribute = xmlDocument.CreateAttribute("SizeZ");
        xmlAttribute.Value = floor.transform.localScale.z.ToString();
        xmlNode.Attributes.Append(xmlAttribute);
        xmlRoot.AppendChild(xmlNode);

        List<GameObject> allGos = new List<GameObject>();
        GameObject[] cncList = GameObject.FindGameObjectsWithTag("CNC");
        allGos.AddRange(cncList);
        GameObject[] turntableList = GameObject.FindGameObjectsWithTag("Turnplate");
        allGos.AddRange(turntableList);
        GameObject[] conveyorList = GameObject.FindGameObjectsWithTag("Conveyor");
        allGos.AddRange(conveyorList);
        GameObject[] conveyorVerticalMergeList = GameObject.FindGameObjectsWithTag("Conveyor_verticalMerge");
        allGos.AddRange(conveyorVerticalMergeList);
        GameObject[] conveyorHorizontalMergeList = GameObject.FindGameObjectsWithTag("Conveyor_horizontalMerge");
        allGos.AddRange(conveyorHorizontalMergeList);
        GameObject[] AssemblyModuleList = GameObject.FindGameObjectsWithTag("AssemblyModule");
        allGos.AddRange(AssemblyModuleList);
        GameObject[] sensorList = GameObject.FindGameObjectsWithTag("Sensor");
        allGos.AddRange(sensorList);
        GameObject[] farbsensorList = GameObject.FindGameObjectsWithTag("Colorsensor");
        allGos.AddRange(farbsensorList);
        GameObject[] inputList = GameObject.FindGameObjectsWithTag("Input");
        allGos.AddRange(inputList);
        GameObject[] outputList = GameObject.FindGameObjectsWithTag("Output");
        allGos.AddRange(outputList);
        GameObject[] barrierList = GameObject.FindGameObjectsWithTag("Barrier");
        allGos.AddRange(barrierList);
        GameObject[] paintingList = GameObject.FindGameObjectsWithTag("Painting");
        allGos.AddRange(paintingList);
        GameObject[] inputModuleList = GameObject.FindGameObjectsWithTag("InputModule");
        allGos.AddRange(inputModuleList);
        GameObject[] outputModuleList = GameObject.FindGameObjectsWithTag("OutputModule");
        allGos.AddRange(outputModuleList);
        GameObject[] sortingModuleList = GameObject.FindGameObjectsWithTag("SortingModule");
        allGos.AddRange(sortingModuleList);
        GameObject[] drillingModuleList = GameObject.FindGameObjectsWithTag("DrillingModule");
        allGos.AddRange(drillingModuleList);
        GameObject[] paintingModuleList = GameObject.FindGameObjectsWithTag("PaintingModule");
        allGos.AddRange(paintingModuleList);
        GameObject[] millingModuleList = GameObject.FindGameObjectsWithTag("MillingModule");
        allGos.AddRange(millingModuleList);
        //GameObject[] storageModuleList = GameObject.FindGameObjectsWithTag("StorageModule");
        //allGos.AddRange(storageModuleList);



        foreach (GameObject go in allGos)
        {
            //Check if the object has a parent. If so, it is part of module which is saved saperatly.
            if(go.transform.parent != null) { continue; };

            xmlNode = xmlDocument.CreateElement("Object");
            xmlAttribute = xmlDocument.CreateAttribute("Name");
            xmlAttribute.Value = go.name;
            xmlNode.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument.CreateAttribute("Tag");
            xmlAttribute.Value = go.tag;
            xmlNode.Attributes.Append(xmlAttribute);
            xmlNode2 = xmlDocument.CreateElement("Position");
            xmlAttribute = xmlDocument.CreateAttribute("x");
            xmlAttribute.Value = go.GetComponent<Transform>().position.x.ToString();
            xmlNode2.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument.CreateAttribute("y");
            xmlAttribute.Value = go.GetComponent<Transform>().position.y.ToString();
            xmlNode2.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument.CreateAttribute("z");
            xmlAttribute.Value = go.GetComponent<Transform>().position.z.ToString();
            xmlNode2.Attributes.Append(xmlAttribute);
            xmlNode.AppendChild(xmlNode2);

            xmlNode2 = xmlDocument.CreateElement("Rotation");
            xmlAttribute = xmlDocument.CreateAttribute("w");
            xmlAttribute.Value = go.GetComponent<Transform>().rotation.w.ToString();
            xmlNode2.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument.CreateAttribute("x");
            xmlAttribute.Value = go.GetComponent<Transform>().rotation.x.ToString();
            xmlNode2.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument.CreateAttribute("y");
            xmlAttribute.Value = go.GetComponent<Transform>().rotation.y.ToString();
            xmlNode2.Attributes.Append(xmlAttribute);
            xmlAttribute = xmlDocument.CreateAttribute("z");
            xmlAttribute.Value = go.GetComponent<Transform>().rotation.z.ToString();
            xmlNode2.Attributes.Append(xmlAttribute);
            xmlNode.AppendChild(xmlNode2);

            switch (go.tag)
            {
                case "CNC":
                    CNC cncScript = go.GetComponent<CNC>();
                    xmlNode2 = xmlDocument.CreateElement("Configuration");
                    xmlAttribute = xmlDocument.CreateAttribute("targetVelocity");
                    xmlAttribute.Value = cncScript.targetVelocity.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("force");
                    xmlAttribute.Value = cncScript.force.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("freeSpin");
                    xmlAttribute.Value = cncScript.freeSpin.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlNode.AppendChild(xmlNode2);
                    break;

                case "Turntable":
                    Turntable turntableScript = go.GetComponent<Turntable>();
                    xmlNode2 = xmlDocument.CreateElement("Configuration");
                    xmlAttribute = xmlDocument.CreateAttribute("targetVelocity");
                    xmlAttribute.Value = turntableScript.targetVelocity.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("isRightTurn");
                    xmlAttribute.Value = turntableScript.isRightTurn.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("isLeftTurn");
                    xmlAttribute.Value = turntableScript.isLeftTurn.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("force");
                    xmlAttribute.Value = turntableScript.force.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("freeSpin");
                    xmlAttribute.Value = turntableScript.freeSpin.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("turningSpeed");
                    xmlAttribute.Value = turntableScript.turningSpeed.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlNode.AppendChild(xmlNode2);
                    break;

                case "Conveyor":
                    Conveyor conveyorScript = go.GetComponent<Conveyor>();
                    xmlNode2 = xmlDocument.CreateElement("Configuration");
                    xmlAttribute = xmlDocument.CreateAttribute("targetVelocity");
                    xmlAttribute.Value = conveyorScript.targetVelocity.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("force");
                    xmlAttribute.Value = conveyorScript.force.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("freeSpin");
                    xmlAttribute.Value = conveyorScript.freeSpin.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlNode.AppendChild(xmlNode2);
                    break;

                case "Conveyor_verticalMerge":
                    Conveyor conveyorVerticalMergeScript = go.GetComponent<Conveyor>();
                    xmlNode2 = xmlDocument.CreateElement("Configuration");
                    xmlAttribute = xmlDocument.CreateAttribute("targetVelocity");
                    xmlAttribute.Value = conveyorVerticalMergeScript.targetVelocity.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("force");
                    xmlAttribute.Value = conveyorVerticalMergeScript.force.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("freeSpin");
                    xmlAttribute.Value = conveyorVerticalMergeScript.freeSpin.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlNode.AppendChild(xmlNode2);
                    break;

                case "Conveyor_horizontalMerge":
                    Conveyor conveyorHorizontalMergeScript = go.GetComponent<Conveyor>();//
                    xmlNode2 = xmlDocument.CreateElement("Configuration");
                    xmlAttribute = xmlDocument.CreateAttribute("targetVelocity");
                    xmlAttribute.Value = conveyorHorizontalMergeScript.targetVelocity.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("force");
                    xmlAttribute.Value = conveyorHorizontalMergeScript.force.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("freeSpin");
                    xmlAttribute.Value = conveyorHorizontalMergeScript.freeSpin.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlNode.AppendChild(xmlNode2);
                    break;

                case "Input":
                    InputCubes inputScript = go.GetComponent<InputCubes>();
                    xmlNode2 = xmlDocument.CreateElement("Configuration");
                    xmlNode.AppendChild(xmlNode2);
                    break;

                case "Painting":
                    Painting paintingScript = go.GetComponent<Painting>();
                    xmlNode2 = xmlDocument.CreateElement("Configuration");
                    xmlAttribute = xmlDocument.CreateAttribute("targetVelocity");
                    xmlAttribute.Value = paintingScript.targetVelocity.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("force");
                    xmlAttribute.Value = paintingScript.force.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlAttribute = xmlDocument.CreateAttribute("freeSpin");
                    xmlAttribute.Value = paintingScript.freeSpin.ToString();
                    xmlNode2.Attributes.Append(xmlAttribute);
                    xmlNode.AppendChild(xmlNode2);
                    break;

                default:
                    break;
            }
            xmlRoot.AppendChild(xmlNode);
        }
        xmlDocument.Save(filename + ".xml");
    }

   
    public void LoadFromXML(string filename)
    { 
        // Loads factory from XML

        CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

        // find all objects and delete from current session, before loading
        List<GameObject> allGos = new List<GameObject>();
        GameObject[] cncList = GameObject.FindGameObjectsWithTag("CNC");
        allGos.AddRange(cncList);
        GameObject[] turntableList = GameObject.FindGameObjectsWithTag("Turnplate");
        allGos.AddRange(turntableList);
        GameObject[] conveyorList = GameObject.FindGameObjectsWithTag("Conveyor");
        allGos.AddRange(conveyorList);
        GameObject[] AssemblyModuleList = GameObject.FindGameObjectsWithTag("AssemblyModule");
        allGos.AddRange(AssemblyModuleList);
        GameObject[] conveyorVerticalMergeList = GameObject.FindGameObjectsWithTag("Conveyor_verticalMerge");
        allGos.AddRange(conveyorVerticalMergeList);
        GameObject[] conveyorHorizontalMergeList = GameObject.FindGameObjectsWithTag("Conveyor_horizontalMerge");
        allGos.AddRange(conveyorHorizontalMergeList);
        //GameObject[] kukaList = GameObject.FindGameObjectsWithTag("Kuka");
        //allGos.AddRange(kukaList);
        GameObject[] sensorList = GameObject.FindGameObjectsWithTag("Sensor");
        allGos.AddRange(sensorList);
        GameObject[] colorsensorList = GameObject.FindGameObjectsWithTag("Colorsensor");
        allGos.AddRange(colorsensorList);
        GameObject[] inputList = GameObject.FindGameObjectsWithTag("Input");
        allGos.AddRange(inputList);
        GameObject[] outputList = GameObject.FindGameObjectsWithTag("Output");
        allGos.AddRange(outputList);
        GameObject[] barrierList = GameObject.FindGameObjectsWithTag("Barrier");
        allGos.AddRange(barrierList);
        GameObject[] paintingList = GameObject.FindGameObjectsWithTag("Painting");
        allGos.AddRange(paintingList);

        allGos.ForEach(Destroy);
        allGos.Clear();

        // Open File and read loading data
        XmlDocument xmldocument = new XmlDocument();
        xmldocument.Load(filename + ".xml");
        foreach (XmlNode xmlNode in xmldocument.DocumentElement.ChildNodes)
        {
            if (xmlNode.Name == "Camera")
            {
                GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
                Camera cam = camObj.GetComponent<Camera>();
                cam.orthographicSize = float.Parse(xmlNode.Attributes["Size"].Value);
                continue;
            }

            if (xmlNode.Name == "Floor")
            {
                GameObject floor = GameObject.FindGameObjectWithTag("Floor");
                Vector3 currentSize = floor.transform.localScale;
                floor.transform.localScale = new Vector3(float.Parse(xmlNode.Attributes["SizeX"].Value), currentSize.y,
                    float.Parse(xmlNode.Attributes["SizeZ"].Value));
                continue;
            }

            //Check if it is actually an object node
            if (xmlNode.Name != "Object") { continue; }

            XmlNode xmlNode2;
            xmlNode2 = xmlNode.ChildNodes[0];
            Vector3 position;
            position.x = float.Parse(xmlNode2.Attributes["x"].Value);
            position.y = float.Parse(xmlNode2.Attributes["y"].Value);
            position.z = float.Parse(xmlNode2.Attributes["z"].Value);
            xmlNode2 = xmlNode.ChildNodes[1];
            Quaternion rotation;
            rotation.w = float.Parse(xmlNode2.Attributes["w"].Value);
            rotation.x = float.Parse(xmlNode2.Attributes["x"].Value);
            rotation.y = float.Parse(xmlNode2.Attributes["y"].Value);
            rotation.z = float.Parse(xmlNode2.Attributes["z"].Value);
            xmlNode2 = xmlNode.ChildNodes[2];

            // module dependent parameters
            switch (xmlNode.Attributes["Tag"].Value)
            {
                case "CNC":
                    GameObject cnc = Instantiate(CNC, position, rotation);
                    cnc.name = xmlNode.Attributes["Name"].Value;
                    CNC cncScript = cnc.GetComponent<CNC>();
                    cncScript.targetVelocity = float.Parse(xmlNode2.Attributes["targetVelocity"].Value);
                    cncScript.force = float.Parse(xmlNode2.Attributes["force"].Value);
                    cncScript.freeSpin = bool.Parse(xmlNode2.Attributes["freeSpin"].Value);
                    cncScript.AddItems();
                    break;

                case "Turnplate":
                    GameObject turntable = Instantiate(Turntable, position, rotation);
                    turntable.name = xmlNode.Attributes["Name"].Value;
                    Turntable turntableScript = turntable.GetComponent<Turntable>();
                    Debug.Log(turntableScript);
                    //turntableScript.isRightTurn = bool.Parse(xmlNode2.Attributes["isRightTurn"].Value);
                    //turntableScript.isLeftTurn = bool.Parse(xmlNode2.Attributes["isLeftTurn"].Value);
                    //turntableScript.targetVelocity = float.Parse(xmlNode2.Attributes["targetVelocity"].Value);
                    //turntableScript.force = float.Parse(xmlNode2.Attributes["force"].Value);
                    //turntableScript.freeSpin = bool.Parse(xmlNode2.Attributes["freeSpin"].Value);
                    turntableScript.AddItems();
                    break;

                case "Conveyor":
                    GameObject conveyor = Instantiate(Conveyor, position, rotation);
                    conveyor.name = xmlNode.Attributes["Name"].Value;
                    Conveyor conveyorScript = conveyor.GetComponent<Conveyor>();
                    conveyorScript.targetVelocity = float.Parse(xmlNode2.Attributes["targetVelocity"].Value);
                    conveyorScript.force = float.Parse(xmlNode2.Attributes["force"].Value);
                    conveyorScript.freeSpin = bool.Parse(xmlNode2.Attributes["freeSpin"].Value);
                    conveyorScript.AddItems();
                    break;

                case "Conveyor_verticalMerge":
                    GameObject conveyorVerticalMerge = Instantiate(Conveyor_verticalMerge, position, rotation);
                    conveyorVerticalMerge.name = xmlNode.Attributes["Name"].Value;
                    Conveyor conveyorVerticalMergeScript = conveyorVerticalMerge.GetComponent<Conveyor>();
                    conveyorVerticalMergeScript.targetVelocity = float.Parse(xmlNode2.Attributes["targetVelocity"].Value);
                    conveyorVerticalMergeScript.force = float.Parse(xmlNode2.Attributes["force"].Value);
                    conveyorVerticalMergeScript.freeSpin = bool.Parse(xmlNode2.Attributes["freeSpin"].Value);
                    conveyorVerticalMergeScript.AddItems();
                    break;

                case "Conveyor_horizontalMerge":
                    GameObject conveyorHorizontalMerge = Instantiate(Conveyor_horizontalMerge, position, rotation);
                    conveyorHorizontalMerge.name = xmlNode.Attributes["Name"].Value;
                    Conveyor conveyorHorizontalMergeScript = conveyorHorizontalMerge.GetComponent<Conveyor>();
                    conveyorHorizontalMergeScript.targetVelocity = float.Parse(xmlNode2.Attributes["targetVelocity"].Value);
                    conveyorHorizontalMergeScript.force = float.Parse(xmlNode2.Attributes["force"].Value);
                    conveyorHorizontalMergeScript.freeSpin = bool.Parse(xmlNode2.Attributes["freeSpin"].Value);
                    conveyorHorizontalMergeScript.AddItems();
                    break;

                case "Sensor":
                    GameObject sensor = Instantiate(Sensor, position, rotation);
                    sensor.name = xmlNode.Attributes["Name"].Value;
                    sensor.GetComponent<Sensor>().AddItems();
                    break;

                case "Colorsensor":
                    GameObject colorsensor = Instantiate(Colorsensor, position, rotation);
                    colorsensor.name = xmlNode.Attributes["Name"].Value;
                    colorsensor.GetComponent<Colorsensor>().AddItems();
                    break;

                case "Input":
                    GameObject input = Instantiate(Input, position, rotation);
                    input.name = xmlNode.Attributes["Name"].Value;
                    InputCubes inputScript = input.GetComponent<InputCubes>();
                    inputScript.AddItems();
                    break;

                case "Output":
                    GameObject output = Instantiate(Output, position, rotation);
                    output.name = xmlNode.Attributes["Name"].Value;
                    break;

                case "Painting":
                    GameObject painting = Instantiate(Painting, position, rotation);
                    painting.name = xmlNode.Attributes["Name"].Value;
                    CNC paintingScript = painting.GetComponent<CNC>();
                    paintingScript.targetVelocity = float.Parse(xmlNode2.Attributes["targetVelocity"].Value);
                    paintingScript.force = float.Parse(xmlNode2.Attributes["force"].Value);
                    paintingScript.freeSpin = bool.Parse(xmlNode2.Attributes["freeSpin"].Value);
                    paintingScript.AddItems();
                    break;

                case "InputModule":
                    GameObject inputModule = Instantiate(InputModule, position, rotation);
                    inputModule.name = xmlNode.Attributes["Name"].Value;
                    InputModule inputModuleScript = inputModule.GetComponent<InputModule>();
                    inputModuleScript.AddItems();
                    break;

                case "SortingModule":
                    GameObject sortingModule = Instantiate(SortingModule, position, rotation);
                    sortingModule.name = xmlNode.Attributes["Name"].Value;
                    SortingModule sortingModuleScript = sortingModule.GetComponent<SortingModule>();
                    sortingModuleScript.AddItems();
                    break;

                case "DrillingModule":
                    GameObject drillingModule = Instantiate(DrillingModule, position, rotation);
                    drillingModule.name = xmlNode.Attributes["Name"].Value;
                    Module drillingModuleSkript = drillingModule.GetComponent<Module>();
                    drillingModuleSkript.AddItems();
                    break;

                case "PaintingModule":
                    GameObject paintingModule = Instantiate(PaintingModule, position, rotation);
                    paintingModule.name = xmlNode.Attributes["Name"].Value;
                    Module paintingModuleSkript = paintingModule.GetComponent<Module>();
                    paintingModuleSkript.AddItems();
                    break;

                case "MillingModule":
                    GameObject millingModule = Instantiate(MillingModule, position, rotation);
                    millingModule.name = xmlNode.Attributes["Name"].Value;
                    Module millingModuleSkript = millingModule.GetComponent<Module>();
                    millingModuleSkript.AddItems();
                    break;

                case "AssemblyModule":
                    GameObject assemblyModule = Instantiate(AssemblyModule, position, rotation);
                    assemblyModule.name = xmlNode.Attributes["Name"].Value;
                    Module assemblyModuleSkript = assemblyModule.GetComponent<Module>();
                    assemblyModuleSkript.AddItems();
                    break;

                case "OutputModule":
                    GameObject outputModule = Instantiate(OutputModule, position, rotation);
                    outputModule.name = xmlNode.Attributes["Name"].Value;
                    OutputModule outputModuleSkript = outputModule.GetComponent<OutputModule>();
                    Debug.Log(outputModuleSkript);
                    outputModuleSkript.AddItems();
                    break;

                case "StorageModule":
                    GameObject storageModule = Instantiate(StorageModule, position, rotation);
                    storageModule.name = xmlNode.Attributes["Name"].Value;
                    StorageModule storageModuleSkript = storageModule.GetComponent<StorageModule>();
                    storageModuleSkript.AddItems();
                    break;

                default:
                    break;
            }
        }
    }

    #region Menü
    // GUI
    private bool saveWindow = false;
    private bool loadWindow = false;
    private bool errorWindow = false;

    public void Save()
    {
        saveWindow = true;
    }

    public void Load()
    {
        loadWindow = true;
    }

    private void OnGUI()
    {
        if (saveWindow)
        {
            GUI.ModalWindow(0, new Rect(100, 100, 700, 100), Window, "Save");
        }
        if (loadWindow)
        {
            GUI.ModalWindow(1, new Rect(100, 100, 700, 100), Window, "Load");
        }
        if (errorWindow)
        {
            GUI.ModalWindow(0, new Rect(100, 100, 300, 100), ErrorWindow, "Error");
        }
    }

    // Save and load window
    private string filename = Path.Combine(Environment.CurrentDirectory, "attachment");
    private void Window(int id)
    {
        GUI.Label(new Rect(20, 30, 100, 20), "directory");
        filename = GUI.TextField(new Rect(150, 30, 500, 20), filename);

        if (id == 0)
        {
            if (GUI.Button(new Rect(150, 60, 100, 20), "Save"))
            {
                if (File.Exists(filename + ".xml"))
                {
                    errorWindow = true;
                    saveWindow = false;
                }
                else
                {
                    SaveAsXML(filename);
                    saveWindow = false;
                }
            }
        }

        if (id == 1)
        {
            if (GUI.Button(new Rect(150, 60, 100, 20), "Load"))
            {
                LoadFromXML(filename);
                loadWindow = false;
            }
        }

        if (GUI.Button(new Rect(250, 60, 100, 20), "Cancel"))
        {
            saveWindow = false;
            loadWindow = false;
        }
    }

    // Error print if file already exists
    private void ErrorWindow(int id)
    {
        GUI.Label(new Rect(20, 30, 250, 20), "This file already exists. Do you want to override it?");

        if (GUI.Button(new Rect(70, 60, 80, 20), "Cancel"))
        {
            errorWindow = false;
            saveWindow = true;
        }
        if (GUI.Button(new Rect(150, 60, 50, 20), "Ok"))
        {
            SaveAsXML(filename);
            errorWindow = false;
        }
    }
    #endregion
}
