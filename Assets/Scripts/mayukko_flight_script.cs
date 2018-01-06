using UnityEngine;
using System.Collections;
using System.Data;

public class mayukko_flight_script : MonoBehaviour {
	public GUIStyle style;
	public float timer=0;
	public static int score = 0;
	public static float bird_time = 0;
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
		GUI.TextArea(new Rect(sw-sw/4,10,sw/6,sh/8),"点数  : "+score.ToString(),style);
		style.normal.textColor=(Color.blue);
		GUI.TextArea(new Rect(0,sh/8,sw/6,sh/8),"捕獲時間  : "+bird_time.ToString(),style);

		//go to mayukko
		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if(Application.loadedLevelName=="mayukko_catch"){
			if(isClicked_main||timer>30)Application.LoadLevel("mayukko_flight");
		}else if(Application.loadedLevelName=="mayukko_flight"){
			if(isClicked_main||timer>30)Application.LoadLevel("mayukko_flight_catch");
		}else if(Application.loadedLevelName=="mayukko_flight_catch"){
			if(isClicked_main||timer>60)Application.LoadLevel("game_end");
		}
		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(Application.loadedLevelName=="mayukko_flight"){
			if(isClicked_back)Application.LoadLevel("mayukko_catch");
		}else if(Application.loadedLevelName=="mayukko_flight_catch"){
			if(isClicked_back)Application.LoadLevel("mayukko_catch");
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
