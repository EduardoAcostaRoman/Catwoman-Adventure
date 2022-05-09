using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLevel2_1 : MonoBehaviour {

    private GameObject P1;
    private GameObject fader;
    private bool sceneChange;
    private GameObject sceneChanger;
    public LayerMask player;

    private GameObject Kat;
    private float minX = 10.1f, minY = -1.415f, maxX = 258.5f, maxY = 1.42f;
    private float posX, posY;
    private Vector2 velocity;
    public GameObject theWall;
    public bool camLock;

    private void FixedUpdate()
    {
        P1 = GameObject.Find("P1 position");
        sceneChanger = GameObject.Find("levelend");
        sceneChange = Physics2D.IsTouchingLayers(sceneChanger.GetComponent<BoxCollider2D>(), player);
        fader = GameObject.Find("Image");
        Kat = GameObject.Find("Catwoman");
        
        if (transform.position.x >= 212.3f && GameObject.Find("Mother") == true)
        {
            camLock = true;
            if (GameObject.Find("Mother").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                posX = Mathf.SmoothDamp(this.transform.position.x, GameObject.Find("Mother").transform.position.x, ref velocity.x, 1f);
                posY = Mathf.SmoothDamp(this.transform.position.y, GameObject.Find("Mother").transform.position.y, ref velocity.y, 1f);
                this.transform.position = new Vector3(
                Mathf.Clamp(posX, minX, maxX),
                Mathf.Clamp(posY, minY, maxY),
                this.transform.position.z);
            }
            else
            {
                posX = Mathf.SmoothDamp(this.transform.position.x, P1.transform.position.x, ref velocity.x, 0.15f);
                posY = Mathf.SmoothDamp(this.transform.position.y, P1.transform.position.y, ref velocity.y, 0.15f);
                this.transform.position = new Vector3(
                212.3f,
                Mathf.Clamp(posY, minY, maxY),
                this.transform.position.z);
            }
            theWall.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else if (transform.position.x < 212.3f && GameObject.Find("Mother") == true && camLock)
        {
            if (GameObject.Find("Mother").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                posX = Mathf.SmoothDamp(this.transform.position.x, GameObject.Find("Mother").transform.position.x, ref velocity.x, 0.5f);
                posY = Mathf.SmoothDamp(this.transform.position.y, GameObject.Find("Mother").transform.position.y, ref velocity.y, 0.5f);
                this.transform.position = new Vector3(
                Mathf.Clamp(posX, minX, maxX),
                Mathf.Clamp(posY, minY, maxY),
                this.transform.position.z);
            }
            else
            {
                posX = Mathf.SmoothDamp(this.transform.position.x, P1.transform.position.x, ref velocity.x, 0.15f);
                posY = Mathf.SmoothDamp(this.transform.position.y, P1.transform.position.y, ref velocity.y, 0.15f);
                this.transform.position = new Vector3(
                212.3f,
                Mathf.Clamp(posY, minY, maxY),
                this.transform.position.z);
            }
        }
        else if (camLock == false)
        {
            posX = Mathf.SmoothDamp(this.transform.position.x, P1.transform.position.x, ref velocity.x, 0.15f);
            posY = Mathf.SmoothDamp(this.transform.position.y, P1.transform.position.y, ref velocity.y, 0.15f);
            this.transform.position = new Vector3(
                Mathf.Clamp(posX, minX, maxX),
                Mathf.Clamp(posY, minY, maxY),
                this.transform.position.z);
            theWall.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        if (GameObject.Find("Mother") == false)
        {
            camLock = false;
        }
    }

    void Start () {
		
	}
	
	void Update () {
        if (sceneChange == true)
        {
            fader.GetComponent<Animator>().SetBool("fadeOUT", true);
        }
        if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f && !Kat.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            SceneManager.LoadScene(7);
        }
        else if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f)
        {
            SceneManager.LoadScene(6);
        }
    }
}
