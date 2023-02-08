using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneManagerScript : MonoBehaviour
{
    private string StartSceneName = "hubworld";
    private string GameSceneName = "integrated_map";
    private string AwardSceneName = "";
    private string TestSceneName = "hatTesting";

    public int timeToEnd = 300;
    public Color loadToColor = Color.black;

    public float sceneTransitionTime = 3f;


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
        Initiate.Fade(GameSceneName, loadToColor, sceneTransitionTime);

        //SceneManager.LoadScene(GameSceneName);
    }

    public void LoadStartScene()
    {
        Initiate.Fade(StartSceneName, loadToColor, sceneTransitionTime);

        //SceneManager.LoadScene(StartSceneName);
    }

    public void LoadAwardScene()
    {
        Initiate.Fade(AwardSceneName, loadToColor, sceneTransitionTime);

        //SceneManager.LoadScene(AwardSceneName);
    }

    void RestartScene()
    {
        Initiate.Fade(SceneManager.GetActiveScene().name, loadToColor, sceneTransitionTime);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}