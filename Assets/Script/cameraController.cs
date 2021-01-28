using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

	public PlayerMotor thePlayer;
	public Rigidbody2D myBody;
	public bool freeze;
	public GameObject leftConstraint;
	public GameObject rightConstraint;
	public Animator anim;
	public bool canMove;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerMotor> ();
	}
	
	// Update is called once per frame
	void Update () {
			if (freeze == false) 
			{
				if (thePlayer.transform.position.x >= leftConstraint.transform.position.x && thePlayer.transform.position.x <= rightConstraint.transform.position.x)
				{
					transform.position = new Vector3(thePlayer.transform.position.x, transform.position.y, -10);
					canMove = true;
				}
				else
				{
					canMove = false;
				}
			}
	}
}
