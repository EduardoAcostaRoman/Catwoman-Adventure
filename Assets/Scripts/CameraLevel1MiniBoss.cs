using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLevel1MiniBoss : MonoBehaviour {

    private GameObject P1;
    private GameObject fader;
    private bool sceneChange;
    private GameObject sceneChanger;
    public LayerMask player;
    private GameObject Kat;
    private GameObject wolf;
    public GameObject rightWall;
    public GameObject arrow;
    private float camX;
    private float camY;
    private float angle;
    private float minX = -1221.7f, minY = -114.5f, maxX = -1214.7f, maxY = -110.4f;
    private float posX, posY;
    private Vector2 velocity;
    private bool camRst;
    private bool camRst2;

    private void FixedUpdate()
    {
        P1 = GameObject.Find("P1 position");
        sceneChanger = GameObject.Find("entrance");
        sceneChange = Physics2D.IsTouchingLayers(sceneChanger.GetComponent<BoxCollider2D>(), player);
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

    void Start() {
        this.GetComponent<Animator>().SetBool("miniBoss", true);
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
	
	void Update () {      
        fader = GameObject.Find("Image");
        Kat = GameObject.Find("Catwoman");
        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("level1MiniBoss"))
        {
            this.GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal;
            this.GetComponent<Animator>().SetBool("miniBoss", false);
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
                minX,
                minY,
                this.transform.position.z);
                camRst2 = true;
            }
        }
        if (sceneChange == true)
        {
            fader.GetComponent<Animator>().SetBool("fadeOUT", true);
        }
        if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f && !Kat.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            SceneManager.LoadScene(4);
        }
        else if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f)
        {
            SceneManager.LoadScene(3);
        }
        
        if (GameObject.Find("swordwolf") == true)
        {
            wolf = GameObject.Find("swordwolf");
            if (wolf.GetComponent<SpriteRenderer>().sprite.name == "wolf boss_13" || wolf.GetComponent<SpriteRenderer>().sprite.name == "wolf boss_14")
            {
                CameraShake(true);
            }
            else
            {
                CameraShake(false);
            }
            if (wolf.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite.name == "blank2")
            {
                arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, -9);
                Destroy(rightWall);
            }
            if (wolf.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("start"))
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
