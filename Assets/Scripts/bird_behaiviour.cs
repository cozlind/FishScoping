using UnityEngine;
using System.Collections;

public class bird_behaiviour : MonoBehaviour {
	public float timer=0;
	public int bird_flag=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 bird_posi;
		if(Random.Range(0,200)==0&&bird_flag==0){
			bird_posi.x=-0.4f;
			bird_posi.y=1.4f;
			bird_posi.z=-10f;
			transform.localPosition=bird_posi;
			bird_flag=1;
		}else if(Random.Range(0,200)==1&&bird_flag==0){
			bird_posi.x=0.4f;
			bird_posi.y=1.4f;
			bird_posi.z=-10f;
			transform.localPosition=bird_posi;
			bird_flag=2;
		}
		if(bird_flag==1){
			transform.Translate(Vector3.up*Time.deltaTime);
			timer += Time.deltaTime;
			if(ami_scripts.ami_flag==1||timer>5){
				mayukko_flight_script.bird_time=timer;
				bird_flag=0;
				timer=0;
			}
		}else if(bird_flag==2){
			transform.Translate(Vector3.up*Time.deltaTime);
			timer += Time.deltaTime;
			if(ami_scripts.ami_flag==2||timer>5){
				mayukko_flight_script.bird_time=timer;
				bird_flag=0;
				timer=0;
			}
		}
	}
}
