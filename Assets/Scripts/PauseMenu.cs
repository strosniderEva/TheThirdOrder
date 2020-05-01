using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject lasers;
    public GameObject pauseUI;
    public GameObject playerInfo;
    public GameObject gameOver;

    public AudioSource ambiance;    //ambient background noise
    public AudioSource engine;      //player's engine - audiosource is attached to the "ship" object
    public AudioSource pause;

    void Start() {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseUI.activeSelf)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        lasers.SetActive(true);
        playerInfo.SetActive(true);
        ambiance.Play();
        engine.Play();
        pauseUI.SetActive(false);
        pause.Pause();
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        lasers.SetActive(false);
        playerInfo.SetActive(false);
        ambiance.Pause();
        engine.Pause();
        pauseUI.SetActive(true);
        pause.Play();
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Destroy(GameManager.Instance);
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit() {
        Destroy(GameManager.Instance);
        SceneManager.LoadSceneAsync(0);
    }
}
