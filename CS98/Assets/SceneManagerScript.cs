using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneManagerScript : MonoBehaviour
{
    public string StartSceneName = "hubworld";
    public string GameSceneName = "integrated_map";
    public string AwardSceneName = "";
    public string TestSceneName = "hatTesting";

    public int timeToEnd = 2;
    public Color loadToColor = Color.black;

    public float sceneTransitionTime = 3f;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == GameSceneName)
        {
            Invoke("LoadStartScene", timeToEnd);
        }
    }

    void Update()
    {
        // Inputs for testing purposes, just call methods when we have actual setup
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            LoadStartScene();
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

    public void PlayerReadyCheck()
    {
        ColorChanging colorChangeScript = FindObjectsOfType<ColorChanging>()[0];
        print("Checking validity");
        if( colorChangeScript.arePlayersReady())
        {
            print("Loading Scene");
            LoadGameScene();
        }
        else
        {
            // Send message to players
        }
    }
}