using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doberman : MonoBehaviour {

    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;
    private Animator animator;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private GameObject P1;
    public LayerMask attack;
    public LayerMask super;
    public LayerMask ground;
    private bool hurtReset;
    private int death;
    private bool hit;
    private bool hitsuper;
    private bool atkRst;
    private bool boomRst;
    private bool grounded;
    private bool noAtk;

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

    void Start () {
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        P1 = GameObject.Find("P1 position");
        Physics2D.IgnoreLayerCollision(10, 10, true);
        Physics2D.IgnoreLayerCollision(10, 13, true);
        Physics2D.IgnoreLayerCollision(10, 16, true);
        Physics2D.IgnoreLayerCollision(13, 16, true);
    }

    void Update()
    {
        if (P1.transform.localScale.x == 1)
        {
            body.velocity = new Vector2(0, 0);
            animator.StartPlayback();
            body.gravityScale = 0;
        }
        else
        {
            if (death < 3)
            {
                body.gravityScale = 5;
            }
            else
            {
                body.gravityScale = 0;
                body.velocity = new Vector2(0, 0);
            }
            animator.StopPlayback();
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                if (hit == true)
                {
                    animator.SetBool("hurt", true);
                }
                if (hitsuper == true)
                {
                    animator.SetBool("hurt", true);
                    death += 3;
                }
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("prepare"))
            {
                if (this.gameObject.transform.GetChild(3).gameObject.GetComponent<AudioSource>().isPlaying == false)
                {
                    this.gameObject.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();
                }
                body.velocity = new Vector2(0, body.velocity.y);
            }
            if (death >= 3)
            {
                animator.SetBool("death", true);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                animator.SetBool("hurt", false);
                if (P1.GetComponent<Transform>().position.x < transform.position.x)
                {
                    body.velocity = new Vector2(15, -5);
                }
                else
                {
                    body.velocity = new Vector2(-15, -5);
                }
                this.gameObject.layer = 13;
                this.transform.GetChild(0).gameObject.layer = 13;
                if (sprite.sprite.name == "hurt2")
                {
                    whiteSprite();
                }
                else
                {
                    normalSprite();
                }
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                this.gameObject.layer = 10;
                this.transform.GetChild(0).gameObject.layer = 10;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && hurtReset == false)
            {
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                this.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();
                this.GetComponent<AudioSource>().Play();
                death += 1;
                hurtReset = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                hurtReset = false;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && atkRst == false && grounded == false)
            {
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();    
                atkRst = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                atkRst = false;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && body.velocity.y > 0)
            {
                GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death") && boomRst == false)
            {
                this.gameObject.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
                boomRst = true;
            }
            if (grounded)
            {
                animator.SetBool("grounded", true);
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
                {
                    body.velocity = new Vector2(0, body.velocity.y);
                    if (P1.GetComponent<Transform>().position.x < transform.position.x)
                    {
                        sprite.flipX = true;
                    }
                    else
                    {
                        sprite.flipX = false;
                    }
                    if (Mathf.Abs(P1.transform.position.x - transform.position.x) <= 12 && Mathf.Abs(P1.transform.position.x - transform.position.x) >= 5)
                    {
                        animator.SetBool("run", true);
                    }
                }                
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
                {
                    if (Mathf.Abs(P1.transform.position.x - transform.position.x) > 12)
                    {
                        animator.SetBool("run", false);
                    }
                    if (sprite.flipX)
                    {
                        body.velocity = new Vector2(-15, body.velocity.y);
                    }
                    else
                    {
                        body.velocity = new Vector2(15, body.velocity.y);
                    }
                }
                if (Mathf.Abs(P1.transform.position.x - transform.position.x) < 8)
                {
                    noAtk = false;
                    animator.SetBool("attack", true);
                    animator.SetBool("run", false);
                    if (sprite.flipX)
                    {
                        body.velocity = new Vector2(-15, 20);
                    }
                    else
                    {
                        body.velocity = new Vector2(15, 20);
                    }
                }
                else
                {
                    noAtk = true;
                }
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && noAtk == true)
                {
                    animator.SetBool("attack", false);
                }
            }
            else
            {
                animator.SetBool("grounded", false);
                animator.SetBool("run", false);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                this.gameObject.layer = 13;
                this.transform.GetChild(0).gameObject.layer = 13;
                body.gravityScale = 0;
                body.velocity = new Vector2(0, 0);
            }
            if (sprite.sprite.name == "blank")
            {
                Destroy(gameObject);
            }
        }
    }
}
