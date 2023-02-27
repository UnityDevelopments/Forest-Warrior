using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private GameObject target;
    public float speed, visibleDistance, damage, timeAttack, hp;
    private GameObject objectMesh;
    private TextMesh textMesh;
    private float lastTime, maxHp;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
        objectMesh = transform.Find("HpText").gameObject;
        textMesh = objectMesh.GetComponent<TextMesh>();
        maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        objectMesh.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
        textMesh.text = $"{hp}/{maxHp}";

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Quaternion.LookRotation(target.transform.position - transform.position).eulerAngles.y, transform.rotation.eulerAngles.z);
        if (Vector3.Distance(transform.position, target.transform.position) <= visibleDistance && Vector3.Distance(transform.position, target.transform.position) > 1.5)
        {
            anim.SetBool("IsAttack", false);
            anim.SetBool("IsRunning", true);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if(Vector3.Distance(transform.position, target.transform.position) <= 1.5)
        {
            anim.SetBool("IsRunning", false);
            if(Time.time - lastTime >= timeAttack)
            {
                anim.SetBool("IsAttack", true);
                lastTime = Time.time;
                target.GetComponent<PlayerControls>().hp -= damage;
            }
            else
            {
                anim.SetBool("IsAttack", false);
            }
        }

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
