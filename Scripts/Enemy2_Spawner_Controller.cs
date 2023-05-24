using UnityEngine;
public class Enemy2_Spawner_Controller : MonoBehaviour
{
    public GameObject Enemy2Prefab;
    public Transform[] spawnPoint;
    public float interval;
    private int a;
    void Start()
    {
        InvokeRepeating("Spawn",0.3f,interval);
    }
    void Update()
    {
        if (a == 5)
        {
            CancelInvoke("Spawn");
        }
    }
    private void Spawn()
    {
        int randPos = Random.Range(0, spawnPoint.Length);
        var newEnemy2 = (GameObject) Instantiate(Enemy2Prefab, spawnPoint[randPos].position,Quaternion.identity);
        a++;
    }
}
