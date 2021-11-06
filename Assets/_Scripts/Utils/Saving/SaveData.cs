using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public Vector3 SpawnPoint { get; set; }
    public List<Item> Items { get; set; }
    public int PlayerHealthPoints { get; set; }

    public SaveData(Vector3 spawnPoint, List<Item> items, int playerHealthPoints)
    {
        SpawnPoint = spawnPoint;
        Items = items;
        PlayerHealthPoints = playerHealthPoints;
    }
}