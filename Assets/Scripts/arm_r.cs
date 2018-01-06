using UnityEngine;
using System.Collections;

public class arm_r : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevelName=="mono_catch"){
			transform.rotation=Quaternion.AngleAxis(180,Vector3.up)
				*Quaternion.AngleAxis(-Controller.fr1/15f+20,Vector3.forward)
				*Quaternion.AngleAxis(180,Vector3.left);
		}else 
			transform.rotation=Quaternion.AngleAxis(180,Vector3.up)
				*Quaternion.AngleAxis(Controller.fr1/15f-20,Vector3.forward);
	}
}
