using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PlayerScript : MonoBehaviour
{
    private bool facingRight = true;
    private bool isGrounded = true;
    private bool isDoubleJumping = false;
    public int Point { get; set; }
    public int Health { get; set; }
    private Rigidbody2D rb;
    private Animator anim;
    public TMP_Text txtPoint;
    public TMP_Text txtHealth;
    public AudioSource fruitPop;

    void Start()
    {
        Health = 100;
        Point = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        txtPoint.text = GetPoint();
        txtHealth.text = GetHealth();
    }

    private void Movement(){
        anim.SetBool("isRunning", false);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRunning", true);
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * 3;
            if (!facingRight)
            {
                transform.Rotate(0, 180, 0);
                facingRight = true;
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isRunning", true);
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * 3;
            if (facingRight)
            {
                transform.Rotate(0, 180, 0);
                facingRight = false;
            }
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(new Vector2(0, 250));
                isGrounded = false;
                anim.SetBool("isJumping", true);
            }
            //double jump
            if (!isGrounded && rb.velocity.y < 0 && !isDoubleJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, 250));
                isDoubleJumping = true;
                anim.SetBool("isDoubleJumping", true);
            }
        }
        //after jump to the highest point
        if (rb.velocity.y < 0)
        {
            anim.SetBool("isDoubleJumping", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb == null || anim == null)
        {
            Debug.LogError("Rigidbody2D or Animator component is not initialized.");
            return;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isDoubleJumping = false;
            anim.SetBool("isFalling", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        CollectItem(collider);
        BeAttaked(collider);
        AttackSlime(collider);
    }

    private void BeAttaked(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Left") || collider.gameObject.CompareTag("Right"))
        {
            Health -= 10;
            if(Health <= 0){
                Health = 0;
            }
        }
    }
    private void AttackSlime(Collider2D collider){
        if(collider.gameObject.CompareTag("Top")){
            var slime = collider.gameObject.GetComponentInParent<SlimeScript>();
            slime.SetDieTrigger();
            Point += 500;
        }
    }
    private void CollectItem(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cherry"))
        {
            fruitPop.Play();
            Destroy(collider.gameObject);
            Point += 100;
        }
        if (collider.gameObject.CompareTag("Apple"))
        {
            fruitPop.Play();
            Destroy(collider.gameObject);
            Point += 200;
        }
        if (collider.gameObject.CompareTag("Banana"))
        {
            fruitPop.Play();
            Destroy(collider.gameObject);
            Point += 300;
        }
    }
    private String GetPoint()
    {
        string point = Point.ToString();
        string _point = "00000000";
        _point = _point.Substring(0, _point.Length - point.Length);
        point = _point + point;
        // Debug.Log(point.Length);
        // if (point.Length < 6)
        // {
        //     for (int i = 0; i < 6 - point.Length; i++)
        //     {
        //         point = "0" + point;
        //     }
        // }
        // Debug.Log(point);
        return point;
    }
    private String GetHealth()
    {
        string health = Health.ToString();
        if (health.Length < 3)
        {
            for (int i = 0; i < 3 - health.Length; i++)
            {
                health = "0" + health;
            }
        }
        return health;
    }
}
