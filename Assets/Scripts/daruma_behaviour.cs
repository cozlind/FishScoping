using UnityEngine;
using System.Collections;

public class daruma_behaviour : MonoBehaviour {
    Rigidbody rigidbody;
    public float all_finger_left;
	public float all_finger_right;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody> ();
        all_finger_left =0;
		all_finger_right=0;
	}
	
	// Update is called once per frame
	void Update () {
		all_finger_left=Controller.fl1+Controller.fl2+Controller.fl3+Controller.fl4+Controller.fl5;
		all_finger_right=Controller.fr1+Controller.fr2+Controller.fr3+Controller.fr4+Controller.fr5;
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "hole"){
            col.isTrigger=true;
		}else if(col.gameObject.tag == "destroy"){
			mini_game_script.score=mini_game_script.score+100;

			//Destroy(gameObject);
		}
	}

	void OnTriggerStay(Collider col){
		Vector3 daruma;
		Debug.Log("col_stay:"+col.gameObject.tag);
		if(col.gameObject.tag=="Player"){
			if(all_finger_left>100&&all_finger_right>100){
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
