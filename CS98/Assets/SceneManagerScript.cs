using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneManagerScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}