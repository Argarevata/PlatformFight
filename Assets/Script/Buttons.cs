using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

	public GameObject pauseScreen;
	public bool tryAgain;
	public string levelName;
	public Text moneyCollected;
	public PlayerMotor player;
    public bool showMoney;
    public Text myMoney;

	// Use this for initialization
	void Start () {
		if (tryAgain == false) {
			PlayerPrefs.SetString ("level", levelName);
		} else {
			letsRetry ();
		}
		player = FindObjectOfType<PlayerMotor> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		try
		{
			moneyCollected.text = "" + player.moneyCollected;
		}
		catch {
			Debug.Log ("no text for collected money");
		}

        if(showMoney == true)
        {
            myMoney.text = "" + PlayerPrefs.GetInt("Money");
        }
	}

	public void playSir()
	{
		Application.LoadLevel ("trial");
	}

	public void pause()
	{
		Time.timeScale = 0;
		pauseScreen.SetActive (true);
	}

	public void resume()
	{
		Time.timeScale = 1f;
		pauseScreen.SetActive (false);
	}

	public void map()
	{
		Application.LoadLevel ("Level Select");
	}

	public void retry()
	{
		Application.LoadLevel (levelName);
	}

	void letsRetry()
	{
		Application.LoadLevel (PlayerPrefs.GetString ("level"));
	}

	public void exit()
	{
		Application.Quit ();
	}

    public void BackToShop()
    {
        Application.LoadLevel("Shop");
    }

    public void IAP()
    {
        Application.LoadLevel("InAppPurchase");
    }

	public void developeMode()
	{
		Application.LoadLevel("Chapter1Map");
	}

	public void normalMode()
	{
		Application.LoadLevel("MainMenu");
	}

	public void shopping()
	{
		Application.LoadLevel ("Shop");
	}

	public void grinding()
	{
		Application.LoadLevel ("Grinding");
	}
}
