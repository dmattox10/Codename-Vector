using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour {
	public GameObject leader_light;
    public GameObject pawn_light0;
	public GameObject pawn_light1;
	public GameObject pawn_light2;
	public GameObject pawn_light3;
    public GameObject leader_shadow;
    public GameObject pawn_shadow0;
	public GameObject pawn_shadow1;
	public GameObject pawn_shadow2;
	public GameObject pawn_shadow3;
	// Use this for initialization
	public Camera DarkCam;
	public Camera LightCam;
	public int redCol;
	public int grnCol;
	public int blucol;

	public Material[] materials;
	Renderer r;

	    public float force = 5;

	private void Update () {
		RaycastHit hit;
		// TODO choose camera based on turn!!!
		Ray ray = DarkCam.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, 100f)) {
			if (hit.transform != null) {
				PrintName(hit.transform.gameObject);
				Debug.DrawLine (ray.origin, hit.point);
				Rigidbody rb;

				if (rb = hit.transform.GetComponent<Rigidbody>())
				{   
					// require unit tag
					// require mouse click 
					// phone interface?
					if (hit.transform.gameObject.tag == "pieces") {
						if (Input.GetMouseButtonDown(0)) {
						PrintName(hit.transform.gameObject);
						LaunchIntoAir(rb);
						}
					}                    
				}
				if (hit.transform.gameObject.tag == "grid") {
					// Light them up here!
						
				}
			}
		}
	}

	private void PrintName(GameObject go){
		print(go.name);
	}

	private void LaunchIntoAir(Rigidbody rig)
    {
        rig.AddForce(rig.transform.up * force, ForceMode.Impulse);
    }
}
