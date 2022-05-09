using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monolito : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D body;
    public LayerMask ground;
    private Transform transformer;
    private GameObject P1;
    private GameObject damage;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        transformer = this.GetComponent<Transform>();
        P1 = GameObject.Find("P1 position");
    }

    void Update()
    {
        damage = GameObject.Find("AttackMono");
        if (P1.transform.localScale.x == 1)
        {
            body.velocity = new Vector2(0, 0);
            body.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.StartPlayback();
        }
        else
        {
            animator.StopPlayback();
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
            {
                body.velocity = new Vector2(0, 0);
                body.constraints = RigidbodyConstraints2D.FreezeAll;
                if (P1.GetComponent<Transform>().position.y < transformer.position.y - 2.55f)
                {
                    if ((P1.GetComponent<Transform>().position.x > transformer.position.x - 1) && (P1.GetComponent<Transform>().position.x < transformer.position.x + 1))
                    {
                        animator.SetBool("fall", true);
                    }
                }
            }
            else
            {
                body.constraints = RigidbodyConstraints2D.None;
                body.constraints = RigidbodyConstraints2D.FreezePositionX;
                body.freezeRotation = true;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("fall"))
            {
                damage.layer = 10;
                body.velocity = new Vector2(0, -10);
                animator.SetBool("fall", false);
            }
            else
            {
                damage.layer = 13;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("rise"))
            {
                if (body.velocity.y < 3)
                {
                    body.velocity = new Vector2(0, 5.5f);
                }
                else
                {
                    body.velocity = new Vector2(0, 3);
                }
            }
        }
    }
}
