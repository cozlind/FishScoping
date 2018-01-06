using UnityEngine;
using System.Collections;

using System.IO.Ports; 
using System.IO;
using System;
using System.Threading;//スレッド用

public class CapsuleBehaviour_mono_idou : MonoBehaviour {
	// Use this for initialization

	public GameObject capsule;
	// Use this for initialization
	void Start () {
		capsule = GameObject.Find("Capsule");
		transform.position=capsule.transform.position;
		Vector3 posi;
		posi.x = 0;
		posi.y = (float)0.7;
		posi.z = -3;
		transform.position = posi;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.eulerAngles = Controller.temp;
		
		transform.rotation=Quaternion.AngleAxis(Controller.pitch,Vector3.left)*transform.rotation;
		transform.rotation=Quaternion.AngleAxis(Controller.roll,Vector3.forward)*transform.rotation;
		transform.rotation=Quaternion.AngleAxis(Controller.yaw,Vector3.down)*transform.rotation;
		
		//transform.eulerAngles = Controller.temp;
		transform.RotateAround(Controller.mayumi,Vector3.left,Controller.pitch_degree/10f);//pitch
		transform.RotateAround(Controller.mayumi,Vector3.down,Controller.yaw_degree*2.0f);//yaw
		
		if(Input.GetKey(KeyCode.Z)){
			Vector3 posi;
			posi.x = 0;
			posi.y = (float)0.7;
			posi.z = -3;
			transform.position = posi;
		}
	}
}
