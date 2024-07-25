using UnityEngine;

/* This script passes enter and exit triggers from the front sensor area of the cnc-machine elementary module 
 * 
 */
public class CNC_SensorFront : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponentInParent<CNC>().enterSensorFront(other.gameObject);
        //this.gameObject.GetComponentInParent<CNC>().hasObject = true;
    }

    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponentInParent<CNC>().exitSensorFront();
    }
}
