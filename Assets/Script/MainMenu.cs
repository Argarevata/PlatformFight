using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public string levelToGo;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		if (PlayerPrefs.GetInt ("Ver2") != 99) {
			PlayerPrefs.SetInt ("MaxHealth", 50);
			PlayerPrefs.SetInt ("MaxEnergy", 20);
			PlayerPrefs.SetInt ("Speed", 5);
			PlayerPrefs.SetInt ("Attack", 10);
			PlayerPrefs.SetInt ("Defense", 0);
			PlayerPrefs.SetInt ("Skill", 30);

			PlayerPrefs.SetInt ("healthcost", 50);
			PlayerPrefs.SetInt ("energycost", 50);
			PlayerPrefs.SetInt ("speedcost", 50);
			PlayerPrefs.SetInt ("attackcost", 50);
			PlayerPrefs.SetInt ("defcost", 50);
			PlayerPrefs.SetInt ("skillcost", 50);

			PlayerPrefs.SetInt ("Money", 500);
			PlayerPrefs.SetInt ("Ver2", 99);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void play()
	{
		Application.LoadLevel ("trial");
	}

	public void shop()
	{
		Application.LoadLevel ("Shop");
	}

	public void survive()
	{
		Application.LoadLevel ("survival");
	}

	public void Boss()
	{
		Application.LoadLevel ("boss");
	}

	public void killAll()
	{
		Application.LoadLevel ("kill all");
	}

	public void mainMenu()
	{
		Application.LoadLevel ("MainMenu");
	}

	public void goToLevel()
	{
		Application.LoadLevel (levelToGo);
	}
}
