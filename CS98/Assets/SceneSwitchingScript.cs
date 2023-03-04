using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void sceneSwitch()
    {
        FindObjectOfType<SceneManagerScript>().PlayerReadyCheck();
    }
}
