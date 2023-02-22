using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;

    private static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Assign the audio sources array
        audioSources = GetComponentsInChildren<AudioSource>();
        SceneManager.activeSceneChanged += OnSceneChanged;
        GetComponent<AudioManager>().PlayAudio("hubworld_theme");


    }

    void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        if (SceneManager.GetActiveScene().name == "hubworld")
        {
            GetComponent<AudioManager>().PlayAudio("hubworld_theme");
        }
        else if (SceneManager.GetActiveScene().name == "integrated_map")
        {
            GetComponent<AudioManager>().PlayAudio("gamemap_theme");
        }
    }

    public void PlayAudio(string audioName)
    {
        // Find the audio source with the matching name
        AudioSource sourceToPlay = null;
        foreach (AudioSource source in audioSources)
        {
            if (source.clip.name == audioName)
            {
                sourceToPlay = source;
                break;
            }
        }

        // Play the audio source, if found
        // Stop any currently playing audio and play the new audio source, if found
        if (sourceToPlay != null && sourceToPlay.loop)
        {
            foreach (AudioSource source in audioSources)
            {
                if (source.isPlaying && source.loop)
                {
                    source.Stop();
                }
            }
            sourceToPlay.Play();
        }
        else if (sourceToPlay != null)
        {
            sourceToPlay.Play();
        }
        else
        {
            Debug.LogWarning("Audio source not found: " + audioName);
        }
    }
}
