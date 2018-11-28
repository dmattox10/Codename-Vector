using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

	public string selectedObject;
	private string objectInternalReference;
	public RaycastHit theObject;
	
	// Update is called once per frame
	void Update () {
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out theObject, Mathf.Infinity))
		{
			
			selectedObject = theObject.collider.transform.gameObject.name;
			objectInternalReference = theObject.collider.transform.gameObject.name;
			
		}
	}
}
