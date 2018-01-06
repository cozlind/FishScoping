using UnityEngine;
using System.Collections;
using System.Data;
using System.IO;

public class mono_idou_script : MonoBehaviour {
	public GUIStyle style;
	public float timer=0;
	public float data_time = 0;
	public int f;
	void Start(){
		if(Application.loadedLevelName=="mono_idou"||Application.loadedLevelName=="mono_idou_2"){
			Controller.gui_set=1;
			Controller.score=0;
			Controller.timer=0;
		}else Controller.gui_set=0;

		FileStream f1 = new FileStream("Assets/mayukko_data/mono_idou_data.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer1 = new StreamWriter(f1);
		writer1.WriteLine("accX,accY,accZ,gyrX,gyrY,gyrZ,cmpX,cmpY,cmpZ,fr1,fr2,fr3,fr4,fr5,fl1,fl2,fl3,fl4,fl5,time");
		if(Application.loadedLevelName=="mono_idou")writer1.WriteLine("left_to_right");
		else if(Application.loadedLevelName=="mono_idou_2")writer1.WriteLine("right_to_left");
		writer1.Close();
		FileStream f2 = new FileStream("Assets/mayukko_data/mono_idou_motion.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer2 = new StreamWriter(f2);
		writer2.WriteLine("pitch,roll,yaw,time");
		if(Application.loadedLevelName=="mono_idou")writer2.WriteLine("left_to_right");
		else if(Application.loadedLevelName=="mono_idou_2")writer2.WriteLine("right_to_left");
		writer2.Close();
		FileStream f3 = new FileStream("Assets/mayukko_data/mono_idou_time.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer3 = new StreamWriter(f3);
		if(Application.loadedLevelName=="mono_idou")writer3.WriteLine("left_to_right");
		else if(Application.loadedLevelName=="mono_idou_2")writer3.WriteLine("right_to_left");
		writer3.Close();
	}
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; 
		if(Application.loadedLevelName=="mono_idou"||Application.loadedLevelName=="mono_idou_2"){
			data_time = data_time +Time.deltaTime;
		}
		FileStream f1 = new FileStream("Assets/mayukko_data/mono_idou_data.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer1 = new StreamWriter(f1);
		if (Application.loadedLevelName=="mono_idou"||Application.loadedLevelName=="mono_idou_2"){
			writer1.WriteLine(Controller.ax+","+Controller.ay+","+Controller.az+","+Controller.gx+","+Controller.gy+","+Controller.gz+","+Controller.cx+","+Controller.cy+","+Controller.cz
			                  +","+Controller.fr1+","+Controller.fr2+","+Controller.fr3+","+Controller.fr4+","+Controller.fr5
			                  +","+Controller.fl1+","+Controller.fl2+","+Controller.fl3+","+Controller.fl4+","+Controller.fl5+","+timer.ToString());
		}writer1.Close();

		FileStream f2 = new FileStream("Assets/mayukko_data/mono_idou_motion.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer2 = new StreamWriter(f2);
		if (Application.loadedLevelName=="mono_idou"||Application.loadedLevelName=="mono_idou_2"){
			writer2.WriteLine(Controller.pitch+","+Controller.roll+","+Controller.yaw+","+timer.ToString());
		}writer2.Close();

		FileStream f3 = new FileStream("Assets/mayukko_data/mono_idou_time.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer3 = new StreamWriter(f3);
		if(Application.loadedLevelName=="mono_idou"||Application.loadedLevelName=="mono_idou_2"){
			if(Controller.score==100&&f==0){
				writer3.WriteLine(data_time.ToString());
				data_time=0;
				f=1;
			}else if (Controller.score==200&&f==1){
				writer3.WriteLine(data_time.ToString());
				data_time=0;
				f=2;
			}else if(Controller.score>200&&f==2){
				writer3.WriteLine(data_time.ToString());
				data_time=0;
				f=3;
			}else if(Controller.score==100&&f==3){
				writer3.WriteLine(data_time.ToString());
				data_time=0;
				f=4;
			}else if(Controller.score==200&&f==4){
				writer3.WriteLine(data_time.ToString());
				data_time=0;
				f=5;
			}else if(Controller.score>200&&f==5){
				writer3.WriteLine(data_time.ToString());
				data_time=0;
				f=6;
			}
		}writer3.Close();
	}

	//Button
	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;

		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Controller.score=0;
			if(Application.loadedLevelName=="mono_idou")Application.LoadLevelAsync("mono_idou_2");
			else if(Application.loadedLevelName=="mono_idou_2")Application.LoadLevelAsync("mono_catch_tutorial");
		}

		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevelAsync("mono_idou_tutorial");
		}
		bool isCricked_reset= GUI.Button(new Rect(0,sh-sh/8,sh/7,sh/8),"リセット");
		if(isCricked_reset){
			Debug.Log("GAME RESET");
			Controller.score=0;
			Controller.timer=0;
		}
		
		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevelAsync("mayukko");
		}
	}
}
