using UnityEngine;
using System.Collections;
using System.Data;

public class tutorial02_script : MonoBehaviour {
	public GUIStyle style;
	private AudioSource sound01;
	public float timer=0;
	void Start(){
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound01.PlayOneShot(sound01.clip);
	}
	// Update is called once per frame
	void Update () {
		if(Controller.pitch>0||Controller.pitch<0.5){
			if(Controller.gx<50&&Controller.gy<50&&Controller.gz<50){
				timer += Time.deltaTime;
				if(timer>8.0f)Application.LoadLevelAsync("tutorial03");
			}
		}
	}

	//Button
	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;

		Rect rect = new Rect (0,sh-sh/8,sw-100,sh/8);
		style.fontSize=sh/15;
		style.normal.textColor=Color.white;
		style.alignment=TextAnchor.MiddleLeft;
		bool isClicked_tuto1 = GUI.Button(rect, " まずは膝の上においてね",style);
		if (isClicked_tuto1)Application.LoadLevelAsync("tutorial03");

		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("tutorial03");
		}
		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevelAsync("tutorial01");
		}
		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevelAsync("mayukko");
		}
	}
}
