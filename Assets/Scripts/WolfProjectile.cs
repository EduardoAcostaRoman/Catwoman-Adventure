using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfProjectile : MonoBehaviour {

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private GameObject wolf;
    private GameObject leftBody;
    private float destruction;

    void Start () {
        body = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        wolf = GameObject.Find("swordwolf");
        leftBody = this.transform.GetChild(0).gameObject;
        if (wolf.GetComponent<SpriteRenderer>().flipX == true)
        {
            sprite.flipX = true;
            leftBody.layer = 10;
        }
        else
        {
            sprite.flipX = false;
            this.gameObject.layer = 10;
        }
    }
	
	void Update () {
        if (sprite.flipX == true)
        {
            body.velocity = new Vector2(-16, 0);
        }
        else
        {
            body.velocity = new Vector2(16, 0);
        }
        destruction += 0.1f;
        if (destruction >= 10)
        {
            Destroy(this.gameObject);
        }
    }
}
