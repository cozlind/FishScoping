using UnityEngine;
using System.Collections;
using System.IO;

public class step4 : MonoBehaviour {
	public GUIStyle style;
	public float timer=0;
	public float data_time=0;
	private AudioSource sound01;
	private AudioSource sound02;
	private AudioSource sound03;
	public int f;
	// Use this for initialization
	void Start () {
		FileStream f1 = new FileStream("Assets/mayukko_data/data.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer1 = new StreamWriter(f1);
		writer1.WriteLine("step4");
		writer1.Close();
		FileStream f2 = new FileStream("Assets/mayukko_data/motion.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer2 = new StreamWriter(f2);
		writer2.WriteLine("step4");
		writer2.Close();
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
		FileStream f1 = new FileStream("Assets/mayukko_data/data.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer1 = new StreamWriter(f1);
		if (timer<18.2f&&data_time>0.1){
			writer1.WriteLine(Controller.ax+","+Controller.ay+","+Controller.az+","+Controller.gx+","+Controller.gy+","+Controller.gz+","+Controller.cx+","+Controller.cy+","+Controller.cz
			                  +","+Controller.fr1+","+Controller.fr2+","+Controller.fr3+","+Controller.fr4+","+Controller.fr5
			                  +","+Controller.fl1+","+Controller.fl2+","+Controller.fl3+","+Controller.fl4+","+Controller.fl5+","+timer.ToString());
		}writer1.Close();
		FileStream f2 = new FileStream("Assets/mayukko_data/motion.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer2 = new StreamWriter(f2);
		if (timer<18.2f&&data_time>0.1){
			writer2.WriteLine(Controller.pitch+","+Controller.roll+","+Controller.yaw+","+timer.ToString());
			data_time=0;
		}writer2.Close();
		if(f==0&&timer>7.5f){
			sound02.PlayOneShot(sound02.clip);
			f=1;
		}else if(f==1&&timer>8.0f){
			//sound03.PlayOneShot(sound03.clip);
			f=2;
		}else if(f==2&&timer>16.0f){
			sound03.PlayOneShot(sound03.clip);
			f=3;
		}
	}
	
	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
		Rect rect = new Rect (0,sh-sh/8,sw-100,sh/8);
		style.fontSize=sh/15;
		style.normal.textColor=Color.white;
		style.alignment=TextAnchor.MiddleLeft;
		bool isClicked_tutorial1 = GUI.Button(rect, " ステップ 4 ",style);
		if (isClicked_tutorial1||timer>22.0f&&Controller.gx<100&&Controller.gy<100&&Controller.gz<100){
			if(Controller.flag==0){
				Application.LoadLevelAsync("step5");
				Controller.flag=1;
			}
		}

		//back to 
		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "終了");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("max_pressure_tutorial");
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
