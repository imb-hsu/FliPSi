using UnityEngine;
using UnityEngine.UI;

public class Conveyor_PopUp : MonoBehaviour
{

    //must be assigned when activated -> One pop up can be used for all Components of the specific type
    public Conveyor My_Ref;
    public GameObject My_GO;

        //copyied attributes and toggle for display (always have Toggle and actual value for debugging; will change later)
    public int Sensor_Value; //maybe privjet
    public Toggle Sensor_Value_Toggle;
    public int Motor_Value; //maybe privjet
    public Toggle Motor_Value_Toggle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(My_Ref != null)
        {
            Sensor_Value = My_Ref.Sensorvalue;   
            Sensor_Value_Toggle.isOn = (Sensor_Value == 1);
            Motor_Value = My_Ref.motorValue;
            Motor_Value_Toggle.isOn = (Motor_Value == 1);
        }
    }

}
