using UnityEngine;
using System.Collections;
using System.Data;

public class mini_game_tutorial : MonoBehaviour {
	public GUIStyle style;
	private int f;
	private AudioSource sound01;
	private AudioSource sound02;
	private AudioSource sound03;
	public float timer=0;
	void Start(){
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
		sound03 = audioSources[2];
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
		style.alignment=TextAnchor.MiddleLeft;
		style.normal.textColor=(Color.red);
		GUI.Box(new Rect(0,0,sw/8,sh/10)," 説明中",style);

		style.normal.textColor=Color.white;
		if(f==0){
			bool isClicked_tuto1 = GUI.Button(rect, " コントローラを動かしてモノを掴んでね",style);
			if (isClicked_tuto1||timer>8.0f){
				sound02.PlayOneShot(sound02.clip);
				f=1;
			}
		}else if(f==1){
			bool isClicked_tuto2=GUI.Button(rect," 掴みながら移動して真ん中の穴に落としてね",style);
			if(isClicked_tuto2||timer>16.0f){
				sound03.PlayOneShot(sound03.clip);
				f=2;
			}
		}else if(f==2){
			bool isClicked_tuto2=GUI.Button(rect," 親指をギュッと押してスタート",style);
			if(isClicked_tuto2||Controller.fr1>10&&Controller.fr2<10&&Controller.fr3<10&&Controller.fr4<10&&Controller.fr5<10
			   &&Controller.fl1>10&&Controller.fl2<10&&Controller.fl3<10&&Controller.fl4<10&&Controller.fl5<10){
				Debug.Log("STAND BY READY!!");
				Application.LoadLevel("mini_game");
			}
		}

		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Application.LoadLevel("mini_game");
		}
		
		//back to 
		Rect rect_back = new Rect(sw - 100,sh-sh/8,50,sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevel("title");
		}
		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevel("mayukko");
		}
	}
}
