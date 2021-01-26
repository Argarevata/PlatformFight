using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

	PlayerHealth theHealth;
	EnergyManager theEnergy;
	public bool energy;
	public bool health;
	public bool coin;
	public int value;
	public PlayerMotor thePlayer;

	public AudioSource coinSound;
	public AudioSource potionSound;
	public SpriteRenderer sprite;
	public bool activated;

	// Use this for initialization
	void Start () {
		if (value <= 0) {
			value = 10;
		}

		activated = true;

		thePlayer = FindObjectOfType<PlayerMotor> ();
		theHealth = FindObjectOfType<PlayerHealth> ();
		theEnergy = FindObjectOfType<EnergyManager> ();
		Debug.Log ("Your money = " + PlayerPrefs.GetInt ("Money"));
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player" && activated==true) {
			activated = false;
			if (energy == true) {
				if(theEnergy.energy+20<=theEnergy.maxEnergy)
				{
					theEnergy.energy += 20;
				}
				else{
					theEnergy.energy=theEnergy.maxEnergy;
				}
			}

			if(health == true)
			{
				if(theHealth.currentHealth+30<=theHealth.maxHealth)
				{
					theHealth.currentHealth+=30;
				}
				else{
					theHealth.currentHealth=theHealth.maxHealth;
				}
			}

			if(coin == true)
			{
				PlayerPrefs.SetInt("Money",PlayerPrefs.GetInt("Money")+value);
				Debug.Log ("Your money = " + PlayerPrefs.GetInt ("Money"));
				thePlayer.moneyCollected += value;
				coinSound.Play ();
			}

			sprite.color = new Color (1, 1, 1, 0);
			StartCoroutine ("done");
		}
	}

	IEnumerator done()
	{
		yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
	}
}
