using UnityEngine;
using System.Collections;

public class BGM2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevel>6&&Application.loadedLevel<9)DontDestroyOnLoad(this.gameObject);
		else Destroy(this.gameObject);
	}
}
