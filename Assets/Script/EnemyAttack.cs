using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public int damage;
	public PlayerHealth thePlayerHealth;
	public LevelManager level;

	// Use this for initialization
	void Start () {
		thePlayerHealth = FindObjectOfType<PlayerHealth> ();
		level = FindObjectOfType<LevelManager> ();
		if (level.hitDamage < 1) {
			damage = 5;
		} else {
			damage = level.hitDamage;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name == "Player") {
			thePlayerHealth.damaged (damage);
		}
	}
}
