using UnityEngine;
public class Enemy2_Controller : MonoBehaviour
{
    public Animator anm;
    public int Health = 100;
    private bool c;
    private bool _c;
    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private float speed = 1.5f;
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>(); 
        rb = GetComponent<Rigidbody2D>(); 
    }
    void Update()
    {
        _Movement();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Fire")
        {
            Health -= 10;
            if (Health <= 0)
            {
                anm.Play("Enemy2_Dead");
                Destroy(gameObject, 0.8f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "E")
        {
            c = true;
            _c = false;
        }
        else if(other.gameObject.tag == "E1")
        {
            c = false;
            _c = true;
        }
    }
    private void _Movement()
    {
        if (c)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            rb.velocity = Vector2.right * speed;
            anm.SetBool("Walk", true);
        }
        else if(_c)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            rb.velocity = Vector2.left * speed;
            anm.SetBool("Walk", true);
        }
    }
}