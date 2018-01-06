using UnityEngine;
using System.Collections;
using System.IO;

public class right_or_left : MonoBehaviour {
	public GUIStyle style;
	public int f;
	public float timer=0;
	public Texture arrow_r;
	public Texture arrow_l;
	// Use this for initialization
	void Start () {
		f=0;

	}
	
	// Update is called once per frame
	void Update () {
		if(Random.Range(0,150)==0&&f==0){
			timer = 0;
			f=1;
		}else if(Random.Range(0,150)==1&&f==0){
			timer=0;
			f=2;
		}
	}

	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
	
		if(f==1){
			GUI.Label(new Rect(sw/2+sw/5,sh/2-sh/4,sw/8,sw/8),arrow_r);
			timer += Time.deltaTime;
			if(Controller.fr1>10){
				f=0;
				Controller.score=timer;
			}
		}else if(f==2){
			GUI.Label(new Rect(sw/2-sw/5-sw/8,sh/2-sh/4,sw/8,sw/8),arrow_l);
			timer += Time.deltaTime;
			if(Controller.fl1>10){
				f=0;
				Controller.score=timer;
			}
		}

		Rect rect = new Rect (0,sh-sh/8,sw-100,sh/8);
		style.fontSize=sh/15;
		style.normal.textColor=Color.white;
		style.alignment=TextAnchor.MiddleLeft;

		bool isClicked_tutorial2=GUI.Button(rect," 矢印が出た方向の親指を押してね！",style);
		if(isClicked_tutorial2){

		}

		//back to 
		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main||timer>30){
			Debug.Log("flag");
			Application.LoadLevelAsync("flag");
		}
		
		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("maze");
			Application.LoadLevelAsync("maze");
		}
	}
}
