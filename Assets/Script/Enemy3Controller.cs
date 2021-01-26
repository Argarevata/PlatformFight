using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Controller : MonoBehaviour {

	public Rigidbody2D myBody;
	public int speed;
	public PlayerMotor thePlayer;
	public BoxCollider2D myBox;
	public bool moving;
	private Animator anim;
	public float health=20;
	private float maxHealth=20;
	private float distance=1.5f;
	private SpriteRenderer sprite;
	public float healthColor;
	public float playerRange;
	public LayerMask playerLayer;
	public bool playerInRange;
	private bool activated;
	//public GameObject launchPoint;
	public float waitBetweenShots;
	public float shotCounter;
	public LevelManager theLevel;
	public bool mandatory;
	public GameObject bomb;
	public bool survivalMode;
	public DropItem drop;
	public GameObject attackedParticle;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		thePlayer = FindObjectOfType<PlayerMotor> ();
		moving = true;
		anim = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();
		theLevel = FindObjectOfType<LevelManager> ();
		drop = FindObjectOfType<DropItem> ();
	}

	// Update is called once per frame
	void Update () {



		playerInRange = Physics2D.OverlapCircle (transform.position, playerRange, playerLayer);
		if (playerInRange == true) {
			activated = true;
		}

		if (activated == true) {
			if (Mathf.Abs (myBody.transform.position.x - thePlayer.transform.position.x) <= distance && Mathf.Abs (myBody.transform.position.y - thePlayer.transform.position.y)<=2 ) {
				shotCounter -= Time.deltaTime;
				moving = false;
				if (myBody.transform.position.x > thePlayer.transform.position.x+1) {
					if (survivalMode == false) {
						transform.localScale = new Vector3 (1f, 1f, 1f);
					} else {
						transform.localScale = new Vector3 (4f, 4f, 1f);
					}
				} else {
					if (survivalMode == false) {
						transform.localScale = new Vector3 (-1f, 1f, 1f);
					} else {
						transform.localScale = new Vector3 (-4f, 4f, 1f);
					}
				}
				//StartCoroutine ("par");
			} else {
				shotCounter = waitBetweenShots;
				moving = true;
				anim.SetBool ("attack", false);
				anim.SetBool ("moving", true);
			}


			healthColor = health / maxHealth;
			sprite.color = new Color (1, healthColor, healthColor, 1);

			if (moving == true) {
				anim.SetBool ("moving", true);
				anim.SetBool ("attack", false);
				if (myBody.transform.position.x > thePlayer.transform.position.x) {
					myBody.velocity = new Vector2 (-speed, myBody.velocity.y);
					if (survivalMode == false) {
						transform.localScale = new Vector3 (1f, 1f, 1f);
					} else {
						transform.localScale = new Vector3 (4f, 4f, 1f);
					}
				} else if (myBody.transform.position.x <= thePlayer.transform.position.x) {
					myBody.velocity = new Vector2 (speed, myBody.velocity.y);
					if (survivalMode == false) {
						transform.localScale = new Vector3 (-1f, 1f, 1f);
					} else {
						transform.localScale = new Vector3 (-4f, 4f, 1f);
					}
				}
			} else {
				myBody.velocity = new Vector2 (0, myBody.velocity.y);
				anim.SetBool ("moving", false);
				anim.SetBool ("attack", true);
				if (shotCounter < 0) {
					Instantiate (bomb, myBody.transform.position, myBody.transform.rotation);
					Destroy (gameObject);
				}
			}

			if (health <= 0) {
				Destroy (gameObject);
				if (mandatory == true) {
					theLevel.killCount++;
				}
				drop.dropItem (transform.position, transform.rotation);
			}

		} else {
			anim.SetBool ("moving", false);
		}
	}


	IEnumerator par()
	{
		distance = 100;
		yield return new WaitForSeconds (3.1f);
		distance = 1.5f;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "weapon") {
			health -= PlayerPrefs.GetInt("Attack");
			Instantiate (attackedParticle, myBody.transform.position, myBody.transform.rotation);
		} else if (other.tag == "Skill") {
			health -= PlayerPrefs.GetInt("Skill");
			Instantiate (attackedParticle, myBody.transform.position, myBody.transform.rotation);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawSphere (transform.position, playerRange);
	}
}
