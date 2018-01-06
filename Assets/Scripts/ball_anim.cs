using UnityEngine;
using System.Collections;

public class ball_anim : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

		if(mono_catch_behaviour.timer>7&&mono_catch_behaviour.jump_flag==0){
			anim.SetBool("ball", true);
		}else{
			anim.SetBool("ball", false);
		}
	}
}
