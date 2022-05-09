using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericProjectile : MonoBehaviour
{
    private GameObject P1;
    private Rigidbody2D body;

    public float velocityX = 5;
    public float velocityY = 0;
    public float destructTime = 2;

    public string objReferenceName;
    GameObject objReference;

    public bool startFlipX;

    public bool absoluteFlipX = false;

    public bool absoluteFlipXForObject = false;

    void Start()
    {
        P1 = GameObject.Find("P1 position");
        body = GetComponent<Rigidbody2D>();

        if (GameObject.Find(objReferenceName))
        {
            objReference = GameObject.Find(objReferenceName);

            if (objReference.GetComponent<SpriteRenderer>().flipX == startFlipX)
            {
                body.velocity = new Vector2(velocityX, velocityY);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                body.velocity = new Vector2(-velocityX, velocityY);
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

            
    }

    void Update()
    {
        Destroy(gameObject, destructTime);

        if (absoluteFlipX)
        {
            GetComponent<SpriteRenderer>().flipX = absoluteFlipXForObject;
        }
    }
}
