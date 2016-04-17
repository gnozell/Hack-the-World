using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public GameObject otherDoor;
	public Vector3 otherPosition;

	// Use this for initialization
	void Start () {
		otherPosition = otherDoor.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
