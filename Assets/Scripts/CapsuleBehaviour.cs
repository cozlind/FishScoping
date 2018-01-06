using UnityEngine;
using System.Collections;
using System.IO.Ports; 
using System.IO;
using System;
using System.Threading;//スレッド用

public class CapsuleBehaviour : MonoBehaviour {
	
	public GameObject capsule;

	// Use this for initialization
	void Start () {
		//capsule = GameObject.Find("Capsule");
		transform.position=capsule.transform.position;
	}

	// Update is called once per frame
	void Update () {

		transform.eulerAngles = Controller.temp;

		transform.rotation=Quaternion.AngleAxis(Controller.pitch,Vector3.left)*transform.rotation;
		transform.rotation=Quaternion.AngleAxis(Controller.roll,Vector3.forward)*transform.rotation;
		transform.rotation=Quaternion.AngleAxis(Controller.yaw,Vector3.down)*transform.rotation;

		//transform.eulerAngles = Controller.temp;
		transform.RotateAround(Controller.mayumi,Vector3.left,Controller.pitch_degree/10f);//pitch
		transform.RotateAround(Controller.mayumi,Vector3.down,Controller.yaw_degree*1.2f);//yaw

		if(Input.GetKey(KeyCode.Z)||Application.loadedLevelName=="tutorial02"||Application.loadedLevelName=="mayukko"){
			Vector3 posi;
			posi.x = 0f;
			posi.y = 1.1f;
			posi.z = -4f;
			transform.position = posi;
		}
	}
}