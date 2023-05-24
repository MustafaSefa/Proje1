using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TerrainTools;
using UnityEngine.UI;
using TMPro;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rb2d;
    private float Speed = 3.5f;
    public float MoveX;
    public Animator anm;
    private float Jump;
    private bool ground;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public GameObject FirePrefab;
    public Transform firePoint;
    public Transform firePoint1;
    public AnimationClip clip;
    private bool c = false;
    private bool isFacingRight;
    private float angle;
    public static bool m;
    private int p = 10;
    public int health = 100;
    public Image FireBar;
    public Image HealthBar;
    private Vector3 respawnPoint;
    private int _health;
    private float healthBar;
    private int ring;
    public TextMeshProUGUI r;
    private void Start()
    {
        respawnPoint = transform.position;
    }
    void Update()
    {
        StartCoroutine(CoUpdate());
        Movement();
        Health();
        Shoot();
    }
    IEnumerator CoUpdate()
    {
        if (Input.GetMouseButtonDown(0) && m)
        {
            p--;
            FireBar.fillAmount -= 0.10f;
            yield return new WaitForSeconds(2);
            p++;
            FireBar.fillAmount += 0.10f;
        }
        yield return null;
    }
    void Movement()
    {
        var settings = AnimationUtility.GetAnimationClipSettings(clip);
        ground = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        MoveX = Input.GetAxis("Horizontal");
        Jump = Input.GetAxis("Jump");
        rb2d.velocity = new Vector2(
            Speed * MoveX,
            rb2d.velocity.y);
        if (MoveX != 0)
        {
            m = true;
            anm.SetBool("Player_Run",true);
            if (MoveX < 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                isFacingRight = false;
            }
            else if (MoveX > 0)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                isFacingRight = true;
            }
        }
        else if (Input.GetButtonDown("Crouch"))
        {
            anm.SetBool("Player_Crouch",true);
            c = true;
            Speed = 0;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            settings.loopTime = true;
            anm.SetBool("Player_Crouch",false);
            c = false;
            Speed = 3.5f;
        }
        else if (Input.GetButtonDown("Jump") && ground == true)
        {
            anm.SetBool("Player_Jump",true);
            rb2d.velocity = new Vector2(
                rb2d.velocity.x,
                Jump * 5.83f);
        }
        else
        {
            anm.SetBool("Player_Jump",false);
            anm.SetBool("Player_Run",false);
        }
    }
    void Shoot()
    {
        if (c != true)
        {
            if (Input.GetMouseButtonDown(0) && m && p > 0)
            {
                angle = isFacingRight ? 0f : 180f;
                Instantiate(FirePrefab, firePoint.position,Quaternion.Euler(new Vector3(0f, 0f, angle)));
                anm.SetBool("Player_Shoot",true);
            }
            else
            {
                anm.SetBool("Player_Shoot",false);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && m && p > 0)
            {
                angle = isFacingRight ? 0f : 180f;
                Instantiate(FirePrefab, firePoint1.position,Quaternion.Euler(new Vector3(0f, 0f, angle)));
            }
        }
    }
    void Health()
    {
        if (health <= 0)
        {
            transform.position = respawnPoint;
            HealthBar.fillAmount = 1;
            health = 100;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "CheckPoint")
        {
            respawnPoint = transform.position;
        }
        else if (col.gameObject.tag == "Spider")
        {
            _health = 20;
            healthBar = 0.20f;
            InvokeRepeating("Damage", 0.5f,3);
        }
        else if (col.gameObject.tag == "Skeleton")
        {
            _health = 25;
            healthBar = 0.25f;
            InvokeRepeating("Damage", 0.5f,3);
        }
        else if (col.gameObject.tag == "Ring")
        {
            ring++;
            r.text = ring.ToString();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        CancelInvoke("Damage");
        anm.SetBool("Player_Hurt",false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy1Sword")
        {
            _health = 20;
            healthBar = 0.20f;
            InvokeRepeating("Damage",0.5f,3);
        }
        else if (col.gameObject.tag == "Enemy2Sword")
        {
            _health = 35;
            healthBar = 0.35f;
            InvokeRepeating("Damage",0.5f,3);
        }
        else if (col.gameObject.tag == "Enemy3Attack")
        {
            _health = 15;
            healthBar = 0.15f;
            InvokeRepeating("Damage",0.5f,3);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        CancelInvoke("Damage");
        anm.SetBool("Player_Hurt",false);
    }
    private void Damage()
    { 
        health -= _health;
        HealthBar.fillAmount -= healthBar;
        anm.SetBool("Player_Hurt",true);
    }
}
