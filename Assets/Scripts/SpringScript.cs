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
		pressed = true;
	}

	void OnCollisionStay2D(Collision2D col){
		col.rigidbody.AddForce(new Vector2(-springForce*Mathf.Sin(transform.eulerAngles.z * Mathf.PI / 180),springForce*Mathf.Cos(transform.eulerAngles.z * Mathf.PI / 180)), ForceMode2D.Impulse);

	}

	void OnCollisionExit2D(Collision2D col){
		pressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		springAnim.SetBool ("pressed", pressed);
	}
}
