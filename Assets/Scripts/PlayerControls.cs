using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    public float speed, speedRotation, hp, acceleration, damage, timeAttack;
    private bool isGround;
    public GameObject rotateObject;
    public Slider hpSlider;
    private Animator anim;
    private Vector3 moveVector;
    public Joystick joystick;
    private float horizontal, vertical;
    public MyButton attackButton, shiftButton;
    private GameObject enemy;
    private float lastTime;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hpSlider.value = hp / 100;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        //Обновление жизней
        hpSlider.value = hp / 100;

        //Перемещение и Вращение
        if ((vertical != 0 || horizontal != 0) && isGround)
        {
            anim.SetBool("IsRunning", true);
            //Ускорение
            if (shiftButton.isPressed)
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
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        //Атака
        if (attackButton.isPressed && Time.time - lastTime >= timeAttack)
        {
            if (enemy != null) enemy.GetComponent<Enemy>().hp -= damage;
            lastTime = Time.time;
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
        if (collision.gameObject.tag == "Enemy")
        {
            enemy = null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemy = collision.gameObject;
        }
    }
}
