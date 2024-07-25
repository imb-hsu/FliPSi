using UnityEngine;

/* This script passes enter and exit triggers from the middle sensor area of the cnc-machine elementary module 
 * 
 */
public class CNC_SensorMiddle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //usually calls CNC.executeCNC(GameObject go) (abstract funktion is implemented for each child)
        this.gameObject.GetComponentInParent<CNC>().enterSensorMiddle(other.gameObject);
        this.gameObject.GetComponentInParent<CNC>().hasObject = true;
        this.gameObject.GetComponentInParent<CNC>().Sensorvalue = 1;
    }

    private void OnTriggerExit(Collider other)
    {
        //this.gameObject.GetComponentInParent<CNC>().hasObject = false;
        this.gameObject.GetComponentInParent<CNC>().Sensorvalue = 0;
    }
}
