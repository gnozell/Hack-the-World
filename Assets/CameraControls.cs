using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	//public float dampTime = 0.15f;
	public float dampTime = 0.05f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	private Camera thiscam;

	void Start () {
		thiscam = GetComponent<Camera> ();
	}


	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = thiscam.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - thiscam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

	}
}