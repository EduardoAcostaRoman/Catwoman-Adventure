using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venusaurio : MonoBehaviour {

    private GameObject plantman;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator animator;
    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;
    private GameObject P1;

    void WhiteSprite()
    {
        sprite.material.shader = shaderGUItext;
        sprite.color = Color.white;
    }

    void NormalSprite()
    {
        sprite.material.shader = shaderSpritesDefault;
        sprite.color = Color.white;
    }

    void Start () {
        P1 = GameObject.Find("P1 position");
        plantman = GameObject.Find("Plantman");
        sprite = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        if (plantman.GetComponent<SpriteRenderer>().flipX == true)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
	}
	
	void Update () {
        if (P1.transform.localScale.x == 1)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            animator.StartPlayback();
        }
        else
        {
            animator.StopPlayback();
            if (sprite.sprite.name == "venusaur_104")
            {
                WhiteSprite();
            }
            if (sprite.sprite.name == "venusaur_0")
            {
                NormalSprite();
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("venusaur"))
            {
                if (sprite.flipX == true)
                {
                    body.velocity = new Vector2(10, 0);
                }
                else
                {
                    body.velocity = new Vector2(-10, 0);
                }
            }
            Destroy(this.gameObject, 20);
        }

            
        
	}
}
