using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    public LayerMask player;
    private bool hit;

    private void FixedUpdate()
    {
        hit = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), player);
    }

    void Start () {
		
	}
	
	
	void Update () {
		if (hit == true)
        {
            Destroy(this.gameObject);
        }
	}
}
