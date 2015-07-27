using UnityEngine;
using System.Collections;
using System.IO;

public abstract class Unit : MonoBehaviour
{

    public string unitName;
    public float healthPoints;
    public float maxHealth;
    public const float moveSpeed = 5.0f;

    public abstract void Move(string dir);

    public abstract void Action();

}
