using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Transform transformer;
    private GameObject P1;
    public GameObject eruption;
    public GameObject thunder;
    public GameObject ball;
    public LayerMask attack;
    public LayerMask super;
    private bool hurtReset;
    private float death;
    private bool hit;
    private bool hitsuper;
    private bool superRst;
    private bool blink;
    private float blinkVal;
    private int blinkStop;
    private float standTime;
    private bool atkRst;
    private bool atkRst2;
    private bool atkBallRst;
    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;
    private float deathVal = 36;
    private int atkPattern = 0;
    private bool deathRst;
    private float posX;
    private float posY;
    private Vector2 velocity;
    private float colorR = 255, colorG = 255;


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
        //print(death);

        if (P1.transform.localScale.x == 1)
        {
            body.velocity = new Vector2(0, 0);
            sprite.color = new Color(255, 255, 255, 255);
            animator.StartPlayback();
            transform.GetChild(0).GetComponent<Animator>().StartPlayback();
        }
        else
        {
            animator.StopPlayback();
            transform.GetChild(0).GetComponent<Animator>().StopPlayback();

            posX += 0.02f;
            posY += 0.02f;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("ballAttack") || animator.GetCurrentAnimatorStateInfo(0).IsName("ballAttack2"))
            {
                transform.position = new Vector3(
                Mathf.SmoothDamp(transform.position.x, 0, ref velocity.x, 0.5f),
                Mathf.SmoothDamp(transform.position.y, 1, ref velocity.y, 0.5f),
                transform.position.z);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
            {          
                transform.position = new Vector3(Mathf.Sin(posX) * 10, (Mathf.Cos(posY) * 2.25f) + 0.5f, transform.position.z);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
            {
                transform.position = new Vector3(
                Mathf.SmoothDamp(transform.position.x, 0, ref velocity.x, 0.5f),
                Mathf.SmoothDamp(transform.position.y, 2.25f, ref velocity.y, 0.5f),
                transform.position.z);
                posX = 0;
                posY = 0;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("transform"))
            {
                animator.SetBool("transform", false);
                animator.SetBool("attack", false);
                animator.SetBool("attack2", false);
                animator.SetBool("ballAtk", false);

                if (sprite.sprite.name == "transform")
                {
                    transform.GetChild(1).GetComponent<Animator>().SetBool("aura", true);
                }
                if (transform.GetChild(1).GetComponent<SpriteRenderer>().sprite.name == "exp5")
                {
                    transform.GetChild(0).GetComponent<Animator>().SetBool("aura", true);
                    transform.GetChild(1).GetComponent<Animator>().SetBool("aura", false);
                    colorR = 0;
                    colorG = 203;
                }
                posX = 0;
                posY = 0;
            }


            if (death < ((deathVal - 6) / 2))
            {
                if (atkPattern >= 0 && atkPattern <= 2)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand") || animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
                    {
                        transform.position = new Vector3(Mathf.Sin(posX) * 10, (Mathf.Cos(posY) * 2.25f) + 0.5f, transform.position.z);
                    }
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") && atkRst == false)
                    {
                        animator.SetBool("attack", false);
                        Instantiate(eruption, new Vector3(P1.transform.position.x - 5, -6.45f, -4), this.transform.rotation);
                        Instantiate(eruption, new Vector3(P1.transform.position.x + 5, -6.45f, -4), this.transform.rotation);
                        atkPattern += 1;
                        atkRst = true;
                    }
                    else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") && standTime >= 3)
                    {
                        atkRst = false;
                        animator.SetBool("attack", true);
                    }
                }
                else if (atkPattern == 3)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("ballAttack") && sprite.sprite.name == "atk1" && atkBallRst == false)
                    {
                        animator.SetBool("ballAtk", false);
                        Instantiate(ball, transform.GetChild(2).transform.position, transform.GetChild(2).transform.rotation);
                        Instantiate(ball, transform.GetChild(3).transform.position, transform.GetChild(3).transform.rotation);
                        atkPattern += 1;
                        atkBallRst = true;
                    }
                    else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ballAttack"))
                    {
                        atkBallRst = false;
                        animator.SetBool("ballAtk", true);
                    }
                }
                else if (atkPattern == 4)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack2") && sprite.sprite.name == "atk2_1" && atkRst2 == false)
                    {
                        animator.SetBool("attack2", false);
                        if (P1.transform.position.x >= 0)
                        {
                            Instantiate(thunder, new Vector3(P1.transform.position.x - 3, 8.92f, -4), thunder.transform.rotation);
                        }
                        else
                        {
                            Instantiate(thunder, new Vector3(P1.transform.position.x + 3, 8.92f, -4), thunder.transform.rotation);
                        }
                        atkPattern += 1;
                        atkRst2 = true;
                    }
                    else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack2") && standTime >= 3)
                    {
                        atkRst2 = false;
                        animator.SetBool("attack2", true);
                    }
                }
                else
                {
                    atkPattern = 0;
                }
            }
            else if (death >= ((deathVal - 6)/2) && death <= (((deathVal - 6) / 2) + 4))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("transform"))
                {
                    animator.SetBool("transform", true);
                }
                if (transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("aura"))
                {
                    transform.position = new Vector3(
                    Mathf.SmoothDamp(transform.position.x, 0, ref velocity.x, 0.5f),
                    Mathf.SmoothDamp(transform.position.y, 2.25f, ref velocity.y, 0.5f),
                    transform.position.z);
                    if (transform.position.y >= 2.24f)
                    {
                        death = ((deathVal - 6) / 2) + 5;
                    }
                }
                else
                {
                    transform.position = new Vector3(
                    Mathf.SmoothDamp(transform.position.x, 0, ref velocity.x, 0.5f),
                    Mathf.SmoothDamp(transform.position.y, 1, ref velocity.y, 0.5f),
                    transform.position.z);
                }
                
                posX = 0;
                posY = 0;
            }
            else
            {
                if (atkPattern >= 0 && atkPattern <= 2)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand") || animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
                    {
                        transform.position = new Vector3(Mathf.Sin(posX) * 10, (Mathf.Cos(posY) * 2.25f) + 0.5f, transform.position.z);
                    }
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") && atkRst == false)
                    {
                        animator.SetBool("attack", false);
                        Instantiate(eruption, new Vector3(P1.transform.position.x - 32, -6.45f, -4), this.transform.rotation);
                        Instantiate(eruption, new Vector3(P1.transform.position.x + 32, -6.45f, -4), this.transform.rotation);
                        Instantiate(eruption, new Vector3(P1.transform.position.x - 24, -6.45f, -4), this.transform.rotation);
                        Instantiate(eruption, new Vector3(P1.transform.position.x + 24, -6.45f, -4), this.transform.rotation);
                        Instantiate(eruption, new Vector3(P1.transform.position.x + 8, -6.45f, -4), this.transform.rotation);
                        Instantiate(eruption, new Vector3(P1.transform.position.x + 16, -6.45f, -4), this.transform.rotation);
                        Instantiate(eruption, new Vector3(P1.transform.position.x - 8, -6.45f, -4), this.transform.rotation);
                        Instantiate(eruption, new Vector3(P1.transform.position.x - 16, -6.45f, -4), this.transform.rotation);
                        Instantiate(eruption, new Vector3(P1.transform.position.x, -6.45f, -4), this.transform.rotation);
                        atkPattern += 1;
                        atkRst = true;
                    }
                    else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") && standTime >= 1)
                    {
                        atkRst = false;
                        animator.SetBool("attack", true);
                    }
                }
                else if (atkPattern >= 3 && atkPattern < 6)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("ballAttack2") && sprite.sprite.name == "atk1" && atkBallRst == false)
                    {
                        animator.SetBool("ballAtk2", false);
                        Instantiate(ball, transform.GetChild(2).transform.position, transform.GetChild(2).transform.rotation);
                        Instantiate(ball, transform.GetChild(3).transform.position, transform.GetChild(3).transform.rotation);
                        atkPattern += 1;
                        atkBallRst = true;
                    }
                    else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ballAttack2"))
                    {
                        atkBallRst = false;
                        animator.SetBool("ballAtk2", true);
                    }
                    if (sprite.sprite.name != "atk1")
                    {
                        atkBallRst = false;
                    }
                }
                else if (atkPattern == 6)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack2") && sprite.sprite.name == "atk2_1" && atkRst2 == false)
                    {
                        animator.SetBool("attack2", false);
                        Instantiate(thunder, new Vector3(P1.transform.position.x - 6, 8.92f, -4), thunder.transform.rotation);
                        Instantiate(thunder, new Vector3(P1.transform.position.x + 6, 8.92f, -4), thunder.transform.rotation);
                        atkPattern += 1;
                        atkRst2 = true;
                    }
                    else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack2") && standTime >= 1)
                    {
                        atkRst2 = false;
                        animator.SetBool("attack2", true);
                    }
                }
                else
                {
                    atkPattern = 0;
                }
            }


            if ((hit || hitsuper) && blink == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("death") && !animator.GetCurrentAnimatorStateInfo(0).IsName("transform"))
            {
                //this.transform.GetChild(0).GetComponent<AudioSource>().Play();
                death += 1f;
            }
            if (hitsuper == true && superRst == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("death") && !animator.GetCurrentAnimatorStateInfo(0).IsName("transform"))
            {
                //animator.SetBool("hurt2", true);
                death += 3;
                superRst = true;
            }
            else if (hitsuper == false)
            {
                superRst = false;
            }
            if (death >= deathVal && hit == true && !animator.GetCurrentAnimatorStateInfo(0).IsName("death") && !animator.GetCurrentAnimatorStateInfo(0).IsName("transform") && superRst == false)
            {
                animator.SetBool("hurt2", true);
                blink = true;
                superRst = true;
            }
            if (hitsuper == true && superRst == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("death") && !animator.GetCurrentAnimatorStateInfo(0).IsName("transform"))
            {
                //animator.SetBool("hurt2", true);
                death += 4;
                blink = true;
                superRst = true;
            }
            else if (hitsuper == false)
            {
                superRst = false;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
            {
                standTime += 0.1f;
                if (hit == true)
                {
                    animator.SetBool("hurt", true);
                }
                body.velocity = new Vector2(0, 0);
                if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
            }
            else if (hit == true || blink == true && !animator.GetCurrentAnimatorStateInfo(0).IsName("death") && !animator.GetCurrentAnimatorStateInfo(0).IsName("transform"))
            {
                standTime = 0;
                blink = true;
            }
            else
            {
                standTime = 0;
            }
            if (hit == true || blink == true || hitsuper == true)
            {
                blink = true;
            }
            if (blink == true)
            {
                hit = false;
                blinkVal += 0.2f;
                if (blinkVal >= 0.9f && blinkVal <= 1.1f)
                {
                    whiteSprite();
                }
                else if (blinkVal >= 1.9f && blinkVal <= 2.1f)
                {
                    normalSprite();
                    blinkStop += 1;
                    blinkVal = 0;
                }
            }
            if (blinkStop == 3)
            {
                blink = false;
                blinkVal = 0;
                blinkStop = 0;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                animator.SetBool("hurt2", false);
                animator.SetBool("hurt", false);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && hurtReset == false)
            {
                //this.transform.GetChild(1).GetComponent<AudioSource>().Stop();
                //this.transform.GetChild(5).GetComponent<AudioSource>().Stop();
                //this.transform.GetChild(6).GetComponent<AudioSource>().Stop();
                //this.transform.GetChild(8).GetComponent<AudioSource>().Stop();
                //this.GetComponent<AudioSource>().Play();
                hurtReset = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                hurtReset = false;
            }
            if (death >= deathVal)
            {
                //this.transform.GetChild(9).GetComponent<AudioSource>().Play();
                animator.SetBool("death", true);
                death = deathVal + 1;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death") && deathRst == false)
            {
                transform.GetChild(1).GetComponent<Animator>().SetBool("aura", true);
                if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
                deathRst = true;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                normalSprite();
                if (transform.GetChild(1).GetComponent<SpriteRenderer>().sprite.name == "exp2")
                {
                    transform.GetChild(0).GetComponent<Animator>().SetBool("aura", false);
                    transform.GetChild(1).GetComponent<Animator>().SetBool("aura", false);
                }
                colorR = 255;
                colorG = 255;
                transform.position = new Vector3(
                    transform.position.x,
                    Mathf.SmoothDamp(transform.position.y, -6.1f, ref velocity.y, 0.3f),
                    transform.position.z);
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("start"))
        {
            posX = 0;
            posY = 0;
        }
        sprite.color = new Color(colorR, colorG, 255, 1);
    }
}
