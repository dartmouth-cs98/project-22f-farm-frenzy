    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieDuck : MonoBehaviour
{
    public bool die = false;
    [SerializeField] private GameObject dieFX;

    void Start()
    {
        //StartCoroutine("MyCoroutine");
    }
    IEnumerator MyCoroutine()
    {
        Debug.Log("here");
        yield return new WaitForSeconds(7f);
        dieFX.transform.localScale = new Vector3(0f, -1f, 0f);
        dieFX.GetComponent<ParticleSystem>().Stop();
        dieFX.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        Debug.Log("herehereherehere");

        Destroy(gameObject);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //if (die) {
        //    Debug.Log("in dieduck: die");
        //    Destroy(gameObject);
        //    //StartCoroutine("testFunction2");
        //}
    }

    public void playFx()
    {
        dieFX.transform.localScale = new Vector3(0f, -1f, 0f);
        dieFX.GetComponent<ParticleSystem>().Stop();
        dieFX.GetComponent<ParticleSystem>().Play();
        StartCoroutine("testFunction2");
    }


    private IEnumerator testFunction2()
    {
        
        dieFX.transform.localScale = new Vector3(0f, -1f, 0f);
        dieFX.GetComponent<ParticleSystem>().Stop();
        dieFX.GetComponent<ParticleSystem>().Play();
        Debug.Log("here");
        yield return new WaitForSeconds(1f);
        Debug.Log("herehereherehere");

        Destroy(gameObject);
    }
}
