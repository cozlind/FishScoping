using UnityEngine;
using System.Collections;
using System.Data;

public class user_script : MonoBehaviour {
	public GUIStyle style;
	private int s,f;
	private Rect cursol = new Rect(0,0,0,0);
	private AudioSource sound01;
	private AudioSource sound02;

	// Use this for initialization
	void Start () {
		s=1;
		f=0;
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
		sound01.PlayOneShot(sound01.clip);
	}
	
	// Update is called once per frame
	void Update () {

	}

	//Button
	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
		Rect rect = new Rect (0,sh-sh/8,sw-100,sh/8);
		style.fontSize=sh/15;
		style.normal.textColor=Color.white;
		style.alignment=TextAnchor.MiddleLeft;
		if(f==0){
			bool isClicked_user1 = GUI.Button(rect, " 人差し指で選んでね！",style);
			if (isClicked_user1||s>2){
				sound01.Stop();
				sound02.PlayOneShot(sound02.clip);
				f=1;
			}
		}else if(f==1){
			bool isClicked_user2=GUI.Button(rect," 右の親指で決定！左の親指でタイトルに戻るよ！",style);
			if(isClicked_user2){
				Debug.Log("STAND BY READY!!");
				Application.LoadLevelAsync("tutorial00");
			}
		}
		if(Controller.fr1>70&&Controller.flag==0){
			Controller.flag=1;
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("tutorial00");
		}
		//back to 
		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("tutorial00");
		}

		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevelAsync("title");
		}

		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevelAsync("mayukko");
		}
		bool sign = GUI.Button(new Rect((int)(sw*0.02),(int)(sh*0.08),(int)(sw*0.14),(int)(sh/4)), "新規登録");
		if(sign){
			Application.LoadLevelAsync("sign_up");
			Debug.Log("sign_up");
		}
		//select window
		GUI.color = Color.blue;
		if(Controller.fl2>100&&s>1&&Controller.flag==0){s--;Controller.flag=1;}
		if(Controller.fr2>100&&s<4&&Controller.flag==0){s++;Controller.flag=1;}
		if(s==1)cursol = new Rect((int)(sw*0.18),(int)(sh*0.08),(int)(sw*0.14),(int)(sh/4));
		if(s==2)cursol = new Rect((int)(sw*0.35),(int)(sh*0.08),(int)(sw*0.14),(int)(sh/4));
		if(s==3)cursol = new Rect((int)(sw*0.51),(int)(sh*0.08),(int)(sw*0.14),(int)(sh/4));
		if(s==4)cursol = new Rect((int)(sw*0.67),(int)(sh*0.08),(int)(sw*0.14),(int)(sh/4));
		bool isClicked = GUI.Button(cursol, "");
		if (isClicked){
			Application.LoadLevelAsync("tutorial00");
			Debug.Log ("select user");
		}

	}
}