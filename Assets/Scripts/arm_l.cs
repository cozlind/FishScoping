using UnityEngine;
using System.Collections;

public class arm_l : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevelName=="mono_catch"){
			transform.rotation=Quaternion.AngleAxis(180,Vector3.up)
				*Quaternion.AngleAxis(Controller.fl1/15f-20,Vector3.forward)
					*Quaternion.AngleAxis(180,Vector3.left);
		}else 
			transform.rotation=Quaternion.AngleAxis(180,Vector3.up)
				*Quaternion.AngleAxis(-Controller.fl1/15f+20,Vector3.forward);
	}
}
