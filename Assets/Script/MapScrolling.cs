using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScrolling : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void map1()
	{
		anim.SetBool ("map1", true);
		anim.SetBool ("map2", false);
		anim.SetBool ("map3", false);
	}

	public void map2()
	{
		anim.SetBool ("map1", false);
		anim.SetBool ("map2", true);
		anim.SetBool ("map3", false);
	}

	public void map3()
	{
		anim.SetBool ("map1", false);
		anim.SetBool ("map2", false);
		anim.SetBool ("map3", true);
	}
}
