using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLevel1FinalBoss : MonoBehaviour {

    private GameObject plantMan;
    private Transform transformer;
    private GameObject P1;
    private GameObject fader;
    private bool sceneChange;
    private GameObject sceneChanger;
    public LayerMask player;
    private GameObject Kat;
    private GameObject Music;
    private float camX;
    private float camY;
    private float angle;
    private float minX = -4.5f, minY = -2f, maxX = 4.5f, maxY = 2f;
    private float posX, posY;
    private Vector2 velocity;
    private bool camRst;
    private bool camRst2;

    private void FixedUpdate()
    {
        transformer = this.GetComponent<Transform>();
        P1 = GameObject.Find("P1 position");
        //sceneChanger = GameObject.Find("entrance");
        //sceneChange = Physics2D.IsTouchingLayers(sceneChanger.GetComponent<BoxCollider2D>(), player);
        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("stand") && camRst2 == true)
        {            
            posX = Mathf.SmoothDamp(this.transform.position.x, P1.transform.position.x, ref velocity.x, 0.15f);
            posY = Mathf.SmoothDamp(this.transform.position.y, P1.transform.position.y, ref velocity.y, 0.15f);
            this.transform.position = new Vector3(
                Mathf.Clamp(posX, minX, maxX),
                Mathf.Clamp(posY, minY, maxY),
                this.transform.position.z);
        }
    }

    void CameraShake(bool activate)
    {
        angle = 360 * Random.value;
        camX = 0.25f * Mathf.Cos(angle);
        camY = 0.25f * Mathf.Sin(angle);
        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("stand"))
        {
            if (activate == true)
            {
                this.transform.position = new Vector3(
                    this.transform.position.x + camX,
                    this.transform.position.y + camY,
                    -10);
            }
            else
            {
                this.transform.position = new Vector3(
                    this.transform.position.x,
                    this.transform.position.y,
                    -10);
            }
        }
    }

    void Start () {
        fader = GameObject.Find("Image");
        Kat = GameObject.Find("Catwoman");
        Music = GameObject.Find("Music entrance");
        this.GetComponent<Animator>().SetBool("finalBoss", true);
    }
	
	void Update () {
        if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f && !Kat.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            SceneManager.LoadScene(6);
        }
        else if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f)
        {
            SceneManager.LoadScene(5);
        }
        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("level1FinalBoss"))
        {
            this.GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal;
            this.GetComponent<Animator>().SetBool("finalBoss", false);
            this.GetComponent<Animator>().enabled = true;
            camRst = true;
        }
        else if (camRst == true)
        {
            this.GetComponent<Animator>().updateMode = AnimatorUpdateMode.AnimatePhysics;
            this.GetComponent<Animator>().enabled = false;
            if (camRst2 == false)
            {
                this.transform.position = new Vector3(
                -4.5f,
                -2,
                this.transform.position.z);
                camRst2 = true;
            }
        }
        if (GameObject.Find("Plantman") == true)
        {
            plantMan = GameObject.Find("Plantman");
            if (plantMan.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("groundSummon") && (plantMan.GetComponent<SpriteRenderer>().sprite.name == "plant man_119" || plantMan.GetComponent<SpriteRenderer>().sprite.name == "wolf boss_120"))
            {
                CameraShake(true);
            }
            else if (plantMan.GetComponent<SpriteRenderer>().sprite.name == "plant man_234" || plantMan.GetComponent<SpriteRenderer>().sprite.name == "wolf boss_235")
            {
                CameraShake(true);
            }
            else if (plantMan.GetComponent<SpriteRenderer>().sprite.name == "plant man_325")
            {
                CameraShake(true);
            }
            else
            {
                CameraShake(false);
            }
            if (plantMan.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("start"))
            {
                P1.transform.localScale = new Vector3(P1.transform.localScale.x, 1, P1.transform.localScale.z);
            }
            else
            {
                P1.transform.localScale = new Vector3(P1.transform.localScale.x, 0, P1.transform.localScale.z);
            }
        }
        else
        {
            CameraShake(false);
        }
    }
}
