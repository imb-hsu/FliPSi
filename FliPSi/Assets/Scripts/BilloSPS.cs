using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilloSPS : MonoBehaviour
{
    //List if ElementComponents
    //[ExportableModule]

    public List<ProductList> products = new List<ProductList>();
    [ExportableModule]
    public List<Conveyor> conveyors = new List<Conveyor>();
    [ExportableModule]
    public List<InputModule> InputModules = new List<InputModule>();
    public List<InputCubes> inputcubes = new List<InputCubes>();
    [ExportableModule]
    public List<Turntable> turntables = new List<Turntable>();
    public List<Sensor> sensors = new List<Sensor>();
    public List<Colorsensor> colorsensors = new List<Colorsensor>();
    [ExportableModule]
    public List<Barrier> barriers = new List<Barrier>();
    //List of Modules
    [ExportableModule]
    public List<CNC> cncs = new List<CNC>();
    [ExportableModule]
    public List<Painting> paintings = new List<Painting>();
    [ExportableModule]
    public List<Drilling> drillings = new List<Drilling>();
    [ExportableModule]
    public List<Milling> millings = new List<Milling>();
    public List<StorageModule> storageModules = new List<StorageModule>();
    public List<Crane> cranes = new List<Crane>();
    public List<Storage> storages = new List<Storage>();
    public List<ConveyorStorage> conveyorStorages = new List<ConveyorStorage>();

    

    public LearningAgent myAgent;
    public DataCollector myDataCollector;


    public void AddProduct(ProductList PL)
    {
        products.Add(PL);
    }

    public void AddInput(InputCubes IC)
    {
        bool b = false;
        foreach(InputCubes INC in inputcubes)
        {
            if(IC == INC)
            {
                b = true;
            }
        }
        if(!b)
        {
            inputcubes.Add(IC);
        }
    }

    public void RemoveInput(InputCubes IC)
    {
        inputcubes.Remove(IC);
    }
    
    public void Add(ElementComponent EC){
    bool b = false;
    if(!(EC is Module))
    {
        if(EC is InputModule)
        {   
            foreach(InputModule C in InputModules)
            {
                if(C == EC)
                {
                    b = true;
                }
            }
            if(!b)
            {
                InputModules.Add(EC as InputModule);
            }
            
        }
        else if(EC is Conveyor)
        {   
            foreach(Conveyor C in conveyors)
            {
                if(C == EC)
                {
                    b = true;
                }
            }
            if(!b)
            {
                conveyors.Add(EC as Conveyor);
            }
            
        } else if(EC is Turntable)
        {
         foreach(Turntable TT in turntables)
         {
            if(EC == TT)
            {
                b = true;
            }
            if(!b)
            {
                turntables.Add(EC as Turntable);
            }
         }   
        }else if(EC is Sensor)
        {
            foreach(Sensor S in sensors)
            {
                if(S == EC)
                {
                    b = true;
                }
            }   
            if(!b)
            {
                sensors.Add(EC as Sensor);
            }
        }else if(EC is Colorsensor)
        {
            foreach(Colorsensor C in colorsensors)
            {
                if(C == EC)
                {
                    b = true;
                }
            }
            if(!b)
            {
                colorsensors.Add(EC as Colorsensor);
            }
        }else if(EC is Barrier)
        {
            foreach(Barrier B in barriers)
            {
                if(B == EC)
                {
                    b = true;
                }
            }
            if(!b)
            {
                barriers.Add(EC as Barrier);
            }
        }
        else
        {
            Debug.Log("unknown EC");
        }
    } else if (EC is Module)
    {   //only important for fuctional modules (Painting; Drilling; Milling;)
        ElementComponent M = EC;
        if (M is Painting)
        {
            paintings.Add(M as Painting);
        }
        else if (M is Drilling)
        {
            drillings.Add(M as Drilling);
        }
        else if (M is Milling)
        {
            millings.Add(M as Milling);
        }
        else if (M is StorageModule)
        {
            StorageModule temp = M as StorageModule;
            storageModules.Add(temp);
        }
        else if(M is Crane)
        {
            cranes.Add(M as Crane);
        }
        else if (M is Storage)
        {
            storages.Add(M as Storage);
        }
        else if (M is ConveyorStorage)
        {
            conveyorStorages.Add(M as ConveyorStorage);
        }
        else
        {
            //Debug.Log("unknown Module " + M);
        }
    }

    }
    public void Remove(ElementComponent EC)
    {
        if (EC is Conveyor)
        {
            conveyors.Remove(EC as Conveyor);
        }
        else if (EC is Turntable)
        {
            turntables.Remove(EC as Turntable);
        }
        else if (EC is Sensor)
        {
            sensors.Remove(EC as Sensor);
        }
        else if (EC is Colorsensor)
        {
            colorsensors.Remove(EC as Colorsensor);
        }
        else if (EC is Barrier)
        {
            barriers.Remove(EC as Barrier);
        }
        else
        {
            Debug.Log("unknown EC");
        }
    }
    /*
    public void Add(Module M)
    { RemoveCubes
        //if (M is CNC)
        //{
        //    cncs.Add(M as CNC);
        //}
        if (M is Painting)
        {
            paintings.Add(M as Painting);
        }
        else if (M is Drilling)
        {
            drillings.Add(M as Drilling);
        }
        else if (M is Milling)
        {
            millings.Add(M as Milling);
        }
        else if (M is StorageModule)
        {
            StorageModule temp = M as StorageModule;
            storageModules.Add(temp);
        }
        else if(M is Crane)
        {
            cranes.Add(M as Crane);
        }
        else if (M is Storage)
        {
            storages.Add(M as Storage);
        }
        else if (M is ConveyorStorage)
        {
            conveyorStorages.Add(M as ConveyorStorage);
        }
        else
        {
            Debug.Log("unknown Module");
        }
    }
    */
        public void Remove(ProductList PL)
    {
        products.Remove(PL);
    }
    public void Remove(Module M)
    {
        if (M is CNC)
        {
            cncs.Remove(M as CNC);
        }
        else if (M is Painting)
        {
            paintings.Remove(M as Painting);
        }
        else if (M is Drilling)
        {
            drillings.Remove(M as Drilling);
        }
        else if (M is Milling)
        {
            millings.Remove(M as Milling);
        }
        else if (M is StorageModule)
        {
            storageModules.Remove(M as StorageModule);
        }
        else if (M is Crane)
        {
            cranes.Remove(M as Crane);
        }
        else if (M is Storage)
        {
            storages.Remove(M as Storage);
        }
        else if (M is ConveyorStorage)
        {
            conveyorStorages.Remove(M as ConveyorStorage);
        }
        else
        {
            Debug.Log("unknown Module");
        }
    }


    public void AllMotorsForward()
    {/*
        foreach (Conveyor conveyor in conveyors)
        {
            conveyor.MotorForward();
        }
        foreach (Turntable turntable in turntables)
        {
            turntable.MotorForward();
        }
        foreach (Barrier barrier in barriers)
        {
            barrier.MotorForward();
        }
        foreach (Painting painting in paintings)
        {
            painting.MotorForward();
        }
        foreach (Milling milling in millings)
        {
            milling.MotorForward();
        }
        foreach (Drilling drilling in drillings)
        {
            drilling.MotorForward();
        }
        foreach (StorageModule storageModule in storageModules)
        {
            storageModule.MotorForward();
        }
        */
    }

    public void AllMotorsBackward()
    {
        foreach (Conveyor conveyor in conveyors)
        {
            conveyor.MotorBackward();
        }
        foreach (Turntable turntable in turntables)
        {
            turntable.MotorBackward();
        }
        foreach (Barrier barrier in barriers)
        {
            barrier.MotorBackward();
        }
        foreach (Painting painting in paintings)
        {
            painting.MotorBackward();
        }
        foreach (Milling milling in millings)
        {
            milling.MotorBackward();
        }
        foreach (Drilling drilling in drillings)
        {
            drilling.MotorBackward();
        }
        foreach (StorageModule storageModule in storageModules)
        {
            storageModule.MotorBackward();
        }
    }

    public void AllMotorsOff()
    {
        foreach (Conveyor conveyor in conveyors)
        {
            conveyor.MotorOff();
        }
        foreach (Turntable turntable in turntables)
        {
            turntable.MotorOff();
        }
        foreach (Barrier barrier in barriers)
        {
            barrier.MotorOff();
        }
        foreach (Painting painting in paintings)
        {
            painting.MotorOff();
        }
        foreach (Milling milling in millings)
        {
            milling.MotorOff();
        }
        foreach (Drilling drilling in drillings)
        {
            drilling.MotorOff();
        }
        foreach (StorageModule storageModule in storageModules)
        {
            storageModule.MotorOff();
        }
    }
    
    public void AllAddCubes()
    {
        foreach (InputCubes inputcube in inputcubes)
        {
            products.Add(inputcube.AddCube());
        }
    }
    public void RemoveCubes()
    {
        //Reset Button
    GameObject[] deleteRaw;
    //GameObject[] deleteProduct;
    //GameObject[] deleteProductDrill;
    GameObject[] deleteProductcncDrill;
    GameObject[] deleteProductrawcncAssembly;

    print("Call Remove Methode"); 
        deleteRaw = GameObject.FindGameObjectsWithTag("Product_raw");
        //deleteProduct = GameObject.FindGameObjectsWithTag("Product_cnc");
       // deleteProductDrill = GameObject.FindGameObjectsWithTag("Product_drill");
        deleteProductcncDrill = GameObject.FindGameObjectsWithTag("Product_cncdrill");
        deleteProductrawcncAssembly = GameObject.FindGameObjectsWithTag("Product_raw_cnc_assembly");


        foreach (GameObject delete in deleteRaw)
        {
            Destroy(delete);
        }

        /*foreach (GameObject delete in deleteProduct)
        {
            Destroy(delete);
        }
        
        foreach (GameObject delete in deleteProductDrill)
        {
            Destroy(delete);
        }
        */
        foreach (GameObject delete in deleteProductcncDrill)
        {
            Destroy(delete);
        }

        foreach (GameObject delete in deleteProductrawcncAssembly)
        {
            Destroy(delete);
        }
    }

    public void AllLeftTurn()
    {
        foreach (Turntable turntable in turntables)
        {
            turntable.ContinouusLeftTurn(true);
            turntable.ContinouusRightTurn(false);
        }
    }

    public void AllRightTurn()
    {
        foreach (Turntable turntable in turntables)
        {
            turntable.ContinouusRightTurn(true);
            turntable.ContinouusLeftTurn(false);

        }
    }

    public void AllStopTurning()
    {
        foreach (Turntable turntable in turntables)
        {
            turntable.ContinouusRightTurn(false);
            turntable.ContinouusLeftTurn(false);
        }
    }

    public void AllClose()
    {
        foreach (Barrier barrier in barriers)
        {
            barrier.Close();
        }
    }

    public void AllOpen()
    {
        foreach (Barrier barrier in barriers)
        {
            barrier.Open();
        }
    }

    public Color[] GetAllColors()
    {
        Color[] colors = new Color[colorsensors.Count];
        int i = 0;
        foreach (Colorsensor colorsensor in colorsensors)
        {
            colors[i] = colorsensor.objectColor;
            i++;
        }
        return colors;
    }

}
