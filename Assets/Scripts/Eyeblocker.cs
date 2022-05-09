using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeblocker : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite;

    private GameObject P1;

    public LayerMask attack;
    public LayerMask super;

    private bool hit;
    private bool hitsuper;
    private bool superRst;

    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;

    private bool blink;
    private float blinkVal;
    private int blinkStop;

    private float death;
    private float deathVal = 1;

    private float summonTime = 2f;
    private float summon;
    public GameObject cam;
    public GameObject mutants;

    bool activate;

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
        hit = Physics2D.IsTouchingLayers(transform.GetChild(2).GetComponent<BoxCollider2D>(), attack);
        hitsuper = Physics2D.IsTouchingLayers(transform.GetChild(2).GetComponent<BoxCollider2D>(), super);
    }

    void Start()
    {
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
        P1 = GameObject.Find("P1 position");
    }


    void Update()
    {
        if (P1.transform.localScale.x == 1)
        {
            sprite.color = new Color(255, 255, 255, 255);
            animator.StartPlayback();
        }
        else
        {
            animator.StopPlayback();

            if (P1.transform.position.x >= transform.position.x - 11.21f && 
                cam.GetComponent<CameraLevel2_2>().camLock)
            {
                activate = true;
            }

            if (activate && !animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                if (summon < summonTime)
                {
                    summon += 0.05f;
                }
                else
                {
                    if (Random.value > 0.5f)
                    {
                        Instantiate(mutants, new Vector3(transform.position.x + 4, 0f, -0.4f), transform.rotation);
                    }
                    else
                    {
                        Instantiate(mutants, new Vector3(transform.position.x - 25f, 0f, -0.4f), transform.rotation);
                    }
                    summon = 0;
                }
            }

            if (hit && blink == false && activate && !animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                //this.transform.GetChild(0).GetComponent<AudioSource>().Play();
                animator.SetBool("hurt", true);
                death += 1f;
            }

            if ((hit == true || blink == true) && !animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
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
                animator.SetBool("hurt", false);
            }

            if (death >= deathVal)
            {
                animator.SetBool("death", true);
                if (sprite.sprite.name == "death4")
                {
                    transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
                }
                if (sprite.sprite.name == "blank")
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
