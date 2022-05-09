using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormon : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer sprite;
    private GameObject P1;
    public LayerMask attack;
    public LayerMask player;
    public LayerMask super;
    private bool hit;
    private bool hit2;
    private bool hitsuper;
    private bool hurtReset;
    private bool boomRst;

    private void FixedUpdate()
    {
        hit = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), attack);
        hit2 = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), player);
        hitsuper = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), super);
    }

    void Start()
    {
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
        P1 = GameObject.Find("P1 position");
    }

    void Update()
    {
        if (this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite.name == "blank")
        {
            Destroy(this.gameObject);
        }
        if (P1.transform.localScale.x == 1)
        {
            animator.StartPlayback();
        }
        else
        {
            animator.StopPlayback();
            if (hit == true || hit2 == true || hitsuper == true)
            {
                animator.SetBool("hurt", true);
            }
            if (sprite.sprite.name == "Minomon_13")
            {
                sprite.color = new Color(0, 0, 0, 0);
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("boom", true);
            }
            if (sprite.color.a == 0 && boomRst == false)
            {
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                boomRst = true;
            }
            if (this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite.name == "blank")
            {
                Destroy(this.gameObject);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") || sprite.color.a == 0)
            {
                this.gameObject.layer = 13;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && hurtReset == false)
            {
                this.GetComponent<AudioSource>().Play();
                hurtReset = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                hurtReset = false;
            }
        }
    }
}
