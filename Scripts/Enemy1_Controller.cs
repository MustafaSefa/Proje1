using UnityEngine;
public class Enemy1_Controller : MonoBehaviour
{
    public Animator anm;
    public int Health = 100;
    private Transform player;
    public GameObject Enemy1Sword;
    private Player_Controller _player;
    private float smoothTime = 1.5f;
    private Vector3 smoothVelocity = Vector3.zero;
    private bool p = true;
    public GameObject RingPrebab;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        if (transform.position.x - player.transform.position.x <= 0.20f &&
            transform.position.x - player.transform.position.x >= -0.20f)
        {
            p = false;
        }
        else
        {
            p = true;
        }
        Movement();
    }
    private void OnCollisionEnter2D(Collision2D col)
    { 
        if (col.gameObject.tag == "Fire")
        {
            Health -= 20;
            if (Health <= 0)
            {
                switch (Enemy1_Spawner_Controller.a)
                {
                    case 5:
                        Invoke("RingSpawn",0.01f);
                        Enemy1_Spawner_Controller.a = 0;
                        break;
                    default:
                        CancelInvoke("RingSpawn");
                        break;
                }
                anm.Play("Enemy1_Dead");
                Destroy(gameObject,0.7f);
            }
        }
    }
    private void Movement()
    {
        if (p)
        {
            anm.SetBool("Walk", true);
            anm.SetBool("Attack1", false);
            Enemy1Sword.SetActive(false);
            smoothTime = 1.5f;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), ref smoothVelocity, smoothTime);
        }
        else
        {
            anm.SetBool("Walk",false);
            anm.SetBool("Attack1",true);
            Enemy1Sword.SetActive(true);
            smoothTime = 100;
        }
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }
    private void RingSpawn()
    {
        var newRing = (GameObject) Instantiate(RingPrebab, new Vector2(transform.position.x, transform.position.y - 0.15f),Quaternion.identity);
    }
}
