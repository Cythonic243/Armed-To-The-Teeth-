using UnityEngine;
using System.Collections;

/// <summary>
/// Handle the projectile launched by the player to fix the robots.
/// </summary>
public class WaterLaunch : AttackBase
{
    static Water water = null;
    //public UnityEngine.LineRenderer lineRenderer;
    void Awake()
    {
        if (water != null) return;
        water = FindObjectOfType<Water>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    void Update()
    {
        
    }

    //called by the player controller after it instantiate a new projectile to launch it.
    public override void Launch(Vector2 direction, float force)
    {
        //rigidbody2d.AddForce(direction * force);
        // water.transform.Rotate(Vector3.forward, Vector2.SignedAngle(direction, Vector2.right));
        water.transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(direction, Vector2.right), Vector3.back);
        //lineRenderer.SetPositions(new Vector3[] { transform.position, transform.position + new Vector3(direction.x, direction.y, 0) });
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Destroy(gameObject);
    }
}
