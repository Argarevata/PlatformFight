using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockLevel : MonoBehaviour {

	//player prefs "stage"
	public Button[] levels;

	// Use this for initialization
	void Start () {
		Debug.Log ("level sekarang : " + PlayerPrefs.GetInt ("stage"));
		if (PlayerPrefs.GetInt ("stage") < 1) {
			PlayerPrefs.SetInt ("stage", 1);
		}
		for (int i = 0; i < PlayerPrefs.GetInt("stage"); i++) {
			levels [i].interactable = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
