using UnityEngine;
public class Fire_Controller : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Animator anm;
    private float speed = 10f;
    void Update()
    {
        Destroy(gameObject, 1.2f);
        if (Player_Controller.m)
        {
            FireMovement();
        }
    }
    void FireMovement()
    {
        anm.Play("Fire");
        rb2d.velocity = transform.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Spider" || col.gameObject.tag == "Enemy1" || col.gameObject.tag == "Enemy2" || col.gameObject.tag == "Skeleton" || col.gameObject.tag == "Wall" || col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy3")
        {
            Destroy(gameObject);
        }
    }
}
