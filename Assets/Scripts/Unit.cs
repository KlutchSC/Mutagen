using UnityEngine;
using System.Collections;
using System.IO;

public abstract class Unit : MonoBehaviour, ISaveable
{

    public string unitName;
    public float healthPoints;
    public float maxHealth;
    public const float moveSpeed = 5.0f;

    public virtual void Save(System.IO.BinaryWriter writer)
    {

    }

    public virtual void Load(System.IO.BinaryReader reader)
    {

    }
    public abstract void Move(string dir);

    public abstract void Action();

}
