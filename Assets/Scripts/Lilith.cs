using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Lilith : MonoBehaviour
{

    Rigidbody2D body;
    SpriteRenderer sprites;
    Animator animator;

    float realtime;
    float prevtime;
    float prevTimeStand;

    GameObject P1;

    bool startColorChange;

    bool posRst;

    bool batAtk;
    public int flyVelX = 10;

    int sideVelocity = 1;

    int teleportPos = 0;

    public int spinJumpX = 10;
    public int spinJumpY = 10;

    bool onGround;
    public LayerMask ground;

    public GameObject parasite;
    bool summonRst;

    int SpriteNumber(SpriteRenderer render, string spriteID) // METHOD THAT RETURNS THE ID NUMBER OF THE SPRITES OF A CERTAIN MOVE
    {
        int spriteNumber = 0;

        Int32.TryParse(render.sprite.name.Replace(spriteID, ""), out spriteNumber);

        return spriteNumber;
    }

    private void FixedUpdate()
    {
        onGround = Physics2D.IsTouchingLayers(GetComponent<BoxCollider2D>(), ground);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        sprites = GetComponent<SpriteRenderer>();
        P1 = GameObject.Find("P1 position");
    }

    
    void Update()
    {
        realtime = Time.fixedTime;

        float randomVal = UnityEngine.Random.value;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))  // all the commands to do while standing
        {
            if (P1.transform.position.x < transform.position.x)    // turn on the player´s direction
            {
                sprites.flipX = true;
                sideVelocity = -1;
            }
            else
            {
                sprites.flipX = false;
                sideVelocity = 1;
            }

            prevtime = realtime;
        }

        // BAT FLY MOVE //

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("batFly2"))  
        {
            body.gravityScale = 0;

            Physics2D.IgnoreLayerCollision(10, 4, true);

            // lilith is flying with a bat attack so box collider changes
            GetComponent<BoxCollider2D>().size = new Vector2(0.6382987f, 0.3738024f);  
            GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<BoxCollider2D>().offset.x, 0.5728371f);

            if (P1.transform.localScale.x != 1)  // so Kat's ultimate doesn't mess with the movements
            {
                if (Mathf.Abs(transform.position.x) >= 40 && !batAtk)
                {
                    body.velocity = new Vector2(0, body.velocity.y);
                    transform.position = new Vector3(transform.position.x, -1.6f, transform.position.z);
                    sprites.flipX = !sprites.flipX;
                    batAtk = true;
                }

                if (Mathf.Abs(transform.position.x) >= 45 && batAtk)
                {
                    animator.SetBool("batFly2", true);
                }

                if (!batAtk)
                {
                    body.velocity = new Vector2(sideVelocity * flyVelX, body.velocity.y);
                }
                else
                {
                    body.velocity = new Vector2(sideVelocity * -1 * flyVelX, body.velocity.y);
                }
            }
        }
        else
        {
            GetComponent<BoxCollider2D>().size = new Vector2(0.1976498f, 0.8221731f);
            GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<BoxCollider2D>().offset.x, 0.4494347f);
            batAtk = false;
            animator.SetBool("batFly2", false);
            Physics2D.IgnoreLayerCollision(10, 4, false);
        }

        // SPIN MOVE //

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("spin"))
        {
            if (SpriteNumber(sprites, "spin") >= 4)    // makes the jump to spin
            {
                body.velocity = new Vector2(spinJumpX * sideVelocity, spinJumpY);
            }

            if (!onGround)
            {
                animator.SetBool("spin2", true);
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("spin2"))   
        {
            transform.GetChild(0).gameObject.layer = 10;

            if (onGround)       // sets the landing spin animation while grounded
            {
                animator.SetBool("spin2", false);
                body.velocity = new Vector2(0, body.velocity.y);
            }
        }
        else
        {
            transform.GetChild(0).gameObject.layer = 13;
        }

        // SUMMON MOVE //

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("summon"))
        {
            if (sprites.sprite.name == "summon8" && !summonRst)    
            {
                Instantiate(parasite, new Vector3(P1.transform.position.x + 12,
                                                  transform.position.y - 3, -3.95f), 
                                                  parasite.transform.rotation);
                Instantiate(parasite, new Vector3(P1.transform.position.x - 12,
                                                  transform.position.y - 3, -3.95f),
                                                  parasite.transform.rotation);
                summonRst = true;
            }
        }
        else
        {
            summonRst = false;
        }

        // TELEPORT //

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("teleport"))  // invincible while teleporting
        {
            gameObject.layer = 13;
            body.velocity = new Vector2(0, 0);
        }

        if (sprites.sprite.name == "intangible" && posRst == false)   // teleport time
        {
            transform.position = new Vector3((teleportPos - 7) + (randomVal * 14), -7, transform.position.z);

            if (P1.transform.position.x < transform.position.x)    // turn on the player´s direction
            {
                sprites.flipX = true;
            }
            else
            {
                sprites.flipX = false;
            }

            posRst = true;
        }
        else if (sprites.sprite.name != "intangible")
        {
            posRst = false;
        }
    }
}
