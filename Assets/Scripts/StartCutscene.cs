using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCutscene : MonoBehaviour {

    private GameObject fader;
    private GameObject miko;
    private GameObject kat;
    private GameObject mia;
    private GameObject kat2;
    private GameObject kat3;
    private GameObject mia2;
    private GameObject mia3;
    private GameObject smoke;
    private GameObject song;
    private GameObject meteor;
    private GameObject trouble;
    private GameObject flames;
    private GameObject duck;
    private GameObject eruption;
    private bool katRst;
    //private bool miaRst;

    void Update()
    {
        fader = GameObject.Find("Image");
        miko = GameObject.Find("Miko cat");
        kat = GameObject.Find("kat");
        kat2 = GameObject.Find("kat2");
        kat3 = GameObject.Find("kat3");
        mia = GameObject.Find("Mia");
        mia2 = GameObject.Find("Mia2");
        mia3 = GameObject.Find("Mia3");
        meteor = GameObject.Find("firemon projectile");
        trouble = GameObject.Find("urban landscape");
        flames = GameObject.Find("background");
        smoke = GameObject.Find("dust");
        duck = GameObject.Find("FireDuck");
        eruption = GameObject.Find("Eruption");

        if (miko.GetComponent<SpriteRenderer>().sprite.name == "Miko (kat's cat)_6")
        {
            miko.GetComponent<AudioSource>().Play();
        }
        if (kat.GetComponent<SpriteRenderer>().sprite.name == "crounch tail 1")
        {
            kat.GetComponent<AudioSource>().Play();
        }
        if (mia.GetComponent<SpriteRenderer>().sprite.name == "Kat family_8" && katRst == false)
        {
            kat3.GetComponent<AudioSource>().Play();
            katRst = true;
        }
        if (mia.GetComponent<SpriteRenderer>().sprite.name == "Kat family_39")
        {
            mia3.GetComponent<AudioSource>().Play();
        }
        if (mia.GetComponent<SpriteRenderer>().sprite.name == "Kat family_42")
        {
            mia.GetComponent<AudioSource>().Play();
        }
        if (mia.GetComponent<SpriteRenderer>().sprite.name == "Kat family_14")
        {
            mia2.GetComponent<AudioSource>().Play();
        }
        if (trouble.GetComponent<SpriteRenderer>().sprite.name == "City House_2")
        {
            flames.GetComponent<AudioSource>().Play();
        }
        if (meteor.GetComponent<Transform>().position.z > 0 && meteor.GetComponent<Transform>().position.z <= 1f)
        {
            this.GetComponent<AudioSource>().Stop();
            meteor.GetComponent<AudioSource>().Play();
            trouble.GetComponent<AudioSource>().Play();
            kat2.GetComponent<AudioSource>().Play();
        }
        if (smoke.GetComponent<SpriteRenderer>().sprite.name == "1")
        {
            smoke.GetComponent<AudioSource>().Play();
        }
        if (Input.anyKeyDown)
        {
            fader.GetComponent<Animator>().SetBool("fadeOUT", true);
        }
        if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f)
        {
            SceneManager.LoadScene(2);
        }
        // if (duck.GetComponent<SpriteRenderer>().sprite.name == "throw6")
        // {
        //     meteor.GetComponent<Animator>().SetBool("fire", true);
        // }
        // if (meteor.GetComponent<Transform>().position.x == 26.43f)
        // {
        //     eruption.GetComponent<Animator>().SetBool("boom", true);
        // }
    }
}
