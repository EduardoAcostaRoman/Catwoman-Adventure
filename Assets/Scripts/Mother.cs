using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour {

    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;
    private Animator animator;
    private SpriteRenderer sprite;
    private GameObject P1;
    public GameObject mutants;
    public LayerMask attack;
    public LayerMask super;
    private bool hurtReset;
    private bool hurtReset2;
    private int death;
    private bool hit;
    private bool hitsuper;
    private bool atkRst;
    private bool boomRst;
    public float summonTime;
    private float summon;
    public GameObject cam;

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

    void Start () {
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
        P1 = GameObject.Find("P1 position");
    }
	
	void Update () {
        if (P1.transform.localScale.x == 1)
        {
            animator.StartPlayback();
        }
        else
        {
            animator.StopPlayback();
            if (cam.GetComponent<CameraLevel2_1>().camLock == true && !animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                if (summon < summonTime)
                {
                    summon += 0.05f;
                }
                else
                {
                    if (Random.value < 0.3333f)
                    {
                        Instantiate(mutants, new Vector3(224.66f, -4.23f, -0.4f), transform.rotation);
                    }
                    else if (Random.value >= 0.3333f && Random.value < 0.6666f)
                    {
                        Instantiate(mutants, new Vector3(203.91f, 9f, -0.4f), transform.rotation);
                    }
                    else if (Random.value >= 0.6666f)
                    {
                        Instantiate(mutants, new Vector3(194.38f, -4.23f, -0.4f), transform.rotation);
                    }
                    summon = 0;
                }
            }            
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                if (hit == true)
                {
                    animator.SetBool("hurt", true);
                }
                if (hitsuper == true)
                {
                    animator.SetBool("hurt", true);
                }
            }
            if (death >= 15)
            {
                animator.SetBool("death", true);
            }
            if (hit && hurtReset == false)
            {
                //this.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                //this.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();
                this.GetComponent<AudioSource>().Play();
                death += 1;
                hurtReset = true;
            }
            else if (hit == false)
            {
                hurtReset = false;
            }
            if (hitsuper && hurtReset2 == false)
            {
                //this.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Stop();
                //this.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();
                //this.GetComponent<AudioSource>().Play();
                death += 4;
                hurtReset2 = true;
            }
            else if (hitsuper == false)
            {
                hurtReset2 = false;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                animator.SetBool("hurt", false);
                if (sprite.sprite.name == "strange shit_1")
                {
                    whiteSprite();
                }
                else
                {
                    normalSprite();
                }
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                this.gameObject.layer = 13;
                transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = true;
                if (sprite.sprite.name != "blank")
                {
                    // freezes the player //
                    P1.transform.localScale = new Vector3(P1.transform.localScale.x, 1, P1.transform.localScale.z);
                }      
                if (sprite.sprite.name == "strange shit_0" || transform.localScale.x < 5)
                {
                    whiteSprite();
                }
                else
                {
                    normalSprite();
                }
            }
            if (sprite.sprite.name == "blank")
            {
                // unfreezes the player //
                P1.transform.localScale = new Vector3(P1.transform.localScale.x, 0, P1.transform.localScale.z);
                Destroy(gameObject);
            }
        }
     }
}
