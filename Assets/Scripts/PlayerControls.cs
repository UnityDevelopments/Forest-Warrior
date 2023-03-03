using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    public float speed, speedRotation, hp, acceleration, damage, timeAttack, endurance;
    private bool isGround;
    public GameObject rotateObject;
    public Slider hpSlider, enduranceSlider;
    private Animator anim;
    private Vector3 moveVector;
    public Joystick joystick;
    private float horizontal, vertical;
    public MyButton attackButton, shiftButton;
    private GameObject enemy;
    private float lastTimeAttack, lastTimeEndurance, recoveryTimeEndurance;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        //Обновление жизней и выносливости
        hpSlider.value = hp / 100;
        enduranceSlider.value = endurance / 100;

        //Перемещение и Вращение
        if ((vertical != 0 || horizontal != 0) && isGround)
        {
            anim.SetBool("IsRunning", true);
            //Ускорение
            if (shiftButton.isPressed && endurance > 0)
            {
                anim.SetFloat("SpeedRun", 2);
                moveVector = new Vector3(horizontal * speed * acceleration * Time.deltaTime, 0, vertical * speed * acceleration * Time.deltaTime);
            }
            else
            {
                anim.SetFloat("SpeedRun", 1);
                moveVector = new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
            }
            transform.position += moveVector;
            transform.rotation = Quaternion.LookRotation(moveVector, Vector3.up);

            //Выносливость
            if (shiftButton.isPressed && Time.time - lastTimeEndurance >= 1 && endurance > 0)
            {
                endurance -= 10;
                lastTimeEndurance = Time.time;
            }
            else if (!shiftButton.isPressed && Time.time - recoveryTimeEndurance >= 2 && endurance < 100)
            {
                endurance += 10;
                recoveryTimeEndurance = Time.time;
            }
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        //Атака
        if (attackButton.isPressed && Time.time - lastTimeAttack >= timeAttack)
        {
            if (enemy != null) enemy.GetComponent<Enemy>().hp -= damage;
            lastTimeAttack = Time.time;
            anim.SetBool("IsAttack", true);
        }
        else
        {
            anim.SetBool("IsAttack", false);
        }
    }

    void OnCollisionStay()
    {
        isGround = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGround = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemy = collision.gameObject;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = null;
        }
    }
}
