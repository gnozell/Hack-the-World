using UnityEngine;
using System.Collections;

public class robot_movement : MonoBehaviour {

	private float speed = 40f;
	private float cSpeed;
	private float maxSpeed = 30f;
	private float jumpSpeed = 10f;
	private bool onGround = true;

	private Rigidbody2D playerRb2D;
	//private Animator playerAnim;

	// Use this for initialization
	void Start () {
		playerRb2D = GetComponent<Rigidbody2D> ();
		//playerAnim = GetComponent<Animator>();
		cSpeed = speed;
	}

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == "Ground") {
			onGround = true;
		}
		
	}

	// Update is called once per frame
	void FixedUpdate () {

		// Get Hor and Vert inputs
		float hor = Input.GetAxis ("Horizontal");
		float vert = Input.GetAxis ("Vertical");

		// Adds speed to bot
		playerRb2D.AddForce (new Vector2 (hor * cSpeed, 0));

		// Handles MAX speed
		if (Mathf.Abs(playerRb2D.velocity.x) > maxSpeed) {
			playerRb2D.AddForce (new Vector2 (-hor * cSpeed, 0));
		}


		// Raycast to see if it hits the ground
		//RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector3.down, .5f);

		//Debug.DrawRay(transform.position, (Vector3.down)*.4f, Color.red,500);


			

		// Jumping of the Bot
		if ((vert > 0)&&(onGround)) {
			playerRb2D.AddForce(new Vector2(0,jumpSpeed), ForceMode2D.Impulse);
			onGround = false;
		}


		if (transform.rotation.w < 0) {
			Quaternion temp = transform.rotation;
			temp.w = Mathf.Abs(temp.w);
			transform.rotation = temp;
		}




		// Handles the rotation of the bot


		if ((transform.rotation.w >= .7) && !(onGround) && (transform.rotation.z < 0.2f)) {
			Debug.Log (transform.rotation);
			Debug.Log ("ran1");
			transform.Rotate (Vector3.forward * 4);
		}

		else if ((transform.rotation.w >= .7) && !(onGround) && (transform.rotation.z > 0.2f)) {
			Debug.Log (transform.rotation);
			Debug.Log ("ran2");
			transform.Rotate (Vector3.forward * -4);

		}
		else if ( (transform.rotation.w <= .7) && !(onGround) && (transform.rotation.z <= -1.0)) {
			Debug.Log (transform.rotation);
			Debug.Log ("ran3");
			transform.Rotate (Vector3.forward * -4);

		}
		else if ((transform.rotation.w <= .7) && !(onGround) && (transform.rotation.z <= 1.0)) {
			Debug.Log (transform.rotation);
			Debug.Log ("ran4");
			transform.Rotate (Vector3.forward * -4);

		}





		// Respawning if falling off the map

		if (transform.position.y <= -10) {
			// respawns if falls off the map
			Vector3 temp = transform.position;
			temp.y = 10f;
			playerRb2D.velocity = new Vector2();

			transform.position= temp;

		}


	}

}
