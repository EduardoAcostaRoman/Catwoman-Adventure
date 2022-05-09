using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpy2 : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Transform transformer;
    private GameObject P1;
    private GameObject tornado;
    private GameObject explosion;
    private GameObject dmg;
    public LayerMask super;
    public LayerMask attack;
    private bool hurtReset;
    private int death;
    private bool hit;
    private bool hitsuper;
    private bool rst;
    private bool boomrst;

    private void FixedUpdate()
    {
        hit = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), attack);
        hitsuper = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), super);
    }

    void Start()
    {
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        transformer = this.GetComponent<Transform>();
        P1 = GameObject.Find("P1 position");
    }

    void Update()
    {
        tornado = GameObject.Find("tornado2");
        explosion = GameObject.Find("explosion2");
        dmg = GameObject.Find("dmgharpy2");
        if (explosion.GetComponent<SpriteRenderer>().sprite.name == "blank")
        {
            Destroy(this.gameObject);
        }
        if (P1.transform.localScale.x == 1)
        {
            body.velocity = new Vector2(0, 0);
            animator.StartPlayback();
        }
        else
        {
            animator.StopPlayback();
            if (sprite.sprite.name == "harpy_4")
            {
                this.GetComponent<AudioSource>().Play();
            }
            if (hit == true)
            {
                animator.SetBool("hurt", true);
            }
            if (hitsuper == true)
            {
                animator.SetBool("hurt", true);
                death = 2;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                animator.SetBool("hurt", false);
                if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                {
                    body.velocity = new Vector2(5, 0);
                }
                else
                {
                    body.velocity = new Vector2(-5, 0);
                }
                this.gameObject.layer = 0;
                tornado.layer = 0;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && hurtReset == false)
            {
                dmg.GetComponent<AudioSource>().Play();
                death += 1;
                hurtReset = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                hurtReset = false;
            }
            if (sprite.color.a == 0 && boomrst == false)
            {
                explosion.GetComponent<AudioSource>().Play();
                boomrst = true;
            }
            if (death >= 2 && sprite.sprite.name == "harpy_24")
            {
                sprite.color = new Color(0, 0, 0, 0);
                explosion.GetComponent<Animator>().SetBool("boom", true);
                body.velocity = new Vector2(0, 0);
            }
            if (explosion.GetComponent<SpriteRenderer>().sprite.name == "blank")
            {
                Destroy(this.gameObject);
            }
            if (sprite.sprite.name == "harpy_12" || sprite.sprite.name == "harpy_13" || sprite.sprite.name == "harpy_14")
            {
                if (sprite.flipX == true)
                {
                    body.velocity = new Vector2(-8, 0);
                }
                else if (sprite.flipX == false)
                {
                    body.velocity = new Vector2(8, 0);
                }
                tornado.layer = 10;
                this.gameObject.layer = 0;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                tornado.layer = 0;
                this.gameObject.layer = 10;
            }
            if (sprite.sprite.name == "harpy_0" || sprite.sprite.name == "harpy_1" || sprite.sprite.name == "harpy_2" || sprite.sprite.name == "harpy_3" || sprite.sprite.name == "harpy_4" || sprite.sprite.name == "harpy_5")
            {
                if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
            }
            if (sprite.sprite.name == "harpy_20")
            {
                body.velocity = new Vector2(0, 0);
            }
            if (sprite.flipX == true)
            {
                this.GetComponent<BoxCollider2D>().offset = new Vector2(-0.0512619f, 0.2171793f);
            }
            else if (sprite.flipX == false)
            {
                this.GetComponent<BoxCollider2D>().offset = new Vector2(0.05937958f, 0.2171793f);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
            {
                body.velocity = new Vector2(0, 0);
                if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
                if ((this.transformer.position.y >= P1.GetComponent<Transform>().position.y - 4) && (this.transformer.position.y <= P1.GetComponent<Transform>().position.y + 3))
                {
                    if ((this.transformer.position.x < P1.GetComponent<Transform>().position.x) && this.transformer.position.x >= P1.GetComponent<Transform>().position.x - 12)
                    {
                        animator.SetBool("attack", true);
                    }
                    else if ((this.transformer.position.x > P1.GetComponent<Transform>().position.x) && this.transformer.position.x <= P1.GetComponent<Transform>().position.x + 12)
                    {
                        animator.SetBool("attack", true);
                    }
                }
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && sprite.sprite.name == "harpy_12" && rst == false)
            {
                tornado.GetComponent<AudioSource>().Play();
                animator.SetBool("attack", false);
                rst = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                rst = false;
            }
            if (sprite.color.a == 0)
            {
                body.velocity = new Vector2(0, 0);
                tornado.layer = 13;
                this.gameObject.layer = 13;
            }
        }
    }

}
