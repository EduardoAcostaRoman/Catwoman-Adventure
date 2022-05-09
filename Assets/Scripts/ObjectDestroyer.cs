using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public bool startFlipX = false;

    public float destroyTime = 2;

    public string objReferenceName;

    GameObject objReference;

    void Start()
    {
        if (GameObject.Find(objReferenceName))
        {
            objReference = GameObject.Find(objReferenceName);

            if (objReference.GetComponent<SpriteRenderer>().flipX == startFlipX)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            print("object of reference not found, sorry dude ;P");
        }
    }

    
    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
