using UnityEngine;
using System.Collections;
using System.Data;

using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
/* 運動評価 */
using System.IO;/* ファイルの読み込みに */

public class mono_idou_tutorial : MonoBehaviour {
	public int point;
	public int f;
	public GUIStyle style;
	private AudioSource sound01;
	private AudioSource sound02;
	public float timer=0;
	// Use this for initialization
	void Start () {
		//FileStream file = new FileStream("Assets/motion.csv",FileMode.Open,FileAccess.Read);
	
		//point=87;
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
		sound01.PlayOneShot(sound01.clip);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; 
		if(f==0&&timer>5.5f){
			sound02.PlayOneShot(sound02.clip);
			f=1;
		}
	}

	//Button
	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;

		style.fontSize=sh/18;
		style.normal.textColor=Color.white;
		style.alignment=TextAnchor.UpperLeft;


		Rect field = new Rect(sw/20,sh/2,sw/2,sh/8);
		Rect field2 = new Rect(sw/25,sh/2.1f,sw/2,sh/2.5f);
		GUI.Box(field2,"");
		//if (point == 0)	point = 57;
		GUI.Box(field,"キャッチャーを動かして\nボールをつかんでね\n\nつかみながら移動して\nボールを穴に落としてね！\n左から右と右から左があるよ\n",style);

		//go to mayukko
		Rect rect6 = new Rect(0, 0,150, sh/9);
		bool isClicked_main = GUI.Button(rect6, "はじめにもどる");
		if (isClicked_main){
			Debug.Log("STAND BY READY!!");
			Application.LoadLevel("title");
		}

		Rect rect_game = new Rect(sw - 150, sh - sh/8,150, sh/8);
		bool isClicked_game = GUI.Button(rect_game, "トレーニングに進む");
		if (isClicked_game){
			Application.LoadLevel("mono_idou");
		}

		//back to 
		Rect rect_back = new Rect(sw - 300, sh - sh/8, 150, sh/8);
		bool isClicked_back = GUI.Button(rect_back,"もどる");
		if(isClicked_back){
			Debug.Log("BACK");
			Application.LoadLevel("reaction_fingaer_tutorial");
		}

		//go to main
		if(Input.GetButton("Jump")) {
			Application.LoadLevel("mayukko");
		}
	}
}

