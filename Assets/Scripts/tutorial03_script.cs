using UnityEngine;
using System.Collections;
using System.Data;

public class tutorial03_script : MonoBehaviour {
	public GUIStyle style;
	private int f;
	private AudioSource sound01;
	private AudioSource sound02;
	public float timer=0;
	void Start(){
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
		sound01.PlayOneShot(sound01.clip);
		f=0;
	}
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; 
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
			bool isClicked_tuto1 = GUI.Button(rect, " 体を中心に腕を回すようにうごかしてね",style);
			if (isClicked_tuto1||timer>10.0f){
				sound01.Stop();
				sound02.PlayOneShot(sound02.clip);
				f=1;
			}
		}else if(f==1){
			bool isClicked_tuto2=GUI.Button(rect," 膝の上でコントローラをギュッとにぎってね",style);
			if(isClicked_tuto2){
				Debug.Log("STAND BY READY!!");
				Application.LoadLevelAsync("mayukko");
			}
		}
		//if(Controller.flag==0){
		if(timer>2.0f){
		if(Controller.fr1>10&&Controller.fr2>10&&Controller.fr3>10&&Controller.fr4>10&&Controller.fr5>10
		   &&Controller.fl1>10&&Controller.fl2>10&&Controller.fl3>10&&Controller.fl4>10&&Controller.fl5>10){
			Controller.flag=1;
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("mayukko");
		}
		}
		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Application.LoadLevelAsync("mayukko");
		}
		
		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevelAsync("tutorial02");
		}
		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevelAsync("mayukko");
		}
	}
}
