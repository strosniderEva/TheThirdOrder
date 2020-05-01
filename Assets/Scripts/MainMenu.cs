using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject aboutMenu;
    public GameObject credits;
    public GameObject controls;

    public Button btnCred;
    public Button btnCon;

    void Start()
    {
        //on load display correct hierarchy of UI
        mainMenu.SetActive(true);
        aboutMenu.SetActive(false);
        credits.SetActive(true);
        controls.SetActive(false);
    }

    void Update() {
        if (aboutMenu.activeSelf) {
            if (credits.activeSelf)
            {
                btnCred.Select();
            }
            else {
                btnCon.Select();
            }
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
