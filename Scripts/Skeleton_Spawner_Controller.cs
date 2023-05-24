using UnityEngine;
public class Skeleton_Spawner_Controller : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public Transform[] spawnPoint;
    public static int a;
    public float SpawnDelay = 3f;
    float timeSinceLastSpawn = Mathf.Infinity;
    private void FixedUpdate()
    {
        if (CheckPoint.c == 3 && a < 5)
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
            Instantiate(skeletonPrefab, spawnPoint[randPos].position, Quaternion.identity);
            timeSinceLastSpawn = 0; 
            a++;
        }
    }
}
