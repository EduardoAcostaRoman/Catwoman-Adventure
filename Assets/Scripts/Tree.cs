using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    private GameObject P1;
    private Animator animator;

    void Start () {
        P1 = GameObject.Find("P1 position");

    }
	
	
	void Update () {
        animator = this.GetComponent<Animator>();
        Destroy(this.gameObject, 1);

	}
}
