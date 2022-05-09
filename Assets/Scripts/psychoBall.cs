using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psychoBall : MonoBehaviour
{

    private bool hit;
    public LayerMask projectile;

    private void FixedUpdate()
    {
        hit = Physics2D.IsTouchingLayers(this.GetComponent<CircleCollider2D>(), projectile);
    }

    void Start()
    {
        if (transform.position.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(20, 0);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 0);
        }
    }

    void Update()
    {
        if (transform.position.x >= 21 || transform.position.x <= -21)
        {
            if (transform.position.x > 0)
            {
                transform.position = new Vector3(-20, -3.67f, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(20, -3.67f, transform.position.z);
            }
        }

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ballLoop"))
        {
            gameObject.layer = 10;
        }
        else
        {
            gameObject.layer = 13;
        }

        if (hit == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Animator>().SetBool("end", true);
        }

        if (GetComponent<SpriteRenderer>().sprite.name == "blank")
        {
            Destroy(this.gameObject, 0.1f);
        }
    }
}
