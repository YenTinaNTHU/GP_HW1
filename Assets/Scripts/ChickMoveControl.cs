using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickMoveControl : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public float BackwardForce;
    public Rigidbody rb;
    public GameObject poop;

    public Transform cam;

    public AudioClip attackedSound;
    private AudioSource mAudioSource;

    private Animator mAnimator;
    private bool isGrounded;
    private bool toJump;
    private bool isAttacked;

    Vector3 movement;
    Vector3 attackedDirection;

    GameObject prefab;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mAudioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        isAttacked = false;

    }

    void Update()
    {
        Move();
        Poop();
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
            rb.MoveRotation( Quaternion.RotateTowards(transform.rotation, toRotate, Speed*0.5f));
        }
        
    }

    void OnCollisionStay(Collision other){
        if(other.gameObject.name == "Ground" || other.gameObject.name == "Terrain")
            isGrounded = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.gameObject.name == "Traps")
        {
            print("Collision!");
            transform.position -= transform.forward * Time.deltaTime * BackwardForce;
            mAudioSource.PlayOneShot(attackedSound);
        }
    }

    void Move()
    {
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        movement = new Vector3(moveX, 0f, moveZ) * Speed * Time.deltaTime;
        movement = Quaternion.AngleAxis(cam.rotation.eulerAngles.y, Vector3.up) * movement;
        
        
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

    void Poop()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            prefab = Instantiate(poop, transform.position, Quaternion.identity);
        }
    }

}
