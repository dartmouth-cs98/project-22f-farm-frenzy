using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanging2 : MonoBehaviour
{

    public ParticleSystem splash;
    public Material body;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerParent")
        {
            splash.Play();
            Color changeColor = new Color(0.737f, 0.611f, 0.27f);
            body.color = changeColor;
        }
    }
}
