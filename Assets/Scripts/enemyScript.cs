using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	public float speed = 40f;
	public float health = 1f;
	private float cSpeed;
	private float maxSpeed = 10f;
	private float sign = 1f;

	bool turning = false;

	private Rigidbody2D playerRb2D;

	// Use this for initialization
	void Start () {
		playerRb2D = GetComponent<Rigidbody2D> ();
		cSpeed = speed;
	
	}
	void OnTriggerEnter2D(Collider2D col){
		if ((col.tag == "EnemyTurn")&&(!turning)) {
			if (sign > 0) {
				sign = -1f;
			} else {
				sign = 1f;

			}
			turning = true;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if ((!turning)&&(col.gameObject.tag != "Ground")) {
			if (sign > 0) {
				sign = -1f;
			} else {
				sign = 1f;

			}
			turning = true;
		}

	}

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			if (col.gameObject.transform.position.y > gameObject.transform.position.y) {
				col.rigidbody.AddForce (new Vector2 (0, 10f), ForceMode2D.Impulse);
				col.gameObject.GetComponent<AudioSource> ().Play ();
				health -= 1f;
				if (health <= 0) {
					Destroy (gameObject);
				}
			} else {
				col.gameObject.GetComponent<robot_movement> ().do_dmg (.5f);
				col.rigidbody.AddForce (new Vector2 (0, 0f), ForceMode2D.Impulse);
			}

		}

	}

	void OnTriggerExit2D(Collider2D col){

		
	}
	
	// Update is called once per frame
	void Update () {
		// Adds speed to bot

		if (turning) {
			if (sign > 0) {
				if (gameObject.transform.eulerAngles.y > 0f) {
					Vector3 temp2 = gameObject.transform.eulerAngles;
					temp2.y -= 10f;
					temp2.y = Mathf.Round(temp2.y * 100f) / 100f;
					gameObject.transform.eulerAngles = temp2;
				}  else {
					turning = false;

				}
			
			} 

			if (sign < 0) {

				if (gameObject.transform.eulerAngles.y < 180f) {
					Vector3 temp2 = gameObject.transform.eulerAngles;
					temp2.y += 10f;
					temp2.y = Mathf.Round(temp2.y * 100f) / 100f;
					gameObject.transform.eulerAngles = temp2;
				} else {
					turning = false;
				}
			}

			playerRb2D.velocity = new Vector2(0,0);
		}


		if (!turning) {
			playerRb2D.AddForce (new Vector2 (cSpeed*sign, 0));

			// Handles MAX speed
			if (Mathf.Abs(playerRb2D.velocity.x) > maxSpeed) {
				playerRb2D.AddForce (new Vector2 (-cSpeed*sign, 0));
			}


			if (transform.position.y <= -10) {
				Destroy (gameObject);

			}
		}
	
	}
}
