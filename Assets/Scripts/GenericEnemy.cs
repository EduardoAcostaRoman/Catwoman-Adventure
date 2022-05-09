using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer sprites;
    Animator animator;

    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;

    float realtime;
    float prevtime;
    float prevTimeStand;

    bool colorChange = false;
    int colorCount;
    bool blink;

    GameObject P1;

    public LayerMask attack;
    public LayerMask super;

    public int death = 2;
    int deathVal;

    bool hit;
    bool hitsuper;
    bool hitRst;

    bool isAttacking;

    

    // ----------- PONER LOS ATAQUES EN ORDEN DE PATRÓN (ARRIBA HACIA ABAJO) EN EL DISEÑADOR DE UNITY --------- //

    public string atkName1 = "attack";
    bool atkRst1 = false;

    public string atkName2 = "attack2";
    bool atkRst2 = false;

    public string atkName3 = "attack3";
    bool atkRst3 = false;

    public string atkName4 = "attack4";
    bool atkRst4 = false;

    public string atkName5 = "attack5";
    bool atkRst5 = false;

    public string atkName6 = "attack6";
    bool atkRst6 = false;

    //---------------- CONFIGURACIÓN DE PATRÓN DE ATAQUE ----------------//

    public bool atkPatternSimple = true;

    public float standTime = 1;


    //---------------- CONFIGURACIONES ----------------//

    public float gravity = 1;

    public float atkDistance = 5;

    public float boxColliderOffset = 0;

    public float circleColliderOffset = 0;

    public bool startFlipX = false;

    public bool getsHurt = true;

    public bool cancelableAttacks = true;

    public bool groundMechanic = false;

    //-------------------------------------------------//


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

    void onAttack (string atkName, bool atkRst)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(atkName) && !atkRst)
        {
            animator.SetBool(atkName, false);
            isAttacking = true;
            atkRst = true;
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName(atkName))
        {
            atkRst = false;
            isAttacking = false;
        }
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
        sprites = this.GetComponent<SpriteRenderer>();
        P1 = GameObject.Find("P1 position");

        Physics2D.IgnoreLayerCollision(10, 10, true);
        Physics2D.IgnoreLayerCollision(10, 13, true);
    }

    
    void Update()
    {
        //---------------- BOX COLLIDER OFFSET ----------------//

        if (GetComponent<BoxCollider2D>())
        {
            if (sprites.flipX)
            {
                GetComponent<BoxCollider2D>().offset = new Vector2(boxColliderOffset, GetComponent<BoxCollider2D>().offset.y);
            }
            else
            {
                GetComponent<BoxCollider2D>().offset = new Vector2(-boxColliderOffset, GetComponent<BoxCollider2D>().offset.y);
            }
        }

        if (GetComponent<CircleCollider2D>())
        {
            if (sprites.flipX)
            {
                GetComponent<CircleCollider2D>().offset = new Vector2(circleColliderOffset, GetComponent<CircleCollider2D>().offset.y);
            }
            else
            {
                GetComponent<CircleCollider2D>().offset = new Vector2(-circleColliderOffset, GetComponent<CircleCollider2D>().offset.y);
            }
        }

        realtime = Time.fixedTime;

        //---------------- TIME STOP ----------------//

        if (P1.transform.localScale.x == 1)
        {
            body.velocity = new Vector2(0, 0);
            animator.StartPlayback();
            body.gravityScale = 0;
        }
        else
        {
            animator.StopPlayback();

            body.gravityScale = gravity;

            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
            //{
            //    if (P1.transform.position.x < transform.position.x)
            //    {
            //        sprites.flipX = startFlipX;
            //    }
            //    else
            //    {
            //        sprites.flipX = !startFlipX;
            //    }
            //}

            if (getsHurt)
            {
                if (cancelableAttacks)
                {
                    // --- "transform.localScale.z == 1" representa daño en hitboxes especiales aparte del default --- //

                    if ((hit || transform.localScale.z == 1) && !animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && !hitRst)
                    {
                        deathVal += 1;

                        animator.SetBool("hurt", true);

                        hitRst = true;
                    }

                    if (hitsuper && !animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && !hitRst)
                    {
                        deathVal += 4;

                        animator.SetBool("hurt", true);

                        hitRst = true;
                    }           
                }
                else
                {
                    if ((hit || transform.localScale.z == 1) && animator.GetCurrentAnimatorStateInfo(0).IsName("stand") && !hitRst)
                    {
                        animator.SetBool("hurt", true);

                        hitRst = true;
                    }

                    if (hitsuper && animator.GetCurrentAnimatorStateInfo(0).IsName("stand") && !hitRst)
                    {
                        animator.SetBool("hurt", true);

                        hitRst = true;
                    }

                    if (GetComponent<SpriteRenderer>().color.a >= 0.9)
                    {
                        if (hit && !blink)
                        {
                            deathVal += 1;
                        }
                        else if (hitsuper && !blink)
                        {
                            deathVal += 4;
                        }
                    }                
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
                {
                    animator.SetBool("hurt", false);
                }

            }
            else
            {
                if (hit && !blink)
                {
                    deathVal += 1;
                }
                else if (hitsuper && !blink)
                {
                    deathVal += 4;
                }
            }

            


            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                gameObject.layer = 13;
            }
            else
            {
                gameObject.layer = 10;
                hitRst = false;
            }

            //---------------- BLINK WHILE IN HURT ----------------//

            if (hit && !blink)
            {
                prevtime = realtime;
                blink = true;
            }

            if (hitsuper && !blink)
            {
                prevtime = realtime;
                blink = true;
            }


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


            //---------------- ATTACK SEQUENCE ----------------//

            if (atkPatternSimple)
            {
                if (!isAttacking && Mathf.Abs(P1.transform.position.x - transform.position.x) <= atkDistance)
                {
                    animator.SetBool(atkName1, true);
                }
            }
            else
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("stand") && !animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
                {
                    prevTimeStand = realtime;
                }

                if ((animator.GetCurrentAnimatorStateInfo(0).IsName("stand") || animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
                    && realtime - prevTimeStand >= standTime)
                {
                    animator.SetBool(atkName1, true);
                }

            }

            onAttack(atkName1, atkRst1);
            onAttack(atkName2, atkRst2);
            onAttack(atkName3, atkRst3);
            onAttack(atkName4, atkRst4);
            onAttack(atkName5, atkRst5);
            onAttack(atkName6, atkRst6);


            //---------------- DEATH ----------------//

            if (deathVal >= death)
            {
                animator.SetBool("death", true);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                animator.SetBool("death", false);
                if (groundMechanic)
                {
                    body.gravityScale = gravity;
                    body.velocity = new Vector2(0, body.velocity.y);
                }
                else
                {
                    body.gravityScale = 0;
                    body.velocity = new Vector2(0, 0);
                }
                
                colorChange = false;
                normalSprite();

                if (sprites.sprite.name == "blank")
                {
                    Destroy(gameObject);
                }
            }

            if (GetComponent<SpriteRenderer>().color.a < 0.9)
            {
                blink = false;
                hit = false;
                hitsuper = false;
                normalSprite();
                gameObject.layer = 13;
            }
        }
    }
}