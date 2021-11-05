using UnityEngine;

public class Smoke : MonoBehaviour
{
    public void Destroy()
    {
        DestroySelf();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out Player player))
        {
            player.TakeDamage(1000);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}