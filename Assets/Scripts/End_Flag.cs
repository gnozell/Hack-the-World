using UnityEngine;
using System.Collections;

public class End_Flag : MonoBehaviour {
	public string levelToLoad;
	public GameObject floor;
	public GameObject inputCamera;
	public GameObject player;

	private bool movefloor = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (movefloor) {
			floor.transform.position -= new Vector3 (0,.02f,0);
			//Debug.Log (floor.transform.position);

			if (floor.transform.position.y < 0f) {
				Application.LoadLevel(levelToLoad);
			}
		}
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			Destroy(inputCamera.GetComponent<CameraControls> ());
			player.GetComponent<robot_movement> ().freeze();
			movefloor = true;
		}
	}
}
