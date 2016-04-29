using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour 
{
	public GUISkin myskin;  //custom GUIskin reference
	public string levelToLoad;
	public bool paused = false;

	public GameObject boss;

	private void Start()
	{
		Time.timeScale=1; //Set the timeScale back to 1 for Restart option to work  
	}

	private void Update()
	{

		if (boss == null ) //check if Escape key/Back key is pressed
		{
			paused = true;  //unpause the game if already paused

		}

		if(paused){
			Time.timeScale = 0;  //set the timeScale to 0 so that all the procedings are halted
		}else{
			Time.timeScale = 1;  //set it back to 1 on unpausing the game
		}

		Debug.Log (Time.timeScale);
	}

	private void OnGUI()
	{
		GUI.skin=myskin;   //use the custom GUISkin

		if (paused){    

			GUI.Box(new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2), "YOU WON!");
			//GUI.Label(new Rect(Screen.width/4+10, Screen.height/4+Screen.height/10+10, Screen.width/2-20, Screen.height/10), "YOUR SCORE: "+ ((int)score));

			//if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+Screen.height/10+10, Screen.width/2-20, Screen.height/10), "RESUME")){
			//	paused = false;
			//}

			if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+2*Screen.height/10+10, Screen.width/2-20, Screen.height/10), "Main Menu")){
				Application.LoadLevel(levelToLoad);
			}

			//if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+3*Screen.height/10+10, Screen.width/2-20, Screen.height/10), "MAIN MENU")){
			//	Application.LoadLevel(levelToLoad);
			//} 
		}
	}
}