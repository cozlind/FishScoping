using UnityEngine;
using System.Collections;

public class tilt_rig : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = 
			Quaternion.AngleAxis(Controller.pitch*0.2f,Vector3.right)*
				Quaternion.AngleAxis(Controller.roll*-0.2f,Vector3.forward);
	}
}
