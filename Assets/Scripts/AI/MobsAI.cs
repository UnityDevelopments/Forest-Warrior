using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsAI : MonoBehaviour
{
    public GameObject target;
    public float speed, visibleDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.black);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Quaternion.LookRotation(target.transform.position - transform.position).eulerAngles.y, transform.rotation.eulerAngles.z);
        if (Vector3.Distance(transform.position, target.transform.position) <= visibleDistance && Vector3.Distance(transform.position, target.transform.position) > 1.5) transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
