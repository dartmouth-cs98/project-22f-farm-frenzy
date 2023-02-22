using UnityEngine;
using System.Collections;

public class BasicRotation : MonoBehaviour {

    public float speed = 5.0f;

	void Update () {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * speed, 0);
        }
        
	}
}
