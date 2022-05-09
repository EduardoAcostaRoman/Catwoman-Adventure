﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishgirl : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Transform transformer;
    private GameObject P1;
    public LayerMask attack;
    public LayerMask super;
    private bool hurtReset;
    private int death;
    private bool hit;
    private bool hitsuper;
    private bool atkRst;
    private bool boomRst;

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
        Physics2D.IgnoreLayerCollision(10, 10, true);
        Physics2D.IgnoreLayerCollision(10, 13, true);
        Physics2D.IgnoreLayerCollision(10, 16, true);
        Physics2D.IgnoreLayerCollision(13, 16, true);
    }

    void Update()
    {
        if (this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite.name == "blank")
        {
            Destroy(this.gameObject);
        }
        if (P1.transform.localScale.x == 1)
        {
            body.gravityScale = 0;
            body.velocity = new Vector2(0, 0);
            animator.StartPlayback();
        }
        else
        {           
            if (death < 2)
            {
                body.gravityScale = 5;
            }
            else
            {
                body.gravityScale = 0;
                body.velocity = new Vector2(body.velocity.x, 0);
            }
            animator.StopPlayback();
            if (hit == true)
            {
                animator.SetBool("hurt", true);
            }
            if (hitsuper == true)
            {
                animator.SetBool("hurt", true);
                death = 2;
            }
            if (sprite.color.a == 0 && boomRst == false)
            {
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                boomRst = true;
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
                this.gameObject.transform.GetChild(1).gameObject.layer = 13;
                this.gameObject.layer = 13;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && hurtReset == false)
            {
                this.gameObject.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
                this.GetComponent<AudioSource>().Stop();
                death += 1;
                hurtReset = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                hurtReset = false;
            }
            if (death >= 2 && sprite.sprite.name == "fish girl_24")
            {
                sprite.color = new Color(0, 0, 0, 0);
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("boom", true);
                body.velocity = new Vector2(0, 0);
            }
            if (sprite.flipX == true)
            {
                this.GetComponent<BoxCollider2D>().offset = new Vector2(0.01402283f, 0.1680532f);
            }
            else if (sprite.flipX == false)
            {
                this.GetComponent<BoxCollider2D>().offset = new Vector2(-0.01630402f, 0.1680532f);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
            {
                body.velocity = new Vector2(0, body.velocity.y);
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
            if (sprite.sprite.name == "fish girl_4" || sprite.sprite.name == "fish girl_5" || sprite.sprite.name == "fish girl_6" || sprite.sprite.name == "fish girl_7")
            {
                this.gameObject.transform.GetChild(1).gameObject.layer = 10;
                this.gameObject.layer = 13;
                if (sprite.flipX == true)
                {
                    body.velocity = new Vector2(-16, body.velocity.y);
                }
                else if (sprite.flipX == false)
                {
                    body.velocity = new Vector2(16, body.velocity.y);
                }
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                this.gameObject.transform.GetChild(1).gameObject.layer = 13;
                this.gameObject.layer = 10;
            }
            if (sprite.sprite.name == "fish girl_0" || sprite.sprite.name == "fish girl_1" || sprite.sprite.name == "fish girl_2" || sprite.sprite.name == "fish girl_3")
            {
                body.velocity = new Vector2(0, body.velocity.y);
                if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
            }
            if (sprite.sprite.name == "fish girl_4" && atkRst == false)
            {
                this.GetComponent<AudioSource>().Play();
                animator.SetBool("attack", false);
                atkRst = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                atkRst = false;
            }
            if (sprite.color.a == 0)
            {
                body.velocity = new Vector2(0, 0);
                this.gameObject.transform.GetChild(1).gameObject.layer = 13;
                this.gameObject.layer = 13;
            }
        }
    }
}
