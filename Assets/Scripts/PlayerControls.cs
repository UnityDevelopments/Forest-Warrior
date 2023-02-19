using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    public float Speed = 5f;
    public float SpeedRotation = 3f;
    public float Hp;
    public float Acceleration;
    public bool IsGround;
    public float JumpSpeed;
    public GameObject RotateObject;
    public Slider HpSlider;
    private Rigidbody Rb;
    private Animator Anim;
    private Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        HpSlider.value = Hp / 100;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Обновление жизней
        HpSlider.value = Hp / 100;

        //Перемещение и Вращение
        if ((vertical != 0 || horizontal != 0) && IsGround)
        {
            Anim.SetBool("IsRunning", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Anim.SetFloat("SpeedRun", 2);
                moveVector = new Vector3(horizontal * Speed * Acceleration * Time.deltaTime, 0, vertical * Speed * Acceleration * Time.deltaTime);
            }
            else
            {
                Anim.SetFloat("SpeedRun", 1);
                moveVector = new Vector3(horizontal * Speed * Time.deltaTime, 0, vertical * Speed * Time.deltaTime);
            }
            transform.position += moveVector;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.up, moveVector, SpeedRotation, 0.0f));
        }
        else
        {
            Anim.SetBool("IsRunning", false);
        }

        //Прыжок
        if (Input.GetKeyDown(KeyCode.Space) && IsGround)
        {
            Rb.AddForce(transform.up * JumpSpeed);
        }

        //Атака
        if (Input.GetMouseButtonDown(0))
        {
            Anim.SetBool("IsAttack", true);
        }
        else
        {
            Anim.SetBool("IsAttack", false);
        }
    }

    private void OnCollisionStay()
    {
        IsGround = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            IsGround = true;
        }
    }
}
