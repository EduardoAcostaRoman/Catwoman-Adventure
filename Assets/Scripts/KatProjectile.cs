using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatProjectile : MonoBehaviour {

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private GameObject kat;
    public float destructionTime = 1;
    public float theVelocity = 30;

    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        kat = GameObject.Find("Catwoman");
        if (kat.GetComponent<SpriteRenderer>().flipX == true)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    void Update()
    {
        if (sprite.flipX == true)
        {
            this.GetComponent<BoxCollider2D>().offset = new Vector2(0.04881039f, -0.003909275f);
            body.velocity = new Vector2(theVelocity, 0);
        }
        else
        {
            this.GetComponent<BoxCollider2D>().offset = new Vector2(-0.04881039f, -0.003909275f);
            body.velocity = new Vector2(-theVelocity, 0);
        }

        Destroy(gameObject, destructionTime);
    }
}
