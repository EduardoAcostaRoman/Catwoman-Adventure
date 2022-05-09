using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBall : MonoBehaviour {

    private GameObject plantman;
    private Rigidbody2D body;
	
	void Start () {
        plantman = GameObject.Find("Plantman");
        body = this.GetComponent<Rigidbody2D>();
		if (plantman.GetComponent<SpriteRenderer>().flipX == true)
        {
            body.velocity = new Vector2(-20, 0);
        }
        else
        {
            body.velocity = new Vector2(20, 0);
        }
	}
	
	
	void Update () {
        Destroy(this.gameObject, 2);
	}
}
