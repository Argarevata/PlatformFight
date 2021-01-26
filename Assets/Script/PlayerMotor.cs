using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

	public Rigidbody2D myBody;
	public int speed;
	private float moveVelocity;
	private float currentVelocity;
	public float jump;
	public Animator anim;
	public Animator animNewChar;
	[SerializeField]
	public int moveCount;
	public bool grounded;
	public bool dead;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public bool doSkill;
	public PlayerHealth theHealth;
	public GameObject skill1;
	public GameObject skill2;
	public GameObject skill1LaunchPoint;
	public Quaternion skill1Rotation;
	public EnergyManager energyManager;
	public GameObject winScreen;
	public bool invulnerable;
	public int moneyCollected;
	public AudioSource jumpSFX;
	public int thisLevel;

	public float punchCounter;
	public float waitBetweenPunch;


	void Start () {
		PlayerPrefs.SetInt("Speed", 5);
		PlayerPrefs.SetInt("Attack", 5);
		PlayerPrefs.SetInt("Skill", 10);
		myBody = GetComponent<Rigidbody2D> ();
		//anim = GetComponentInChildren<Animator> ();
		speed = PlayerPrefs.GetInt ("Speed");
		Time.timeScale = 1;
		theHealth = FindObjectOfType<PlayerHealth> ();
		energyManager = FindObjectOfType<EnergyManager> ();
		Debug.Log ("ini level " + PlayerPrefs.GetInt ("stage"));
	}
	
	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}

	void Update () {

		punchCounter -= Time.deltaTime;

		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Main Menu");
		}

		if (grounded) {
			anim.SetBool ("grounded", true);
			animNewChar.SetBool("grounded", true);
		} else {
			anim.SetBool ("grounded", false);
			animNewChar.SetBool("grounded", false);
		}

		//MOVING
		if (Input.GetKey (KeyCode.A) && punchCounter < -0.15 && dead==false) {
			moveVelocity = -speed;
			anim.SetBool ("moving", true);
			animNewChar.SetBool("moving", true);
			moveCount = 0;
		} else if (Input.GetKey (KeyCode.D) && punchCounter < -0.15 && dead==false) {
			moveVelocity = speed;
			anim.SetBool ("moving", true);
			animNewChar.SetBool("moving", true);
			moveCount = 0;
		} else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
		{
			//anim.SetBool ("moving", false);
			unPressed();
		}

		if (Input.GetKeyDown (KeyCode.Space)&&grounded&&doSkill==false && punchCounter < -0.15 && dead==false) {
			Jumping ();
		}

		//TURNING
		if (myBody.velocity.x > 0) {
			transform.localScale = new Vector3 (-1f, 1f, 1f);
			//anim.SetBool ("moving", true);
		} else if (myBody.velocity.x < 0) {
			transform.localScale = new Vector3 (1f, 1f, 1f);
		}

		//ATTACK AND COMBO
		if (Input.GetKeyDown (KeyCode.J) &&grounded && punchCounter<0 && dead==false) {
			anim.SetBool ("moving", false);
			animNewChar.SetBool("moving", false);
			moveVelocity = 0;
			punchCounter = waitBetweenPunch;
			if (moveCount <2 ) {
				moveCount += 1;
				anim.SetBool ("attack1", true);
				animNewChar.SetBool("attack1", true);
			} else if (moveCount >= 2) {
				moveCount = 0;
				anim.SetBool ("attack2", true);
				animNewChar.SetBool("attack2", true);
			}
		}

		if (punchCounter < 0) {
			anim.SetBool ("attack1", false);
			anim.SetBool ("attack2", false);
			animNewChar.SetBool("attack1", false);
			animNewChar.SetBool("attack2", false);
		}

		//SKILL
		if (Input.GetKeyDown (KeyCode.K)&&grounded && energyManager.energy>=10 && doSkill==false && dead==false) {
			//moveVelocity = 0;
			anim.SetBool ("Skill1", true);
			animNewChar.SetBool("Skill1", true);
			invulnerable = true;
			energyManager.useEnergy (10);
			Instantiate (skill1, skill1LaunchPoint.transform.position, skill1LaunchPoint.transform.rotation);
			doSkill = true;
			StartCoroutine ("wait");
		} else {
			//anim.SetBool ("Skill1", false);
		}
		if (Input.GetKeyDown (KeyCode.L)&&grounded && energyManager.energy>=10 &&doSkill==false && dead==false) {
			//moveVelocity=0;
			anim.SetBool ("Skill2", true);
			animNewChar.SetBool("Skill2", true);
			invulnerable = true;
			energyManager.useEnergy (10);
			Instantiate (skill2, myBody.transform.position, myBody.transform.rotation);
			doSkill = true;
			StartCoroutine ("wait");
		} else {
			//anim.SetBool ("Skill2", false);
		}

		//MOTOR
		if (doSkill == false && punchCounter<-0.2f) {
			myBody.velocity = new Vector2 (moveVelocity, myBody.velocity.y);
			if (myBody.velocity.x != 0) {
				anim.SetBool ("moving", true);
				animNewChar.SetBool("moving", true);
			} else {
				anim.SetBool ("moving", false);
				animNewChar.SetBool("moving", false);
			}
		}
		//moveVelocity = 0;

		//check WIN unlock NEXT LEVEL
		if (winScreen.activeInHierarchy) {
			unlockNextLevel ();
		}
	}

	IEnumerator wait()
	{
		yield return new WaitForSeconds (1.2f);
		doSkill = false;
		invulnerable = false;

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "FinishBox") {
			winScreen.SetActive (true);
			Time.timeScale = 0;
		}
	}

	//TOUCH CONTROL
	public void moveLeft()
	{
		moveVelocity = -speed;
		anim.SetBool ("moving", true);
		animNewChar.SetBool("moving", true);
		moveCount = 0;
	}

	public void moveRight()
	{
		moveVelocity = speed;
		anim.SetBool ("moving", true);
		animNewChar.SetBool("moving", true);
		moveCount = 0;
	}

	public void Jumping()
	{
		if (grounded && doSkill == false && punchCounter < -0.15 && dead == false) {
			myBody.velocity = new Vector2 (0, jump);
			jumpSFX.Play ();
		}
	}

	public void attack()
	{
		if (grounded && punchCounter<0 && dead==false) {
			anim.SetBool ("moving", false);
			animNewChar.SetBool("moving", false);
			currentVelocity = moveVelocity;
			//moveVelocity = 0;
			punchCounter = waitBetweenPunch;
			if (moveCount <2 ) {
				moveCount += 1;
				anim.SetBool ("attack1", true);
				animNewChar.SetBool("attack1", true);
			} else if (moveCount >= 2) {
				moveCount = 0;
				anim.SetBool ("attack2", true);
				animNewChar.SetBool("attack2", true);
			}
			moveVelocity = currentVelocity;
		}
	}

	public void skillA()
	{
		if (grounded && energyManager.energy>=10 && doSkill==false && dead==false) {
			//moveVelocity = 0;
			anim.SetBool ("Skill1", true);
			animNewChar.SetBool("Skill1", true);
			invulnerable = true;
			energyManager.useEnergy (10);
			Instantiate (skill1, skill1LaunchPoint.transform.position, skill1LaunchPoint.transform.rotation);
			doSkill = true;
			StartCoroutine ("wait");
		} 
	}

	public void skillB()
	{
		if (grounded && energyManager.energy>=10 &&doSkill==false && dead==false) {
			//moveVelocity=0;
			anim.SetBool ("Skill2", true);
			animNewChar.SetBool("Skill2", true);
			invulnerable = true;
			energyManager.useEnergy (10);
			Instantiate (skill2, myBody.transform.position, myBody.transform.rotation);
			doSkill = true;
			StartCoroutine ("wait");
		} 
	}

	public void unPressed()
	{
		anim.SetBool ("Skill2", false);
		anim.SetBool ("Skill1", false);
		anim.SetBool ("moving", false);

		animNewChar.SetBool("Skill2", false);
		animNewChar.SetBool("Skill1", false);
		animNewChar.SetBool("moving", false);
		moveVelocity = 0;

	}

	public void unAttack()
	{
		anim.SetBool ("Skill2", false);
		anim.SetBool ("Skill1", false);
		anim.SetBool ("attack1", false);
		anim.SetBool ("attack2", false);

		animNewChar.SetBool("Skill2", false);
		animNewChar.SetBool("Skill1", false);
		animNewChar.SetBool("attack1", false);
		animNewChar.SetBool("attack2", false);
		//anim.SetBool ("moving", false);
	}

	public void unlockNextLevel()
	{
		if (PlayerPrefs.GetInt ("stage") <= thisLevel) {
			Debug.Log ("YEY MENANG!");
			PlayerPrefs.SetInt ("stage", (thisLevel+1));
		}
	}
		
}
