using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float speed;
    Vector3 movement;
    public Rigidbody rb;
    public float maxX = 7, minX = -7;
    public float maxZ = 7, minZ = -7;

    bool isHit;
    int dir;
    Vector3 initPos;
    
    // Start is called before the first frame update
    void Start()
    {
        movement = Vector3.zero;
        dir = -1;
        StartCoroutine("ChangeDir");
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Move();
        rb.MovePosition(transform.position + movement);
        if(movement != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(movement, Vector3.up);
            rb.MoveRotation( Quaternion.RotateTowards(transform.rotation, toRotate, speed*2f));
        }
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Monster")
        {
            print("hit");
            isHit = true;

        } 
    }
    void OnCollisionExit(Collision other){
        if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Monster")
        {
            print("stop hit");
            isHit = false;
        } 
    }

    void Move()
    {
        if(isHit || !inRange())
        {
            movement = -1 * transform.right * speed * Time.deltaTime; 
        }
        else
            movement = transform.forward * speed * Time.deltaTime;
    }
    bool inRange()
    {
        float x, z;
        x = transform.position.x;
        z = transform.position.z;
        return x <= maxX && x >= minX && z <= maxZ && z >= minZ;
    }
    IEnumerator ChangeDir()
    {
        while (true)
        {
            dir *= -1;
            print(dir);
            yield return new WaitForSeconds(2);
        }
    }
}
