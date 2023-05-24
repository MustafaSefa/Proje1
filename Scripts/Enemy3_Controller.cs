using UnityEngine;

public class Enemy3_Controller : MonoBehaviour
{
    public Animator anm;
    public int Health = 100;
    private Transform player;
    public GameObject Enemy3Attack;
    private float smoothTime = 1.5f;
    private Vector3 smoothVelocity = Vector3.zero;
    private bool p = true;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        Movement();
        if (transform.position.x - player.transform.position.x <= 0.35f &&
            transform.position.x - player.transform.position.x >= -0.35f)
        {
            p = false;
        }
        else
        {
            p = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Fire")
        {
            Health -= 25;
            if (Health <= 0)
            {
                anm.Play("Enemy3_Dead");
                Destroy(gameObject, 0.8f);
            }
        }
    }
    private void Movement()
    {
        if (p)
        {
            anm.SetBool("Walk", true);
            anm.SetBool("Attack", false);
            Enemy3Attack.SetActive(false);
            smoothTime = 1.5f;
            transform.position = Vector3.SmoothDamp(transform.position,
                new Vector3(player.position.x, transform.position.y, player.position.z), ref smoothVelocity,
                smoothTime);
        }
        else
        {
            anm.SetBool("Walk", false);
            anm.SetBool("Attack", true);
            Enemy3Attack.SetActive(true);
            smoothTime = 100;
        }

        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1.85f, transform.localScale.y, transform.localScale.z);
        }

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1.85f, transform.localScale.y, transform.localScale.z);
        }
    }
}
