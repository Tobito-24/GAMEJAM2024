using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGamePaused = false;
    private SceneLoader sceneLoader;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameFinished;


    private void Start()
    {
        Time.timeScale = 1;
        mainMenuCanvas.SetActive(false);
        // mainMenuCanvas.SetActive(true);
        // Time.timeScale = 0;
        winCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    void Update()
    {
        CheckInput();
        CheckEnemiesAndSwitchScene();
        CheckHealth();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainMenuCanvas.activeInHierarchy)
        {
            Time.timeScale = 0;
            mainMenuCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && mainMenuCanvas.activeInHierarchy)
        {
            mainMenuCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }
    void CheckEnemiesAndSwitchScene()
    {
        // Check if there are no objects with the specified tags
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 &&
            GameObject.FindGameObjectsWithTag("RedEnemy").Length == 0 &&
            GameObject.FindGameObjectsWithTag("RedTurret").Length == 0 &&
            GameObject.FindGameObjectsWithTag("WhiteEnemy").Length == 0 &&
            GameObject.FindGameObjectsWithTag("WhiteTurret").Length == 0 &&
            GameObject.FindGameObjectsWithTag("GreenEnemy").Length == 0 &&
            GameObject.FindGameObjectsWithTag("GreenTurret").Length == 0 &&
            GameObject.FindGameObjectsWithTag("BlueEnemy").Length == 0 &&
            GameObject.FindGameObjectsWithTag("BlueTurret").Length == 0) 
        
        {
            WinScene();
        }
    }

    void CheckHealth()
    {
        PlayerHealth health = FindObjectOfType<PlayerHealth>();
        if (health.IsDead())
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;

        }
    }

    void WinScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "LevelThree")
        {
            Time.timeScale = 0;
            gameFinished.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            winCanvas.SetActive(true);
        }
    }

}