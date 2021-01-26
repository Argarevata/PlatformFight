using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	public float speed;
	public Rigidbody2D myBody;
	public PlayerMotor thePlayer;
	public PlayerHealth playerHealth;
	public int damage;
	public bool BOMB;
	public float lifeTime;
	public LevelManager level;

	// Use this for initialization
	void Start () {

		level = FindObjectOfType<LevelManager> ();
		if (level.bulletDamage < 1) {
			damage = 10;
		} else {
			damage = level.bulletDamage;
		}
		thePlayer = FindObjectOfType<PlayerMotor> ();
		if (BOMB == false) {
			myBody = GetComponent<Rigidbody2D> ();
		}
		StartCoroutine ("life");
		playerHealth = FindObjectOfType<PlayerHealth> ();

		if (BOMB == false) {
			if (myBody.transform.position.x < thePlayer.transform.position.x) {
				myBody.velocity = new Vector2 (speed, myBody.velocity.y);
				myBody.transform.localScale = new Vector3 (-0.3f, 0.3f, 1);
			} else {
				myBody.velocity = new Vector2 (-speed, myBody.velocity.y);
				myBody.transform.localScale = new Vector3 (0.3f, 0.3f, 1);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {



	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player") {
			playerHealth.damaged (damage);
			if (BOMB == false) {
				Destroy (gameObject);
			}
		}

		if (other.tag == "Skill" || other.tag == "weapon") {
			Destroy (gameObject);
		}
	}

	IEnumerator life()
	{
		yield return new WaitForSeconds (lifeTime);
		Destroy (gameObject);
	}
}
