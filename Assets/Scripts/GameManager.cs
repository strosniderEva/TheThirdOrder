using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //player stats UI
    public float shield;
    public float health;
    public int credits;

    public int levelsComplete;
    public bool isDead;
    public bool gameWon;

    //player upgrades
    public bool sUpgrade1;
    public bool sUpgrade2;
    public bool sUpgrade3;
    public bool lUpgrade1;
    public bool lUpgrade2;
    public bool lUpgrade3;
    public bool rUpgrade1;
    public bool rUpgrade2;
    public bool rUpgrade3;

    //values to upgrade
    public int shieldPerSecond;
    public int shieldTimerTillRegen;
    public int missileRange;
    public int missileDamage;
    public int missileLimit;
    public int laserRange;
    public int laserDamage;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;

            //initialize player variables
            shield = 100;
            health = 100;
            credits =1000;
            isDead = false;
            gameWon = false;
            levelsComplete = 0;
            sUpgrade1 = false;
            sUpgrade2 = false;
            sUpgrade3 = false;
            lUpgrade1 = false;
            lUpgrade2 = false;
            lUpgrade3 = false;
            rUpgrade1 = false;
            rUpgrade2 = false;
            rUpgrade3 = false;
            shieldPerSecond = 5;
            shieldTimerTillRegen = 15;
            missileRange = 50;
            missileDamage = 50;
            missileLimit = 1;
            laserRange = 50;
            laserDamage = 50;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(levelsComplete == 4)
        {
            gameWon = true;
        }

        if (sUpgrade3 == true)
        {
            shieldPerSecond = 10;
        }
        else if (sUpgrade2 == true)
        {
            shieldTimerTillRegen = 5;
        }

        if (lUpgrade3 == true)
        {
            laserRange = 200;
        }
        else if (lUpgrade2 == true)
        {
            laserDamage = 100;
        }
        else if (lUpgrade1 == true)
        {
            laserRange = 100;
        }

        if (rUpgrade3 == true)
        {
            missileRange = 150;
            missileDamage = 150;
            missileLimit = 3;
        }
        else if (rUpgrade2 == true)
        {
            missileRange = 100;
            missileLimit = 3;
        }
        else if (rUpgrade1 == true)
        {
            missileRange = 80;
            missileDamage = 75;
            missileLimit = 2;
        }
    }
}
