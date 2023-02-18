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
        //Обновление жизней
        HpSlider.value = Hp / 100;

        //Движение вперед
        if (Input.GetKey(KeyCode.W) && IsGround)
        {
            Anim.SetBool("IsRunning", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Anim.SetFloat("SpeedRun", 2);
                gameObject.transform.position += gameObject.transform.forward * Speed * Acceleration * Time.deltaTime;
            }
            else
            {
                Anim.SetFloat("SpeedRun", 1);
                gameObject.transform.position += gameObject.transform.forward * Speed * Time.deltaTime;
            }
        }else
        {
            Anim.SetBool("IsRunning", false);
        }


        //Движение назад
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.position -= gameObject.transform.forward * Speed * Time.deltaTime;
        }
        //Движение вправо
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Rotate(Vector3.up * SpeedRotation);

        }
        //Движение влево
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Rotate(-Vector3.up * SpeedRotation);
        }

        //Движение вверх
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
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            IsGround = value;
        }
    }
}
