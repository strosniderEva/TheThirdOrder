using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWon : MonoBehaviour
{
    public GameObject lasers;
    public GameObject winUI;
    public GameObject pauseUI;
    public GameObject gameOverUI;
    public GameObject playerStats;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        winUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameWon)
        {
            GameWon();
        }
    }

    void GameWon()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        lasers.SetActive(false);
        Time.timeScale = 0f;
        winUI.SetActive(true);
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        playerStats.SetActive(false);
    }
}
