using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantmanmusic : MonoBehaviour {

    private GameObject theme;
    private bool themeRst;

	void Start () {
		
	}
	
	
	void Update () {
        theme = GameObject.Find("theme");
        if (this.GetComponent<AudioSource>().isPlaying == false && themeRst == false)
        {
            theme.GetComponent<AudioSource>().Play();
            themeRst = true;
        }
	}
}
