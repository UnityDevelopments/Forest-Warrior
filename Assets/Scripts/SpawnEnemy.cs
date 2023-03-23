using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public int maxEnenmy;
    public int delaySpawn;
    public int countEnemy;
    private float timeSpawn;
    private BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        while(countEnemy < maxEnenmy && timeSpawn + delaySpawn < Time.time)
        {
            GameObject obj =  Instantiate(enemy, new Vector3(Random.Range(transform.position.x - boxCollider.size.x, transform.position.x + boxCollider.size.x), 1, Random.Range(transform.position.z - boxCollider.size.z, transform.position.z + boxCollider.size.z)), Quaternion.identity);
            obj.GetComponent<Enemy>().spawnEnemy = gameObject;
            timeSpawn = Time.time;
            countEnemy++;
        }
    }
}
