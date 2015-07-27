using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {

    GameObject[] alreadyHit;

	// Use this for initialization
	void Start () {
        Invoke("SelfDestruct", 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
        //gameObject.transform.Translate(new Vector2(0.5f, 0.0f)*Time.deltaTime);
	}

    public void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
}
