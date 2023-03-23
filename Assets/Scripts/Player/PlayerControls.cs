using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    public float speed, speedRotation, hp, acceleration, damage, timeAttack, endurance;
    public int money;
    private bool isGround;
    public GameObject rotateObject;
    private Slider hpSlider, enduranceSlider;
    private Animator anim;
    private Vector3 moveVector;
    private Joystick joystick;
    private float horizontal, vertical;
    private MyButton attackButton, shiftButton;
    private GameObject enemy;
    private float lastTimeAttack, lastTimeEndurance, recoveryTimeEndurance;
    private Text textMoney;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hpSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        enduranceSlider = GameObject.Find("EnduranceBar").GetComponent<Slider>();
        joystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        attackButton = GameObject.Find("AttackButton").GetComponent<MyButton>();
        shiftButton = GameObject.Find("ShiftButton").GetComponent<MyButton>();
        textMoney = GameObject.Find("TextMoney").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        //Обновление жизней, выносливости, денег
        hpSlider.value = hp / 100;
        enduranceSlider.value = endurance / 100;
        textMoney.text = money.ToString();

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
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

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
