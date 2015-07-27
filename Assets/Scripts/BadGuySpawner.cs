using UnityEngine;
using System.Collections;

public class BadGuySpawner : MonoBehaviour {

    public GameObject badGuyPre;
    public GameObject mutantPre;

	// Update is called once per frame
	void Awake () {
        InvokeRepeating("SpawnBadGuy", 1.0f, 5.0f);
        InvokeRepeating("SpawnMutant", 1.0f, 12.0f);
	}

    void SpawnBadGuy()
    {
        float xPos = Random.Range(-15.0f, 15.0f);

        Instantiate(badGuyPre, (this.gameObject.transform.position + (new Vector3(xPos, 0.0f, 0.0f))), this.gameObject.transform.rotation);
    }

    void SpawnMutant()
    {
        float xPos = Random.Range(-10.0f, 10.0f);

        Instantiate(mutantPre, (this.gameObject.transform.position + (new Vector3(xPos, 0.0f, 0.0f))), this.gameObject.transform.rotation);
    }
}
