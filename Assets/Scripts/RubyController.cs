using System;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // ========= MOVEMENT =================
    public float speed = 4;
    public float sprintSpeed = 5;
    public float sprintSec = 0.1f;
    public float slowDownSec = 1;
    float sprintTimer = 0;
    float slowDownTimer = 0;
    // ======== HEALTH ==========
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public Transform respawnPosition;
    public ParticleSystem hitParticle;
    
    // ======== PROJECTILE ==========
    public GameObject projectilePrefab;
    public GameObject meleePrefab;
    public GameObject repairPrefab;

    // ======== AUDIO ==========
    public AudioClip hitSound;
    public AudioClip shootingSound;
    public AudioClip attackSound;
    
    // ======== HEALTH ==========
    public int health
    {
        get { return currentHealth; }
    }
    
    // =========== MOVEMENT ==============
    Rigidbody2D rigidbody2d;
    Vector2 currentInput;
    
    // ======== HEALTH ==========
    int currentHealth;
    float invincibleTimer;
    bool isInvincible;
   
    // ==== ANIMATION =====
    //Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    bool isSprint = false;
    
    // ================= SOUNDS =======================
    AudioSource audioSource;

    Dictionary<string,bool> axisInUse = new Dictionary<string, bool>();
    void Start()
    {
        // =========== MOVEMENT ==============
        rigidbody2d = GetComponent<Rigidbody2D>();
                
        // ======== HEALTH ==========
        invincibleTimer = -1.0f;
        currentHealth = maxHealth;
        
        // ==== ANIMATION =====
        //animator = GetComponent<Animator>();
        
        // ==== AUDIO =====
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ================= HEALTH ====================
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        // ============== MOVEMENT ======================
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
                
        Vector2 move = new Vector2(horizontal, vertical);
        if (move.magnitude > 0)
        {
            move.Normalize();
        }

        if (isSprint)
        {
            sprintTimer -= Time.deltaTime;
            if (sprintTimer < 0)
            {
                isSprint = false;
            }
        }

        if (isSprint)
        {
            move = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            move.Normalize();
            move = new Vector2(lookDirection.x, lookDirection.y);
        }

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        if (move.magnitude > 0)
        {
            currentInput = move;
            slowDownTimer = slowDownSec;
        }
        else if (currentInput.magnitude > 0)// slow down
        {
            slowDownTimer -= Time.deltaTime;
            if (slowDownTimer < 0)
            {
                slowDownTimer = 0;
                currentInput = Vector2.zero;
            }
        }

        // ============== ANIMATION =======================

        //animator.SetFloat("Look X", lookDirection.x);
        //animator.SetFloat("Look Y", lookDirection.y);
        //animator.SetFloat("Speed", move.magnitude);

        // ============== PROJECTILE ======================
        if (Input.GetMouseButtonDown(0) || GetAxisRawDown("Fire1"))
            MeleeAttack();

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.C) || GetAxisRawDown("Fire2"))
            LaunchProjectile();

        if (Input.GetKeyDown(KeyCode.E) || GetAxisRawDown("Fire3"))
            Repair();

        if (Input.GetKeyDown(KeyCode.Space) || GetAxisRawDown("Jump"))
            Sprint();

        

        // ======== DIALOGUE ==========
        if (Input.GetKeyDown(KeyCode.X)|| GetAxisRawDown("Fire3"))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, 1 << LayerMask.NameToLayer("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }  
            }
        }
 
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if (!isSprint)
        {
            position = position + currentInput * speed * Time.deltaTime;
        }
        else
        {
            position = position + currentInput * sprintSpeed * Time.deltaTime;
        }

        rigidbody2d.MovePosition(position);
    }

    // ===================== HEALTH ==================
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        { 
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            
            //animator.SetTrigger("Hit");
            audioSource.PlayOneShot(hitSound);

            Instantiate(hitParticle, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        if(currentHealth == 0)
            Respawn();
        
        UIHealthBar.Instance.SetValue(currentHealth / (float)maxHealth);
    }
    
    void Respawn()
    {
        ChangeHealth(maxHealth);
        transform.position = respawnPosition.position;
    }
    
    // =============== PROJECTICLE ========================
    void LaunchProjectile()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position /*+ Vector2.up * 0.5f*/, Quaternion.identity);

        AttackBase projectile = projectileObject.GetComponent<AttackBase>();
        Vector2 projectDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        projectDirection.Normalize();
        projectile.Launch(projectDirection, 300);
        
        //animator.SetTrigger("Launch");
        audioSource.PlayOneShot(shootingSound);
    }
    
    void Repair()
    {
        //animator.SetTrigger("Launch");
        repairPrefab.SetActive(true);
        AttackBase projectile = repairPrefab.GetComponent<AttackBase>();
        Vector2 projectDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        projectDirection.Normalize();
        projectile.Launch(projectDirection, 300);
        audioSource.PlayOneShot(attackSound);
    }

    void MeleeAttack()
    {
        //animator.SetTrigger("Launch");
        meleePrefab.SetActive(true);
        AttackBase projectile = meleePrefab.GetComponent<AttackBase>();
        Vector2 projectDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        projectDirection.Normalize();
        projectile.Launch(projectDirection, 300);
        audioSource.PlayOneShot(attackSound);
    }

    void Sprint()
    {
        if (isSprint) return;
        isSprint = true;
        sprintTimer = sprintSec;
    }

    bool GetAxisRawDown(string axisName)
    {
        if (!axisInUse.ContainsKey(axisName))
        {
            axisInUse.Add(axisName, false);
        }

        if (Input.GetAxisRaw(axisName) != 0)
        {
            if (axisInUse[axisName] == false)
            {
                axisInUse[axisName] = true;
                return true;
            }

            return false;
        }
        else
        {
            axisInUse[axisName] = false;
            return false;
        }
    }

    // =============== SOUND ==========================

    //Allow to play a sound on the player sound source. used by Collectible
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
