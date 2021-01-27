using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public Rigidbody2D myBody;
	public int speed;
	public PlayerMotor thePlayer;
	public BoxCollider2D myBox;
	public bool moving;
	public Animator anim;
	public Animator animModel;
	public float health=0;
	private float maxHealth=0;
	public float distance=1.5f;
	public SpriteRenderer sprite;
	public float healthColor;
	public float playerRange;
	public LayerMask playerLayer;
	public bool playerInRange;
	private bool activated;
	public bool mandatory;
	public LevelManager theLevel;
	public bool survivalMode;
	public DropItem drop;
	public GameObject attackedParticle;
	public GameObject centerPoint;
	public GameObject deathPrticle;
	public GameObject shadow;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		thePlayer = FindObjectOfType<PlayerMotor> ();
		moving = true;
		//anim = GetComponent<Animator> ();
		//sprite = GetComponent<SpriteRenderer> ();
		theLevel = FindObjectOfType<LevelManager> ();
		drop = FindObjectOfType<DropItem> ();
		shadow.SetActive (false);

		if (maxHealth <= 0) {
			maxHealth = theLevel.Enemy1Health;
			health = maxHealth;
		}
	}

	// Update is called once per frame
	void Update () {

		playerInRange = Physics2D.OverlapCircle (transform.position, playerRange, playerLayer);
		if (playerInRange == true && Mathf.Abs (myBody.transform.position.y - thePlayer.transform.position.y)<=1) {
			activated = true;
		}

		if (activated == true) {
			if (Mathf.Abs (myBody.transform.position.x - thePlayer.transform.position.x) <= distance  && Mathf.Abs (myBody.transform.position.y - thePlayer.transform.position.y)<=1 ) {
				moving = false;
				if (myBody.transform.position.x > thePlayer.transform.position.x) {
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
				StartCoroutine ("par");
			} else {
				moving = true;
				anim.SetBool ("attack", false);
				anim.SetBool ("moving", true);

				animModel.SetBool("attack", false);
				animModel.SetBool("moving", true);
			}
		
			
			healthColor = health / maxHealth;
			sprite.color = new Color (1, healthColor, healthColor, 1);

			if (moving == true) {
				anim.SetBool ("moving", true);
				anim.SetBool ("attack", false);
				animModel.SetBool("moving", true);
				animModel.SetBool("attack", false);
				if (myBody.transform.position.x > thePlayer.transform.position.x) {
					myBody.velocity = new Vector2 (-speed, myBody.velocity.y);
					if (survivalMode == false) {
						transform.localScale = new Vector3 (1f, 1f, 1f);
					} else {
						transform.localScale = new Vector3 (4f, 4f, 1f);
					}
				} else if (myBody.transform.position.x < thePlayer.transform.position.x) {
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
				animModel.SetBool("moving", false);
				animModel.SetBool("attack", true);
			}

			if (health <= 0) {
				anim.SetBool ("moving", true);
				anim.SetBool ("attack", false);
				animModel.SetBool("moving", true);
				animModel.SetBool("attack", false);
				Instantiate (deathPrticle, myBody.transform.position, myBody.transform.rotation);
				Destroy (gameObject);
				if (mandatory == true) {
					theLevel.killCount++;
				}
				drop.dropItem (transform.position, transform.rotation);
			}

		} else {
			anim.SetBool ("moving", false);
			animModel.SetBool("moving", false);
		}
	}


	IEnumerator par()
	{
		distance = 100;
		yield return new WaitForSeconds (2.5f);
		distance = 1.5f;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "weapon") {
			health -= PlayerPrefs.GetInt("Attack");
			Instantiate (attackedParticle, centerPoint.transform.position, myBody.transform.rotation);
		} else if (other.tag == "Skill") {
			health -= PlayerPrefs.GetInt("Skill");
			Instantiate (attackedParticle, centerPoint.transform.position, myBody.transform.rotation);
		}


	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawSphere (transform.position, playerRange);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "platform") {
			shadow.SetActive (true);
		}
	}
}
