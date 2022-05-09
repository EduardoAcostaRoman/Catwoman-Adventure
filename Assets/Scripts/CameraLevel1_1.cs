using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraLevel1_1 : MonoBehaviour {

    private GameObject P1;
    private GameObject fader;
    private bool sceneChange;
    private GameObject sceneChanger;
    public LayerMask player;
    private GameObject Mia;
    private GameObject Kat;
    private bool faderRst;
    private float startTime = 0.01f;
    private GameObject P1status;
    private float minX = -1207.94f, minY = -116.4555f, maxX = -922.03f, maxY = -107.532f;
    private float posX, posY;
    private Vector2 velocity;

    private void FixedUpdate()
    {
        P1 = GameObject.Find("P1 position");
        sceneChanger = GameObject.Find("entrance");
        sceneChange = Physics2D.IsTouchingLayers(sceneChanger.GetComponent<BoxCollider2D>(), player);
        posX = Mathf.SmoothDamp(this.transform.position.x, P1.transform.position.x, ref velocity.x, 0.15f);
        posY = Mathf.SmoothDamp(this.transform.position.y, P1.transform.position.y, ref velocity.y, 0.15f);
        this.transform.position = new Vector3(
            Mathf.Clamp(posX, minX, maxX),
            Mathf.Clamp(posY, minY, maxY),
            this.transform.position.z);
    }

    void Start()
    {
        
    }


    void Update()
    {
        fader = GameObject.Find("Image");
        Mia = GameObject.Find("Mia");
        Kat = GameObject.Find("Catwoman");
        P1status = GameObject.Find("lifeManaMemory");
        DontDestroyOnLoad(P1status);
        if (sceneChange == true)
        {
            fader.GetComponent<Animator>().SetBool("fadeOUT", true);
        }
        if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f && !Kat.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            SceneManager.LoadScene(3);
        }
        else if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f)
        {
            SceneManager.LoadScene(2);
        }
        if (startTime >= 1.1f)
        {            
            Mia.transform.GetChild(0).GetComponent<Animator>().SetBool("dust", true);
            Mia.transform.GetChild(0).GetComponent<AudioSource>().Play();
            startTime = 0;
        }
        else if (startTime != 0)
        {
            startTime += 0.02f;
        }
        if (Mia.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "4")
        {
            Mia.GetComponent<Animator>().SetBool("start", true);
            Kat.transform.position = new Vector3(Mia.transform.position.x + 1.77f, Mia.transform.position.y, -1);
        }
        if (Mia.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("New State 0"))
        {
            P1.transform.localScale = new Vector3(P1.transform.localScale.x, 0, P1.transform.localScale.z);
        }
        else
        {
            P1.transform.localScale = new Vector3(P1.transform.localScale.x, 1, P1.transform.localScale.z);
        }
        if (Mia.GetComponent<SpriteRenderer>().sprite.name == "Kat family_25" && faderRst == false)
        {
            Mia.GetComponent<AudioSource>().Play();
            faderRst = true;
        }
        if (Mia.GetComponent<SpriteRenderer>().sprite.name == "Kat family_22" && faderRst == true)
        {
            Mia.transform.GetChild(1).GetComponent<AudioSource>().Play();
            faderRst = false;
        }
    }
}
