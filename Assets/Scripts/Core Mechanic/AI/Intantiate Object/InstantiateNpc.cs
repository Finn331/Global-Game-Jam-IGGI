using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateNpc : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public int numberOfInstances = 5;

    // List untuk menyimpan posisi-posisi acak
    public List<Vector3> randomPositions = new List<Vector3>();

    void Start()
    {
        // Memulai coroutine untuk meng-instantiate prefab secara berkala
        StartCoroutine(InstantiatePrefabPeriodically());
    }

    void Update()
    {
        GenerateRandomPositions();
    }

    void GenerateRandomPositions()
    {
        for (int i = 0; i < numberOfInstances; i++)
        {
            // Mendapatkan posisi acak di dalam area tertentu, misalnya (0, 0) hingga (10, 10)
            float randomX = Random.Range(0f, 10f);
            float randomY = Random.Range(0f, 10f);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

            // Menambahkan posisi acak ke dalam list
            randomPositions.Add(randomPosition);
        }
    }

    IEnumerator InstantiatePrefabPeriodically()
    {
        while (true)
        {
            // Tunggu selama 10 detik
            yield return new WaitForSeconds(10f);

            // Instantiate prefab di setiap posisi acak
            foreach (Vector3 randomPosition in randomPositions)
            {
                Instantiate(prefabToInstantiate, randomPosition, Quaternion.identity);
            }
        }
    }
}
