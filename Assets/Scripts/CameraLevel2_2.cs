using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLevel2_2 : MonoBehaviour
{
    private GameObject P1;
    private GameObject fader;
    private bool sceneChange;
    private GameObject sceneChanger;
    public LayerMask player;
    private GameObject Kat;
    private float minX = 9.92f, minY = -1.8f, maxX = 275.86f, maxY = -1.31f;
    private float posX, posY;
    private Vector2 velocity;
    private GameObject theWall;
    public bool camLock;

    private GameObject eyeblocker1;
    bool rst1;

    private GameObject eyeblocker2;
    bool rst2;

    private GameObject eyeblocker3;
    bool rst3;

    GameObject eyeblockFake;

    public GameObject backGround;

    void camLockEyeblocker(GameObject eyeblocker)
    {
        if (transform.position.x >= eyeblocker.transform.position.x - 11.21f && 
            eyeblocker.GetComponent<SpriteRenderer>().sprite.name != "death5" && 
            eyeblocker.GetComponent<SpriteRenderer>().sprite.name != "blank")
        {
            camLock = true;
        }
        else if (eyeblocker.GetComponent<SpriteRenderer>().sprite.name == "death5" || 
                 eyeblocker.GetComponent<SpriteRenderer>().sprite.name == "blank")
        {
            camLock = false;
        }

        if (camLock)
        {
            posX = Mathf.SmoothDamp(this.transform.position.x, P1.transform.position.x, ref velocity.x, 0.15f);
            posY = Mathf.SmoothDamp(this.transform.position.y, P1.transform.position.y, ref velocity.y, 0.15f);
            this.transform.position = new Vector3(
            eyeblocker.transform.position.x - 11,
            Mathf.Clamp(posY, minY, maxY),
            this.transform.position.z);
            theWall.GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if (!camLock)
        {
            posX = Mathf.SmoothDamp(this.transform.position.x, P1.transform.position.x, ref velocity.x, 0.15f);
            posY = Mathf.SmoothDamp(this.transform.position.y, P1.transform.position.y, ref velocity.y, 0.15f);
            this.transform.position = new Vector3(
                Mathf.Clamp(posX, minX, maxX),
                Mathf.Clamp(posY, minY, maxY),
                this.transform.position.z);
            theWall.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void FixedUpdate()
    {
        P1 = GameObject.Find("P1 position");
        eyeblockFake = GameObject.Find("levelend");
        sceneChanger = GameObject.Find("wayout");
        sceneChange = Physics2D.IsTouchingLayers(sceneChanger.GetComponent<BoxCollider2D>(), player);
        fader = GameObject.Find("Image");
        Kat = GameObject.Find("Catwoman");
        theWall = transform.GetChild(12).gameObject;

        if (GameObject.Find("Eyeblocker"))
        {
            eyeblocker1 = GameObject.Find("Eyeblocker");
        }
        else
        {
            eyeblocker1 = eyeblockFake;
        }
        if (GameObject.Find("Eyeblocker 2"))
        {
            eyeblocker2 = GameObject.Find("Eyeblocker 2");
        }
        else
        {
            eyeblocker2 = GameObject.Find("levelend 2");
        }
        if (GameObject.Find("Eyeblocker 3"))
        {
            eyeblocker3 = GameObject.Find("Eyeblocker 3");
        }
        else
        {
            eyeblocker3 = GameObject.Find("levelend 3");
        }


        if (eyeblocker1.GetComponent<SpriteRenderer>().sprite.name != "blank")
        {
            camLockEyeblocker(eyeblocker1);
        }
        else if (eyeblocker2.GetComponent<SpriteRenderer>().sprite.name != "blank")
        {
            camLockEyeblocker(eyeblocker2);
        }
        else if (eyeblocker3.GetComponent<SpriteRenderer>().sprite.name != "blank")
        {
            camLockEyeblocker(eyeblocker3);
        }
        else
        {
            posX = Mathf.SmoothDamp(transform.position.x, P1.transform.position.x, ref velocity.x, 0.15f);
            posY = Mathf.SmoothDamp(transform.position.y, P1.transform.position.y, ref velocity.y, 0.15f);
            this.transform.position = new Vector3(
                Mathf.Clamp(posX, minX, maxX),
                Mathf.Clamp(posY, minY, maxY),
                transform.position.z);
            theWall.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        
    }

    void Start()
    {
        //---------------- creating a repeating background ----------------//

        for (int i = 1; i < 8; i++)
        {
            Instantiate(backGround, new Vector3((i * 51.1f) + 15.9f, -1.5f, 2), new Quaternion(0,0,0,1));

        }
    }

    void Update()
    {
        if (sceneChange == true)
        {
            fader.GetComponent<Animator>().SetBool("fadeOUT", true);
        }
        if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f && !Kat.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            SceneManager.LoadScene(9);
        }
        else if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f)
        {
            SceneManager.LoadScene(8);
        }

        
    }
}