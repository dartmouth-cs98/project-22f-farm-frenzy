using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanging : MonoBehaviour
{

    public ParticleSystem splash;
    public Material body;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerParent")
        {
            print("11111");
            splash.Play();
            Color changeColor = new Color(0.631f, 0.439f, 0.521f);
            body.color = changeColor;
        }
    }
}
