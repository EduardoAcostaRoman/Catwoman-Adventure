using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveShark : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer sprites;
    Animator animator;

    GameObject P1;

    public float dash = 5;

    public LayerMask attack;
    bool hit;

    float startPos;
    bool posRst;

    bool standRst;

    float posX;
    float posY;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        sprites = this.GetComponent<SpriteRenderer>();
        P1 = GameObject.Find("P1 position");
    }

    private void FixedUpdate()
    {
        hit = Physics2D.IsTouchingLayers(transform.GetChild(0).GetComponent<BoxCollider2D>(), attack);
    }

    void Update()
    {
        if (P1.transform.localScale.x != 1)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand") && !posRst)
            {
                startPos = transform.position.x;
                posRst = true;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
            {

                if (transform.position.x >= (startPos + 5) && !standRst)
                {
                    sprites.flipX = false;
                    standRst = true;
                }
                else if (transform.position.x <= (startPos - 5) && !standRst)
                {
                    sprites.flipX = true;
                    standRst = true;
                }

                if (transform.position.x <= (startPos + 1) && transform.position.x >= (startPos - 1))
                {
                    standRst = false;
                }

                if (sprites.flipX)
                {
                    body.velocity = new Vector2(3, 0);
                }
                else
                {
                    body.velocity = new Vector2(-3, 0);
                }
            }
            else
            {
                posRst = false;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("bite"))
            {
                if (sprites.sprite.name == "bite2" && transform.GetChild(0).gameObject.layer != 10)
                {
                    if (P1.transform.position.x < transform.position.x)
                    {
                        sprites.flipX = false;
                    }
                    else
                    {
                        sprites.flipX = true;
                    }
                }

                if (sprites.sprite.name == "bite2" || sprites.sprite.name == "bite3")
                {
                    if (sprites.flipX)
                    {
                        body.velocity = new Vector2(dash, 0);

                        transform.GetChild(0).GetComponent<BoxCollider2D>().offset = new Vector2(0.4f,
                                         transform.GetChild(0).GetComponent<BoxCollider2D>().offset.y);
                    }
                    else
                    {
                        body.velocity = new Vector2(-dash, 0);

                        transform.GetChild(0).GetComponent<BoxCollider2D>().offset = new Vector2(-0.4f,
                                         transform.GetChild(0).GetComponent<BoxCollider2D>().offset.y);
                    }

                    transform.GetChild(0).gameObject.layer = 10;
                }
                else
                {
                    body.velocity = new Vector2(0, 0);
                    transform.GetChild(0).gameObject.layer = 13;
                }

                if (hit)
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
                }
                else
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
                }
            }
            else
            {
                transform.GetChild(0).gameObject.layer = 13;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                if (P1.transform.position.x < transform.position.x)
                {
                    body.velocity = new Vector2(5, 0);
                }
                else
                {
                    body.velocity = new Vector2(-5, 0);
                }
            }
        }

            
    }
}
