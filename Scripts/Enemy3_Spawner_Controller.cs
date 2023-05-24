using UnityEngine;
public class Enemy3_Spawner_Controller : MonoBehaviour
{
    public GameObject Enemy3Prefab;
    public Transform[] spawnPoint;
    public static int a;
    public float SpawnDelay = 3f;
    float timeSinceLastSpawn = Mathf.Infinity;
    void Update()
    {
        if (CheckPoint.c == 7 && a < 5)
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= SpawnDelay)
        {
            int randPos = Random.Range(0, spawnPoint.Length);
            Instantiate(Enemy3Prefab, spawnPoint[randPos].position, Quaternion.identity);
            timeSinceLastSpawn = 0; 
            a++;
        }
    }
}
