using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	//public float dampTime = 0.15f;
	public float dampTime = 0.15f;
	public float lowerLimit = -4f;

	public Transform background;


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

			if (destination.y < lowerLimit) {
				destination.y = lowerLimit;
			}




			if (this.velocity.x > 1f) {
				//background.position = Vector3.SmoothDamp(background.position, (background.position + new Vector3(.05f,0,0)), ref velocity, 0.15f);
			}

			else if (this.velocity.x < -1f) {
				//background.position = Vector3.SmoothDamp(background.position, (background.position + new Vector3(-.05f,0,0)), ref velocity, 0.15f);
			}


			if (this.velocity.y > 1f) {
				//background.position = Vector3.SmoothDamp(background.position, (background.position + new Vector3(0,.2f,0)), ref velocity, 0.15f);
			}

			else if (this.velocity.y < -1f) {
				//background.position = Vector3.SmoothDamp(background.position, (background.position + new Vector3(0,.2f,0)), ref velocity, 0.15f);

			}




			// moves the camera
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

	}
}