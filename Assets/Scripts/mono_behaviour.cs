using UnityEngine;
using System.Collections;

public class mono_behaviour : MonoBehaviour {

    Rigidbody rigidbody;
    void Start () {
        rigidbody = GetComponent<Rigidbody> ();
    }
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "hole"){
            col.isTrigger=true;
		}else if(col.gameObject.tag == "destroy"){
			Vector3 rebirth;
			Controller.score=Controller.score+100;
			if(Application.loadedLevelName=="mono_idou"||Application.loadedLevelName=="mono_idou_tutorial"){
				rebirth.x = -(float)1.4;
				rebirth.y = 20;
				rebirth.z = -(float)3.4;
				transform.position = rebirth;
			}else if(Application.loadedLevelName=="mono_idou_2"){
				rebirth.x = (float)1.4;
				rebirth.y = 20;
				rebirth.z = -(float)3.4;
				transform.position = rebirth;
			}
            col.isTrigger=false;
			//Destroy(gameObject);
		}
	}

	void OnTriggerStay(Collider col){
		Vector3 daruma;
		Debug.Log("col_stay:"+col.gameObject.tag);
		if(col.gameObject.tag=="Player"){
			if(Controller.fl1>30&&Controller.fr1>30){
				//rigidbody.constraints = RigidbodyConstraints.FreezeAll;
				rigidbody.Sleep();
				daruma.x=col.transform.position.x;
				daruma.y=col.transform.position.y-(float)0.4;
				daruma.z=col.transform.position.z;
				transform.position = daruma;
				//Controller.mono_flag=1;
			}else{
				rigidbody.WakeUp();
			}
		}
	}

	void OnTriggerExit(Collider col){
		transform.DetachChildren();
		rigidbody.WakeUp();
	}
}
