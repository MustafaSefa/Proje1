using UnityEngine;
public class Spider_Controller : MonoBehaviour
{
    private Transform Player;
    public Animator anm;
    public float smoothTime = 1.5f;
    private Vector3 smoothVelocity = Vector3.zero;
    private int health = 30;
    private bool p = true;
    public GameObject RingPrebab;
    void Update()
    {
        Player = GameObject.FindWithTag("Player").transform;
        Movement();
    }
    void Movement()
    {
        if (Player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1.8f, transform.localScale.y, transform.localScale.z);
        }
        else if (Player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1.8f, transform.localScale.y, transform.localScale.z);
        }
        if (p)
        {
            smoothTime = 1.5f;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(Player.position.x, transform.position.y, Player.position.z), ref smoothVelocity, smoothTime);
        }
        else
        {
            smoothTime = 100;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            p = false;
            anm.speed = 0.2f;
        }
        else if (col.gameObject.tag == "Fire")
        {
            health -= 10;
            if (health == 0)
            {
                smoothTime = 100;
                switch (Spider_Spawner_Controller.a)
                {
                    case 2:
                        Invoke("RingSpawn",0.01f);
                        Spider_Spawner_Controller.a = 0;
                        break;
                    default:
                        CancelInvoke("RingSpawn");
                        break;
                }
                anm.Play("Spider_Death");
                Destroy(gameObject,0.4f);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        p = true;
        anm.speed = 1;
    }
    private void RingSpawn()
    {
        var newRing = (GameObject) Instantiate(RingPrebab, new Vector2(transform.position.x, transform.position.y - 0.15f),Quaternion.identity);
    }
}
