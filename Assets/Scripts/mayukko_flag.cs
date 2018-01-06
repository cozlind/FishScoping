using UnityEngine;
using System.Collections;

public class mayukko_flag : MonoBehaviour {
	public GUIStyle style;
	public int f;
	public float timer=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
	}

	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
		
		Rect rect = new Rect (0,sh-sh/8,sw-100,sh/8);
		style.fontSize=sh/15;
		style.normal.textColor=Color.white;
		style.alignment=TextAnchor.MiddleLeft;
		
		bool isClicked_tutorial2=GUI.Button(rect," 動きを覚えてね！",style);
		if(isClicked_tutorial2){

		}

		if(timer>32f){
			bool isClicked_end=GUI.Button(rect," 動きを再現してみて！",style);
			if(isClicked_end){
				Application.LoadLevelAsync("game_end2");
			}
		}
		
		//back to 
		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main){
			Application.LoadLevelAsync("game_end2");
		}
		
		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevelAsync("right_or_left");
		}

	}
}
