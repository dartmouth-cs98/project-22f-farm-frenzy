using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    public GameObject scoreMenu;

    // Start is called before the first frame update
    void Start()
    {
            pauseMenu.SetActive(false);
            scoreMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame() 
    {
        scoreMenu.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame() 
    {
        scoreMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu() {
        Time.timeScale = 1f;

        // Place holder until we get a real main menu scene
        SceneManager.LoadScene("Scene2");

    }

    public void GoToOptions() {
        Time.timeScale = 1f;

        // Place holder until we get a real options scene
        // SceneManager.LoadScene("ExampleScene");
    }

    public void Quitgame() {
        
        Application.Quit();

    }
}
