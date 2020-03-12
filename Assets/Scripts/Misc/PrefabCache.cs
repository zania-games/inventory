using System.Collections.Generic;
using UnityEngine;

public class PrefabCache: MonoBehaviour
{
    private IDictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

    void Awake()
    {
        foreach (GameObject obj in GetComponents<GameObject>())
            cache.Add(obj.name, obj);
    }

    public GameObject this[string name]
    {
        get => cache[name];
    }
}
