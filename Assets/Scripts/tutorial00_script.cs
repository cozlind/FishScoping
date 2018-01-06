using UnityEngine;
using System.Collections;
using System.Data;

public class tutorial00_script : MonoBehaviour {
	public GUIStyle style;
	private AudioSource sound01;


	void Start(){
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound01.PlayOneShot(sound01.clip);
	}
	// Update is called once per frame
	void Update () {
	}

	//Button
	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;

		Rect rect = new Rect (0,sh-sh/8,sw-100,sh/8);
		style.fontSize=sh/15;
		style.normal.textColor=Color.white;
		style.alignment=TextAnchor.MiddleLeft;
		bool isClicked_tuto1 = GUI.Button(rect, " こうやって掴んでね",style);
			if (isClicked_tuto1){
				Debug.Log("STAND BY READY!!");
				Application.LoadLevelAsync("tutorial01");
		}
		if(Controller.flag==0){
			if(Controller.fr1>10||Controller.fr2>10||Controller.fr3>10||Controller.fr4>10||Controller.fr5>10||Controller.fl1>10||Controller.fl2>10||Controller.fl3>10||Controller.fl4>10||Controller.fl5>10){
				Controller.flag=1;
				Debug.Log("STAND BY READY!!");
				Application.LoadLevelAsync("tutorial01");
			}
		}
		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("tutorial01");
		}
		
		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevelAsync("user_select");
		}

		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevelAsync("mayukko");
		}
	}
}
