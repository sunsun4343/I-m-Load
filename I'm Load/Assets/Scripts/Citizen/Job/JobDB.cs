using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Job", menuName = "GameDB/Job")]
public class JobDB : ScriptableObject
{
    public enum Type
    {
        Worker,

    }
    public Type type;
    public string labelKey;
}
