using UnityEngine;
using System.Collections;

public class CloudScroller : MonoBehaviour {

	void Update () 
    {
        transform.Translate((-1.0f*Vector3.right) * Time.deltaTime);
        if (transform.position.x <= -30.0f)
        {
            transform.position = new Vector3 (30.0f, 3.0f, 0.0f);
        }
	}
}
