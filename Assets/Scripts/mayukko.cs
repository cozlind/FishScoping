using UnityEngine;
using System.Collections;
using System.IO;

public class mayukko : MonoBehaviour {
	public GUIStyle style;
	public int f;
	public float timer=0;
	private AudioSource sound01;
	private AudioSource sound02;

	// Use this for initialization
	void Start () {
		f=0;
		FileStream f1 = new FileStream("Assets/mayukko_data/data.csv",FileMode.Create,FileAccess.Write);
		StreamWriter writer1 = new StreamWriter(f1);
		writer1.WriteLine("accX,accY,accZ,gyrX,gyrY,gyrZ,cmpX,cmpY,cmpZ,fr1,fr2,fr3,fr4,fr5,fl1,fl2,fl3,fl4,fl5,time");
		writer1.Close();
		FileStream f2 = new FileStream("Assets/mayukko_data/motion.csv",FileMode.Create,FileAccess.Write);
		StreamWriter writer2 = new StreamWriter(f2);
		writer2.WriteLine("pitch,roll,yaw,time");
		writer2.Close();
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
		sound01.PlayOneShot(sound01.clip);
		Controller.timer=0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(f==0&&timer>6.0f){
			sound02.PlayOneShot(sound02.clip);
			f=1;
		}else if(f==1&&Controller.fr1>10&&Controller.fr2>10&&Controller.fr3>10&&Controller.fr4>10&&Controller.fr5>10
		         &&Controller.fl1>10&&Controller.fl2>10&&Controller.fl3>10&&Controller.fl4>10&&Controller.fl5>10){

			Application.LoadLevelAsync("step1");
		}
	}

	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
	
		Rect rect = new Rect (0,sh-sh/8,sw-100,sh/8);
		style.fontSize=sh/15;
		style.normal.textColor=Color.white;
		style.alignment=TextAnchor.MiddleLeft;

		bool isClicked_tutorial2=GUI.Button(rect," 膝の上ですべての指をギュッとしてスタート！",style);
		if(isClicked_tutorial2){
			Debug.Log("training");
			Application.LoadLevelAsync("step1");
		}

		//back to 
		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("step1");
		}
		
		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevelAsync("tutorial03");
		}

		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevelAsync("mayukko");
		}
	}
}
