using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralDragon : MonoBehaviour {

    private GameObject P1;
    private Animator animator;
    private Rigidbody2D body;
    public LayerMask playerFeet;
    private bool move;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        move = true;
        if (collision.gameObject.tag == "kat")
        {
            move = true;
        }
    }

    void Update()
    {
        P1 = GameObject.Find("P1 position");
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        if (P1.transform.localScale.x == 1)
        {
            body.velocity = new Vector2(0, 0);
            animator.StartPlayback();
        }
        else
        {
            animator.StopPlayback();
            if (move == true && this.transform.position.x < 187)
            {
                body.velocity = new Vector2(5, 0);
            }
            else
            {
                body.velocity = new Vector2(0, 0);
            }
        }
    }
}
