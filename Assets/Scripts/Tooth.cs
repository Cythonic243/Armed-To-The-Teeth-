using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooth : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 150;
    public Sprite sprOverheal;
    public Sprite sprVulnerable;
    public Sprite sprInfected;
    SpriteRenderer spriteRenderer;
    SpawnPosition spawnPosition;
    public State state = State.VULNERABLE;
    List<Enemy> enemiesInRange = new List<Enemy>();
    float enemyTimer = 0;
    float enemyInterval = 1;
    public TMPro.TextMeshProUGUI textMesh;
    public enum State
    {
        OVERHEAL, VULNERABLE, INFECTED
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = GetComponent<SpawnPosition>();
        EnterState(state);
    }

    // Update is called once per frame
    void Update()
    {
        // update per interval
        enemyTimer += Time.deltaTime;
        if (enemyTimer > enemyInterval)
        {
            enemyTimer -= enemyInterval;
            ChangeHealth(-enemiesInRange.Count);
        }
        textMesh.text = health.ToString();
    }

    void EnterState(State s)
    {
        switch (s)
        {
            case State.OVERHEAL:
                spriteRenderer.sprite = sprOverheal;
                spawnPosition.enabled = false;
                break;
            case State.INFECTED:
                spriteRenderer.sprite = sprInfected;
                spawnPosition.enabled = true;
                if (LevelManager.instance != null && !LevelManager.instance.infectTeeth.Contains(this))
                    LevelManager.instance.infectTeeth.Add(this);
                break;
            case State.VULNERABLE:
                spriteRenderer.sprite = sprVulnerable;
                spawnPosition.enabled = false;
                if (LevelManager.instance != null && !LevelManager.instance.vulnerableTeeth.Contains(this))
                    LevelManager.instance.vulnerableTeeth.Add(this);
                break;
        }
        state = s;
    }

    void ExitState(State s)
    {
        switch (s)
        {
            case State.OVERHEAL:
                break;
            case State.INFECTED:
                if (LevelManager.instance != null && LevelManager.instance.infectTeeth.Contains(this))
                    LevelManager.instance.infectTeeth.Remove(this);
                break;
            case State.VULNERABLE:
                if (LevelManager.instance != null && LevelManager.instance.vulnerableTeeth.Contains(this))
                    LevelManager.instance.vulnerableTeeth.Remove(this);
                break;
        }
    }

    public void ChangeHealth(int n)
    {
        int healthBefore = health;
        health += n;
        if (health > 150) health = 150;
        if (health < -50) health = -50;
        if (n < 0 && healthBefore > 0 && health <= 0)
        {
            health = -50;
            ExitState(state);
            EnterState(State.INFECTED);
        }
        if (n > 0 && healthBefore < 0 && health >= 0)
        {
            ExitState(state);
            EnterState(State.VULNERABLE);
        }
        if (n > 0 && healthBefore < 150 && health == 150)
        {
            ExitState(state);
            EnterState(State.OVERHEAL);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null && enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }

}
