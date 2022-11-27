using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Tooth> infectTeeth;
    public List<Tooth> vulnerableTeeth;
    public List<Enemy> enemies;
    public List<SpawnPosition> spawnPositionsFromTongue;
    public int infectedTeethSpawnSec = 5;
    float infectedTeethSpawnTimer = 0;
    public int tongueSpawnSec = 10;
    float tongueSpawnTimer = 0;
    static private LevelManager _instance;
    public static LevelManager instance
    {
        get
        {
            return _instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null) Debug.LogError("_instance!=null");
        _instance = this;
        {
            var teeth = GameObject.FindObjectsOfType<Tooth>();
            var results = teeth.Where((Tooth tooth) =>
            {
                return tooth.state == Tooth.State.VULNERABLE;
            });
            vulnerableTeeth.Clear();
            vulnerableTeeth.AddRange(results);
        }
        {
            var teeth = GameObject.FindObjectsOfType<Tooth>();
            var results = teeth.Where((Tooth tooth) =>
            {
                return tooth.state == Tooth.State.INFECTED;
            });
            infectTeeth.Clear();
            infectTeeth.AddRange(results);
        }
        {
            var enemies_behvaiour = GameObject.FindObjectsOfType<Enemy>();
            enemies.Clear();
            enemies.AddRange(enemies_behvaiour);
        }
        {
            var spawnPositions = GameObject.FindObjectsOfType<SpawnPosition>();
            spawnPositions = spawnPositions.Where((SpawnPosition spawnPosition)=>{
                return spawnPosition.GetComponent<Tooth>() == null;
            }).ToArray();
            spawnPositionsFromTongue.Clear();
            spawnPositionsFromTongue.AddRange(spawnPositions);
        }
    }

    // Update is called once per frame
    void Update()
    {
        infectedTeethSpawnTimer += Time.deltaTime;
        if (infectedTeethSpawnTimer > infectedTeethSpawnSec)
        {
            infectedTeethSpawnTimer -= infectedTeethSpawnSec;
            if (infectTeeth.Count > 0)
            {
                infectTeeth[Random.Range(0, infectTeeth.Count)].GetComponent<SpawnPosition>().Spawn();
            }
        }

        tongueSpawnTimer += Time.deltaTime;
        if (tongueSpawnTimer > tongueSpawnSec)
        {
            tongueSpawnTimer -= tongueSpawnSec;
            if (spawnPositionsFromTongue.Count > 0)
            {
                spawnPositionsFromTongue[Random.Range(0, spawnPositionsFromTongue.Count)].Spawn();
            }
        }
    }
}
