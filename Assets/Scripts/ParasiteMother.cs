using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteMother : MonoBehaviour
{

    public GameObject parasite;

    public int parasiteMaxNumber = 2;

    float realtime;
    float prevtime;

    public float spawnTime = 2;

    GameObject parasiteCounter;

    void Start()
    {
        parasiteCounter = GameObject.Find("ParasiteCounter");
    }

    
    void Update()
    {
        realtime = Time.fixedTime;

        // ONLY SPAWNS NEW PARASITES IF A FIXED NUMBER OF THEM IS ALREADY INSTANTIATED 

        if ((realtime - prevtime > spawnTime) && parasiteCounter.transform.position.x < parasiteMaxNumber)  
        {
            Instantiate(parasite, new Vector3(transform.position.x - 0.4f, transform.position.y - 0.8f, -3.95f), parasite.transform.rotation);
            prevtime = realtime;
        }
    }
}
