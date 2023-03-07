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

    // Update is called once per frame
    void LateUpdate()
    {
        if (die)
        {   
            Debug.Log("in dieduck: die");
            Destroy(gameObject);
            playFx();
            //StartCoroutine("testFunction2");
        }
    }

    public void playFx()
    {
        Debug.Log("here");
        Instantiate(dieFX, transform.position, Quaternion.identity);
        dieFX.transform.localScale = new Vector3(0f, -1f, 0f);
        dieFX.GetComponent<ParticleSystem>().Stop();
        dieFX.GetComponent<ParticleSystem>().Play();
        StartCoroutine("testFunction2");
    }


    private IEnumerator testFunction2()
    {
        //this.GetComponent<Shopper>().lifelimit = true;
        Debug.Log("hereeeeeeeeeetestFunction2");
        dieFX.transform.localScale = new Vector3(0f, -1f, 0f);
        dieFX.GetComponent<ParticleSystem>().Stop();
        dieFX.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1.5f);
        Debug.Log("herehereherehere testFunction2");

        Destroy(gameObject);
    }
}
