using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour {

	public Text title;
	public Buttons daButton;
	// Use this for initialization
	void Start () {
		daButton = GetComponentInParent<Buttons> ();
		title = GetComponent<Text> ();
		title.text = daButton.levelName;
		StartCoroutine ("fade");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator fade()
	{
		yield return new WaitForSeconds (2);
		title.CrossFadeAlpha (0, 1, false);
	}
}
