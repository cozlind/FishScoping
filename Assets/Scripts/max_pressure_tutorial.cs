using UnityEngine;
using System.Collections;
using System.IO;

public class max_pressure_tutorial : MonoBehaviour {
	public GUIStyle style;
	public GUIStyle tutorial;
	public float timer=0;
	public float data_time =0;
	private AudioSource sound01;
	private AudioSource sound02;
	private AudioSource sound03;
	public int f;
	// Use this for initialization
	void Start () {

		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
		sound03 = audioSources[2];
		sound01.PlayOneShot(sound01.clip);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		data_time = data_time+Time.deltaTime;

		if(f==0&&timer>8.5f){
			sound02.PlayOneShot(sound02.clip);
			f=1;
		}else if(f==1&&timer>11.0f){
			sound03.PlayOneShot(sound03.clip);
			f=2;
		}
	}
	
	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
		Rect rect = new Rect (0,sh-sh/8,sw-100,sh/8);

		tutorial.fontSize=sh/18;
		tutorial.normal.textColor=Color.white;
		tutorial.alignment=TextAnchor.UpperLeft;
		Rect field = new Rect(sw/7,sh/2,sw/2,sh/8);
		Rect field2 = new Rect(sw/9,sh/2.1f,sw/2,sh/2.5f);
		GUI.Box(field2,"");
		GUI.Box(field,"全ての指の力を\n２回測るよ\n\n下の青枠の指示に従って\n力一杯コントローラを握ってね！\n\n",tutorial);

		style.fontSize=sh/15;
		style.normal.textColor=Color.white;
		style.alignment=TextAnchor.MiddleLeft;
		bool isClicked_tutorial1 = GUI.Button(rect, " 全部の指が反応することを確認してね！ ",style);
		if (isClicked_tutorial1){
			if(Controller.flag==0){
				Application.LoadLevelAsync("step6");
				Controller.flag=1;
			}
		}

		//back to 
		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "終了");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("step6");
		}
		
		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevelAsync("mayukko");
		}
	}
}
