using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class Video : MonoBehaviour {

    public MovieTexture movieClip;
    private AudioSource audio;
    public string levelToLoad;

	// Use this for initialization
	void Start () {
        GetComponent<RawImage>().texture = movieClip as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movieClip.audioClip;
        movieClip.Play();
        audio.Play();
        StartCoroutine("go");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            Application.LoadLevel(levelToLoad);
        }
	}

    IEnumerator go()
    {
        yield return new WaitForSeconds(47);
        Application.LoadLevel(levelToLoad);
    }

    public void skip()
    {
        Application.LoadLevel(levelToLoad);
    }
}
