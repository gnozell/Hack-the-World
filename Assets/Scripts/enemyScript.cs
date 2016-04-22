using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	private float speed = 40f;
	private float cSpeed;
	private float maxSpeed = 10f;

	private Rigidbody2D playerRb2D;

	// Use this for initialization
	void Start () {
		playerRb2D = GetComponent<Rigidbody2D> ();
		cSpeed = speed;
	
	}
	
	// Update is called once per frame
	void Update () {
		// Adds speed to bot
		playerRb2D.AddForce (new Vector2 (cSpeed, 0));

		// Handles MAX speed
		if (Mathf.Abs(playerRb2D.velocity.x) > maxSpeed) {
			playerRb2D.AddForce (new Vector2 (-cSpeed, 0));
		}


		if (transform.position.y <= -10) {
			Destroy (gameObject);

		}
	
	}
}
