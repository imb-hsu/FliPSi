using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runAllBelts : MonoBehaviour
{
    BilloSPS sps;
    // Start is called before the first frame update
    void Start()
    {
        sps.AllMotorsForward();
    }
}
