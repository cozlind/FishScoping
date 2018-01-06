using UnityEngine;
using System.Collections;
using System.IO;

public class step6 : MonoBehaviour {
	public GUIStyle style;
	public float timer=0;
	public float data_time =0;
	public int f;
	// Use this for initialization
	void Start () {
		FileStream f1 = new FileStream("Assets/mayukko_data/max_pressure.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer1 = new StreamWriter(f1);
		writer1.WriteLine("max_pressure");
		writer1.Close();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		data_time = data_time+Time.deltaTime;
		FileStream f1 = new FileStream("Assets/mayukko_data/max_pressure.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer1 = new StreamWriter(f1);
		if (Application.loadedLevelName=="step6"){
			writer1.WriteLine(Controller.fr1+","+Controller.fr2+","+Controller.fr3+","+Controller.fr4+","+Controller.fr5
			                  +","+Controller.fl1+","+Controller.fl2+","+Controller.fl3+","+Controller.fl4+","+Controller.fl5+","+timer.ToString());
		}writer1.Close();
	}
	
	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
		Rect rect = new Rect (0,sh-sh/8,sw-100,sh/8);
		style.fontSize=sh/15;
		style.normal.textColor=Color.white;
		style.alignment=TextAnchor.MiddleLeft;

		if(f==0){
			bool isClicked_tuto1 = GUI.Button(rect, " 全部の指で握って！ ",style);
			if (isClicked_tuto1||timer>3.0f){
				f=1;
			}
		}else if(f==1){
			bool isClicked_tuto2=GUI.Button(rect," 緩めて～ ",style);
			if(isClicked_tuto2||timer>8.0f){
				f=2;
			}
		}else if(f==2){
			bool isClicked_tuto3=GUI.Button(rect," 全部の指で握って！ ",style);
			if(isClicked_tuto3||timer>11.0f){
				f=3;
			}
		}else if(f==3){
			bool isClicked_tuto4=GUI.Button(rect," 緩めて～  ",style);
			if(isClicked_tuto4||timer>15.0f){
				f=4;
				Application.LoadLevelAsync("reaction_finger_tutorial");
			}
		}

		//back to 
		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "終了");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("reaction_finger_tutorial");
		}
		
		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevelAsync("max_pressure_tutorial");
		}
	}
}
