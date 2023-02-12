using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    GameObject Soldier_demo;
    GameObject playecontrol;
    GameObject[] enemies;
    public float horizontalSpeed = 2.0f;
    public float vertical;
    public float horisontal;
    public float moveAmount;
    public float speed = 5f;
    public float speedRotation = 3f;
    public bool isGround;
    //камера
    public Transform targetRotate;
    public Animator anim;
    CharacterController characterController;
    /*public AnimationClip animation;*/ //Анимация
    private Rigidbody _rb;
    //переменная удара
    const float ComboTime = 0.2f;
    float m_LastClickTime;
    public float Gravity = 9.8f;
    public float GravityScale = 1;
    public float velocity = 5;
    public float jumpHeight = 4;
    public float jumpSpeed;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        // Обьявление героя
        Soldier_demo = gameObject;
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //задержка 3 сек
        anim.SetTrigger("Idle");
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        anim.SetFloat("Walk", z);
        anim.SetFloat("Rotate", x);

        //Движение вперед
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Soldier_demo.transform.position += Soldier_demo.transform.forward * speed * Time.deltaTime;
        }
        //Движение назад
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Soldier_demo.transform.position -= Soldier_demo.transform.forward * speed * Time.deltaTime;
        }
        //Движение вправо
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Soldier_demo.transform.Rotate(Vector3.up * speedRotation);

        }
        //Движение влево
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Soldier_demo.transform.Rotate(Vector3.down * speedRotation);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - m_LastClickTime >= ComboTime)
            {
                anim.SetBool("Attacke", true);
            }
            else
            {
                anim.SetTrigger("Idle");
            }
            m_LastClickTime = Time.time;
        }
        //Движение вверх
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpSpeed);
            //velocity = Mathf.Sqrt(jumpHeight * -2f * (Gravity * GravityScale));
        }
        //velocity += Gravity * GravityScale * Time.deltaTime;
        //MovePlayer();
    }

    //void MovePlayer()
    //{
    //    characterController.Move(new Vector3(0, velocity, 0) * Time.deltaTime);
    //}
    //button
    //void Button()
    //{
    //    GameObject newButton = new GameObject("New button", typeof(Image), typeof(Button));
    //}

    ////физика обьекта
    //void FixedUpdate()
    //{
    //   // MovementLogic();
    //    JumpLogic();
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        if (timer <= 0.01f)
    //        {
    //            if(Attacke != 1)
    //            {
    //                Attacke += 1;
    //            }
    //            else
    //            {
    //                Attacke = 0;
    //            }
    //            Vector3 dir = targetRotate.position - transform.position;
    //            dir.y = 0;
    //            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 40 * Time.deltaTime);
    //        }
    //    }
    //}

    //private void MovementLogic()
    //{
    //    vertical = Input.GetAxis("Vertical");
    //    horisontal = Input.GetAxis("Horizontal");
    //    moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horisontal));

    //    anim.SetFloat("Red_Gnome_walk",vertical,0.15f, Time.deltaTime);
    //}



    private void OnCollisionStay()
    {
        isGround = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }

    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGround = value;
        }
    }
    ////использование анимации
    //private void Anim()
    //{

    //}
}
