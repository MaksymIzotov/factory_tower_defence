using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Create a list of positions where object can be placed. Adjust type of placeable at each point. Adjust the min and max of placeable. 
public class MapGenerator : MonoBehaviour
{
    [Serializable]
    public struct Entity{
        public GameObject prefab;
        public int amount;
        public int offset;
    }

    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private float spawnChance;

    [SerializeField] private Vector2Int size;
    [SerializeField] private float spacing;

    [SerializeField] private List<Entity> entites;

    private List<Vector3> availableLocations = new List<Vector3>();

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for (float i = size.x * -spacing; i < size.x * spacing; i += spacing)
        {
            for (float j = size.y * -spacing; j < size.y * spacing; j += spacing)
            {
                availableLocations.Add(new Vector3(i, 0,j));               
            }
        }

        foreach(Entity entity in entites)
        {
            for (int i = 0; i < entity.amount; i++)
            {
                Instantiate(entity.prefab, GetSpawnPos(entity), Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(15, 165), 0)));
            }      
        }
    }

    private Vector3 GetSpawnPos(Entity entity)
    {
        List<Vector3> positions = new List<Vector3>();

        foreach (Vector3 pos in availableLocations)
        {
            if (pos.x < -entity.offset * spacing || pos.x > entity.offset * spacing || pos.z < -entity.offset * spacing || pos.z > entity.offset * spacing)
                    positions.Add(pos);
        }

        int index = UnityEngine.Random.Range(0, positions.Count);

        availableLocations.Remove(positions[index]);
        return positions[index];
    }
}
