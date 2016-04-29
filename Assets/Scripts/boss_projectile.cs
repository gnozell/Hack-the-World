using UnityEngine;
using System.Collections;

public class boss_projectile : MonoBehaviour {

	public float health = 1f;

	private Rigidbody2D playerRb2D;

	bool falling = true;

	// Use this for initialization
	void Start () {
		playerRb2D = GetComponent<Rigidbody2D> ();

	}
	void OnTriggerEnter2D(Collider2D col){

	}

	void OnCollisionEnter2D(Collision2D col){
		//gameObject.AddComponent<enemyScript> ();
		//gameObject.GetComponent<enemyScript> ().speed = 80f;
		//Destroy (this);
		

	}

	void OnCollisionStay2D(Collision2D col){
		falling = false;
		if (col.gameObject.tag == "Player") {
			if (col.gameObject.transform.position.y > gameObject.transform.position.y) {
				col.rigidbody.AddForce (new Vector2 (0, 10f), ForceMode2D.Impulse);
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
		if (transform.position.y <= -10) {
			Destroy (gameObject);

		}

		if (!falling) {
			playerRb2D.AddForce (new Vector2 (80f, 0));
		}
		

	}
}
