using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string level1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void goLevel1(){
		Application.LoadLevel(level1);
	}
}
