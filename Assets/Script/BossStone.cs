using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStone : MonoBehaviour {

	public PlayerHealth health;
	public Animator anim;
	public bool activated;
	public Rigidbody2D body;

	// Use this for initialization
	void Start () {
		health = FindObjectOfType<PlayerHealth> ();
		anim = GetComponent<Animator> ();
		activated = true;
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (activated == true) {
			if (other.tag == "ground") {
				body.velocity = new Vector2 (0, 0);
				activated = false;
				StartCoroutine ("pop");
			} else if (other.name == "Player") {
				activated = false;
				body.velocity = new Vector2 (0, 0);
				health.damaged (15);
				StartCoroutine ("pop");
			} else if (other.tag == "Skill") {
				activated = false;
				body.velocity = new Vector2 (0, 0);
				StartCoroutine ("pop");
			}
		}

	}

	IEnumerator pop()
	{
		anim.SetBool ("pop", true);
		yield return new WaitForSeconds (0.7f);
		Destroy (gameObject);
	}
}
