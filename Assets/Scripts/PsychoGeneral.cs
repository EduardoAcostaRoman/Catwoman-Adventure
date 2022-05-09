using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsychoGeneral : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer sprites;
    Animator animator;
    GameObject P1;

    float realtime;
    float prevtime;
    float prevtimeSummon;

    public LayerMask ground;

    bool grounded;

    float velX;
    float velY;

    public GameObject platformL;
    public GameObject platformM;
    public GameObject platformR;

    public GameObject jumpingPosL;
    public GameObject jumpingPosR;

    bool hit;
    bool hitsuper;
    bool hitRst;

    int jumpCount = 0;

    public GameObject punchBlast;
    public GameObject kickProj;
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bullet4;
    public GameObject bullet5;
    public GameObject psychoBall;

    public GameObject clones;
    public float clonesTime = 5;
    bool activationRst ;

    bool atkRst;

    bool dashRst;

    public float walkVel = 15;

    int floorPos;  // 1 = jumpL; 2 = platformL; 3 = platformM; 4 = platformR; 5 = jumpR;

    bool jumpDir = false;  // false = Left; true = Right;

    int jumpLimit;


    void turn (bool defaultFlipX)
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

    private void FixedUpdate()
    {
        grounded = Physics2D.IsTouchingLayers(transform.GetChild(5).GetComponent<BoxCollider2D>(), ground);
    }

    void Start()
    {
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        sprites = this.GetComponent<SpriteRenderer>();
        P1 = GameObject.Find("P1 position");
    }


    void Update()
    {
        realtime = Time.fixedTime;


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
        {
            body.velocity = new Vector2(0, 0);


            if (P1.transform.position.x < transform.position.x)
            {
                sprites.flipX = true;
            }
            else
            {
                sprites.flipX = false;
            }       
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
        {
            body.velocity = new Vector2(0, body.velocity.y);

            clones.GetComponent<Animator>().SetBool("atk", false);

            clones.transform.GetChild(0).GetComponent<Animator>().SetBool("absoluteEnd", true);
            clones.transform.GetChild(1).GetComponent<Animator>().SetBool("absoluteEnd", true);

            clones.transform.GetChild(0).transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            clones.transform.GetChild(1).transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        }


        // --- JUMPING LIMITS --- //

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("jump") || animator.GetCurrentAnimatorStateInfo(0).IsName("fall"))
        {
            switch (jumpLimit)
            {
                case 1:

                    if (transform.position.x >= platformL.transform.position.x)
                    {
                        body.velocity = new Vector2(0, transform.position.y);
                    }

                    break;

                case 2:

                    if (transform.position.x <= jumpingPosL.transform.position.x)
                    {
                        body.velocity = new Vector2(0, transform.position.y);
                    }

                    break;

                case 3:

                    if (transform.position.x >= platformM.transform.position.x)
                    {
                        body.velocity = new Vector2(0, transform.position.y);
                    }

                    break;

                case 4:

                    if (transform.position.x <= platformL.transform.position.x)
                    {
                        body.velocity = new Vector2(0, transform.position.y);
                    }

                    break;

                case 5:

                    if (transform.position.x >= platformR.transform.position.x)
                    {
                        body.velocity = new Vector2(0, transform.position.y);
                    }

                    break;

                case 6:

                    if (transform.position.x <= platformM.transform.position.x)
                    {
                        body.velocity = new Vector2(0, transform.position.y);
                    }

                    break;

                case 7:

                    if (transform.position.x >= jumpingPosR.transform.position.x)
                    {
                        body.velocity = new Vector2(0, transform.position.y);
                    }

                    break;

                case 8:

                    if (transform.position.x <= platformR.transform.position.x)
                    {
                        body.velocity = new Vector2(0, transform.position.y);
                    }

                    break;

                default:
                    break;
            }
        }

        // ------- ATTACK PATTERN ------- //

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
        {
            jumpCount = 0;
            animator.SetBool("kick", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("kick"))
        {
            animator.SetBool("walk", true);
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("jump") && jumpCount == 0)
        {
            animator.SetBool("psychicBall", true);
            jumpCount = 1;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("psychicBall") && jumpCount == 1)
        {
            animator.SetBool("jump", true);
            jumpCount = 2;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("jump") && jumpCount == 2)
        {
            animator.SetBool("psychicSummon", true);
            jumpCount = 3;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("psychicSummon") && jumpCount == 3)
        {
            animator.SetBool("jump", true);
            jumpCount = 4;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("jump") && jumpCount == 4)
        {
            animator.SetBool("psychicBall", true);
            jumpCount = 5;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("psychicBall") && jumpCount == 5)
        {
            animator.SetBool("jump", true);
            jumpCount = 6;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("fall") && jumpCount == 6)
        {
            animator.SetBool("shotgun", true);
            jumpCount = 7;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("shotgun") && jumpCount == 7)
        {
            animator.SetBool("punch", true);
        }


        // --- WALKING CODE --- //

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            turn(true);

            if (Mathf.Abs(transform.position.x - jumpingPosL.transform.position.x) >= 
                Mathf.Abs(transform.position.x - jumpingPosR.transform.position.x))
            {
                if (transform.position.x > jumpingPosR.transform.position.x)
                {
                    body.velocity = new Vector2(-walkVel, body.velocity.y);
                }
                else
                {
                    body.velocity = new Vector2(walkVel, body.velocity.y);
                }
                
            }
            else
            {
                if (transform.position.x > jumpingPosL.transform.position.x)
                {
                    body.velocity = new Vector2(-walkVel, body.velocity.y);
                }
                else
                {
                    body.velocity = new Vector2(walkVel, body.velocity.y);
                }
            }

            if ((transform.position.x > jumpingPosR.transform.position.x - 0.1f && transform.position.x < jumpingPosR.transform.position.x + 0.1f)
                || (transform.position.x > jumpingPosL.transform.position.x - 0.1f && transform.position.x < jumpingPosL.transform.position.x + 0.1f))
            {
                animator.SetBool("walk", false);              
                animator.SetBool("jump", true);
            }
        }

        // --- FLOOR POSITIONS TO JUMP --- //

        if (transform.position.x > jumpingPosL.transform.position.x - 0.1f &&
            transform.position.x < jumpingPosL.transform.position.x + 0.1f)
        {
            floorPos = 1;
        }
        else if (transform.position.x > platformL.transform.position.x - 0.1f &&
                 transform.position.x < platformL.transform.position.x + 0.1f &&
                 transform.position.y > -7)
        {
            floorPos = 2;
        }
        else if (transform.position.x > platformM.transform.position.x - 0.1f &&
                 transform.position.x < platformM.transform.position.x + 0.1f &&
                 transform.position.y > -9)
        {
            floorPos = 3;
        }
        else if (transform.position.x > platformR.transform.position.x - 0.1f &&
                 transform.position.x < platformR.transform.position.x + 0.1f &&
                 transform.position.y > -7)
        {
            floorPos = 4;
        }
        else if (transform.position.x > jumpingPosR.transform.position.x - 0.1f &&
                 transform.position.x < jumpingPosR.transform.position.x + 0.1f)
        {
            floorPos = 5;
        }

        // --- JUMPS --- //

        if (animator.GetBool("jump") && grounded && animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
        {
            switch (floorPos)
            {
                case 1:
                    // ------- FLOOR TO LEFT SIDE ------- //

                    jumpLimit = 1;
                    body.velocity = new Vector2(9, 25);
                    jumpDir = true;

                    break;

                case 2:

                    // ------- LEFT SIDE TO FLOOR OR MIDDLE ------- //

                    if (jumpDir)
                    {
                        jumpLimit = 3;
                        body.velocity = new Vector2(12, 10);
                    }
                    else
                    {
                        jumpLimit = 2;
                        body.velocity = new Vector2(-12, 10);
                    }

                    break;
                case 3:

                    // ------- MIDDLE TO LEFT SIDE OR RIGHT SIDE ------- //

                    if (jumpDir)
                    {
                        jumpLimit = 5;
                        body.velocity = new Vector2(12, 16);
                    }
                    else
                    {
                        jumpLimit = 4;
                        body.velocity = new Vector2(-12, 16);
                    }

                    break;
                case 4:

                    // ------- RIGHT SIDE TO MIDDLE OR FLOOR ------- //

                    if (jumpDir)
                    {
                        jumpLimit = 7;
                        body.velocity = new Vector2(12, 10);
                    }
                    else
                    {
                        jumpLimit = 6;
                        body.velocity = new Vector2(-12, 10);
                    }

                    break;

                case 5:

                    // -------  FLOOR TO RIGHT SIDE ------- //

                    jumpLimit = 8;
                    body.velocity = new Vector2(-9, 25);
                    jumpDir = false;

                    break;
                default:
                    break;
            }            
        }
        else if (!grounded)
        {
            animator.SetBool("jump", false);
        }

        if (body.velocity.y < 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            animator.SetBool("fall", true);
        }

        if (grounded && animator.GetCurrentAnimatorStateInfo(0).IsName("fall"))
        {
            animator.SetBool("fall", false);
        }

        // --- INSTANTIATION OF BLAST PUNCH --- //

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("punch") && sprites.sprite.name == "punch5" && !atkRst)
        {
            if (sprites.flipX)
            {
                Instantiate(punchBlast,
                            new Vector3(transform.GetChild(1).transform.position.x,
                                        transform.GetChild(1).transform.position.y,
                                        -2),
                            transform.GetChild(1).transform.rotation);
            }
            else
            {
                Instantiate(punchBlast,
                            new Vector3(transform.GetChild(2).transform.position.x,
                                        transform.GetChild(2).transform.position.y,
                                        -2),
                            transform.GetChild(2).transform.rotation);
            }

            atkRst = true;
        }

        // --- INSTANTIATION OF KICK PROJECTILE --- //

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("kick"))
        {
            if (sprites.sprite.name == "dash1")
            {
                if (sprites.flipX)
                {
                    body.velocity = new Vector2(-20, 0);
                }
                else
                {
                    body.velocity = new Vector2(20, 0);
                }
            }

            if (sprites.sprite.name == "dash3")
            {
                if (P1.transform.position.x >= 3.8f && !dashRst)
                {                   
                    transform.position = new Vector3(Mathf.Clamp(P1.transform.position.x - 10, -11, 18), -10.7f);
                    dashRst = true;
                }
                else if (P1.transform.position.x < 3.8f && !dashRst)
                {                   
                    transform.position = new Vector3(Mathf.Clamp(P1.transform.position.x + 10, -11, 18), -10.7f);
                    dashRst = true;
                }

                turn(true);
                
                if (sprites.flipX)
                {
                    body.velocity = new Vector2(-20, 0);
                }
                else
                {
                    body.velocity = new Vector2(20, 0);
                }
            }

            if (sprites.sprite.name == "kick1")
            {
                body.velocity = new Vector2(0, body.velocity.y);
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("kick") && sprites.sprite.name == "kick9" && !atkRst)
        {
            if (sprites.flipX)
            {
                Instantiate(kickProj,
                            new Vector3(transform.GetChild(3).transform.position.x,
                                        transform.GetChild(3).transform.position.y,
                                        -2),
                            transform.GetChild(3).transform.rotation);
            }
            else
            {
                Instantiate(kickProj,
                            new Vector3(transform.GetChild(4).transform.position.x,
                                        transform.GetChild(4).transform.position.y,
                                        -2),
                            transform.GetChild(4).transform.rotation);
            }

            atkRst = true;
        }

        // --- INSTANTIATION OF BULLETS --- //

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("shotgun") && sprites.sprite.name == "shotgun9" && !atkRst)
        {
            if (sprites.flipX)
            {
                Instantiate(bullet,
                            new Vector3(transform.GetChild(1).transform.position.x,
                                        transform.GetChild(1).transform.position.y,
                                        -2),
                            transform.GetChild(1).transform.rotation);
                Instantiate(bullet2,
                            new Vector3(transform.GetChild(1).transform.position.x,
                                        transform.GetChild(1).transform.position.y,
                                        -2),
                            transform.GetChild(1).transform.rotation);
                Instantiate(bullet3,
                            new Vector3(transform.GetChild(1).transform.position.x,
                                        transform.GetChild(1).transform.position.y,
                                        -2),
                            transform.GetChild(1).transform.rotation);
                Instantiate(bullet4,
                            new Vector3(transform.GetChild(1).transform.position.x,
                                        transform.GetChild(1).transform.position.y,
                                        -2),
                            transform.GetChild(1).transform.rotation);
                Instantiate(bullet5,
                            new Vector3(transform.GetChild(1).transform.position.x,
                                        transform.GetChild(1).transform.position.y,
                                        -2),
                            transform.GetChild(1).transform.rotation);
            }
            else
            {
                Instantiate(bullet,
                            new Vector3(transform.GetChild(2).transform.position.x,
                                        transform.GetChild(2).transform.position.y,
                                        -2),
                            transform.GetChild(2).transform.rotation);
                Instantiate(bullet2,
                            new Vector3(transform.GetChild(2).transform.position.x,
                                        transform.GetChild(2).transform.position.y,
                                        -2),
                            transform.GetChild(2).transform.rotation);
                Instantiate(bullet3,
                            new Vector3(transform.GetChild(2).transform.position.x,
                                        transform.GetChild(2).transform.position.y,
                                        -2),
                            transform.GetChild(2).transform.rotation);
                Instantiate(bullet4,
                            new Vector3(transform.GetChild(2).transform.position.x,
                                        transform.GetChild(2).transform.position.y,
                                        -2),
                            transform.GetChild(2).transform.rotation);
                Instantiate(bullet5,
                            new Vector3(transform.GetChild(2).transform.position.x,
                                        transform.GetChild(2).transform.position.y,
                                        -2),
                            transform.GetChild(2).transform.rotation);
            }

            atkRst = true;
        }

        // --- SUMMON CLONES --- //

        // platform clone pos = (3.04f, -4.19f, -0.7f);     floor clone pos = (P1.transform.position.x, -8.56f, -0.7f);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("psychicSummon") && sprites.sprite.name == "psychic5" && !atkRst)
        {
            clones.gameObject.SetActive(true);

            clones.transform.GetChild(0).transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            clones.transform.GetChild(1).transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            
            clones.transform.position = new Vector3(P1.transform.position.x, -8.56f, -0.7f);
            atkRst = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("psychicSummon") && sprites.sprite.name == "psychic14")
        {
            clones.transform.GetChild(0).GetComponent<Animator>().SetBool("start", true);
            clones.transform.GetChild(1).GetComponent<Animator>().SetBool("start", true);
        }

        if (clones.gameObject.activeInHierarchy)
        {
            if (clones.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("cloneRsummon"))
            {
                float scaleX = clones.transform.GetChild(0).localScale.x + 0.2f;
                float scaleY = clones.transform.GetChild(0).localScale.y + 0.1f;

                // --- MAKES THE CLONES BIGGER --- //

                if (clones.transform.GetChild(0).localScale.x < 3)
                {
                    clones.transform.GetChild(0).localScale = new Vector2(scaleX, clones.transform.GetChild(0).localScale.y);
                    clones.transform.GetChild(1).localScale = new Vector2(scaleX, clones.transform.GetChild(0).localScale.y);
                }
                else
                {
                    clones.transform.GetChild(0).localScale = new Vector2(3, clones.transform.GetChild(0).localScale.y);
                    clones.transform.GetChild(1).localScale = new Vector2(3, clones.transform.GetChild(0).localScale.y);
                }

                if (clones.transform.GetChild(0).localScale.y < 1.6f)
                {
                    clones.transform.GetChild(0).localScale = new Vector2(clones.transform.GetChild(0).localScale.x, scaleY);
                    clones.transform.GetChild(1).localScale = new Vector2(clones.transform.GetChild(0).localScale.x, scaleY);
                }
                else
                {
                    clones.transform.GetChild(0).localScale = new Vector2(clones.transform.GetChild(0).localScale.x, 1.6f);
                    clones.transform.GetChild(1).localScale = new Vector2(clones.transform.GetChild(0).localScale.x, 1.6f);
                }

                if (clones.transform.GetChild(0).localScale.x >= 3 && clones.transform.GetChild(0).localScale.y >= 1.6f)
                {
                    clones.transform.GetChild(0).GetComponent<Animator>().SetBool("atk", true);
                    clones.transform.GetChild(1).GetComponent<Animator>().SetBool("atk", true);

                    clones.transform.GetChild(0).gameObject.layer = 10;
                    clones.transform.GetChild(1).gameObject.layer = 10;
                }

                prevtimeSummon = realtime;
            }

            if (clones.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("cloneR"))
            {
                clones.transform.GetChild(0).transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                clones.transform.GetChild(1).transform.GetChild(0).GetComponent<ParticleSystem>().Stop();

                // --- RESET THE DEACTIVATION OF THE OBJECT --- //
                activationRst = true;
            }

            if (clones.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("cloneRatk"))
            {
                clones.GetComponent<Animator>().SetBool("atk", true);
                clones.transform.GetChild(0).GetComponent<Animator>().SetBool("start", false);
                clones.transform.GetChild(1).GetComponent<Animator>().SetBool("start", false);
                clones.gameObject.layer = 10;

                if (realtime - prevtimeSummon >= clonesTime)
                {
                    clones.transform.GetChild(0).GetComponent<Animator>().SetBool("atk", false);
                    clones.transform.GetChild(1).GetComponent<Animator>().SetBool("atk", false);
                }
            }
            else
            {
                clones.GetComponent<Animator>().SetBool("atk", false);
                clones.gameObject.layer = 13;
            }


            if (clones.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("cloneRend"))
            {
                clones.transform.GetChild(0).gameObject.layer = 13;
                clones.transform.GetChild(1).gameObject.layer = 13;

                float scaleX = clones.transform.GetChild(0).localScale.x - 0.2f;
                float scaleY = clones.transform.GetChild(0).localScale.y - 0.1f;

                // --- MAKES THE CLONES TINIER --- //

                if (clones.transform.GetChild(0).localScale.x > 0.1f)
                {
                    clones.transform.GetChild(0).localScale = new Vector2(scaleX, clones.transform.GetChild(0).localScale.y);
                    clones.transform.GetChild(1).localScale = new Vector2(scaleX, clones.transform.GetChild(0).localScale.y);
                }
                else
                {
                    clones.transform.GetChild(0).localScale = new Vector2(0.1f, clones.transform.GetChild(0).localScale.y);
                    clones.transform.GetChild(1).localScale = new Vector2(0.1f, clones.transform.GetChild(0).localScale.y);
                }

                if (clones.transform.GetChild(0).localScale.y > 0.1f)
                {
                    clones.transform.GetChild(0).localScale = new Vector2(clones.transform.GetChild(0).localScale.x, scaleY);
                    clones.transform.GetChild(1).localScale = new Vector2(clones.transform.GetChild(0).localScale.x, scaleY);
                }
                else
                {
                    clones.transform.GetChild(0).localScale = new Vector2(clones.transform.GetChild(0).localScale.x, 0.1f);
                    clones.transform.GetChild(1).localScale = new Vector2(clones.transform.GetChild(0).localScale.x, 0.1f);
                }

                if (clones.transform.GetChild(0).localScale.x <= 0.1f && clones.transform.GetChild(0).localScale.y <= 0.1f && activationRst)
                {
                    clones.transform.GetChild(0).localScale = new Vector2(0.1f, 0.1f);
                    clones.transform.GetChild(1).localScale = new Vector2(0.1f, 0.1f);
                    clones.gameObject.SetActive(false);

                    // --- RESET THE ACTIVATION OF THE OBJECT --- //
                    activationRst = false;
                }
            }
        }

        // --- INSTANTIATION OF PSYCHOBALL --- //

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("psychicBall") && sprites.sprite.name == "psychic4" && !atkRst)
        {
            Instantiate(psychoBall,
                           new Vector3(transform.position.x,
                                       transform.position.y + 6,
                                       -2),
                                       transform.rotation);

            atkRst = true;
        }

        

        // --- RESET OF EVERY ATTACK --- //

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand") || animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
        {
            atkRst = false;
            dashRst = false;
        }

        


        // --- HITBOXES --- //

        

        string[] punchFrames = { "punch6", "punch7", "punch8" };
        hitboxesLayers("punch", punchFrames, transform.GetChild(1).gameObject, transform.GetChild(2).gameObject);

        
        //sprites.color = new Color(0.4575472f, 1, 0.9307507f, 1);
    }





    void hitboxesLayers(string animName, string[] frameNames, GameObject leftHitBox, GameObject rightHitBox)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            int frameCount = 0;

            foreach (string frame in frameNames)
            {
                frameCount += 1;
            }

            switch (frameCount)
            {
                case 1:
                    if (sprites.sprite.name == frameNames[0])
                    {
                        if (sprites.flipX)
                        {
                            leftHitBox.layer = 10;
                        }
                        else
                        {
                            rightHitBox.layer = 10;
                        }
                    }
                    else
                    {
                        leftHitBox.layer = 13;
                        rightHitBox.layer = 13;
                    }
                    break;

                case 2:
                    if (sprites.sprite.name == frameNames[0] || sprites.sprite.name == frameNames[1])
                    {
                        if (sprites.flipX)
                        {
                            leftHitBox.layer = 10;
                        }
                        else
                        {
                            rightHitBox.layer = 10;
                        }
                    }
                    else
                    {
                        leftHitBox.layer = 13;
                        rightHitBox.layer = 13;
                    }
                    break;

                case 3:
                    if (sprites.sprite.name == frameNames[0] || sprites.sprite.name == frameNames[1] || sprites.sprite.name == frameNames[2])
                    {
                        if (sprites.flipX)
                        {
                            leftHitBox.layer = 10;
                        }
                        else
                        {
                            rightHitBox.layer = 10;
                        }
                    }
                    else
                    {
                        leftHitBox.layer = 13;
                        rightHitBox.layer = 13;
                    }
                    break;

                case 4:
                    if (sprites.sprite.name == frameNames[0] || sprites.sprite.name == frameNames[1] || sprites.sprite.name == frameNames[2]
                        || sprites.sprite.name == frameNames[3])
                    {
                        if (sprites.flipX)
                        {
                            leftHitBox.layer = 10;
                        }
                        else
                        {
                            rightHitBox.layer = 10;
                        }
                    }
                    else
                    {
                        leftHitBox.layer = 13;
                        rightHitBox.layer = 13;
                    }
                    break;

                case 5:
                    if (sprites.sprite.name == frameNames[0] || sprites.sprite.name == frameNames[1] || sprites.sprite.name == frameNames[2]
                        || sprites.sprite.name == frameNames[3] || sprites.sprite.name == frameNames[4])
                    {
                        if (sprites.flipX)
                        {
                            leftHitBox.layer = 10;
                        }
                        else
                        {
                            rightHitBox.layer = 10;
                        }
                    }
                    else
                    {
                        leftHitBox.layer = 13;
                        rightHitBox.layer = 13;
                    }
                    break;

                case 6:
                    if (sprites.sprite.name == frameNames[0] || sprites.sprite.name == frameNames[1] || sprites.sprite.name == frameNames[2]
                        || sprites.sprite.name == frameNames[3] || sprites.sprite.name == frameNames[4] || sprites.sprite.name == frameNames[5])
                    {
                        if (sprites.flipX)
                        {
                            leftHitBox.layer = 10;
                        }
                        else
                        {
                            rightHitBox.layer = 10;
                        }
                    }
                    else
                    {
                        leftHitBox.layer = 13;
                        rightHitBox.layer = 13;
                    }
                    break;

                default:
                    break;
            }
        }      
    }
}
