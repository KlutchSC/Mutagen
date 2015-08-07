using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController control;
    
    public float timerTime;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }
	
	void Update () 
    {
        Debug.Log("Game time is " + GameTimer());
	}

    float GameTimer()
    {
        float curTime = timerTime - Time.time;
        return curTime;
    }

    public void AddGameTime(float time)
    {
        timerTime = timerTime + time;
    }
}
