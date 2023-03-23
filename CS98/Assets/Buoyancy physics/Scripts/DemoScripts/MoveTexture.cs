using UnityEngine;
using System.Collections;

public class MoveTexture : MonoBehaviour {


    public float speed = 0.1f;
    private float offset;

	void Update () {
        //Move texture offset
        offset +=  speed * Time.deltaTime;

        //If this renderer has a main texture, move it
        if (GetComponent<Renderer>().material.HasProperty("_MainTex"))
        {
            GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }

        //If this renderer has a bumpmap, move it in the opposite direction
        if (GetComponent<Renderer>().material.HasProperty("_BumpMap"))
        {
            GetComponent<Renderer>().material.SetTextureOffset("_BumpMap", new Vector2(-offset, -offset * 2));
        }
        

	}
}
