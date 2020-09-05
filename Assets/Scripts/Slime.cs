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
    public float kbSpeed;
    public float minSpeed;
    public float moveSpeed;
    public float maxSpeed;
    public float minScale = .5f;
    public float maxScale = 10f;
    public int fix = 1;


     float actionTime = .2f;
     float currentTime = 0;

    public GameObject explosion;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.isKinematic = true;
        fix = 0;


        if (collision.gameObject.transform.GetChild(0).tag == gameObject.tag || collision.gameObject.transform.GetChild(0).tag == "Rainbow")
        {
            if (gameObject.transform.localScale.x < maxScale)
            {
                gameObject.transform.localScale += new Vector3(.2f, .2f, 0f);

            }
            else
            {
                Instantiate(explosion,transform.position,explosion.transform.rotation);
                Destroy(gameObject);
            }

            if (moveSpeed > minSpeed)
            {
                moveSpeed -= .25f;
            }

        }
        else if (collision.gameObject.transform.tag == "Bullet")
        {
            if (gameObject.transform.localScale.x > minScale)
            {
                gameObject.transform.localScale += new Vector3(-.2f, -.2f, 0f);
            }

            if (moveSpeed < maxSpeed)
            {
                moveSpeed += 1f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(moveSpeed < maxSpeed) { moveSpeed += .0001f; }
       
        currentTime += Time.deltaTime;
        if(currentTime >= actionTime)
        {
            currentTime = 0;
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
        }


   

        if (fix == 0)
        {
            MoveCharacter(-movement, kbSpeed);
            rb.isKinematic = false;
            fix = 1;
        }

    }

    void FixedUpdate()
    {
         Debug.Log("move"); MoveCharacter(movement, moveSpeed); 

    }

    private void MoveCharacter(Vector2 direction, float speed)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    IEnumerator KnockBack()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);

    }
}
