using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalPsychoBall : MonoBehaviour
{

    Rigidbody2D body;
    SpriteRenderer sprites;
    Animator animator;

    float realtime;
    float prevtime;

    GameObject P1;

    float velX;
    float velY;

    public float velocityX = 0.05f;
    public float velocityY = 0.05f;

    public float destroyTime = 10;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        sprites = this.GetComponent<SpriteRenderer>();
        P1 = GameObject.Find("P1 position");

    }

    void Update()
    {
        float scaleXY = transform.localScale.x + 0.05f;

        realtime = Time.fixedTime;

 
        if (transform.localScale.x >= 5)
        {
            transform.localScale = new Vector2(5, 5);

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

            if (realtime - prevtime >= destroyTime)
            {
                animator.SetBool("destroy", true);
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            }
        }
        else
        {
            transform.localScale = new Vector2(scaleXY, scaleXY);
            prevtime = realtime;
        }

        if (animator.GetBool("destroy"))
        {
            if (realtime - prevtime >= destroyTime + 0.5f)
            {                
                transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            }
            gameObject.layer = 13;
            body.velocity = new Vector2(0,0);
            Destroy(gameObject, 5);
        }
    }
}
