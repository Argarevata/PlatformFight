using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

	//Daftar PlayerPrefs
	//MaxHealth
	//Speed
	//MaxEnergy
	//Attack
	//Defense
	//Skill
	//Money
	//NewGame
	//level (ini buat nama level kalo misal retry)
	//stage (ini buat tau level yg udah ke-unlock)

	//healthcost
	//energycost
	//speedcost
	//attackcost
	//defcost
	//skillcost

	//private int healthCost;
	//private int energyCost;
	//private int speedCost;
	//private int attackCost;
	//private int defCost;
	//private int skillCost;

	public Text currentHealth;
	public Text currentEnergy;
	public Text currentSpeed;
	public Text currentAttack;
	public Text currentDef;
	public Text currentSkill;

	public Text upHealthCost;
	public Text upEnergyCost;
	public Text upSpeedCost;
	public Text upSkillCost;
	public Text upAttCost;
	public Text upDefCost;

	public Text myMoney;

	public GameObject healthButton;
	public GameObject energyButton;
	public GameObject speedButton;
	public GameObject attButton;
	public GameObject defButton;
	public GameObject skillButton;

	public bool thisIsShop;

	// Use this for initialization
	void Start () {
		//Reset ();
	}
	
	// Update is called once per frame
	void Update () {

		currentHealth.text = "" + PlayerPrefs.GetInt ("MaxHealth");
		//currentDef.text = "" + PlayerPrefs.GetInt ("Defense");
		currentEnergy.text = "" + PlayerPrefs.GetInt ("MaxEnergy");
		currentSkill.text = "" + PlayerPrefs.GetInt ("Skill");
		//currentSpeed.text = "" + PlayerPrefs.GetInt ("Speed");
		currentAttack.text = "" + PlayerPrefs.GetInt ("Attack");
		myMoney.text = "" + PlayerPrefs.GetInt ("Money")+" $";

		upSkillCost.text = "" + PlayerPrefs.GetInt("skillcost") +" $";
		//upDefCost.text = "" + PlayerPrefs.GetInt("defcost") +" $";
		upAttCost.text = "" + PlayerPrefs.GetInt("attackcost") +" $";
		upHealthCost.text = "" + PlayerPrefs.GetInt("healthcost") +" $";
		upEnergyCost.text = "" + PlayerPrefs.GetInt("energycost") +" $";
		//upSpeedCost.text = "" + PlayerPrefs.GetInt("speedcost") +" $";

		if (PlayerPrefs.GetInt ("healthcost") >= 300) {
			healthButton.SetActive (false);
			upHealthCost.gameObject.SetActive (false);
			currentHealth.text = ""+" 200 (MAX)";
		}
		if (PlayerPrefs.GetInt ("energycost") >= 300) {
			energyButton.SetActive (false);
			upEnergyCost.gameObject.SetActive (false);
			currentEnergy.text = ""+" 50 (MAX)";
		}
		if (PlayerPrefs.GetInt ("attackcost") >= 300) {
			attButton.SetActive (false);
			upAttCost.gameObject.SetActive (false);
			currentAttack.text = ""+" 40 (MAX)";
		}
	    /*if (PlayerPrefs.GetInt ("defcost") >= 300) {
			defButton.SetActive (false);
			upDefCost.gameObject.SetActive (false);
			currentDef.text = ""+" 10 (MAX)";
		}
		if (PlayerPrefs.GetInt ("speedcost") >= 300) {
			speedButton.SetActive (false);
			upSpeedCost.gameObject.SetActive (false);
			currentSpeed.text = ""+" 15 (MAX)";
		}*/
		if (PlayerPrefs.GetInt ("skillcost") >= 300) {
			skillButton.SetActive (false);
			upSkillCost.gameObject.SetActive (false);
			currentSkill.text = ""+" 110 (MAX)";
		}


		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("Main Menu");
		}
	}

	public void Reset()
	{
		PlayerPrefs.SetInt ("MaxHealth", 50);
		PlayerPrefs.SetInt ("MaxEnergy", 20);
		PlayerPrefs.SetInt ("Speed", 5);
		PlayerPrefs.SetInt ("Attack", 10);
		PlayerPrefs.SetInt ("Defense", 0);
		PlayerPrefs.SetInt ("Skill", 30);

		PlayerPrefs.SetInt ("healthcost", 50);
		PlayerPrefs.SetInt ("energycost", 50);
		//PlayerPrefs.SetInt ("speedcost", 50);
		PlayerPrefs.SetInt ("attackcost", 50);
		//PlayerPrefs.SetInt ("defcost", 50);
		PlayerPrefs.SetInt ("skillcost", 50);
		PlayerPrefs.SetInt ("Money", 500);
		PlayerPrefs.SetInt ("stage", 0);
	
		if (thisIsShop == true) {
			healthButton.SetActive (true);
			upHealthCost.gameObject.SetActive (true);
			energyButton.SetActive (true);
			upEnergyCost.gameObject.SetActive (true);
			attButton.SetActive (true);
			upAttCost.gameObject.SetActive (true);
			//defButton.SetActive (true);
			//upDefCost.gameObject.SetActive (true);
			//speedButton.SetActive (true);
			//upSpeedCost.gameObject.SetActive (true);
			skillButton.SetActive (true);
			upSkillCost.gameObject.SetActive (true);

			//PlayerPrefs.SetInt ("Money", 500);
		}
	}

	public void upHealth()
	{
		if (PlayerPrefs.GetInt ("Money") >= PlayerPrefs.GetInt("healthcost")) {
			PlayerPrefs.SetInt ("Money", (PlayerPrefs.GetInt ("Money") - PlayerPrefs.GetInt("healthcost")));
			PlayerPrefs.SetInt ("MaxHealth", (PlayerPrefs.GetInt ("MaxHealth") + 15));
			PlayerPrefs.SetInt ("healthcost", (PlayerPrefs.GetInt ("healthcost") + 25));
		}
	}

	public void upEnergy()
	{
		if (PlayerPrefs.GetInt ("Money") >= PlayerPrefs.GetInt("energycost")) {
			PlayerPrefs.SetInt ("Money", (PlayerPrefs.GetInt ("Money") - PlayerPrefs.GetInt("energycost")));
			PlayerPrefs.SetInt ("MaxEnergy", (PlayerPrefs.GetInt ("MaxEnergy") + 3));
			PlayerPrefs.SetInt ("energycost", (PlayerPrefs.GetInt ("energycost") + 25));
		}
	}

	public void upSpeed()
	{
		if (PlayerPrefs.GetInt ("Money") >= PlayerPrefs.GetInt("speedcost")) {
			PlayerPrefs.SetInt ("Money", (PlayerPrefs.GetInt ("Money") - PlayerPrefs.GetInt("speedcost")));
			PlayerPrefs.SetInt ("Speed", (PlayerPrefs.GetInt ("Speed") + 1));
			PlayerPrefs.SetInt ("speedcost", (PlayerPrefs.GetInt ("speedcost") + 25));
		}
	}

	public void upAttack()
	{
		if (PlayerPrefs.GetInt ("Money") >= PlayerPrefs.GetInt("attackcost")) {
			PlayerPrefs.SetInt ("Money", (PlayerPrefs.GetInt ("Money") - PlayerPrefs.GetInt("attackcost")));
			PlayerPrefs.SetInt ("Attack", (PlayerPrefs.GetInt ("Attack") + 3));
			PlayerPrefs.SetInt ("attackcost", (PlayerPrefs.GetInt ("attackcost") + 25));
		}
	}

	public void upDef()
	{
		if (PlayerPrefs.GetInt ("Money") >= PlayerPrefs.GetInt("defcost")) {
			PlayerPrefs.SetInt ("Money", (PlayerPrefs.GetInt ("Money") - PlayerPrefs.GetInt("defcost")));
			PlayerPrefs.SetInt ("Defense", (PlayerPrefs.GetInt ("Defense") + 1));
			PlayerPrefs.SetInt ("defcost", (PlayerPrefs.GetInt ("defcost") + 25));
		}
	}

	public void upSkill()
	{
		if (PlayerPrefs.GetInt ("Money") >= PlayerPrefs.GetInt("skillcost")) {
			PlayerPrefs.SetInt ("Money", (PlayerPrefs.GetInt ("Money") - PlayerPrefs.GetInt("skillcost")));
			PlayerPrefs.SetInt ("Skill", (PlayerPrefs.GetInt ("Skill") + 8));
			PlayerPrefs.SetInt ("skillcost", (PlayerPrefs.GetInt ("skillcost") + 25));
		}
	}

	public void exitToMain()
	{
		Application.LoadLevel ("MainMenu");
	}
}
