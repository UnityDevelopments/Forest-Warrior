using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsAI : MonoBehaviour
{
    public GameObject target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.black);
        if(Vector3.Distance(transform.position, target.transform.position) > 1.5) transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.LookAt(target.transform, Vector3.up);
    }
}
