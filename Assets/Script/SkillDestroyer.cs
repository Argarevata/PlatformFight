using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDestroyer : MonoBehaviour {

	public float lifeTime;
	public PlayerMotor player;
	public Rigidbody2D body;
	public int speed;
	public bool hadoken;

	// Use this for initialization
	void Start () {
		StartCoroutine ("life");
		player = FindObjectOfType<PlayerMotor> ();
		body = GetComponent<Rigidbody2D> ();

		if (hadoken == true) {
			if (player.transform.localScale.x < 0) {
				body.velocity = new Vector2 (speed, 0);
				body.transform.localScale = new Vector3 (-1, 1, 1);
			} else {
				body.velocity = new Vector2 (-speed, 0);
				body.transform.localScale = new Vector3 (1, 1, 1);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		

		//body.velocity = new Vector2 (speed, 0);
	}

	IEnumerator life()
	{
		yield return new WaitForSeconds (lifeTime);
		Destroy (gameObject);
	}
}
