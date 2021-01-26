using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour {

	private Animator anim;
	public PlayerHealth health;
	public Boss1Controller theBoss;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		health = FindObjectOfType<PlayerHealth> ();
		StartCoroutine ("life");
		theBoss = FindObjectOfType<Boss1Controller> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (theBoss.health <= 0) {
			Destroy (gameObject);
		}
	}

	IEnumerator life()
	{
		yield return new WaitForSeconds (4);
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player") {
			health.damaged (26);
		} else if (other.tag == "Skill") {
			Destroy (gameObject);
		}
	}
}
