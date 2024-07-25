using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// This script opens a window in the GUI for changing the size of the factory's floor area (x-, z-dimension)
// and adjusting the height of the main camera (y-dimension)

public class Factory : MonoBehaviour
{
    private string inputX = "18";
    private string inputZ = "9"; 
    public float sizeX;	// x-dimension of the factory floor
    public float sizeZ;	// z-dimension of the factory floor
    public float camHeight = 4f;	// height of the main Cameras bird perspective    
    
    void Awake()
    {
    	camHeight = Camera.main.orthographicSize;
	sizeX = this.transform.localScale.x*10;
	sizeZ = this.transform.localScale.z*10;	
    }

    public void ChangeSize()
    {
	window = true;
    }

    private bool window;
    private void OnGUI()
    {
	if (window)
	{
	    GUI.ModalWindow(0, new Rect(100, 100, 350, 150), Window, "Change factory area");
	}
    }

    private void Window(int id)
    {
	inputX = GUI.TextField(new Rect(20, 30, 100, 20), inputX);
	GUI.Label(new Rect(220, 30, 100, 20), "Width (x-axis) [m]");
	sizeX = float.Parse(inputX);
	
	inputZ = GUI.TextField(new Rect(20, 60, 100, 20), inputZ);
	GUI.Label(new Rect(220, 60, 100, 20), "Depth (z-axis) [m]");
	sizeZ = float.Parse(inputZ);
	
	camHeight = GUI.HorizontalSlider(new Rect(20, 90, 100, 20), camHeight, 0, 50f);
	GUI.Label(new Rect(150, 90, 50, 20), Convert.ToString(camHeight));
	GUI.Label(new Rect(220, 90, 100, 20), "Camera Height");	
	
	Camera.main.orthographicSize = camHeight;
	
	
	this.transform.localScale = new Vector3(sizeX/10, 1, sizeZ/10);

	if (GUI.Button(new Rect(150, 120, 50, 20), "OK"))
	{
	    window = false;
	}
    }
}
