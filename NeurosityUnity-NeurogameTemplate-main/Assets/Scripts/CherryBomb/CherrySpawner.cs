using UnityEngine;

public class CherrySpawner : MonoBehaviour
{
    [SerializeField] public GameObject cherryPrefab;
    [SerializeField] public GameObject bombPrefab;
    [SerializeField] public GameObject strawberryPrefab;
    public float spawnInterval = 5f;
    public float timer = 0f;
    public float bombSpawnProbability = 0.2f;
    public float strawberrySpawnProbability = 0.3f; 

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnFruit();
        }
    }

    // Determines which object spawns
    public void SpawnFruit()
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= bombSpawnProbability)
        {
            Instantiate(bombPrefab);
        }
        else if (randomValue <= bombSpawnProbability + strawberrySpawnProbability)
        {
            Instantiate(strawberryPrefab);
        }
        else
        {
            Instantiate(cherryPrefab);
        }
    }
}

