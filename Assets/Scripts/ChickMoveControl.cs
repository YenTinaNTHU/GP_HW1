using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickMoveControl : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public Rigidbody rb;

    private Animator mAnimator;
    private bool isGrounded;
    private bool toJump;
    Vector3 movement;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        if(toJump)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
            toJump = false;
            mAnimator.ResetTrigger("Jump");
        }
        rb.MovePosition(transform.position + movement);
        if(movement != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(movement, Vector3.up);
            rb.MoveRotation( Quaternion.RotateTowards(transform.rotation, toRotate, Speed));
        }
        
    }

    void OnCollisionStay(){
        isGrounded = true;
    }

    void Move()
    {
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        movement = new Vector3(moveX, 0f, moveZ) * Speed * Time.deltaTime;
        
        
        if(movement != Vector3.zero)
            mAnimator.SetTrigger("Run");
        else
            mAnimator.ResetTrigger("Run");

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            mAnimator.SetTrigger("Jump");
            isGrounded = false;
            toJump = true;
        }
    }

}
