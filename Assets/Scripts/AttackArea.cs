using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {

	void Start () 
    {
        Invoke("SelfDestruct", 0.3f);
	}
	
    public void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
}
