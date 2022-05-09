using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLevel3_1 : MonoBehaviour
{
    private GameObject P1;
    private GameObject fader;
    private bool sceneChange;
    private GameObject sceneChanger;
    public LayerMask player;
    private GameObject Kat;
    public float minX = 9.92f, minY = -1.8f, maxX = 275.86f, maxY = -1.31f;
    private float posX, posY;
    private Vector2 velocity;
    private GameObject theWall;
    public bool camLock;

    public GameObject arrow;
    public GameObject arrow2;
    public float arrowPosition = 108f;
    public float arrowMoveStep = 0.0025f;
    public float arrowMoveDistance = 4;

    public GameObject backGround;

    private void FixedUpdate()
    {
        P1 = GameObject.Find("P1 position");
        sceneChanger = GameObject.Find("wayout");
        sceneChange = Physics2D.IsTouchingLayers(sceneChanger.GetComponent<BoxCollider2D>(), player);
        fader = GameObject.Find("Image");
        Kat = GameObject.Find("Catwoman");
        theWall = transform.GetChild(12).gameObject;

        posX = Mathf.SmoothDamp(transform.position.x, P1.transform.position.x, ref velocity.x, 0.15f);
        posY = Mathf.SmoothDamp(transform.position.y, P1.transform.position.y, ref velocity.y, 0.15f);

        this.transform.position = new Vector3(Mathf.Clamp(posX, minX, maxX),
                                              Mathf.Clamp(posY, minY, maxY),
                                              transform.position.z);

        theWall.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void Start()
    {
        //---------------- creating a repeating background ----------------//

        for (int i = 1; i < 8; i++)
        {
            Instantiate(backGround, new Vector3(0, i * 15, 5 - (i * 0.1f)), new Quaternion(0, 0, 0, 1));
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
            SceneManager.LoadScene(11);
        }
        else if (fader.GetComponent<RectTransform>().pivot.x <= 0.402f)
        {
            SceneManager.LoadScene(10);
        }


        // --- ARROW MOVEMENT --- //

        if (arrow.transform.position.y <= arrowPosition)
        {
            arrow.transform.position = new Vector3(arrow.transform.position.x, arrowPosition + arrowMoveDistance, arrow.transform.position.z);
            arrow2.transform.position = new Vector3(arrow2.transform.position.x, arrowPosition + arrowMoveDistance, arrow.transform.position.z);
        }
        else
        {
            arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y - arrowMoveStep, arrow.transform.position.z);
            arrow2.transform.position = new Vector3(arrow2.transform.position.x, arrow.transform.position.y - arrowMoveStep, arrow.transform.position.z);
        }
        
    }
}
