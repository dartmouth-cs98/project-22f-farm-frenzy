using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanging : MonoBehaviour
{

    public ParticleSystem splash;
    public Material body;
    public Color changeColor = new Color(0.631f, 0.439f, 0.521f);
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("11111");
            splash.Play();
            GameObject duck = other.transform.parent.parent.Find("duck").gameObject;
            duck.transform.Find("body").GetComponent<Renderer>().material.color = changeColor;
            duck.transform.Find("leftHand").gameObject.GetComponent<Renderer>().material.color = changeColor;
            duck.transform.Find("rightHand").gameObject.GetComponent<Renderer>().material.color = changeColor;

            body.color = changeColor;
        }
    }
}
