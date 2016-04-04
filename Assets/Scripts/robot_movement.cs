using UnityEngine;
using System.Collections;

public class robot_movement : MonoBehaviour {

	private float speed = 40f;
	private float cSpeed;
	private float maxSpeed = 30f;
	private float jumpSpeed = 10f;
	private bool onGround = false;

	private Rigidbody2D playerRb2D;
	private Animator playerAnim;

	// Use this for initialization
	void Start () {
		playerRb2D = GetComponent<Rigidbody2D> ();
		playerAnim = GetComponent<Animator> ();
		cSpeed = speed;
	}

	void OnCollisionEnter2D(Collision2D col){
		
	}

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == "Ground") {
			onGround = true;
		}
		if(col.gameObject.tag == "MovingPlatform"){
			transform.parent = col.transform;
			onGround = true;

		}
	}

	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "Ground") {
			onGround = false;
		}
		if (col.gameObject.tag == "MovingPlatform") {
			transform.parent = null;
			onGround = false;
		}
	}


	void Update(){
		


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

		playerAnim.SetBool ("jumping", !onGround);

		// Jumping of the Bot
		if ((vert > 0)&&(onGround)) {
			playerRb2D.AddForce(new Vector2(0,jumpSpeed), ForceMode2D.Impulse);
			onGround = false;
		}


			
		// Handles the rotation of the bot
		if(!onGround){
			// if NOT on the ground
			if ((transform.eulerAngles.z < 16) || (transform.eulerAngles.z > 344)) {
				
			} else {
				transform.Rotate (Vector3.forward * 4);
			}
			
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
