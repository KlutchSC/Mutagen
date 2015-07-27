using UnityEngine;
using System.Collections;

public class EnemyAttackArea : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Invoke("SelfDestruct", 0.3f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
}
