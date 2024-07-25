using UnityEngine;

/* This script passes enter and exit triggers from the back sensor area of the cnc-machine elementary module 
 * 
 */
public class CNC_SensorBack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponentInParent<CNC>().enterSensorBack(other.gameObject);
        //this.gameObject.GetComponentInParent<CNC>().hasObject = false;
    }

    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponentInParent<CNC>().exitSensorBack();
    }
}
