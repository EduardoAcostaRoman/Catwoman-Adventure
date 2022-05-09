using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutations : MonoBehaviour
{

    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;
    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D body;
    private GameObject P1;
    public LayerMask attack;
    public LayerMask super;
    private bool walkRst;
    private bool hit;
    private bool hitsuper;
    private bool atkRst;
    private bool boomRst;
    public LayerMask ground;
    private bool grounded;

    void whiteSprite()
    {
        sprite.material.shader = shaderGUItext;
        sprite.color = Color.white;
    }

    void normalSprite()
    {
        sprite.material.shader = shaderSpritesDefault;
        sprite.color = Color.white;
    }

    private void FixedUpdate()
    {
        hit = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), attack);
        hitsuper = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), super);
        grounded = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), ground);
    }

    void Start()
    {
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
        body = this.GetComponent<Rigidbody2D>();
        P1 = GameObject.Find("P1 position");
        Physics2D.IgnoreLayerCollision(10, 4, true);
        Physics2D.IgnoreLayerCollision(13, 4, true);
        Physics2D.IgnoreLayerCollision(10, 10, true);
        Physics2D.IgnoreLayerCollision(10, 13, true);
        Physics2D.IgnoreLayerCollision(13, 10, true);
        Physics2D.IgnoreLayerCollision(13, 13, true);
    }

    void Update()
    {
        if (grounded)
        {
            animator.SetBool("grounded", true);
        }
        else
        {
            animator.SetBool("grounded", false);
        }
        if (P1.transform.localScale.x == 1)
        {
            body.gravityScale = 0;
            body.velocity = new Vector2(0, 0);
            animator.StartPlayback();
        }
        else
        {
            body.gravityScale = 5;
            animator.StopPlayback();
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("explode"))
            {
                if (hit == true)
                {
                    animator.SetBool("death", true);
                }
                if (hitsuper == true)
                {
                    animator.SetBool("death", true);
                }
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("explode") && 
                !(sprite.sprite.name == "explosion 16" || sprite.sprite.name == "explosion 17" ||
                sprite.sprite.name == "explosion 18" || sprite.sprite.name == "explosion 19" ||
                sprite.sprite.name == "explosion 20" || sprite.sprite.name == "explosion 21" ||
                sprite.sprite.name == "explosion 15" || sprite.sprite.name == "explosion 14"))
            {
                transform.GetChild(0).gameObject.layer = 10;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                body.velocity = new Vector2(0, 0);
                gameObject.layer = 13;
                transform.GetChild(0).gameObject.layer = 13;
            }
            if (sprite.sprite.name == "explode 1" && atkRst == false)
            {
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                atkRst = true;
            }
            if (sprite.sprite.name == "explosion 1" && boomRst == false)
            {
                this.gameObject.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();
                boomRst = true;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death") && boomRst == false)
            {
                GetComponent<AudioSource>().Play();
                this.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();
                boomRst = true;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            {
                if (Mathf.Abs(P1.transform.position.x - transform.position.x) <= 25)
                {
                    if (P1.GetComponent<Transform>().position.x < transform.position.x)
                    {
                        sprite.flipX = false;
                        body.velocity = new Vector2(-6, 0);
                    }
                    else
                    {
                        sprite.flipX = true;
                        body.velocity = new Vector2(6, 0);
                    }
                }               
                if (Mathf.Abs(P1.transform.position.x - transform.position.x) <= 2.5f)
                {
                    animator.SetBool("attack", true);
                }
            }
            else
            {
                body.velocity = new Vector2(0, body.velocity.y);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk") && walkRst == false)
            {
                this.gameObject.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
                walkRst = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            {
                this.gameObject.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Stop();
                walkRst = false;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
            {
                if (P1.GetComponent<Transform>().position.x < transform.position.x)
                {
                    sprite.flipX = false;
                }
                else
                {
                    sprite.flipX = true;
                }
            }
            if (sprite.sprite.name == "death 3" || sprite.sprite.name == "death 4" || sprite.sprite.name == "death 7" || sprite.sprite.name == "death 8")
            {
                whiteSprite();
            }
            else
            {
                normalSprite();
            }
            if (sprite.sprite.name == "blank")
            {
                transform.GetChild(0).gameObject.layer = 13;
                Destroy(gameObject);
            }
        }
    }
}