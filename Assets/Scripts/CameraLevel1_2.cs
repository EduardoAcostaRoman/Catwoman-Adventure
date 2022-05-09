using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLevel1_2 : MonoBehaviour {

    private GameObject P1;
    private GameObject fader;
    private bool sceneChange;
    private GameObject sceneChanger;
    public LayerMask player;
    private GameObject arrow;
    private float arrowPosition = 4f;
    private float arrowMove;
    private GameObject Kat;
    private float minX = 4.896f, minY = -0.9484f, maxX = 377.4f, maxY = 3.18838f;
    private float posX, posY;
    private Vector2 velocity;
    public GameObject deathBeforeSceneChange;

    private void FixedUpdate()
    {
        P1 = GameObject.Find("P1 position");
        sceneChanger = GameObject.Find("levelend");
        sceneChange = Physics2D.IsTouchingLayers(sceneChanger.GetComponent<BoxCollider2D>(), player);
        fader = GameObject.Find("Image");
        Kat = GameObject.Find("Catwoman");
        posX = Mathf.SmoothDamp(this.transform.position.x, P1.transform.position.x, ref velocity.x, 0.15f);
        posY = Mathf.SmoothDamp(this.transform.position.y, P1.transform.position.y, ref velocity.y, 0.15f);
        this.transform.position = new Vector3(
            Mathf.Clamp(posX, minX, maxX),
            Mathf.Clamp(posY, minY, maxY),
            this.transform.position.z);
        if (this.transform.GetChild(3).transform.GetChild(0).transform.localScale.x <= 0)
        {
            deathBeforeSceneChange.layer = 17;
        }
    }

    void Start () {
		
	}

	void Update () {
        fader = GameObject.Find("Image");
        arrow = GameObject.Find("Arrow");
        if (sceneChange == true)
        {
            fader.GetComponent<Animator>().SetBool("fadeOUT", true);
        }
        if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f && !Kat.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            SceneManager.LoadScene(5);
        }
        else if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f)
        {
            SceneManager.LoadScene(4);
        }
        if (arrowPosition <= 0)
        {
            arrowMove = 0;
            arrow.GetComponent<AudioSource>().Play();
            arrowPosition = 4f;            
        }
        else
        {
            arrowPosition = arrow.transform.position.y;
            arrowMove += 0.0025f;
        }
        arrow.transform.position = new Vector3(arrow.transform.position.x, arrowPosition - arrowMove, arrow.transform.position.z);
    }
    
}
