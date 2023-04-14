using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    private GameObject target;
    public float speed, visibleDistance, damage, timeAttack, hp;
    public int rewardOfKill;
    public GameObject spawnEnemy;
    private GameObject objectMesh;
    private TextMesh textMesh;
    private float lastTime, maxHp;
    private PlayerControls player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
        objectMesh = transform.Find("HpText").gameObject;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        textMesh = objectMesh.GetComponent<TextMesh>();
        maxHp = hp;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        objectMesh.transform.rotation = Quaternion.Euler(objectMesh.transform.rotation.eulerAngles.x, gameObject.transform.rotation.z * -1.0f, objectMesh.transform.rotation.eulerAngles.z);
        textMesh.text = $"{hp}/{maxHp}";

        if (Vector3.Distance(transform.position, target.transform.position) <= visibleDistance && Vector3.Distance(transform.position, target.transform.position) > 1.5)
        {
            anim.SetBool("IsAttack", false);
            anim.SetBool("IsRunning", true);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Quaternion.LookRotation(target.transform.position - transform.position).eulerAngles.y, transform.rotation.eulerAngles.z);
            rb.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }else if(Vector3.Distance(transform.position, target.transform.position) > visibleDistance)
        {
            if(Vector3.Distance(transform.position, spawnEnemy.transform.position) > Random.Range(1.5f, 2.5f))
            {
                anim.SetBool("IsRunning", true);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Quaternion.LookRotation(spawnEnemy.transform.position - transform.position).eulerAngles.y, transform.rotation.eulerAngles.z);
                rb.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("IsRunning", false);
            }
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
            player.money += rewardOfKill;
            spawnEnemy.GetComponent<SpawnEnemy>().countEnemy--;
            Destroy(gameObject);
        }
    }
}
