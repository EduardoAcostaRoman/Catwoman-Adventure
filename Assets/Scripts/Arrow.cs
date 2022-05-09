using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private bool audioRst;

	void Start () {
		
	}
	
	void Update () {
        if (this.transform.position.z == -9)
        {
            if (this.GetComponent<SpriteRenderer>().sprite.name == "flecha_1" && audioRst == false)
            {
                this.GetComponent<AudioSource>().Play();
                audioRst = true;
            }
            else if (this.GetComponent<SpriteRenderer>().sprite.name != "flecha_1")
            {
                audioRst = false;
            }
        }
	}
}
