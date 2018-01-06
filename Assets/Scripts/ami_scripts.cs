using UnityEngine;
using System.Collections;

public class ami_scripts : MonoBehaviour {
	public static int ami_flag=0;
	public float timer=0;
	public float ami_time=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; 
		Vector3 ami_posi;
		if(ami_flag==0){
			Vector3 ami_rote;
				ami_rote.x=-90f;
				ami_rote.y=0f;
				ami_rote.z=0f;
				transform.eulerAngles = ami_rote;
		}
		if(Controller.fl1>10||ami_flag==1){
			ami_posi.x=-0.2f;
			ami_posi.y=1.1f;
			ami_posi.z=-9.8f;
			transform.localPosition=ami_posi;
			transform.Rotate(Vector3.right*110*Time.deltaTime);
			ami_time += Time.deltaTime;
			if(ami_time>1){
				ami_flag=0;
				ami_time=0;
			}else ami_flag=1;
		}else if(
			Controller.fr1>10||ami_flag==2){
			ami_posi.x=0.2f;
			ami_posi.y=1.1f;
			ami_posi.z=-9.8f;
			transform.localPosition=ami_posi;
			transform.Rotate(Vector3.right*110*Time.deltaTime);
			ami_time += Time.deltaTime;
			if(ami_time>1){
				ami_flag=0;
				ami_time=0;
			}else ami_flag=2;
		}
	}
}
