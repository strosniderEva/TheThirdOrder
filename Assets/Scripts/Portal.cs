using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int levelsComplete;
    // Start is called before the first frame update
    void Start()
    {
        levelsComplete = GameManager.Instance.levelsComplete;
    }

    // Update is called once per frame
    void Update()
    {
        levelsComplete = GameManager.Instance.levelsComplete;
    }

    void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Upgrade"))
        {
            if (levelsComplete == 0) {
                SceneManager.LoadSceneAsync("Wave1");
            }
            else if(levelsComplete == 1) {
                SceneManager.LoadSceneAsync("Wave2");
            }
            else if (levelsComplete == 2){
                SceneManager.LoadSceneAsync("Wave3");
            }
            else if (levelsComplete == 3){
                SceneManager.LoadSceneAsync("Boss");
            }
        }
        else
        {
            GameObject[] allEnemies;
            allEnemies = GameObject.FindGameObjectsWithTag("enemy");

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Wave1") && allEnemies.Length == 0)
            {
                GameManager.Instance.levelsComplete = 1;
                SceneManager.LoadSceneAsync("Upgrade");
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Wave2") && allEnemies.Length == 0)
            {
                GameManager.Instance.levelsComplete = 2;
                SceneManager.LoadSceneAsync("Upgrade");
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Wave3") && allEnemies.Length == 0)
            {
                GameManager.Instance.levelsComplete = 3;
                SceneManager.LoadSceneAsync("Upgrade");
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Boss") && allEnemies.Length == 0)
            {
                GameManager.Instance.levelsComplete = 4;
                SceneManager.LoadSceneAsync("Upgrade");
            }
        }
    }
}
