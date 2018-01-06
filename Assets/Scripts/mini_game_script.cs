using UnityEngine;
using System.Collections;
using System.Data;

public class mini_game_script : MonoBehaviour {
	public GUIStyle style;
	public float timer=0;
	public static int score = 0;
	void Start(){

	}
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; 
		//score=(int)((float)score-Time.deltaTime);
		//score=score-(int)timer;
		//score=(int)score/100;
	}

	//Button
	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
		style.fontSize=sh/15;
		style.alignment=TextAnchor.MiddleLeft;
		style.normal.textColor=(Color.green);
		GUI.TextArea(new Rect(0,0,sw/5,sh/8),"時間 :  " + timer.ToString(),style);
		style.normal.textColor=(Color.blue);
		GUI.TextArea(new Rect(sw-sw/5,10,sw/6,sh/8),"点数  : "+score.ToString(),style);

		//go to mayukko
		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main||score>200){
			Debug.Log("STAND BY READY!!");
			score=0;
			Application.LoadLevel("mayukko");
		}
		
		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevel("mini_game_tutorial");
		}
		bool isCricked_reset= GUI.Button(new Rect(0,sh-sh/8,sh/7,sh/8),"リセット");
		if(isCricked_reset){
			Debug.Log("GAME RESET");
			score=0;
			timer=0;
		}
		
		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevel("mayukko");
		}
	}
}
