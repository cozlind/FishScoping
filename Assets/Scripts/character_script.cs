using UnityEngine;
using System.Collections;
using System.Data;

public class character_script : MonoBehaviour {
	//public GUIStyleState styleState;
	//private GUIStyle style;
	void Start(){
		//style = new GUIStyle ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	//Button
	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
		Rect user = new Rect(10,10,400,50);
		GUI.Label (user, "SELECT CHARACTER");

		//sselect1
		Rect rect = new Rect(220, 60, 180, 180);
		bool isClicked_start = GUI.Button(rect, "");
		if (isClicked_start){
			Application.LoadLevel("tutorial0");
			Debug.Log ("select CHARACTER");
		}

		//select2
		Rect rect1 = new Rect (433,60,180,180);
		bool isClicked_stop = GUI.Button(rect1, "");
		if (isClicked_stop){
			Application.LoadLevel("tutorial0");
			Debug.Log ("select CHARACTER");
		}

		//select3
		Rect rect2 = new Rect (640,60,180,180);
		bool isClicked_get_start = GUI.Button(rect2, "");
		if (isClicked_get_start){
			Application.LoadLevel("tutorial0");
			Debug.Log ("select CHARACTER");
		}

		//select4
		Rect rect3 = new Rect (860,60,180,180);
		bool isClicked_get_end = GUI.Button(rect3, "");
		if (isClicked_get_end){
			Application.LoadLevel("tutorial0");
			Debug.Log ("select CHARACTER");
		}

		//go to mayukko
        Rect rect6 = new Rect(sw - 160, sh - 60, 150, 50);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main||Controller.fr1>100&&Controller.flag==0){
			Controller.flag=1;
			Debug.Log("STAND BY READY!!");
			Application.LoadLevel("tutorial0");
		}		

		//back to 
        Rect rect_back = new Rect(sw - 330, sh - 60, 150, 50);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back||Controller.fl1>100&&Controller.flag==0){
			Controller.flag=1;
			Debug.Log("BACK");
			Application.LoadLevel("user_select");
		}

		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevel("mayukko");
		}
	}
}
