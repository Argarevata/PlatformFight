using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour {

	public PlayerMotor thePlayer;
	public Rigidbody2D myBody;
	public Animator anim;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerMotor> ();
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (thePlayer.transform.position.x, myBody.transform.position.y,0);
		if (thePlayer.grounded == false) {
			anim.SetBool ("Jump", true);
		} else {
			anim.SetBool ("Jump", false);
		}
	}
}
