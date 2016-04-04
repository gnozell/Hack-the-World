using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour {
	private float speed = 1f;

	public Transform endMarker;

	private float startTime;
	private float journeyLength;

	private Vector3 startPosition;
	private Vector3 endPosition;

	// Use this for initialization
	void Start () {
		startTime = Time.time;

		startPosition = transform.position;
		endPosition = endMarker.position;

		journeyLength = Vector3.Distance (startPosition, endPosition);
	
	}

	// Update is called once per frame
	void Update () {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		this.transform.position = Vector2.Lerp (startPosition, endPosition, fracJourney);

		if (transform.position == endPosition) {
			Vector3 temp = endPosition;
			endPosition = startPosition;
			startPosition = temp;

			startTime = Time.time;
		}
	}
}
