using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : AttackBase
{
    //Rigidbody2D rigidbody2d;
    float inactiveTimer = 0.0f;
    public float inactiveSec = 0.5f;
    HashSet<Enemy> enemies = new HashSet<Enemy>();
    void Awake()
    {
        //rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        enemies.Clear();
        inactiveTimer = 0;
    }

    void Update()
    {
        inactiveTimer += Time.deltaTime;
        //destroy the projectile when it reach a distance of 1000.0f from the origin
        if (inactiveTimer > inactiveSec)
            gameObject.SetActive(false);
    }

    //called by the player controller after it instantiate a new projectile to launch it.
    public override void Launch(Vector2 direction, float force)
    {
        transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(direction, Vector2.right)+90, Vector3.back);
        transform.localPosition = direction.normalized * 0.3f;
        //rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Enemy e = other.collider.GetComponent<Enemy>();

        //if the object we touched wasn't an enemy, just destroy the projectile.
        if (e != null && !enemies.Contains(e))
        {
            enemies.Add(e);
            e.Fix();
        }
    }
}