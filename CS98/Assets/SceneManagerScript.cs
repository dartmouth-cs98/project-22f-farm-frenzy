using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneManagerScript : MonoBehaviour
{
    private string StartSceneName = "";
    private string GameSceneName = "integrated_map";
    private string AwardSceneName = "";
    private string TestSceneName = "testing_map_scenes";

    public int timeToEnd = 300;
    public Color loadToColor = Color.black;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == GameSceneName)
        {
            Invoke("LoadGameScene", timeToEnd);
        }
    }

    void Update()
    {
        // Inputs for testing purposes, just call methods when we have actual setup
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            RestartScene();
        }
        else if (Input.GetKeyDown(KeyCode.Backslash))
        {
            LoadGameScene();
        }
    }

    public void LoadGameScene()
    {
        Initiate.Fade(GameSceneName, loadToColor, 1.0f);

        //SceneManager.LoadScene(GameSceneName);
    }

    public void LoadStartScene()
    {
        Initiate.Fade(StartSceneName, loadToColor, 1.0f);

        //SceneManager.LoadScene(StartSceneName);
    }

    public void LoadAwardScene()
    {
        Initiate.Fade(AwardSceneName, loadToColor, 1.0f);

        //SceneManager.LoadScene(AwardSceneName);
    }

    void RestartScene()
    {
        Initiate.Fade(SceneManager.GetActiveScene().name, loadToColor, 1.0f);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}