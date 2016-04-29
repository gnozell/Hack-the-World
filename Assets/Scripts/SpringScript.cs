using UnityEngine;
using System.Collections;

public class SpringScript : MonoBehaviour {

	public float springForce = 10f;
	private Animator springAnim;
	private bool pressed;

	// Use this for initialization
	void Start () {
		springAnim = GetComponent<Animator> ();
		pressed = false;
	}

	void OnCollisionEnter2D(Collision2D col){

	}

	void OnCollisionStay2D(Collision2D col){

		if (col.gameObject.transform.position.y > gameObject.transform.position.y) {
			pressed = true;
			col.rigidbody.AddForce(new Vector2(-springForce*Mathf.Sin(transform.eulerAngles.z * Mathf.PI / 180),springForce*Mathf.Cos(transform.eulerAngles.z * Mathf.PI / 180)), ForceMode2D.Impulse);
			if (col.gameObject.GetComponent<AudioSource> () != null){
				col.gameObject.GetComponent<AudioSource> ().Play ();
			}
		}

	}

	void OnCollisionExit2D(Collision2D col){
		pressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		springAnim.SetBool ("pressed", pressed);
	}
}
