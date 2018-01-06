using UnityEngine;
using System.Collections;

public class ring_script : MonoBehaviour {
	public static float ring_position=0;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 ring;
		transform.Translate(Vector3.down*Time.deltaTime);
		if(transform.position.z<-10f){
			ring_position=ring_position+(float)Random.Range(-1,2)/10f;
			if(ring_position>0.6)ring_position=0.5f;
			else if(ring_position<-0.6)ring_position=-0.4f;
			ring.x=ring_position;
			ring.y=1f;
			ring.z = -3f;
			transform.localPosition=ring;
			//ring0.renderer.material.color = Color.green;
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			mayukko_flight_script.score = mayukko_flight_script.score+100;
		}
	}
}
