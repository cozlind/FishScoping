using UnityEngine;
using System.Collections;
using System.Data;

public class tutorial_script : MonoBehaviour {
	public GUIStyle style;
	private int f;
	void Start(){
		f=0;
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
		if(f==0){
			bool isClicked_tutorial1 = GUI.Button(rect, " 体を中心に腕を回すように動かしてね",style);
			if (isClicked_tutorial1||Controller.fr1>10||Controller.fr2>10||Controller.fr3>10||Controller.fr4>10||Controller.fr5>10
			    ||Controller.fl1>10||Controller.fl2>10||Controller.fl3>10||Controller.fl4>10||Controller.fl5>10){
				if(Controller.flag==0){
					Controller.flag=1;
					f=1;
				}
			}
		}else if(f==1){
			bool isClicked_tutorial2=GUI.Button(rect," すべての指でギュッとしてね！",style);
			if(isClicked_tutorial2){
				Debug.Log("STAND BY READY!!");
				Application.LoadLevelAsync("mini_game");
			}
		}

		if(Controller.fr1>10&&Controller.fr2>10&&Controller.fr3>10&&Controller.fr4>10&&Controller.fr5>10
		   &&Controller.fl1>10&&Controller.fl2>10&&Controller.fl3>10&&Controller.fl4>10&&Controller.fl5>10&&Controller.flag==0){
			Controller.flag=1;
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("mini_game");
		}
		
		//back to 
		bool isClicked_back = GUI.Button(new Rect(0,0,160,30),"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevelAsync("tutorial00");
		}

		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevelAsync("mayukko");
		}
	}
}
