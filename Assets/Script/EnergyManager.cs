using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {

	public int maxEnergy;
	public int energy;
	public Text theText;
	public float coolDown;
	private bool dontAdd;
	public Slider energyBar;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("MaxEnergy", 30);
		maxEnergy = PlayerPrefs.GetInt ("MaxEnergy");
		energy = maxEnergy;
		dontAdd = false;
		energyBar = GetComponent<Slider>();
		energyBar.maxValue = maxEnergy;
	}

	public void BoostEnergy()
	{
		PlayerPrefs.SetInt("MaxEnergy", 60);
		maxEnergy = PlayerPrefs.GetInt("MaxEnergy");
		energy = maxEnergy;
		dontAdd = false;
		energyBar = GetComponent<Slider>();
		energyBar.maxValue = maxEnergy;
	}

	// Update is called once per frame
	void Update () {

		energyBar.value = energy;

		if ((energy < maxEnergy) && dontAdd==false) {
			energy++;
			dontAdd = true;
			StartCoroutine ("recharge");
		}
	}

	public void useEnergy(int amount)
	{
		energy -= amount;
	}

	IEnumerator recharge()
	{
		yield return new WaitForSeconds (1);
		dontAdd = false;
	}
}
