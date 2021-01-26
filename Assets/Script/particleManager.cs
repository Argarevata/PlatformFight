using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ("life");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator life()
	{
		yield return new WaitForSeconds (1);
		Destroy (gameObject);
	}
}
