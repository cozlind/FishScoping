using UnityEngine;
using System.Collections;

public class mono_catch_behaviour : MonoBehaviour {
	public static float timer=0;
	public static int jump_flag=0;
	public int catch_flag=0;
	public GameObject capsule;
	public float all_finger_left;
	public float all_finger_right;
    Rigidbody rigidbody;
    void Start () {
        rigidbody = GetComponent<Rigidbody> ();
        Controller.timer = 0;
		Controller.score = 0;
		all_finger_left=0;
		all_finger_right=0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer>8&&jump_flag==0){
			rigidbody.AddForce(0,400,-100);
			jump_flag=1;
		}
		all_finger_left=Controller.fl1+Controller.fl2+Controller.fl3+Controller.fl4+Controller.fl5;
		all_finger_right=Controller.fr1+Controller.fr2+Controller.fr3+Controller.fr4+Controller.fr5;
		if(all_finger_left>100&&all_finger_right>100){
			if(timer>9&&timer<13){
				if(catch_flag==0){
					Vector3 daruma;
					capsule = GameObject.Find("Capsule");
					rigidbody.Sleep();
					daruma.x=capsule.transform.position.x;
					daruma.y=capsule.transform.position.y+0.4f;
					daruma.z=capsule.transform.position.z;
					transform.position = daruma;

					if(jump_flag==1&&Application.loadedLevelName=="mono_catch")Controller.score=Controller.score+100;
					jump_flag=2;
					catch_flag=0;
				}
			}else{
				catch_flag=1;
			}
		}else if(Controller.fl1<30||Controller.fr1<30){
			catch_flag=0;
			rigidbody.WakeUp();
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "hole"){
            col.isTrigger=true;
		}else if(col.gameObject.tag == "destroy"){
			Vector3 rebirth;
			rebirth.x = 0;
			rebirth.y = 1;
			rebirth.z = (float)0.5;
			transform.position = rebirth;
			timer=4;
			jump_flag=0;
            col.isTrigger=false;
		}
			//collider.isTrigger=false;
			//Destroy(gameObject);
	}

	void OnTriggerExit(Collider col){
		transform.DetachChildren();
		rigidbody.WakeUp();
	}
}
