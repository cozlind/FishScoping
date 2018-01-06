using UnityEngine;
using System.Collections;

public class title_texture : MonoBehaviour {
    //public GUITexture texture;
    GUITexture guiTexture;
    void Start () {
        guiTexture = GetComponent<GUITexture> ();
        //texture = this.guiTexture;
    }
	
	void OnGUI(){
		//GUI.DrawTexture(new Rect(0, 0, Screen.width,Screen.height), texture);
		guiTexture.pixelInset= new Rect(-Screen.width/2,-Screen.height/2,Screen.width,Screen.height);
	}
}
