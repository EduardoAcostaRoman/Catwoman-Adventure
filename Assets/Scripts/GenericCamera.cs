using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericCamera : MonoBehaviour {

    private GameObject deathUI;
    private float deathUImoveY;
    private GameObject fader;
    private GameObject Kat;
    private GameObject P1;
    private bool noMoreChoosing;

    public GameObject harpy;
    public GameObject fishgirl;
    public GameObject wormon;
    public GameObject spike;
    public GameObject swordwolf;
    public GameObject plantman;
    public GameObject doberman;
    public GameObject dragon;
    public GameObject mutant;
    public GameObject witch;



    void loadLevel(int level, KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            SceneManager.LoadScene(level);
        }
    }

    void loadMob(GameObject mob, KeyCode key)
    {
        if(Input.GetKeyDown(key))
        {
            Instantiate(mob, new Vector3(P1.transform.position.x + 7,
                                     P1.transform.position.y + 2, 
                                     mob.transform.position.z),
                                     transform.rotation);
        }        
    }

    void Start () {
		
	}
	
	void Update ()
    {
        fader = GameObject.Find("Image");
        Kat = GameObject.Find("Catwoman");
        deathUI = GameObject.Find("deathUI");

        P1 = GameObject.Find("P1 position");
        if (P1.transform.localScale.x == 1)
        {
            GetComponent<Camera>().orthographicSize = 4;
            transform.position = new Vector3(P1.transform.position.x, P1.transform.position.y + 1, transform.position.z);
            //transform.GetChild(9).GetComponent<Animator>().SetBool("super", true);
        }
        else
        {
            GetComponent<Camera>().orthographicSize = 7.531403f;
            //transform.GetChild(9).GetComponent<Animator>().SetBool("super", false);
        }

        loadLevel(2, KeyCode.Alpha1);
        loadLevel(3, KeyCode.Alpha2);
        loadLevel(4, KeyCode.Alpha3);
        loadLevel(5, KeyCode.Alpha4);
        loadLevel(6, KeyCode.Alpha5);
        loadLevel(7, KeyCode.Alpha6);
        loadLevel(8, KeyCode.Alpha7);
        loadLevel(9, KeyCode.Alpha8);
        loadLevel(10, KeyCode.Alpha9);

        //loadMob(harpy, KeyCode.R);
        //loadMob(fishgirl, KeyCode.T);
        //loadMob(wormon, KeyCode.Y);
        //loadMob(spike, KeyCode.F);
        //loadMob(swordwolf, KeyCode.Z);
        //loadMob(plantman, KeyCode.X);
        //loadMob(doberman, KeyCode.G);
        //loadMob(dragon, KeyCode.H);
        //loadMob(mutant, KeyCode.M);
        //loadMob(witch, KeyCode.C);


        if (noMoreChoosing == false)
        {
            if (Kat.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("death") && deathUI.transform.position.y < this.transform.position.y - 0.5f)
            {
                deathUImoveY += 0.001f;
                deathUI.transform.position = new Vector3(deathUI.transform.position.x, deathUI.transform.position.y + deathUImoveY, deathUI.transform.position.z);
            }
            else if (deathUI.transform.position.y >= this.transform.position.y - 0.5f)
            {
                deathUI.transform.position = new Vector3(deathUI.transform.position.x, this.transform.position.y - 0.5f, deathUI.transform.position.z);
                if (deathUI.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("restart2"))
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        deathUI.transform.GetChild(0).GetComponent<Animator>().SetBool("restart", false);
                        deathUI.transform.GetChild(1).GetComponent<Animator>().SetBool("quit", true);
                    }
                    if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
                    {
                        fader.GetComponent<Animator>().SetBool("fadeOUT", true);
                        noMoreChoosing = true;
                    }
                }
                else if (deathUI.transform.GetChild(1).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("quit2"))
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        deathUI.transform.GetChild(1).GetComponent<Animator>().SetBool("quit", false);
                        deathUI.transform.GetChild(0).GetComponent<Animator>().SetBool("restart", true);
                    }
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        deathUI.transform.GetChild(1).GetComponent<Animator>().SetBool("quit", false);
                        deathUI.transform.GetChild(2).GetComponent<Animator>().SetBool("end", true);
                    }
                }
                else if (deathUI.transform.GetChild(2).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("end2"))
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        deathUI.transform.GetChild(2).GetComponent<Animator>().SetBool("end", false);
                        deathUI.transform.GetChild(1).GetComponent<Animator>().SetBool("quit", true);
                    }
                    if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
                    {
                        Application.Quit();
                        noMoreChoosing = true;
                    }
                }
            }
        }
    }
}
