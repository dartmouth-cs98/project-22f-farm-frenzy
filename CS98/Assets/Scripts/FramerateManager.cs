using UnityEngine;

public class FramerateManager : MonoBehaviour
{
    public int targetFrameRate = 60;
    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = targetFrameRate;
    }
}