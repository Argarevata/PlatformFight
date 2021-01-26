using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogCutScene : MonoBehaviour {

    public Image char1;
    public Image char2;
    private bool char1Act;
    private bool char2Act;

    public string levelToLoad;

    public int[] char1List;
    public int[] char2List;

    public GameObject name1;
    public GameObject name2;

    public GameObject[] text;

    private int x;
    public int max;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (x == 0)
        {
            name1.SetActive(false);
            name2.SetActive(false);

            for (int i = 0; i < text.Length; i++)
            {
                text[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < text.Length; i++)
            {
                text[i].SetActive(false);
            }
            text[x - 1].SetActive(true);

            if (char1List[x-1] == 1)
            {
                char1Act = true;
                char2Act = false;
            }
            else if (char2List[x-1] == 1)
            {
                char1Act = false;
                char2Act = true;
            }
        }

        if (char1Act == true)
        {
            name1.SetActive(true);
            name2.SetActive(false);
            char1.color = new Color(1, 1, 1, 1);
            char2.color = new Color(0.35f, 0.35f, 0.35f, 1);
        }

        if(char2Act == true)
        {
            name2.SetActive(true);
            name1.SetActive(false);
            char2.color = new Color(1, 1, 1, 1);
            char1.color = new Color(0.35f, 0.35f, 0.35f, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (x < max)
            {
                x++;
            }
            else
            {
                Application.LoadLevel(levelToLoad);
            }
        }
	}

    public void next()
    {
        if (x<max)
            {
                x++;
            }
            else
            {
                Application.LoadLevel(levelToLoad);
            }
    }
}
