using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySelect : MonoBehaviour {

	public string selectedObject;
	private string objectInternalReference;

	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
			/*
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            
	 		*/

			 Debug.Log("Hit: " + hit.collider.gameObject.name);
			 selectedObject = hit.collider.gameObject.name;
		}
        else
        {
			/* 
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.green);
            Debug.Log(“Missed.”);
			*/
        }
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.magenta, 0.1f);
	}
}
