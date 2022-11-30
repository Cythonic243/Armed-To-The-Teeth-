using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    LevelDataScriptObject levelDataScriptableObject;
    public List<Tooth> infectTeeth;
    public List<Tooth> vulnerableTeeth;
    public List<Tooth> teeth;
    public List<Enemy> enemies;
    public List<SpawnPosition> spawnPositionsFromTongue;
    [SerializeField] public PanettoneGames.GenericEvents.IntEvent enemyCountEvent;
    [SerializeField] public PanettoneGames.GenericEvents.IntEvent toothCountEvent;
    //public int infectedTeethSpawnSec = 5;
    float infectedTeethSpawnTimer = 0;
    //public int tongueSpawnSec = 10;
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
    IEnumerator Start()
    {
        if (_instance != null)
        {
            Debug.LogError("_instance!=null");
            Destroy(this.gameObject);
            yield break;
        }
        yield return new WaitForSeconds(1);
        while (SystemInstance.systemInstance == null)
        {
            Debug.Log("SystemInstance.systemInstance == null waiting");
            yield return null;
        }

        levelDataScriptableObject = SystemInstance.systemInstance.levelData[SystemInstance.systemInstance.levelIndex];
        if (levelDataScriptableObject == null)
        {
            levelDataScriptableObject = ScriptableObject.CreateInstance<LevelDataScriptObject>();
            Debug.LogWarning("levelDataScriptableObject == null");
        }

        _instance = this;
        var teeth = GameObject.FindObjectsOfType<Tooth>();
        this.teeth = teeth.ToList();
        teeth = teeth.OrderBy((Tooth tooth) =>
        {
            return Random.Range(0, 1000000);
        }).ToArray();

        vulnerableTeeth.Clear();
        //vulnerableTeeth.AddRange(teeth.Take(levelDataScriptableObject.numTeethVulnerable));
        foreach(var t in teeth.Take(levelDataScriptableObject.numTeethVulnerable))
        {
            t.EnterState(Tooth.State.VULNERABLE);
        }
        infectTeeth.Clear();
        //infectTeeth.AddRange(teeth.Skip(vulnerableTeeth.Count).Take(levelDataScriptableObject.numTeethInfected));
        foreach (var t in teeth.Skip(levelDataScriptableObject.numTeethVulnerable).Take(levelDataScriptableObject.numTeethInfected))
        {
            t.EnterState(Tooth.State.INFECTED);
        }

        foreach (var t in teeth.Skip(levelDataScriptableObject.numTeethInfected + levelDataScriptableObject.numTeethVulnerable))
        {
            t.EnterState(Tooth.State.HEALTH);
        }
        // toothCountEvent.Raise(vulnerableTeeth.Count + infectTeeth.Count);

        {
            var enemies_behvaiour = GameObject.FindObjectsOfType<Enemy>();
            enemies.Clear();
            enemies.AddRange(enemies_behvaiour);
            enemyCountEvent.Raise(enemies.Count);
        }
        {
            var spawnPositions = GameObject.FindObjectsOfType<SpawnPosition>();
            spawnPositions = spawnPositions.Where((SpawnPosition spawnPosition)=>{
                return spawnPosition.GetComponent<Tooth>() == null;
            }).ToArray();
            spawnPositionsFromTongue.Clear();
            spawnPositionsFromTongue.AddRange(spawnPositions);
        }
        while (true)
        {
            yield return null;
        }
    }

    private void OnDisable()
    {
        _instance = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelDataScriptableObject == null)
        {
            return;
        }

        infectedTeethSpawnTimer += Time.deltaTime;
        if (infectedTeethSpawnTimer > levelDataScriptableObject.infectedTeethSpawnSec)
        {
            infectedTeethSpawnTimer -= levelDataScriptableObject.infectedTeethSpawnSec;
            if (infectTeeth.Count > 0)
            {
                infectTeeth[Random.Range(0, infectTeeth.Count)].GetComponent<SpawnPosition>().Spawn();
            }
        }

        tongueSpawnTimer += Time.deltaTime;
        if (tongueSpawnTimer > levelDataScriptableObject.tongueSpawnSec)
        {
            tongueSpawnTimer -= levelDataScriptableObject.tongueSpawnSec;
            if (spawnPositionsFromTongue.Count > 0)
            {
                spawnPositionsFromTongue[Random.Range(0, spawnPositionsFromTongue.Count)].Spawn();
            }
        }
    }

    public void EnemyAdd(Enemy enemy)
    {
        if (enemies.Contains(enemy)) return;
        enemies.Add(enemy);
        enemyCountEvent.Raise(enemies.Count);
    }

    public void EnemyRemove(Enemy enemy)
    {
        if (!enemies.Contains(enemy)) return;
        enemies.Remove(enemy);
        enemyCountEvent.Raise(enemies.Count);
    }

    public void AddVulnerableTooth(Tooth tooth)
    {
        if (vulnerableTeeth.Contains(tooth)) return;
        vulnerableTeeth.Add(tooth);
        toothCountEvent.Raise(vulnerableTeeth.Count + infectTeeth.Count);
    }

    public void RemoveVulnerableTooth(Tooth tooth)
    {
        if (!vulnerableTeeth.Contains(tooth)) return;
        vulnerableTeeth.Remove(tooth);
        toothCountEvent.Raise(vulnerableTeeth.Count + infectTeeth.Count);
    }

    public void AddInfectedTooth(Tooth tooth)
    {
        if (infectTeeth.Contains(tooth)) return;
        infectTeeth.Add(tooth);
        toothCountEvent.Raise(vulnerableTeeth.Count + infectTeeth.Count);
    }

    public void RemoveInfectedTooth(Tooth tooth)
    {
        if (!infectTeeth.Contains(tooth)) return;
        infectTeeth.Remove(tooth);
        toothCountEvent.Raise(vulnerableTeeth.Count + infectTeeth.Count);
    }
}
