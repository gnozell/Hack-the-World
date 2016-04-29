using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BossScript : MonoBehaviour {

	public GameObject waitwilldead;
	public Transform IntroMoveHere;
	private Transform startMarker;
	public Text textbox;
	private string phase = "";
	public GameObject attack1;
	public GameObject attack0_obj;
	public GameObject attack1_obj;
	private float spawntimer = 1f;

	public GameObject wall;
	public GameObject theCamera;
	public GameObject player;

	public float speed = 0.05F;
	private float startTime;
	private float journeyLength;

	private float textoutput = -1;
	private float text_one_at_a_time = -1;

	private float phaselen = 0;



	// Use this for initialization
	void Start () {
		startMarker = this.transform;
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if ((col.gameObject.tag == "Player")&(col.gameObject.transform.position.y > -2f)) {
			col.rigidbody.AddForce (new Vector2 (0, 10f), ForceMode2D.Impulse);
			Destroy (gameObject);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (phase) {
		case "offscreen":
			if (waitwilldead == null) {
				phase = "intro";
				//Debug.Log ("my turn");

				startTime = Time.time;
				journeyLength = Vector3.Distance(startMarker.position, IntroMoveHere.position);
			
			}
				break;
		case "intro":{
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			
			transform.position = Vector3.Lerp(startMarker.position, IntroMoveHere.position, fracJourney);
				//Debug.Log (IntroMoveHere.position + " " + transform.position);
				//Debug.Log (Vector3.Distance(transform.position,IntroMoveHere.position));
				if (Vector3.Distance(transform.position,IntroMoveHere.position) <= 0.5){
					phase = "chat";
				}
			break;
		}

		case "chat": {
				string speach_line = "YOU KILLED MY BABIES!";
				//Debug.Log ("Talking");
				if (textoutput <= 0) {
					textoutput = speach_line.Length;
					text_one_at_a_time = textoutput;
				} else {
					int latest = (int) (text_one_at_a_time - textoutput);
					textbox.text = speach_line.Substring(0,latest);
					textoutput -= (Time.deltaTime*3);
					if (textoutput <= 0) {
						phase = "attack1";
						phaselen = 30f;
					}
				}

				break;
			}

		case "attack1":{
				spawntimer -= Time.deltaTime;
				phaselen -= Time.deltaTime;
				if (phaselen <= 0) {
					phase = "chat2"; 
					break;
				}

				if (spawntimer <= 0) {
					Quaternion roat = Random.rotation;
					roat.eulerAngles = new Vector3(0f,0f, roat.eulerAngles.z);
					switch (Random.Range (1, 4)) {
					case 1:
						{	
							if (Random.Range (0, 2) > 1) {
								Instantiate (attack1_obj, attack1.transform.Find("1").gameObject.transform.position, roat);
							} else {
								Instantiate (attack0_obj, attack1.transform.Find("1").gameObject.transform.position, roat);
							}

							break;
						}
					case 2:
						{
							if (Random.Range (1, 3) > 1) {
								Instantiate (attack1_obj, attack1.transform.Find("2").gameObject.transform.position, roat);
							} else {
								Instantiate (attack0_obj, attack1.transform.Find("2").gameObject.transform.position, roat);
							}
							break;
						}
					case 3:
						{
							if (Random.Range (1, 3) > 1) {
								Instantiate (attack1_obj, attack1.transform.Find("3").gameObject.transform.position, roat);
							} else {
								Instantiate (attack0_obj, attack1.transform.Find("3").gameObject.transform.position, roat);
							}
							break;
						}
					}
					spawntimer = 1f;
				}
				break;
				
			}

		case "chat2": {
				string speach_line = "Beep boop, Beep boop.";
				if (textoutput <= 0) {
					textoutput = speach_line.Length;
					text_one_at_a_time = textoutput;
				} else {
					int latest = (int) (text_one_at_a_time - textoutput);
					textbox.text = speach_line.Substring(0,latest);
					textoutput -= (Time.deltaTime*3);
					if (textoutput <= 0) {
						phase = "attack2";
						phaselen = 30f;
					}
				}

				break;
			}

		case "attack2": {
				if (wall != null) {
					Destroy (wall);
				}

				if ((theCamera.GetComponent<CameraControls> () == null)&&(player != null)) {
					theCamera.AddComponent<CameraControls> ();
					theCamera.GetComponent<CameraControls> ().target = player.transform;
				}

				transform.position += new Vector3 (.1f,0,0);
				break;
			}

		default:{
				phase = "offscreen";
			break;
		}

		}
	
	}
}
