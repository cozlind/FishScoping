using UnityEngine;
using System.Collections;

public class l_kusu_reaction : MonoBehaviour {
    //public GUITexture texture;
    GUITexture guiTexture;
    void Start () {
        guiTexture = GetComponent<GUITexture> ();
        //texture = this.guiTexture;
    }
	
	void OnGUI(){
		//GUI.DrawTexture(new Rect(0, 0, Screen.width,Screen.height), texture);
		if(reaction_finger_tutorial.f==4){
			guiTexture.pixelInset= new Rect(-Screen.width/2,-Screen.height/2,Screen.width,Screen.height);
		}else guiTexture.pixelInset= new Rect(0,0,0,0);
	}
}
