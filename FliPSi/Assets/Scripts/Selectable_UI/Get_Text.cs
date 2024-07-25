using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Get_Text : MonoBehaviour
{   
    //currently only for Input Module; expand to all possible pop ups

    
    //public for debug; references to text and GO that is displayed in dragable window
    public GameObject gameObj; 
    public Text uiText; 
    // Start is called before the first frame update
    void Start()
    {
        PopUp_Input_Module tmp = GetComponentInParent<PopUp_Input_Module>();
        gameObj = tmp.My_GO;
        uiText = this.GetComponent<Text>();
        uiText.text = gameObj.name;
    }

    // Update is called once per frame
    void Update()
    {   //idk why its not called properly in start(); needs fix later
        PopUp_Input_Module tmp = GetComponentInParent<PopUp_Input_Module>();
        gameObj = tmp.My_GO;
        uiText = this.GetComponent<Text>();
        uiText.text = gameObj.name;
    }
}
