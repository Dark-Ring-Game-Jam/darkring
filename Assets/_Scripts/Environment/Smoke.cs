using UnityEngine;

public class Smoke : MonoBehaviour
{
    public void Destroy()
    {
        DestroySelf();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TakeDamage(1000);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}