using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMover : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D myBody, playerBody;
    public PlayerMotor thePlayer;
    public cameraController theCam;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<PlayerMotor>();
        playerBody = thePlayer.GetComponent<Rigidbody2D>();
        theCam = FindObjectOfType<cameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theCam.canMove && !theCam.freeze)
        {
            myBody.velocity = new Vector2(playerBody.velocity.x / 5, 0);
        }
        else
        {
            myBody.velocity = Vector2.zero;
        }
    }
}
