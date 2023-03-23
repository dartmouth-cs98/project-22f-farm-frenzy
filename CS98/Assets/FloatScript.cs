using UnityEngine;

public class FloatScript : MonoBehaviour
{
    public float amplitude = 1.0f; // range of movement
    public float speed = 1.0f; // speed of movement
    public float rotationSpeed = 5.0f; // speed of rotation
    private Vector3 startPos; // starting position of the gameobject

    void Start()
    {
        startPos = transform.position; // save the starting position
        speed *= Random.Range(.5f, 2.0f);
    }

    void Update()
    {
        // calculate the y offset based on time
        float yOffset = Mathf.Sin(Time.time * speed) * amplitude;

        // set the gameobject's new position
        transform.position = startPos + new Vector3(0, yOffset, 0);

        // rotate the gameobject
        transform.Rotate(Vector3.up, yOffset * Time.deltaTime * rotationSpeed);
    }
}
