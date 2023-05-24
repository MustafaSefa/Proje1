using UnityEngine;
public class Skeleton_Controller : MonoBehaviour
{
    public Animator anm;
    public int Health = 100;
    private Transform player;
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
        Movement();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Fire")
        {
            Health -= 20;
            if (Health <= 0)
            {
                switch (Skeleton_Spawner_Controller.a)
                {
                    case 2:
                        Invoke("RingSpawn",0.01f);
                        Skeleton_Spawner_Controller.a = 0;
                        break;
                    default:
                        CancelInvoke("RingSpawn");
                        break;
                }
                anm.Play("Skeleton_Death");
                Destroy(gameObject, 0.6f);
            }
        }
        else if (col.gameObject.tag == "Player")
        {
            p = false;
            anm.speed = 0.2f;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        p = true;
        anm.speed = 1;
    }
    private void Movement()
    {
        if (p)
        {
            smoothTime = 1.5f;
            transform.position = Vector3.SmoothDamp(transform.position,
                new Vector3(player.position.x, transform.position.y, player.position.z), ref smoothVelocity,
                smoothTime);
        }
        else
        {
            smoothTime = 100;
        }

        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }
    private void RingSpawn()
    {
        var newRing = (GameObject) Instantiate(RingPrebab, new Vector2(transform.position.x, transform.position.y - 0.15f),Quaternion.identity);
    }
}
