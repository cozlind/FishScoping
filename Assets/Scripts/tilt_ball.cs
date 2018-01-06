using UnityEngine;
using System.Collections;
using System.IO;

public class tilt_ball : MonoBehaviour {
	public float timer=0;
	public float data_time = 0;
	public int f;
	public GUIStyle tutorial;
    Rigidbody rigidbody;
    void Start () {
        rigidbody = GetComponent<Rigidbody> ();
        if (Application.loadedLevelName=="tilt_ball"){
			Controller.gui_set=1;
			Controller.score=0;
			Controller.timer=0;
			FileStream f1 = new FileStream("Assets/mayukko_data/tilt_ball_data.csv",FileMode.Append,FileAccess.Write);
			StreamWriter writer1 = new StreamWriter(f1);
			writer1.WriteLine("accX,accY,accZ,gyrX,gyrY,gyrZ,cmpX,cmpY,cmpZ,fr1,fr2,fr3,fr4,fr5,fl1,fl2,fl3,fl4,fl5,time");
			writer1.WriteLine("left_to_right");
			writer1.Close();
			FileStream f2 = new FileStream("Assets/mayukko_data/tilt_ball_motion.csv",FileMode.Append,FileAccess.Write);
			StreamWriter writer2 = new StreamWriter(f2);
			writer2.WriteLine("pitch,roll,yaw,time");
			writer2.Close();
			FileStream f3 = new FileStream("Assets/mayukko_data/tilt_ball_time.csv",FileMode.Append,FileAccess.Write);
			StreamWriter writer3 = new StreamWriter(f3);
			writer3.Close();
		}else Controller.gui_set=0;
	}

	void OnTriggerEnter(Collider col){
		if(Application.loadedLevelName=="tilt_ball"){
			if(col.gameObject.tag == "hole"){
                col.isTrigger=true;
			}else if(col.gameObject.tag == "destroy"){
				Vector3 rebirth;
				Controller.score=Controller.score+100;
				if(Controller.score==100){
					rebirth.x = (float)1.4;
					rebirth.y = 0.5f;
					rebirth.z = -(float)3.4;
					transform.position = rebirth;
				}else if(Controller.score==200){
					rebirth.x = (float)2.4;
					rebirth.y = 0.5f;
					rebirth.z = (float)3.4;
					transform.position = rebirth;
				}
                col.isTrigger=false;
				//Destroy(gameObject);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		rigidbody.WakeUp();
		if(Application.loadedLevelName=="tilt_ball"){
			timer += Time.deltaTime; 
			data_time = data_time +Time.deltaTime;
			FileStream f1 = new FileStream("Assets/mayukko_data/tilt_ball_data.csv",FileMode.Append,FileAccess.Write);
			StreamWriter writer1 = new StreamWriter(f1);
			writer1.WriteLine(Controller.ax+","+Controller.ay+","+Controller.az+","+Controller.gx+","+Controller.gy+","+Controller.gz+","+Controller.cx+","+Controller.cy+","+Controller.cz
				               +","+Controller.fr1+","+Controller.fr2+","+Controller.fr3+","+Controller.fr4+","+Controller.fr5
				               +","+Controller.fl1+","+Controller.fl2+","+Controller.fl3+","+Controller.fl4+","+Controller.fl5+","+timer.ToString());
			writer1.Close();
			
			FileStream f2 = new FileStream("Assets/mayukko_data/tilt_ball_motion.csv",FileMode.Append,FileAccess.Write);
			StreamWriter writer2 = new StreamWriter(f2);
			writer2.WriteLine(Controller.pitch+","+Controller.roll+","+Controller.yaw+","+timer.ToString());
			writer2.Close();
			
			FileStream f3 = new FileStream("Assets/mayukko_data/tilt_ball_time.csv",FileMode.Append,FileAccess.Write);
			StreamWriter writer3 = new StreamWriter(f3);
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
			}writer3.Close();
		}
	}

	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
			if(Application.loadedLevelName=="tilt_ball_tutorial"){
			tutorial.fontSize=sh/18;
			tutorial.normal.textColor=Color.white;
			tutorial.alignment=TextAnchor.UpperLeft;
			Rect field = new Rect(sw/3,sh/7,sw/2,sh/8);
			Rect field2 = new Rect(sw/4,sh/8,sw/2,sh/2.5f);
			GUI.Box(field2,"");
			GUI.Box(field,"コントローラを傾けて\nボールを中心の穴に\n移動させてね\n\n\n\n",tutorial);
		}
		if (Controller.score>200){
			Debug.Log("STAND BY READY!!");
			Controller.score=0;
			Application.LoadLevelAsync("game_end2");
		}
		bool isCricked_reset= GUI.Button(new Rect(0,sh-sh/8,sh/7,sh/8),"リセット");
		if(isCricked_reset){
			Debug.Log("GAME RESET");
			Vector3 posi;
			posi.x = -3.5f;
			posi.y = 0.5f;
			posi.z = -3.4f;
			transform.position = posi;
		}

		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main){
			if(Application.loadedLevelName=="tilt_ball_tutorial"){
				Controller.timer=0;
				Application.LoadLevelAsync("tilt_ball");
			}
			else if(Application.loadedLevelName=="tilt_ball"){
				Controller.timer=0;
				Application.LoadLevelAsync("game_end2");
			}
		}
		
		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevelAsync("mono_catch");
		}
	}
}
