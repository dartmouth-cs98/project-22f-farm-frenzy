using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object.DontDestroyOnLoad example.


public class DontDestroyScript : MonoBehaviour
{
    void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
    }
}