#undef OUTLINE_ASSET_IMPORTED //change to define once the OUTLINE asset is imported

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select_script : MonoBehaviour
{
    private InputModule parent_script;
    public GameObject parent_Go;
    private GameObject popUp_manager;
#if OUTLINE_ASSET_IMPORTED
        private Outline outline;
#endif

    void Start()
    {
        parent_Go = transform.parent.gameObject;
        parent_script = this.gameObject.GetComponentInParent<InputModule>();
        popUp_manager = GameObject.Find("PopUpManager");
#if OUTLINE_ASSET_IMPORTED
            outline = GetComponent<Outline>();
#endif
    }  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

#if OUTLINE_ASSET_IMPORTED
                outline.enabled = false;
#endif

            return;
        }
    }

    void OnMouseDown()
    {
        if(popUp_manager.GetComponent<PopUp_Manager>().is_open == false)
        {
            //Debug
            //Debug.Log("GameObject clicked: " + gameObject.name);

            activatePopUp(parent_script);

        }
        
    }

    public void activatePopUp(InputModule Module)
    {
        //convoluted way to get the script of the pop up (maybe improve later)
        GameObject go = popUp_manager.GetComponent<PopUp_Manager>().activate_InputModule();
        PopUp_Input_Module PopUp_IM_script = go.GetComponent<PopUp_Input_Module>();
        //hand this as the current reference
        PopUp_IM_script.activate(parent_script);
        //activate outline

#if OUTLINE_ASSET_IMPORTED
        outline.enabled = true;
#endif
    }
}
