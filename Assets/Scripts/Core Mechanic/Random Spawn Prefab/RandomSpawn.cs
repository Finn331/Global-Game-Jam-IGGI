using System.Collections;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public float spawnRadius = 5f;
    public int maxObjects = 15;

    void Start()
    {
        StartCoroutine(SpawnPrefabsRoutine());
    }

    IEnumerator SpawnPrefabsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            if (CountSpawnedObjects() < maxObjects)
            {
                SpawnPrefab();
            }
        }
    }

    void SpawnPrefab()
    {
        Vector2 randomPosition = GetRandomPosition();
        GameObject randomPrefab = GetRandomPrefab();
        Instantiate(randomPrefab, randomPosition, Quaternion.identity);
    }

    int CountSpawnedObjects()
    {
        return GameObject.FindGameObjectsWithTag("NPC").Length;
    }

    GameObject GetRandomPrefab()
    {
        if (prefabsToSpawn.Length == 0)
        {
            Debug.LogError("No prefabs assigned to TimedRandomSpawner script on " + gameObject.name);
            return null;
        }

        return prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
    }

    Vector2 GetRandomPosition()
    {
        float randomAngle = Random.Range(0f, 360f);
        float randomDistance = Random.Range(0f, spawnRadius);

        float x = transform.position.x + randomDistance * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
        float y = transform.position.y + randomDistance * Mathf.Sin(randomAngle * Mathf.Deg2Rad);

        return new Vector2(x, y);
    }
}
