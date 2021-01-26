/*using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D myrigidbody;
	public float speed;
	public float speedup;
	public float jump;
	public float wait;


	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;
	private bool doubleJumped;

	private Animator anim;
	public Transform firePoint;
	public GameObject bullet;
	public float shotDelay;
	public float shotDelayCounter;

	public float knockback;
	public float knockbackCount;
	public float knockbackLength;
	public bool knockFromRight;
	private float moveVelocity;
	//public HealthManager myhealth;

	public AudioSource shootSound;
	public AudioSource jumpSound;
	public AudioSource ouch;
	//public Gameplay2 theGameplay2;


	// Use this for initialization
	void Start () {


		myrigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
		moveVelocity = speed;
		knockbackCount = -1;
		//myhealth = GetComponent<HealthManager> ();
		//theGameplay2 = GetComponent<Gameplay2> ();
	}

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}
	
	// Update is called once per frame
	void Update () {

		if (grounded)
			doubleJumped = false;
		anim.SetBool ("grounded", grounded);

		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			Jump ();
		}
		if (Input.GetKeyDown (KeyCode.Space) && !doubleJumped && !grounded) {
			Jump ();
			doubleJumped = true;
		}
		if (Input.GetKey (KeyCode.D)) {
			//myrigidbody.velocity = new Vector2 (speed, myrigidbody.velocity.y);
			moveVelocity = speed;
			anim.SetBool ("left", true);
		} else {
			anim.SetBool ("left", false);
			//moveVelocity=0;
		}

		if (Input.GetKey (KeyCode.A)) {
			//myrigidbody.velocity = new Vector2 (-speed, myrigidbody.velocity.y);
			moveVelocity = -speed;
			anim.SetBool ("left", true);
		} else {
			anim.SetBool ("left", false);
			//moveVelocity=0;
		} 
		if (knockbackCount <= 0) {
			myrigidbody.velocity = new Vector2 (moveVelocity, myrigidbody.velocity.y);
			moveVelocity = 0;
		} else {
			if (knockFromRight)
				myrigidbody.velocity = new Vector2 (-knockback, knockback);
			if (!knockFromRight)
				myrigidbody.velocity = new Vector2 (knockback, knockback);
			knockbackCount -= Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.LeftShift) && grounded) {
			speed = speedup;
		} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			speed = 7;
		}
	
		if (myrigidbody.velocity.x > 0) {
			transform.localScale = new Vector3 (-1f, 1f, 1f);
			anim.SetBool ("left", true);
		} else if (myrigidbody.velocity.x < 0) {
			transform.localScale = new Vector3 (1f, 1f, 1f);
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			shotDelayCounter=shotDelay;
			anim.SetBool("Shoot", true);
			shootSound.Play();
			//if(theGameplay2 == false)
			//{
			//	Instantiate (bullet, firePoint.position, firePoint.rotation);
			//	shotDelayCounter = shotDelay;
			//}
		}
		if (Input.GetKey (KeyCode.L)) {


			//if(theGameplay2 == true)
			{
				if(grounded)
				{
					anim.SetBool("Shoot", true);
					//shootSound.Play();
					speed=0;
				}
				else
				{
					anim.SetBool("Shoot", true);
					//shootSound.Play();
				}
			}



				if (theGameplay2 == false) {
					anim.SetBool ("Shoot", true);
					shotDelayCounter -= Time.deltaTime;
					if (shotDelayCounter <= 0) {
						shotDelayCounter = shotDelay;
						shootSound.Play ();
						Instantiate (bullet, firePoint.position, firePoint.rotation);
					}
				}
				if (grounded && moveVelocity != 0) {
					anim.SetBool ("left", true);
				}

			} else if(Input.GetKeyUp (KeyCode.L)){
				anim.SetBool ("Shoot", false);
				shotDelayCounter = shotDelay;
				speed=7;
				speedup=14;
			}
			//if (anim.GetBool ("sword")) {
			//	anim.SetBool ("sword", false);
			//}
			//if (Input.GetKey (KeyCode.J)) {
			//	anim.SetBool ("sword", true);
			//}


		}

	void Jump()
	{
		jumpSound.Play ();
	myrigidbody.velocity=new Vector2(0,jump);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy") 
		{
			ouch.Play();
		}


	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "MovingPlatform") 
		{
			transform.parent = other.transform;
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if (other.transform.tag == "MovingPlatform") 
		{
			transform.parent = null;
		}
	}
	public IEnumerator holdup()
	{
		yield return new WaitForSeconds (shotDelay);
	}
}
*/