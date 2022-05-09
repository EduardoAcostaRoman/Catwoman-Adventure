using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasite : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprites;
    Rigidbody2D body;
    private GameObject P1;

    GameObject parasiteCounter;

    public LayerMask attack;
    public LayerMask player;
    public LayerMask super;

    private bool hit;
    private bool hit2;
    private bool hitsuper;
    private bool hurtReset;
    private bool boomRst;

    bool colorChange = false;
    int colorCount;
    bool blink;
    float realtime;
    float prevtime;

    float initialPosY;

    float velX;
    float velY;

    public float velocityX = 0.05f;
    public float velocityY = 0.05f;

    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;

    bool atkRst;

    void whiteSprite()
    {
        sprites.material.shader = shaderGUItext;
        sprites.color = Color.white;
    }

    void normalSprite()
    {
        sprites.material.shader = shaderSpritesDefault;
        sprites.color = Color.white;
    }

    private void FixedUpdate()
    {
        hit = Physics2D.IsTouchingLayers(GetComponent<BoxCollider2D>(), attack);
        hit2 = Physics2D.IsTouchingLayers(GetComponent<BoxCollider2D>(), player);
        hitsuper = Physics2D.IsTouchingLayers(GetComponent<BoxCollider2D>(), super);

        Physics2D.IgnoreLayerCollision(10, 10, true);
        Physics2D.IgnoreLayerCollision(10, 13, true);
    }

    void turn(bool defaultFlipX)
    {
        if (P1.transform.position.x >= transform.position.x)
        {
            sprites.flipX = !defaultFlipX;
        }
        else
        {
            sprites.flipX = defaultFlipX;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        sprites = GetComponent<SpriteRenderer>();
        P1 = GameObject.Find("P1 position");
        body = GetComponent<Rigidbody2D>();

        initialPosY = transform.position.y;

        parasiteCounter = GameObject.Find("ParasiteCounter");

        parasiteCounter.transform.position = new Vector2(parasiteCounter.transform.position.x + 1,
                                                         parasiteCounter.transform.position.y);
    }

    void Update()
    {
        
        if (sprites.sprite.name == "blank")
        {
            Destroy(gameObject);
        }
        if (P1.transform.localScale.x == 1)
        {
            body.velocity = new Vector2(0,0);
            animator.StartPlayback();
        }
        else
        {
            animator.StopPlayback();

            if (hit == true || hit2 == true || hitsuper == true)
            {
                animator.SetBool("death", true);
            }

            //if (sprites.color.a == 0 && boomRst == false)
            //{
            //    gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
            //    boomRst = true;
            //}

            if (sprites.sprite.name == "blank")
            {
                parasiteCounter.transform.position = new Vector2(parasiteCounter.transform.position.x - 1,
                                                         parasiteCounter.transform.position.y);
                Destroy(gameObject);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
            {
                turn(false);

                if (transform.position.y >= initialPosY + 5 && !atkRst)
                {
                    atkRst = true;        
                }               
                else if (!atkRst)
                {
                    body.velocity = new Vector2(0, 5);
                }
                else
                {
                    if (P1.transform.position.x >= transform.position.x)
                    {
                        velX = Mathf.Clamp(body.velocity.x, -5, 5) + velocityX;
                    }
                    else
                    {
                        velX = Mathf.Clamp(body.velocity.x, -5, 5) - velocityX;
                    }

                    if (P1.transform.position.y + 1 >= transform.position.y)
                    {
                        velY = Mathf.Clamp(body.velocity.y, -5, 5) + velocityY;
                    }
                    else
                    {
                        velY = Mathf.Clamp(body.velocity.y, -5, 5) - velocityY;
                    }

                    body.velocity = new Vector2(velX, velY);
                }
            }
            else
            {
                body.velocity = new Vector2(0, 0);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                gameObject.layer = 13;
            }

            //---------------- BLINK WHILE IN HURT ----------------//

            if ((hit == true || hit2 == true || hitsuper == true) && !blink)
            {
                prevtime = realtime;
                blink = true;
            }


            realtime = Time.fixedTime;

            if (blink)
            {
                if (realtime - prevtime > 0.08f)
                {
                    if (!colorChange)
                    {
                        whiteSprite();
                    }
                    else
                    {
                        normalSprite();
                    }

                    colorChange = !colorChange;

                    colorCount += 1;

                    prevtime = realtime;

                    
                }

                if (colorCount >= 4)
                {
                    colorCount = 0;
                    normalSprite();
                    blink = false;
                }
            }
            else
            {
                colorChange = false;
            }

            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && hurtReset == false)
            //{
            //    GetComponent<AudioSource>().Play();
            //    hurtReset = true;
            //}
        }
    }
}
