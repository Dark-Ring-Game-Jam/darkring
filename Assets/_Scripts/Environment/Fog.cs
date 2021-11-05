using UnityEngine;

public class Fog : MonoBehaviour
{
    public static Fog Instance { get; private set; }

    private void Awake() 
    {
        Instance = this;
    }
    
    /// <summary>
    /// Заполнить карту "туманом войны".
    /// </summary>
    /// <param name="point1">Крайняя точка карты.</param>
    /// <param name="point2">Диагонально противоположная крайняя точка карты.</param>
    /// <param name="step">Шаг заполнения.</param>
    public static void FillTheMap(Vector2 point1, Vector2 point2, float step)
    {
        // TODO - реализовать
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out Player player))
        {
            // TODO - добавить анимайию исчезновения, после - уничтожить
            
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}