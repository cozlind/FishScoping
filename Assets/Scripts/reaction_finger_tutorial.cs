using UnityEngine;
using System.Collections;
using System.IO;

public class reaction_finger_tutorial : MonoBehaviour {
	public GUIStyle style;
	public GUIStyle tutorial;
	public float timer=0;
	public float data_time =0;
	public float reaction_time = 0;
	public static int f;
	// Use this for initialization
	void Start () {
		f=0;
		FileStream f1 = new FileStream("Assets/mayukko_data/reaction.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer1 = new StreamWriter(f1);
		writer1.WriteLine("fr1,fr2,fr3,fr4,fr5,fl1,fl2,fl3,fl4,fl5,f,time,timer");
		writer1.Close();
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		//data_time = data_time+Time.deltaTime;
		if(Application.loadedLevelName=="reaction_finger"){
			FileStream f1 = new FileStream("Assets/mayukko_data/reaction.csv",FileMode.Append,FileAccess.Write);
			StreamWriter writer1 = new StreamWriter(f1);
			writer1.WriteLine(Controller.fr1+","+Controller.fr2+","+Controller.fr3+","+Controller.fr4+","+Controller.fr5
			                  +","+Controller.fl1+","+Controller.fl2+","+Controller.fl3+","+Controller.fl4+","+Controller.fl5+","+f.ToString()+","+reaction_time.ToString()+","+timer.ToString());
			writer1.Close();
			if(timer>5.0f&&timer<10.0f){
				if(f!=61){
					f=6;
					if(Controller.fr1>10){
						f=61;
						reaction_time=data_time;
						data_time=0;
					}else data_time = data_time+Time.deltaTime;
				}
			}else if(timer>10.0f&&timer<15.0f){
				if(f!=41){
					f=4;
					if(Controller.fl4>10){
						f=41;
						reaction_time=data_time;
						data_time=0;
					}else data_time = data_time+Time.deltaTime;
				}
			}else if(timer>15.0f&&timer<20.0f){
				if(f!=31){
					f=3;
					if(Controller.fl3>10){
						f=31;
						reaction_time=data_time;
						data_time=0;
					}else data_time = data_time+Time.deltaTime;
				}
			}else if(timer>20.0f&&timer<25.0f){
				if(f!=71){
					f=7;
					if(Controller.fr2>10){
						f=71;
						reaction_time=data_time;
						data_time=0;
					}else data_time = data_time+Time.deltaTime;
				}
			}else if(timer>25.0f&&timer<30.0f){
				if(f!=91){
					f=9;
					if(Controller.fr4>10){
						f=91;
						reaction_time=data_time;
						data_time=0;
					}else data_time = data_time+Time.deltaTime;
				}
			}else if(timer>30.0f&&timer<35.0f){
				if(f!=101){
					f=10;
					if(Controller.fr5>10){
						f=101;
						reaction_time=data_time;
						data_time=0;
					}else data_time = data_time+Time.deltaTime;
				}
			}else if(timer>35.0f&&timer<40.0f){
				if(f!=21){
					f=2;
					if(Controller.fl2>10){
						f=21;
						reaction_time=data_time;
						data_time=0;
					}else data_time = data_time+Time.deltaTime;
				}
			}else if(timer>40.0f&&timer<45.0f){
				if(f!=51){
					f=5;
					if(Controller.fl5>10){
						f=51;
						reaction_time=data_time;
						data_time=0;
					}else data_time = data_time+Time.deltaTime;
				}
			}else if(timer>45.0f&&timer<50.0f){
				if(f!=81){
					f=8;
					if(Controller.fr3>10){
						f=81;
						reaction_time=data_time;
						data_time=0;
					}else data_time = data_time+Time.deltaTime;
				}
			}else if(timer>50.0f&&timer<55.0f){
				if(f!=11){
					f=1;
					if(Controller.fl1>10){
						f=11;
						reaction_time=data_time;
						data_time=0;
					}else data_time = data_time+Time.deltaTime;
				}
			}else if(timer>55.0f&&timer<60.0f){
				f=0;
			}
		}else {
			if(timer>5.0f&&timer<10.0f){
				if(f!=11)f=2;
			}else if(timer>10.0f&&timer<15.0f){
				if(f!=11)f=4;
			}else if(timer>15.0f&&timer<20.0f){
				if(f!=11)f=8;
			}else if(timer>20.0f&&timer<25.0f){
				if(f!=11)f=3;
			}else if(timer>25.0f&&timer<30.0f){
				if(f!=11)f=10;
			}else if(timer>30.0f&&timer<35.0f){
				if(f!=11)f=1;
			}else if(timer>35.0f&&timer<40.0f){
				if(f!=11)f=5;
			}else if(timer>40.0f&&timer<45.0f){
				if(f!=11)f=9;
			}else if(timer>45.0f&&timer<50.0f){
				if(f!=11)f=7;
			}else if(timer>50.0f&&timer<55.0f){
				if(f!=11)f=6;
			}else if(timer>55.0f&&timer<60.0f){
				f=0;
				timer=0;
			}
		}
	}
	
	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
		Rect rect = new Rect (0,sh-sh/8,sw-100,sh/8);

		if(Application.loadedLevelName=="reaction_finger"){
			style.fontSize=sh/15;
			style.normal.textColor=Color.white;
			style.alignment=TextAnchor.MiddleLeft;
			bool isClicked_tutorial1 = GUI.Button(rect,"時間 :  " + reaction_time.ToString(),style);
			if (isClicked_tutorial1){
					Application.LoadLevelAsync("mono_idou_tutorial");
			}
		}else{
			tutorial.fontSize=sh/18;
			tutorial.normal.textColor=Color.white;
			tutorial.alignment=TextAnchor.UpperLeft;
			Rect field = new Rect(sw/3,sh/2,sw/2,sh/8);
			Rect field2 = new Rect(sw/4,sh/2.1f,sw/2,sh/2.5f);
			GUI.Box(field2,"");
			GUI.Box(field,"矢印がでた位置の指を\nできるだけ早く押してね！\n\n\n\n\n",tutorial);

			style.fontSize=sh/15;
			style.normal.textColor=Color.white;
			style.alignment=TextAnchor.MiddleLeft;
			bool isClicked_tutorial1 = GUI.Button(rect, " 練習してみてね！ ",style);
			if (isClicked_tutorial1){
				Application.LoadLevelAsync("reaction_finger");
			}
		}

		if(timer>65.0f){
			Application.LoadLevelAsync("mono_idou_tutorial");
		}

		//back to 
		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "終了");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("mono_idou_tutorial");
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
