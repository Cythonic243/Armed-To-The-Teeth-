using UnityEngine;

/// <summary>
/// Will handle giving health to the character when they enter the trigger.
/// </summary>
public class HealthCollectible : MonoBehaviour 
{
    public Sprite[] sprites;
    private void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if (Random.Range(0, 2) == 0)
            {
                controller.ChangeHealth(1);
            }
            else
            {
                controller.level++;
            }
            Destroy(gameObject);
        }
    }
}
