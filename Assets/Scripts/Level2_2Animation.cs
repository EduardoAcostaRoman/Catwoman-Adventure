using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_2Animation : MonoBehaviour
{
    Rigidbody2D body;

    public GameObject phoenixProjectile;

    float realtime;
    float prevtime;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        body.velocity = new Vector2(3, 0);

        // -------------- BACKGROUND CUTSCENE -------------- //

        realtime = Time.fixedTime;

        if (realtime - prevtime >= 3)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("atk", true);
            transform.GetChild(1).GetComponent<Animator>().SetBool("atk", true);

            Instantiate(phoenixProjectile,
                        transform.GetChild(2).transform.position,
                        transform.GetChild(2).transform.rotation);

            prevtime = realtime;
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("atk", false);
            transform.GetChild(1).GetComponent<Animator>().SetBool("atk", false);
        }

        if (transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "Kat family_43")
        {
            transform.GetChild(3).GetComponent<Animator>().SetBool("boom", true);
        }
        else
        {
            transform.GetChild(3).GetComponent<Animator>().SetBool("boom", false);
        }
    }
}
