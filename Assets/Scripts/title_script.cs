using UnityEngine;
using System.Collections;
using System.Data;
using System.Text;
using System.Linq;

using System.IO.Ports; 
using System.IO;
using System.Timers;

using System.Threading;//スレッド用

public class title_script : MonoBehaviour {
	public GUIStyle style;
	public GUIStyle mayumi;
	private int f;
	private AudioSource sound01;
	private AudioSource sound02;
	public GameObject capsule;
	void Start () {
		capsule = GameObject.Find("Capsule");
		f=0;
		//AudioSourceコンポーネントを取得し、変数に格納
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
		//sound01.PlayOneShot(sound01.clip);	
		sound01.Play();	
	}
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
		Rect rect = new Rect (0,sh-sh/8,sw,sh/8);
		style.fontSize=sh/15;
		style.normal.textColor=Color.white;
		style.alignment=TextAnchor.MiddleLeft;
		if(f==0){
			bool isClicked_main = GUI.Button(rect, " コントローラを持ってね！",style);
			if (isClicked_main||Controller.gx>100&&Controller.flag==0){
				//sound02.PlayOneShot(sound02.clip);
				sound01.Stop();
				sound02.Play();
				Controller.flag=1;
				f=1;
			}
		}else if(f==1){
			bool isClicked_2=GUI.Button(rect," 親指をギュッと押してゲームスタート！！",style);
			if(isClicked_2||Controller.fl1>50||Controller.fr1>50){
				Controller.flag=1;
				Debug.Log("STAND BY READY!!");
				Application.LoadLevelAsync("user_select");
			}
		}

		bool isClicked_back = GUI.Button(new Rect(0,0,160,30),"ミニゲーム");
		if(isClicked_back){
			Debug.Log("mini game");
			Application.LoadLevelAsync("mini_game");
		}
		bool isCricked_reset= GUI.Button(new Rect(0,30,160,30),"トレーニング");
		if(isCricked_reset){
			Debug.Log("mayukko");
			//Object.Destroy(Capsule);
			Application.LoadLevelAsync("mayukko");
		}
		bool isCricked_ecl= GUI.Button(new Rect(0,60,160,30),"ECL");
		if(isCricked_ecl){
			Debug.Log("ECL_training");
			//Object.Destroy(Capsule);
			Application.LoadLevelAsync("mono_idou_tutorial");
			//Destroy(capsule);
		}

		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevelAsync("mayukko");
		}
	}
}
