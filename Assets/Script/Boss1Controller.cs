using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1Controller : MonoBehaviour {

	public Rigidbody2D myBody;
	public int speed;
	public PlayerMotor thePlayer;
	public BoxCollider2D myBox;
	public bool moving;
	private Animator anim;
	public float health=720;
	private float maxHealth=660;
	private SpriteRenderer sprite;
	private bool invulnerable;
	public Slider bossHealth;

	public GameObject leftPos;
	public GameObject rightPos;
	public GameObject midPos;

	public GameObject stones;
	public GameObject spikes;
	public GameObject bullets;

	public float waitBetweenShots;
	public float shotCounter;
	public GameObject launchPoint;

	public float waitBetweenDrops;
	public float dropCounter;
	public GameObject dropPoint;
	private int randomSpace;

	public float waitBetweenPop;
	public float popCounter;
	public GameObject popPoint;

	public float waitBetweenChange;
	public float changeCounter;

	public float chargeCounter;
	public float resetCharge;
	public float pauseCounter;
	public float waitPause;

	public int rewardMoney;

	public GameObject damagedEffect;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		thePlayer = FindObjectOfType<PlayerMotor> ();
		moving = true;
		anim = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();
		shotCounter = waitBetweenShots;
		dropCounter = waitBetweenDrops;
		anim.SetBool ("idle", true);
		waitPause = 2;
		if (rewardMoney < 1) {
			rewardMoney = 500;
		}
		//StartCoroutine ("dashingLeft");
	}

	// Update is called once per frame
	void Update () {

		bossHealth.value = health;
		shotCounter -= Time.deltaTime;
		dropCounter -= Time.deltaTime;
		popCounter -= Time.deltaTime;
		changeCounter -= Time.deltaTime;

		//launchPoint.transform.position = new Vector3 (launchPoint.transform.position.x, thePlayer.transform.position.y, launchPoint.transform.position.z);
		dropPoint.transform.position = new Vector3 (thePlayer.transform.position.x, dropPoint.transform.position.y, dropPoint.transform.position.z);
		popPoint.transform.position = new Vector3 (thePlayer.transform.position.x, popPoint.transform.position.y, dropPoint.transform.position.z);

		//Nembakin Player
		if (shotCounter < 0 && anim.GetBool ("idle") == true) {
			Instantiate (bullets, launchPoint.transform.position, launchPoint.transform.rotation);
			shotCounter = waitBetweenShots;
		}

		//Jatohin Batu
		if (dropCounter < 0 && anim.GetBool ("attack1") == true) {
			Instantiate (stones, dropPoint.transform.position, dropPoint.transform.rotation);
			randomSpace = Random.Range (-10, 10);
			Instantiate (stones, new Vector3 ((dropPoint.transform.position.x + randomSpace), dropPoint.transform.position.y + randomSpace, dropPoint.transform.position.z), dropPoint.transform.rotation);
			randomSpace = Random.Range (-10, 10);
			Instantiate (stones, new Vector3 ((dropPoint.transform.position.x + randomSpace), dropPoint.transform.position.y + randomSpace, dropPoint.transform.position.z), dropPoint.transform.rotation);
			dropCounter = waitBetweenDrops;
		}

		//Bikin Spike
		if (popCounter < 0 && anim.GetBool ("attack2") == true) {
			Instantiate (spikes, popPoint.transform.position, popPoint.transform.rotation);
			randomSpace = Random.Range (-15, 15);
			Instantiate (spikes, new Vector3 (Mathf.Round (popPoint.transform.position.x + 6), popPoint.transform.position.y, popPoint.transform.position.z), popPoint.transform.rotation);
			randomSpace = Random.Range (-15, 15);
			Instantiate (spikes, new Vector3 (Mathf.Round (popPoint.transform.position.x - 6), popPoint.transform.position.y, popPoint.transform.position.z), popPoint.transform.rotation);
			randomSpace = Random.Range (-15, 15);
			Instantiate (spikes, new Vector3 (Mathf.Round (popPoint.transform.position.x - 12), popPoint.transform.position.y, popPoint.transform.position.z), popPoint.transform.rotation);
			randomSpace = Random.Range (-15, 15);
			Instantiate (spikes, new Vector3 (Mathf.Round (popPoint.transform.position.x + 12), popPoint.transform.position.y, popPoint.transform.position.z), popPoint.transform.rotation);
			popCounter = waitBetweenPop;
		}


			//AI BOSS
		if (health <= 600 && health>480) {
			moveLeft ();
		}

		if (health <= 480 && health>360) {
			moveRight ();
			if (myBody.transform.position.x > rightPos.transform.position.x) {
				anim.SetBool ("attack1", true);
				anim.SetBool ("attack3", false);
				anim.SetBool ("idle", false);
			}
		}

		if (health <= 360 && health > 240) {
			anim.SetBool ("attack3", true);
			anim.SetBool ("attack1", false);
			moveLeft ();
			if (myBody.transform.position.x < leftPos.transform.position.x) {
				anim.SetBool ("attack1", true);
				anim.SetBool ("attack3", false);
				anim.SetBool ("idle", false);
			}
		}

		if (health <= 240 && health > 120) {
			anim.SetBool ("attack3", true);
			anim.SetBool ("attack1", false);
			anim.SetBool ("idle", false);
			moveRight ();
			if (myBody.transform.position.x > rightPos.transform.position.x) {
				anim.SetBool ("attack2", true);
				anim.SetBool ("attack3", false);
				anim.SetBool ("idle", false);
			}
		}

		if (health <= 120) {
			anim.SetBool ("attack3", true);
			anim.SetBool ("attack2", false);
			anim.SetBool ("idle", false);
			moveLeft ();
			if (myBody.transform.position.x < leftPos.transform.position.x) {
				anim.SetBool ("attack2", true);
				anim.SetBool ("attack3", false);
				anim.SetBool ("idle", false);
			}
		}

		if (health <= 0) {
			pauseCounter -= Time.deltaTime;
			//Destroy (gameObject);
			if (pauseCounter <= 0) {
				Time.timeScale = 0;
				thePlayer.moneyCollected += rewardMoney;
				PlayerPrefs.SetInt("Money",PlayerPrefs.GetInt("Money")+rewardMoney);
				thePlayer.winScreen.SetActive (true);
				Destroy (gameObject);
			}
		}

			//facing BOSS
			if (myBody.transform.position.x > thePlayer.transform.position.x) {
				transform.localScale = new Vector3 (1f, 1f, 1f);
			} else {
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			}
		
	}

	void moveLeft()
	{
		//chargeCounter = resetCharge;
		if (myBody.transform.position.x > leftPos.transform.position.x) {
			anim.SetBool ("attack3", true);
			invulnerable = true;
			anim.SetBool ("idle", false);
			chargeCounter -= Time.deltaTime;
			if (chargeCounter < 0) {
				myBody.velocity = new Vector2 (-speed, myBody.velocity.y);
			}
		} else if(Mathf.Abs (myBody.transform.position.x - leftPos.transform.position.x) <= 1){
			myBody.velocity = new Vector2 (0, myBody.velocity.y);
			anim.SetBool ("attack3", false);
			invulnerable = false;
			anim.SetBool ("idle", true);
			changeCounter = waitBetweenShots;
			chargeCounter = resetCharge;
		}
	}

	void moveRight()
	{
		if (myBody.transform.position.x < rightPos.transform.position.x) {
			anim.SetBool ("attack3", true);
			invulnerable = true;
			anim.SetBool ("idle", false);
			chargeCounter -= Time.deltaTime;
			if (chargeCounter < 0) {
			myBody.velocity = new Vector2 (speed, myBody.velocity.y);
			}
		} else if(Mathf.Abs (myBody.transform.position.x - rightPos.transform.position.x) <= 1) {
			myBody.velocity = new Vector2 (0, myBody.velocity.y);
			anim.SetBool ("attack3", false);
			invulnerable = false;
			anim.SetBool ("idle", true);
			changeCounter = waitBetweenShots;
			chargeCounter = resetCharge;
		}
	}

	void moveMid()
	{
		if (myBody.transform.position.x < midPos.transform.position.x) {
			anim.SetBool ("attack3", true);
			anim.SetBool ("idle", false);
			myBody.velocity = new Vector2 (speed, myBody.velocity.y);
		}
		else if (myBody.transform.position.x > midPos.transform.position.x) {
			anim.SetBool ("attack3", true);
			anim.SetBool ("idle", false);
			myBody.velocity = new Vector2 (-speed, myBody.velocity.y);
		}

		if (Mathf.Abs (myBody.transform.position.x - midPos.transform.position.x) <= 1) {
			myBody.velocity = new Vector2 (0, myBody.velocity.y);
			anim.SetBool ("attack3", false);
			anim.SetBool ("idle", true);
		}
	}
		

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "weapon" && invulnerable == false) {
			health -= PlayerPrefs.GetInt("Attack");
			Instantiate (damagedEffect, launchPoint.transform.position, myBody.transform.rotation);
		} else if (other.tag == "Skill" && invulnerable == false) {
			health -= PlayerPrefs.GetInt("Skill");
			Instantiate (damagedEffect, launchPoint.transform.position, myBody.transform.rotation);
		}
	}
		
}
