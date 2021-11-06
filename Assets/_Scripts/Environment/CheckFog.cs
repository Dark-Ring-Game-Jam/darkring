using UnityEngine;

public class CheckFog : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, Range(0, 10)] private float _checkRadius;
    [SerializeField, Range(0, 10)] private float _checkDistance;
    [SerializeField] private float _lightRadius;
    
    private RaycastHit2D[] _hit;
    private int _frames;
    
    void FixedUpdate()
    {
        _frames++;
        if (_frames == 4)
        {
            _frames = 0;

            _hit = Physics2D.CircleCastAll(transform.position, _checkRadius, new Vector2(0, 0), _checkDistance);
            
            foreach (var item in _hit)
            {
                if (item.collider.transform.gameObject != this.gameObject && item.collider.transform.gameObject.GetComponent<FogTile>() != null)
                {
                    Vector3 dis = transform.position - item.collider.transform.position;
                    SpriteRenderer sprite = item.collider.transform.gameObject.GetComponent<SpriteRenderer>();
                    if (sprite != null)
                    {
                        if (dis.sqrMagnitude < _lightRadius * _lightRadius)
                        {
                            sprite.color = new Color(1.0f, 1.0f, 1.0f, 0f);
                        }
                        else
                        {
                            sprite.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
                        }
                    }
                }
            }
        }
    }
}