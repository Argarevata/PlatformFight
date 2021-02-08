using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public int segment;
	public int segmentDone;
	public GameObject[] triggers;
	public GameObject[] Margins;
	public PlayerMotor thePlayer;
	public cameraController theCam;
	public int[] enemiesPerSegment;
	public GameObject[] enemies;
	public int killCount;
	private int i=0;

	public GameObject Go;
	public Text fight;

	public bool sideScroll;

	public bool KillAllGameplay;
	public int killCountTarget;
	public float pauseCounter;
	public float waitPause;
	public Text enemiesRemaining;

	public bool survival;
	public bool grinding;
	public float summonCounter;
	public float waitBetweenSummon;
	public int minutes;
	public float seconds;
	public Text minutesText;
	public Text secondsText;
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject spawnPoint;
	private int randNumb;

	public int bulletDamage;
	public int hitDamage;
	public float Enemy1Health;
	public float Enemy2Health;
	public int playerAttack, playerSkill;

	void Start () {
		thePlayer = FindObjectOfType<PlayerMotor> ();
		theCam = FindObjectOfType<cameraController> ();
		pauseCounter = waitPause;

		if (Enemy1Health < 1) {
			Enemy1Health = 30;
		}

		if (Enemy2Health < 1) {
			Enemy2Health = 30;
		}
	}

	void Update () {

		if (sideScroll == true) {
			if (segmentDone < segment) {
				if (thePlayer.transform.position.x >= triggers [segmentDone].transform.position.x) {
					fight.gameObject.SetActive (true);
					Margins [segmentDone].SetActive (true);
					triggers [segmentDone].transform.position = new Vector2 (-1000, 1000);
					theCam.freeze = true;

					for (i = i; i < enemiesPerSegment [segmentDone]; i++) {
						enemies [i].SetActive (true);
					}
					
					if (killCount >= enemiesPerSegment [segmentDone]) {
						StartCoroutine ("goToNext");
						fight.gameObject.SetActive (false);
						segmentDone++;
						//triggers [segmentDone - 1].transform.position = new Vector2 (1000, 1000);
						theCam.freeze = false;
						Margins [segmentDone - 1].SetActive (false);
					}
				}
			}
		}

		if (KillAllGameplay == true) {
			enemiesRemaining.text = ""+(killCountTarget-killCount)+" Enemies Left!";
			if (segmentDone < segment) 
			{
				for(i=i;i<enemiesPerSegment[segmentDone];i++)
				{
					enemies[i].SetActive(true);
				}

				if(killCount >= enemiesPerSegment[segmentDone])
				{
					segmentDone++;
				}
			}

			if (killCount >= killCountTarget) {
				pauseCounter -= Time.deltaTime;
				if (pauseCounter <= 0) {
					Debug.Log ("WIN");
					Time.timeScale = 0;
					thePlayer.winScreen.SetActive (true);
				}
			}
		}

		if (survival == true) {
			summonCounter -= Time.deltaTime;
			seconds -= Time.deltaTime;
			if (seconds <= 0) {
				minutes--;
				seconds = 59;
			}

			minutesText.text = "" + minutes+" : ";
			secondsText.text = "" + Mathf.Round (seconds);

			spawnPoint.transform.position = new Vector2 (thePlayer.transform.position.x, spawnPoint.transform.position.y);
			if (summonCounter < 0) {
				randNumb = Random.Range (1, 4);
				if (randNumb == 1) {
					Instantiate (enemy1, spawnPoint.transform.position, spawnPoint.transform.rotation);
				} else if (randNumb == 2) {
					Instantiate (enemy2, spawnPoint.transform.position, spawnPoint.transform.rotation);
				} else {
					Instantiate (enemy3, spawnPoint.transform.position, spawnPoint.transform.rotation);
				}

				randNumb = Random.Range (1, 4);
				if (randNumb == 1) {
					Instantiate (enemy1, new Vector3(spawnPoint.transform.position.x+15,spawnPoint.transform.position.y+5,spawnPoint.transform.position.z), spawnPoint.transform.rotation);
				} else if (randNumb == 2) {
					Instantiate (enemy2, new Vector3(spawnPoint.transform.position.x+15,spawnPoint.transform.position.y+5,spawnPoint.transform.position.z), spawnPoint.transform.rotation);
				} else {
					Instantiate (enemy3, new Vector3(spawnPoint.transform.position.x+15,spawnPoint.transform.position.y+5,spawnPoint.transform.position.z), spawnPoint.transform.rotation);
				}

				randNumb = Random.Range (1, 4);
				if (randNumb == 1) {
					Instantiate (enemy1, new Vector3(spawnPoint.transform.position.x-15,spawnPoint.transform.position.y+8,spawnPoint.transform.position.z), spawnPoint.transform.rotation);
				} else if (randNumb == 2) {
					Instantiate (enemy2, new Vector3(spawnPoint.transform.position.x-15,spawnPoint.transform.position.y+8,spawnPoint.transform.position.z), spawnPoint.transform.rotation);
				} else {
					Instantiate (enemy3, new Vector3(spawnPoint.transform.position.x-15,spawnPoint.transform.position.y+8,spawnPoint.transform.position.z), spawnPoint.transform.rotation);
				}


				summonCounter = waitBetweenSummon;
			}

			if (minutes < 0) {
				minutesText.text="00 :";
				secondsText.text=" 00";
				thePlayer.winScreen.SetActive (true);
				Time.timeScale = 0;
			}


		}

		if (grinding == true) {
			summonCounter -= Time.deltaTime;
			seconds += Time.deltaTime;
			if (seconds >= 59) {
				minutes++;
				seconds = 0;
			}

			minutesText.text = "" + minutes+" : ";
			secondsText.text = "" + Mathf.Round (seconds);

			spawnPoint.transform.position = new Vector2 (thePlayer.transform.position.x, spawnPoint.transform.position.y);
			if (summonCounter < 0) {
				randNumb = Random.Range (1, 4);
				if (randNumb == 1) {
					Instantiate (enemy1, spawnPoint.transform.position, spawnPoint.transform.rotation);
				} else if (randNumb == 2) {
					Instantiate (enemy2, spawnPoint.transform.position, spawnPoint.transform.rotation);
				} else {
					Instantiate (enemy3, spawnPoint.transform.position, spawnPoint.transform.rotation);
				}

				randNumb = Random.Range (1, 4);
				if (randNumb == 1) {
					Instantiate (enemy1, new Vector3(spawnPoint.transform.position.x+15,spawnPoint.transform.position.y+5,spawnPoint.transform.position.z), spawnPoint.transform.rotation);
				} else if (randNumb == 2) {
					Instantiate (enemy2, new Vector3(spawnPoint.transform.position.x+15,spawnPoint.transform.position.y+5,spawnPoint.transform.position.z), spawnPoint.transform.rotation);
				} else {
					Instantiate (enemy3, new Vector3(spawnPoint.transform.position.x+15,spawnPoint.transform.position.y+5,spawnPoint.transform.position.z), spawnPoint.transform.rotation);
				}

				randNumb = Random.Range (1, 4);
				if (randNumb == 1) {
					Instantiate (enemy1, new Vector3(spawnPoint.transform.position.x-15,spawnPoint.transform.position.y+8,spawnPoint.transform.position.z), spawnPoint.transform.rotation);
				} else if (randNumb == 2) {
					Instantiate (enemy2, new Vector3(spawnPoint.transform.position.x-15,spawnPoint.transform.position.y+8,spawnPoint.transform.position.z), spawnPoint.transform.rotation);
				} else {
					Instantiate (enemy3, new Vector3(spawnPoint.transform.position.x-15,spawnPoint.transform.position.y+8,spawnPoint.transform.position.z), spawnPoint.transform.rotation);
				}


				summonCounter = waitBetweenSummon;
			}

			if ((seconds >= 30.5 && seconds < 31) || (seconds>=58.5 && seconds<59)) {
				Enemy1Health+=5;
				Enemy2Health+=5;

				bulletDamage+=2;
				hitDamage+=1;
				Debug.Log ("Difficulty raised");
				seconds++;
			}

		}


	}

	IEnumerator goToNext()
	{
		Go.SetActive (true);
		yield return new WaitForSeconds (3);
		Go.SetActive (false);
	}

	IEnumerator fightNow()
	{
		fight.gameObject.SetActive (true);
		yield return new WaitForSeconds (2);
		fight.gameObject.SetActive (false);
	}
		
}
