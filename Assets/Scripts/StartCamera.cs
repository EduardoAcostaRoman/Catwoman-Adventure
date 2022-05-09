using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCamera : MonoBehaviour {

    private GameObject fader;
    private bool rst;
	
	void Update () {
        fader = GameObject.Find("Image");
        if (Input.anyKeyDown && rst == false)
        {
            this.GetComponent<AudioSource>().Stop();
            fader.GetComponent<AudioSource>().Play();
            fader.GetComponent<Animator>().SetBool("fadeOUT", true);
            rst = true;
        }
        if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f)
        {
            SceneManager.LoadScene(1);
        }
    }
}
