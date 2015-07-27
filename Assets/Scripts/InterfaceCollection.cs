using UnityEngine;
using System.Collections;
using System.IO;

public interface ISaveable
{
    void Save(BinaryWriter writer);
    void Load(BinaryReader reader);
}

public class InterfaceCollection : MonoBehaviour {

}
