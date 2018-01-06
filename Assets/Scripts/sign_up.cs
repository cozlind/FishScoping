using UnityEngine;
using System.Collections;
using System.IO;

public class sign_up : MonoBehaviour {
	public GUIStyle style;
	public GUIStyle style_text;
	public GUIStyle style_toggle;
	public GUIStyle style_toolbar;
	private string name = "";
	private bool toggleL1 = true;private bool toggleL2 = true;private bool toggleL3 = true;private bool toggleL4 = true;private bool toggleL5 = true;
	private bool toggleR1 = true;private bool toggleR2 = true;private bool toggleR3 = true;private bool toggleR4 = true;private bool toggleR5 = true;
	private int selected = 0;
	private string[] texts = new string[]{"易しい"," 普通 ","難しい"};
    GUITexture guiTexture;
	void Start () {
        guiTexture = GetComponent<GUITexture> ();

    }
	
	// Update is called once per frame
	void Update () {
		FileStream f = new FileStream("data/user.csv",FileMode.Append,FileAccess.Write);
		StreamWriter writer1 = new StreamWriter(f);

		writer1.WriteLine("");
		writer1.Close();
	}

	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;
		style.fontSize=sh/13;
		style.normal.textColor=Color.blue;
		style.alignment=TextAnchor.UpperLeft;
		guiTexture.pixelInset= new Rect(-sw/2,-sh/2,sw,sh);
		GUI.Label(new Rect(sw/16,sh/9,sw/2,sh/13),"名前を入力してね",style);
		style_text.fontSize = sh/10;
		name = GUI.TextField(new Rect(sw/12,sh/9+sh/12,sw/2,sh/9), name,style_text);
		GUI.Label(new Rect(sw/16,sh/3,sw/2,sh/13),"動かせる指を教えてね",style);
		style.fontSize = sh/15;
			GUI.Label(new Rect(sw/12,sh/3+sh/12,sw/23,sh/15),"左",style);
			style_toggle.fontSize = sh/17;
		toggleL1 = GUI.Toggle(new Rect(sw/12+sw/22,sh/3+sh/12,sw/23,sh/15),toggleL1,"     親指",style_toggle);
		toggleL2 = GUI.Toggle(new Rect(sw/12+sw/22+sw/9,sh/3+sh/12,sw/23,sh/15),toggleL2,"     人差",style_toggle);
		toggleL3 = GUI.Toggle(new Rect(sw/12+sw/22+sw/9*2,sh/3+sh/12,sw/23,sh/15),toggleL3,"     中指",style_toggle);
		toggleL4 = GUI.Toggle(new Rect(sw/12+sw/22+sw/9*3,sh/3+sh/12,sw/23,sh/15),toggleL4,"     薬指",style_toggle);
		toggleL5 = GUI.Toggle(new Rect(sw/12+sw/22+sw/9*4,sh/3+sh/12,sw/23,sh/15),toggleL5,"     小指",style_toggle);
			GUI.Label(new Rect(sw/12,sh/3+sh/12+sh/14,sw/23,sh/15),"右",style);
		toggleR1 = GUI.Toggle(new Rect(sw/12+sw/22,sh/3+sh/12+sh/14,sw/23,sh/15),toggleR1,"     親指",style_toggle);
		toggleR2 = GUI.Toggle(new Rect(sw/12+sw/22+sw/9,sh/3+sh/12+sh/14,sw/23,sh/15),toggleR2,"     人差",style_toggle);
		toggleR3 = GUI.Toggle(new Rect(sw/12+sw/22+sw/9*2,sh/3+sh/12+sh/14,sw/23,sh/15),toggleR3,"     中指",style_toggle);
		toggleR4 = GUI.Toggle(new Rect(sw/12+sw/22+sw/9*3,sh/3+sh/12+sh/14,sw/23,sh/15),toggleR4,"     薬指",style_toggle);
		toggleR5 = GUI.Toggle(new Rect(sw/12+sw/22+sw/9*4,sh/3+sh/12+sh/14,sw/23,sh/15),toggleR5,"     小指",style_toggle);
		style.fontSize=sh/13;
		GUI.Label(new Rect(sw/16,sh/2+sh/12,sw/2,sh/13),"難易度を設定してね",style);
		style_toolbar.fontSize=sh/13;
		selected = GUI.Toolbar(new Rect(sw/12,sh/2+sh/6,sw/2,sh/12),selected,texts);
		Debug.Log(texts[selected]);
	}
}
