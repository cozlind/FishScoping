using UnityEngine;
using System.Collections;

public class BGM3 : MonoBehaviour {

	void Awake(){

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevel>6&&Application.loadedLevel<14)DontDestroyOnLoad(this.gameObject);
		else Destroy(this.gameObject);
	}
}
