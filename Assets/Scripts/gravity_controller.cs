using UnityEngine;
using System.Collections;

public class gravity_controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Physics.gravity = 
			Quaternion.AngleAxis(Controller.pitch*0.4f,Vector3.right)*
				Quaternion.AngleAxis(Controller.roll*-0.4f,Vector3.forward)*(Vector3.up*-20f);
	}
}
