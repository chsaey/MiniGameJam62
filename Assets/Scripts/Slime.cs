using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    float kbSpeed=1;

    float minSpeed;
    float maxSpeed;

    float moveSpeed;

    float minScale;
    float maxScale;

    float scale;
    float slow;
    float fast;

    public int knockback = 3;

    //direction update
    float actionTime;
    float currentTime = 0;

    public GameObject explosion;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        switch (gameObject.tag)
        {
            case "Red":
                minSpeed = 8f;
                maxSpeed = 12f;
                moveSpeed = 8f;
                minScale = .5f;
                maxScale = 8f;
                actionTime = .1f;
                scale = .2f;
                fast = .8f;
                slow = .5f;
                break;
            case "Blue":
                minSpeed = 5f;
                maxSpeed = 8f;
                moveSpeed = 5f;
                minScale = .5f;
                maxScale = 12f;
                actionTime = .5f;
                scale = .2f;
                fast = .5f;
                slow = .2f;
                break;
            case "Green":
                minSpeed = 10f;
                maxSpeed = 20f;
                moveSpeed = 15f;
                minScale = .5f;
                maxScale = 5f;
                actionTime = 2f;
                scale = .2f;
                fast = 1f;
                slow = 1f;
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.isKinematic = true;
        knockback = 0;


        if (collision.gameObject.transform.GetChild(0).tag == gameObject.tag || collision.gameObject.transform.GetChild(0).tag == "Rainbow")
        {
            if (gameObject.transform.localScale.x < maxScale)
            {
                gameObject.transform.localScale += new Vector3(scale, scale, 0f);
            }
            else
            {
                Instantiate(explosion, transform.position, explosion.transform.rotation);
                Destroy(gameObject);
                Destroy(explosion, 5);
            }

            if (moveSpeed > minSpeed)
            {
                moveSpeed -= slow;
            }

        }
        else if (collision.gameObject.transform.tag == "Bullet")
        {
            if (gameObject.transform.localScale.x > minScale)
            {
                gameObject.transform.localScale += new Vector3(-scale, -scale, 0f);
            }
            if (moveSpeed < maxSpeed)
            {
                moveSpeed += fast;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveSpeed < maxSpeed) { moveSpeed += .005f; }
        currentTime += Time.deltaTime;


        if (currentTime >= actionTime)
        {
            currentTime = 0;
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
        }

        if (knockback == 0)
        {
            rb.isKinematic = true;
            MoveCharacter(-movement, kbSpeed);
            rb.isKinematic = false;
            knockback = 1;
        }

    }

    void FixedUpdate()
    {
        MoveCharacter(movement, moveSpeed);

    }

    private void MoveCharacter(Vector2 direction, float speed)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
}
