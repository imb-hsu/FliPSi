using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp_Manager : MonoBehaviour
{   
    //just to get acess to the deactivated pop ups
    public GameObject InputModule;
    public bool is_open;

    void Start()
    {
        is_open = false;
    }

    public GameObject activate_InputModule()
    {
        is_open = true;
        InputModule.SetActive(true);
        return InputModule;
    }

}
