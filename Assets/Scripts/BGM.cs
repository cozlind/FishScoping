using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {
	void Awake(){
		//if(Application.loadedLevel==0)Destroy (this.gameObject);
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevel<7)DontDestroyOnLoad(this.gameObject);
		else Destroy(this.gameObject);
	}
}
