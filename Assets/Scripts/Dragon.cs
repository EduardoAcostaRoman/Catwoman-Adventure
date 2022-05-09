using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour {

    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;
    private Animator animator;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private GameObject P1;
    public LayerMask attack;
    public LayerMask super;
    private bool hurtReset;
    private int death;
    private bool hit;
    private bool hitsuper;
    private bool atkRst;
    private bool boomRst;
    private bool flyRst;
    private Vector2 velocity;

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
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("stand") || animator.GetCurrentAnimatorStateInfo(0).IsName("attack")) && P1.transform.localScale.x != 1)
        {
            body.velocity = new Vector2(0, 0);
            if (Mathf.Abs(P1.transform.position.x - transform.position.x) <= 20 && Mathf.Abs(P1.transform.position.y - transform.position.y) <= 10)
            {
                this.transform.position = new Vector3(
                Mathf.SmoothDamp(this.transform.position.x, P1.transform.position.x, ref velocity.x, 1f),
                Mathf.SmoothDamp(this.transform.position.y, P1.transform.position.y - 2, ref velocity.y, 0.8f),
                this.transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y, this.transform.position.z);
            }
        }
    }

    void Start()
    {
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
        }
        else
        {
            if (sprite.flipX)
            {
                GetComponent<BoxCollider2D>().offset = new Vector2(-0.0385128f, GetComponent<BoxCollider2D>().offset.y);
            }
            else
            {
                GetComponent<BoxCollider2D>().offset = new Vector2(0.0385128f, GetComponent<BoxCollider2D>().offset.y);
            }
            if (death < 4)
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
                    death += 4;
                }
            }
            if (death >= 4)
            {
                animator.SetBool("death", true);
            }
            if (sprite.sprite.name == "punch 5")
            {
                if (sprite.flipX)
                {
                    this.transform.GetChild(1).gameObject.layer = 10;
                }
                else
                {
                    this.transform.GetChild(2).gameObject.layer = 10;
                }
            }
            else if (sprite.sprite.name == "tail 4")
            {
                if (sprite.flipX)
                {
                    this.transform.GetChild(3).gameObject.layer = 10;
                }
                else
                {
                    this.transform.GetChild(4).gameObject.layer = 10;
                }
            }
            else
            {
                this.transform.GetChild(1).gameObject.layer = 13;
                this.transform.GetChild(2).gameObject.layer = 13;
                this.transform.GetChild(3).gameObject.layer = 13;
                this.transform.GetChild(4).gameObject.layer = 13;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                animator.SetBool("hurt", false);
                if (P1.GetComponent<Transform>().position.x < transform.position.x)
                {
                    body.velocity = new Vector2(7, 0);
                    sprite.flipX = true;
                }
                else
                {
                    body.velocity = new Vector2(-7, 0);
                    sprite.flipX = false;
                }
                this.gameObject.layer = 13;
                if (sprite.sprite.name == "hurt 2")
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
                this.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Stop();
                this.gameObject.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();
                this.GetComponent<AudioSource>().Play();
                death += 1;
                hurtReset = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                hurtReset = false;
            }
            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && atkRst == false)
            //{
            //    this.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();
            //    atkRst = true;
            //}
            //else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            //{
            //    atkRst = false;
            //}
            if ((sprite.sprite.name == "punch 5" || sprite.sprite.name == "tail 4") && atkRst == false)
            {
                if (sprite.sprite.name == "punch 5")
                {
                    this.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();
                }
                else
                {
                    this.gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Play();
                }
                atkRst = true;
            }
            else if (!(sprite.sprite.name == "punch 5" || sprite.sprite.name == "tail 4"))
            {
                atkRst = false;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                animator.SetBool("attack", false);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
            {
                if (P1.GetComponent<Transform>().position.x < transform.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
                if (Mathf.Abs(P1.transform.position.x - transform.position.x) <= 8)
                {
                    animator.SetBool("attack", true);
                }
            }
            if (sprite.sprite.name == "fly 4" && flyRst == false)
            {
                this.gameObject.transform.GetChild(5).gameObject.GetComponent<AudioSource>().Play();
                flyRst = true;
            }
            else if (sprite.sprite.name != "fly 4")
            {
                flyRst = false;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                this.gameObject.layer = 13;
                body.velocity = new Vector2(0, 0);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death") && boomRst == false)
            {
                this.gameObject.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                boomRst = true;
            }
            if (sprite.sprite.name == "blank")
            {
                Destroy(gameObject);
            }
        }
    }
}
