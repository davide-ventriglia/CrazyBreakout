using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPool", menuName = "Elements/ObjectPool")]
public class ObjectPool_SO : ScriptableObject
{
    public GameObject objectPrefab;
    public string label;
    public int size;
}
