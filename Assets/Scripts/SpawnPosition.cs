using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    //public float intervalSec = 3;
    //public float startingSec = 5;
    //public int maxNum = 10;
    public GameObject spawnObject;
    //float startTimer = 0;
    //float spawnTimer = 0;
    //float spawnCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (startTimer < startingSec)
        //{
        //    startTimer += Time.deltaTime;
        //    return;
        //}

        //if (spawnCount >= maxNum)
        //{
        //    return;
        //}

        //if (spawnTimer <= 0)
        //{
        //    Spawn();
        //    spawnTimer = intervalSec;
        //    spawnCount++;
        //}
        //else
        //{
        //    spawnTimer -= Time.deltaTime;
        //}
    }

    public void Spawn()
    {
        GameObject o = GameObject.Instantiate(spawnObject);
        o.transform.position = transform.position;
        o.SetActive(true);
    }
}
