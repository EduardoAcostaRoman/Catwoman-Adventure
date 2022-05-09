using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Transform transformer;
    private GameObject P1;

    private bool hit;
    private bool hitsuper;

    public LayerMask attack;
    public LayerMask super;

    private int rst;

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
        if ((hit || hitsuper) && rst == 0)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("hit", true);
            transform.GetChild(0).GetComponent<AudioSource>().Play();
            rst = 1;
        }

        if (transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("damage"))
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("hit", false);
        }

        if (!transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("damage") && rst == 1)
        {
            rst = 0;
        }
    }
}
