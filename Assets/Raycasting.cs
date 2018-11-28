using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour {

	// Use this for initialization
//This is Main Camera in the Scene
    Camera m_MainCamera;
    //This is the second Camera and is assigned in inspector

    void Start()
    {
        //This gets the Main Camera from the Scene
        m_MainCamera = Camera.main;
        //This enables Main Camera
        m_MainCamera.enabled = true;
    }

	private void Update() {

		Ray mouseRay = m_MainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		if (Input.GetMouseButtonUp(0))
{
    Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward));
    Debug.Break();// pause the editor
}
	}
}
