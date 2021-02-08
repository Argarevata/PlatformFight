using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour {

	public GameObject money;
	public GameObject potionBlue;
	public GameObject potionRed;
	public GameObject prize;

	int luckNumb;
	int type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void dropItem(Vector3 x,Quaternion y)
	{
		luckNumb = Random.Range (0, 11);

		if(luckNumb>=5)
		{
			type = Random.Range (0, 15);
			if (type <= 12) {
				prize = money;
			} else if (type > 12 && type <= 13) {
				prize = potionBlue;
				Instantiate(prize, x, y);
			} else {
				prize = potionRed;
				Instantiate(prize, x, y);
			}
			
		}
		

	}
}
