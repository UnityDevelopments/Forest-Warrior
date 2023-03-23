using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<GameObject> prefabObjects;
    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;
    public float Y;
    public int count;
    private List<GameObject> objects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Spawn")]
    public void RandomSpawn()
    {
        for (int i = 0; i < count; i++)
        {
            objects.Add(Instantiate(prefabObjects[Random.Range(0, prefabObjects.Count)], new Vector3(Random.Range(minX, maxX), Y, Random.Range(minZ, maxZ)), Quaternion.identity));
        }
    }

    [ContextMenu("DeleteObjects")]
    public void DeleteObjects()
    {
        foreach (GameObject item in objects)
        {
            DestroyImmediate(item);
        }
    }
}
