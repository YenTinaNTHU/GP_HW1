using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAttack : MonoBehaviour
{
    GameObject trap;
    // Start is called before the first frame update
    void Start()
    {
        trap = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Toon Chick")
        {
            print("Collision!");
        }
    }
}
