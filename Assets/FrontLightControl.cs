using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLightControl : MonoBehaviour
{
    
    Light light;
    public float lightBoost;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        print(light.intensity);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Equals))
        {
            light.intensity += lightBoost;
            print(light.intensity);
        }
        if(Input.GetKey(KeyCode.Minus))
        {
            light.intensity -= lightBoost;
            print(light.intensity);
        }
    }
}
