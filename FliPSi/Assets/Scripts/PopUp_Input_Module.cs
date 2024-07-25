using UnityEngine;
using UnityEngine.UI;

public class PopUp_Input_Module : MonoBehaviour
{   
    //must be assigned when activated -> One pop up can be used for all Modules of the specific type
    public InputModule My_Ref;
    public GameObject My_GO;
    public Dragable_Window[] windows = new Dragable_Window[4];

    //copyied attributes and toggle for display (always have Toggle and actual value for debugging; will change later)
    public int Sensor_Value; //maybe privjet
    public Toggle Sensor_Value_Toggle;

    //components
    public Conveyor_PopUp C1;
    public Conveyor_PopUp C2;
    public Conveyor_PopUp C3;



    // Update is called once per frame
    void Update()
    {   
        //copy the important data from reference Module note: only exportable value(s)
        if(My_Ref != null)
        {
            Sensor_Value = My_Ref.lightsensorVal;   
            Sensor_Value_Toggle.isOn = (Sensor_Value == 1);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.GetComponentInParent<PopUp_Manager>().is_open = false;
            //reset position of all movable windows
            foreach(Dragable_Window W in windows)
            {
                W.reset();
            }
            gameObject.SetActive(false);

        }
    }

    //Manual controll of the reference Module
    public void callAddCube()
    {
        My_Ref.AddCube();
    }

    public void activate(InputModule Mod)
    {
        My_Ref = Mod;
        My_GO = Mod.gameObject;
        //assign conveyors (script)
        if(Mod.MyComponents[0] is Conveyor)
        {C1.My_Ref = Mod.MyComponents[0] as Conveyor;}
        if(Mod.MyComponents[1] is Conveyor)
        {C2.My_Ref = Mod.MyComponents[1] as Conveyor;}
        if(Mod.MyComponents[2] is Conveyor)
        {C3.My_Ref = Mod.MyComponents[2] as Conveyor;}

        //assign conveyors (GO)
        C1.My_GO = C1.My_Ref.gameObject;
        C2.My_GO = C2.My_Ref.gameObject;
        C3.My_GO = C3.My_Ref.gameObject;

    }
}
