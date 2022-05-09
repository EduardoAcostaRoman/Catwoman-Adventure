using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMonster : MonoBehaviour
{

    Rigidbody2D body;
    SpriteRenderer sprites;
    Animator animator;
    GameObject P1;

    public GameObject KKRool;
    bool atkRst;
    int atkCount;

    float random;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        sprites = this.GetComponent<SpriteRenderer>();
        P1 = GameObject.Find("P1 position");
    }


    void Update()
    {
        random = Random.value;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            if (sprites.sprite.name == "frame-3" && !atkRst)
            {      
                atkCount += 1;
                atkRst = true;
            }
            else if (sprites.sprite.name != "frame-3")
            {
                atkRst = false;
            }

            if (atkCount >= 5)
            {
                if (random < 0.333)
                {
                    Instantiate(KKRool,
                            new Vector3(transform.position.x + 12, transform.position.y + 2, (random * 0.2f) - 1),
                            KKRool.transform.rotation);
                }
                else if (random < 0.666)
                {
                    Instantiate(KKRool,
                            new Vector3(transform.position.x + 12, transform.position.y + 5, (random * 0.2f) - 1),
                            KKRool.transform.rotation);
                }
                else
                {
                    Instantiate(KKRool,
                            new Vector3(transform.position.x + 12, transform.position.y + 8, (random * 0.2f) - 1),
                            KKRool.transform.rotation);
                }
               
                atkCount = 0;
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            transform.GetChild(0).gameObject.layer = 13;
        }
    }
}
