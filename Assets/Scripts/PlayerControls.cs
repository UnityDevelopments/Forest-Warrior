using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 5f;
    public float speedRotation = 3f;
    public float acceleration;
    public bool isGround;
    public float jumpSpeed;
    public GameObject rotateObject;
    private Rigidbody rb;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Движение вперед
        if (Input.GetKey(KeyCode.W) && isGround)
        {
            anim.SetBool("IsRunning", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                gameObject.transform.position += gameObject.transform.forward * speed * acceleration * Time.deltaTime;
            }
            else
            {
                gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
            }
        }else
        {
            anim.SetBool("IsRunning", false);
        }


        //Движение назад
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.position -= gameObject.transform.forward * speed * Time.deltaTime;
        }
        //Движение вправо
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Rotate(Vector3.up * speedRotation);

        }
        //Движение влево
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Rotate(-Vector3.up * speedRotation);
        }

        //Движение вверх
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(transform.up * jumpSpeed);
        }

        //Атака
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("IsAttack", true);
        }
        else
        {
            anim.SetBool("IsAttack", false);
        }
    }


    private void OnCollisionStay()
    {
        isGround = true;
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
}
