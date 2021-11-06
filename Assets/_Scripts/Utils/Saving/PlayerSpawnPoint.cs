using _Scripts;
using UnityEngine;

public class PlayerSpawnPoint : SpawnPoint
{
    private bool _isUsed;
    public bool IsUsed => _isUsed;

    public void SetUsed()
    {
        _isUsed = true;
    }
}