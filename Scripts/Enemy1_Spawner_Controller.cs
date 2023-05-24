using UnityEngine;
public class Enemy1_Spawner_Controller : MonoBehaviour
{
    public GameObject Enemy1Prefab;
    public Transform[] spawnPoint;
    public float interval;
    public static int a;
    private int b;
    public static Vector2 e;
    void Start()
    {
        InvokeRepeating("Spawn",0.3f,interval);
    }
    void Update()
    {
        if (b == 10)
        {
            CancelInvoke("Spawn");
        }
    }
    private void Spawn()
    {
        int randPos = Random.Range(0, spawnPoint.Length);
        var newEnemy1 = (GameObject) Instantiate(Enemy1Prefab, spawnPoint[randPos].position,Quaternion.identity);
        e = new Vector2(newEnemy1.transform.position.x, newEnemy1.transform.position.y - 0.15f);
        a++;
        b++;
    }
}
