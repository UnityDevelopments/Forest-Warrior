using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public Joystick joystick;
    private float horizontal, vertical;
    public Button attackButton, shiftButton;

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
        /*horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");*/
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        //Обновление жизней
        HpSlider.value = Hp / 100;

        //Перемещение и Вращение
        if ((vertical != 0 || horizontal != 0) && IsGround)
        {
            Anim.SetBool("IsRunning", true);
            transform.position += moveVector;
            transform.rotation = Quaternion.LookRotation(moveVector, Vector3.up);
            //transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.up, moveVector, SpeedRotation, 0.0f));
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

    public void Attack(PointerEventData eventData)
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Anim.SetBool("IsAttack", true);
        }
        else
        {
            Anim.SetBool("IsAttack", false);
        }*/
    }

    public void Shift(Button button)
    {
        //Ускорение
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
    }
}
