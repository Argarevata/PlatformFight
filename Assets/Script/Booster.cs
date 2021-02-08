using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public EnergyManager theEnergyMan;
    public PlayerMotor thePLayer;
    public PlayerHealth theHealth;

    private void Start()
    {
        Time.timeScale = 0;    
    }

    public void BoostHealth()
    {
        theHealth.BoostHealth();
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void BoostEnergy()
    {
        theEnergyMan.BoostEnergy();
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void BoostAttack()
    {
        PlayerPrefs.SetInt("Attack", 10);
        PlayerPrefs.SetInt("Skill", 20);
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void JustPlay()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

}
