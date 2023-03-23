using UnityEngine;
using System.Collections;
using System;

public class WaterDeformator2D : MonoBehaviour {


    public GameObject point;
    Vector3[] vertices;
    Vector3[] originalVertices;
    Mesh mesh;

    public void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        originalVertices = mesh.vertices;
    }

    public Vector3 NearestVertexTo(Vector3 point) {
        point = transform.InverseTransformPoint(point);

        float minDistanceSqr = 1;
        Vector3 nearestVertex = Vector3.zero;

        int counter = 0;
        // scan all vertices to find nearest
        foreach (Vector3 vertex in vertices)
        {
            Vector3 diff = point - vertex;
            float distSqr = diff.sqrMagnitude;

            if (distSqr < minDistanceSqr)
            {
                minDistanceSqr = distSqr;
 
                vertices[counter].y -= 1f;
                mesh.vertices = vertices;
                StartCoroutine(AddSplash(counter));
                break;
            }
            counter++;
        }

        
        
        

        // convert nearest vertex back to world space
        return nearestVertex;

    }

    bool maxed;
    IEnumerator AddSplash(int vertexIndex)
    {

        
        //Reset vector position
        
            float force = 1.0f;
            float f = force;

            while (Math.Round(vertices[vertexIndex].y,2) != Math.Round((originalVertices[vertexIndex].y),2))
            {
                for (int i = 0; i < 5; i++)
                {
                if (vertices[vertexIndex+i].y >= (originalVertices[vertexIndex+i].y + (f)) && !maxed)
                {
                    f *= -1;
                    maxed = true;
                }
                else if (vertices[vertexIndex+i].y <= originalVertices[vertexIndex+i].y + (f) && maxed)
                {
                    f = Mathf.Abs(force);
                    maxed = false;
                }

                if (force < 0.03) f = 0;
                vertices[vertexIndex+i].y = Mathf.SmoothDamp(vertices[vertexIndex+i].y, originalVertices[vertexIndex+i].y + (f * 2), ref velocity, 0.2f);
                mesh.vertices = vertices;
                force -= 0.8f * Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
        }
       
       
    }

    private float velocity = 0.0f; 
    void OnTriggerEnter2D(Collider2D collider)
    {

            NearestVertexTo(collider.transform.position);
        
        
       // Instantiate(point, v, transform.rotation);
    }
}
