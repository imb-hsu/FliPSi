using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// controlls Camera funktion: Zoom, position & perspective
public class CameraController : MonoBehaviour
{
    public Camera camNow;
    public Camera camNext;
    
    private Vector3 position;
    private float size;

    private void Awake()
    {
		position = camNow.transform.position;

		if (camNow.orthographic)
			size = camNow.orthographicSize;
		else
			size = camNow.fieldOfView;
    }
	//logic to controll camera
    void Update()
    {
    		
		if (Input.GetAxis("Mouse ScrollWheel") != 0 & Input.GetKey("left shift"))
		{
			Vector3 temp = new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel"));
			this.transform.position += temp;
		}

		if (Input.GetAxis("Mouse ScrollWheel") != 0 & Input.GetKey("tab"))
		{
			Vector3 temp = new Vector3(Input.GetAxis("Mouse ScrollWheel"), 0, 0);
			this.transform.position += temp;
		}

		if (Input.GetAxis("Mouse ScrollWheel") != 0 & Input.GetKey("left ctrl"))
		{
			if (camNow.orthographic)
			    camNow.orthographicSize += Input.GetAxis("Mouse ScrollWheel");
			else
			    camNow.fieldOfView += Input.GetAxis("Mouse ScrollWheel");
		}

		if (Input.GetKeyDown("v"))
		{
			camNext.gameObject.SetActive(true);
			camNow.gameObject.SetActive(false);
		}

		if (Input.GetKeyDown("r") & Input.GetKey("left ctrl"))
		{
			this.transform.position = position;

			if (camNow.orthographic)
			    camNow.orthographicSize = size;
			else
			    camNow.fieldOfView = size;
		}
    }
}
