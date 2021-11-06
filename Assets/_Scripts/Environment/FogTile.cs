using UnityEngine;

public class FogTile : MonoBehaviour
{
    public static void FillTheMap(Vector2 topLeftPoint, int w, int h, FogTile fogTile, Transform parentAnchor = null)
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                var transform = Instantiate(fogTile, new Vector3(topLeftPoint.x + i, topLeftPoint.y - j, 0), Quaternion.identity);
                transform.gameObject.name = $"FogTile_{i}_{j}";
                if (parentAnchor != null)
                {
                    transform.gameObject.transform.SetParent(parentAnchor);
                }
            }
        }
    }
}