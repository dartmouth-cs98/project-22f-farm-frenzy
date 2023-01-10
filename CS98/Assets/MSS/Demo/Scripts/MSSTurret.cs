using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSSTurret : MonoBehaviour
{
    private Vector2 turn;
    public float sensitivity = 2f;
    private Vector3 deltaMove;

    public SpawnerManager spawnerManager;

    void Start()

    {

    }

    void Update()

    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        if(Input.GetButton("Fire1")){
            spawnerManager.StartSpawner(0);
        }

        if(Input.GetButtonUp("Fire1")){
            spawnerManager.StopSpawner(0);
        }
    }

}
