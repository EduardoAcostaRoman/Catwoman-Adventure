using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder : MonoBehaviour
{
    private GameObject P1;
    public float velocity = 5;
    public float destructTime = 2;
    private Rigidbody2D body;

    void Start()
    {
        P1 = GameObject.Find("P1 position");
        body = GetComponent<Rigidbody2D>();

        if (P1.transform.position.x >= transform.position.x)
        {
            body.velocity = new Vector2(velocity, 0);
        }
        else
        {
            body.velocity = new Vector2(-velocity, 0);
        }
    }

    void Update()
    {
        Destroy(this.gameObject, destructTime);
        
        
    }
}
