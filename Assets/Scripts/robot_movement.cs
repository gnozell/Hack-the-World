using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class robot_movement : MonoBehaviour {

	private float speed = 40f;
	private float cSpeed;
	private float maxSpeed = 10f;
	private float jumpSpeed = 10f;

	public float health = 3f;

	private bool invul = false;
	private float invul_timer = 2f;

	private bool onGround = false;
	private bool inDoor = false;

	private Vector3 regularSize;
	private Vector3 smallSize;

	private Vector3 otherDoorLocation;


	public Transform spawnPoint;

	private Rigidbody2D playerRb2D;
	private Animator playerAnim;
	private AudioSource musicPlayer;
	private SpriteRenderer playerSprite;

	//UI
	public Image h1;
	public Image h2;
	public Image h3;

	public Sprite FullHeart;
	public Sprite HalfHeart;
	public Sprite EmptyHeart;

	// Use this for initialization
	void Start () {
		playerRb2D = GetComponent<Rigidbody2D> ();
		playerAnim = GetComponent<Animator> ();
		musicPlayer = GetComponent<AudioSource> ();
		playerSprite = GetComponent<SpriteRenderer> ();
		cSpeed = speed;

		regularSize = transform.localScale;
		smallSize = regularSize - new Vector3 (.5f, .5f, 0);
		do_dmg (0);
		invul = false;
	}

	public void freeze(){

	}


	public void unfreeze(){

	}

	public void do_dmg(float dmg){
		if (!invul) {
			health -= dmg;

			if (health < 0) {
				health = 0;
			}

			invul = true;

			if (health == 3) {
				h1.sprite = FullHeart;
				h2.sprite = FullHeart;
				h3.sprite = FullHeart;
			}

			else if (health == 2.5f){
				h1.sprite = HalfHeart;
				h2.sprite = FullHeart;
				h3.sprite = FullHeart;
			}

			else if (health == 2f){
				h1.sprite = EmptyHeart;
				h2.sprite = FullHeart;
				h3.sprite = FullHeart;
			}
			else if (health == 1.5f){
				h1.sprite = EmptyHeart;
				h2.sprite = HalfHeart;
				h3.sprite = FullHeart;
			}
			else if (health == 1f){
				h1.sprite = EmptyHeart;
				h2.sprite = EmptyHeart;
				h3.sprite = FullHeart;
			}
			else if (health == .5f){
				h1.sprite = EmptyHeart;
				h2.sprite = EmptyHeart;
				h3.sprite = HalfHeart;
			}
			else if (health == 0f){
				h1.sprite = EmptyHeart;
				h2.sprite = EmptyHeart;
				h3.sprite = EmptyHeart;
			}


		}
	}

	void OnTriggerStay2D(Collider2D col){
		if((col.tag == "Door")&&((transform.eulerAngles.z < 16) || (transform.eulerAngles.z > 344))){
			otherDoorLocation = (col.GetComponent<DoorScript> ().otherPosition) + new Vector3(0,.5f,0);
			inDoor = true;
		}

	}

	void OnTriggerExit2D(Collider2D col){
		inDoor = false;
	}

	void OnCollisionEnter2D(Collision2D col){
		
	}

	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.transform.position.y < gameObject.transform.position.y) {
			if (col.gameObject.tag == "Ground") {
				onGround = true;
			}
			if (col.gameObject.tag == "MovingPlatform") {
				transform.parent = col.transform;
				onGround = true;

			}
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
		if (invul) {
			invul_timer -= Time.deltaTime;
			if (invul_timer < 0) {
				invul = false;
				invul_timer = 2f;
			}

			if (playerSprite.color == Color.white) {
				playerSprite.color = Color.red;
			} else {
				playerSprite.color = Color.white;
			}
		} else {
			if (playerSprite.color != Color.white) {
				playerSprite.color = Color.white;
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (health == 0) {
			Destroy (gameObject);
			return;
		}

		// Get Hor and Vert inputs
		float hor = Input.GetAxis ("Horizontal");
		float vert = Input.GetAxis ("Vertical");


		// Adds speed to bot
		playerRb2D.AddForce (new Vector2 (hor * cSpeed, 0));


		if ( !((transform.eulerAngles.z < 16) || (transform.eulerAngles.z > 344)) && (onGround) ) {
			playerRb2D.AddForce (new Vector2 (-hor * cSpeed, 0));
		}

		// Handles MAX speed
		if (Mathf.Abs(playerRb2D.velocity.x) > maxSpeed) {
			playerRb2D.AddForce (new Vector2 (-hor * cSpeed, 0));
		}

		playerAnim.SetBool ("jumping", !onGround);

		// Jumping of the Bot
		if ((vert > 0)&&(onGround)&&(!inDoor)) {
			playerRb2D.AddForce(new Vector2(0,jumpSpeed), ForceMode2D.Impulse);
			onGround = false;
			musicPlayer.Play ();
		}
			
		// Jumping of the Bot
		if ((vert > 0)&&(onGround)&&(inDoor)) {
			
			if (LayerMask.LayerToName (gameObject.layer) == "Background_Level") {
				gameObject.layer = LayerMask.NameToLayer ("Default");
				GetComponent<SpriteRenderer> ().sortingLayerName = "Level";
				transform.localScale = regularSize;
			} else {
				gameObject.layer = LayerMask.NameToLayer ("Background_Level");
				GetComponent<SpriteRenderer> ().sortingLayerName = "Background";
				transform.localScale = smallSize;
			}
			transform.position = otherDoorLocation;
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
			playerRb2D.velocity = new Vector2();
			gameObject.layer = LayerMask.NameToLayer ("Default");
			GetComponent<SpriteRenderer> ().sortingLayerName = "Level";
			transform.localScale = regularSize;
			if (spawnPoint == null) {
				do_dmg (100);
			} else {
				transform.position= spawnPoint.position;
				do_dmg (1);
			}

		}


	}

}
