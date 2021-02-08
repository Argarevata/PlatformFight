using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;
	//public Text theText;
	public Slider healthBar;
	public GameObject loseScreen;
	public PlayerMotor thePlayer;
	public GameObject blood;
	public GameObject redScreen;
	public AdmobScript theAdmob;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("MaxHealth", 100);
		maxHealth = PlayerPrefs.GetInt ("MaxHealth");
		currentHealth = maxHealth;
		healthBar.maxValue = maxHealth;
		thePlayer = FindObjectOfType<PlayerMotor> ();
		theAdmob = FindObjectOfType<AdmobScript>();
	}

	public void BoostHealth()
	{
		PlayerPrefs.SetInt("MaxHealth", 200);
		maxHealth = PlayerPrefs.GetInt("MaxHealth");
		currentHealth = maxHealth;
		healthBar.maxValue = maxHealth;
		thePlayer = FindObjectOfType<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
		
		healthBar.value = currentHealth;

		if (currentHealth <= 0) {
			//loseScreen.SetActive (true);
			//Time.timeScale = 0;
			
			thePlayer.anim.SetBool("die", true);
			thePlayer.dead=true;
			StartCoroutine ("death");
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			//currentHealth = 1;
		}
	}

	public void damaged(int damage)
	{
		if (thePlayer.invulnerable == false) {
			if (damage - PlayerPrefs.GetInt ("Defense") <= 0) {
				currentHealth--;
			} else {
				currentHealth -= (damage - PlayerPrefs.GetInt ("Defense"));
				Instantiate (blood, thePlayer.transform.position, thePlayer.transform.rotation);
				//StartCoroutine ("turnRed");
			}
		}
	}

	public IEnumerator death()
	{
		//redScreen.SetActive (true);
		yield return new WaitForSeconds (1);
		loseScreen.SetActive (true);
		if (theAdmob != null)
		{
			theAdmob.ShowInterstitial();
		}
		Time.timeScale = 0;
	}

	IEnumerator turnRed()
	{
		redScreen.SetActive (true);
		yield return new WaitForSeconds (0.3f);
		redScreen.SetActive (false);
	}
}
