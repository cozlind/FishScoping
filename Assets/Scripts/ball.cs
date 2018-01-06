using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {

    Rigidbody rigidbody;
	void Start () {
        rigidbody = GetComponent<Rigidbody> ();

    }
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "hole"){
            col.isTrigger=true;
		}else if(col.gameObject.tag == "destroy"){
			//write time
			//************
			Application.LoadLevelAsync("right_or_left");

		}
	}
	// Update is called once per frame
	void Update () {
		rigidbody.WakeUp();
	}

	void OnGUI(){
		int sw = Screen.width;
		int sh = Screen.height;

		bool isCricked_reset= GUI.Button(new Rect(0,sh-sh/8,sh/7,sh/8),"リセット");
		if(isCricked_reset){
			Debug.Log("GAME RESET");
			Vector3 posi;
			posi.x = -4.5f;
			posi.y = 0.8f;
			posi.z = -4.4f;
			transform.position = posi;
		}

		Rect rect6 = new Rect(sw - 50,sh-sh/8,50,sh/8);
		bool isClicked_main = GUI.Button(rect6, "すすむ");
		if (isClicked_main){
			Application.LoadLevelAsync("right_or_left");
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
