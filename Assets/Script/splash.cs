using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splash : MonoBehaviour {

	public float waitingTime;
	public string levelToLoad;
	public bool splashScreen;

	// Use this for initialization
	void Start () {
		if (splashScreen == true) {
			StartCoroutine ("now");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)) {
			Application.LoadLevel(levelToLoad);
		}
	}

	IEnumerator now()
	{
		yield return new WaitForSeconds (waitingTime);
		Application.LoadLevel (levelToLoad);
	}

	public void playNow()
	{
		Application.LoadLevel(levelToLoad);
	}
}
