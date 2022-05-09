using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public GameObject cam;
    float var = 0;

    private void FixedUpdate()
    {
        //transform.position = new Vector3(cam.transform.position.x, transform.position.y, transform.position.z);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(var, 0);
        
    }
}
