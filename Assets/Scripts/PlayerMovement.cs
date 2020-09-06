using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 11f;
    public Rigidbody2D rb;
    public GameObject[] bullet;
    public int color = 0;
    public int rainbow = 0;

    Vector2 movement;

    float shotCoolDown = .01f;
    float currentTime = 0;
    int cd = 0;


    // Use for input
    void Update()
    {
        currentTime += Time.deltaTime;
   

        faceMouse();
        //movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //set color

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            color = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){
            color = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){
            color = 2;
        }

        if (rainbow == 1)
        {
            color = 3;
        }
        //fire
        if (Input.GetButton("Fire1"))
        {
            if (cd == 0) {
                Instantiate(bullet[color], gameObject.transform.GetChild(0).position, transform.rotation);
                cd = 1;
            }

            if (currentTime >= shotCoolDown)
            {
                currentTime = 0;
                cd = 0;
            }

        }
    }

    //used or physics since update can be unreliable
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); 
    }
    void faceMouse() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(
        mousePosition.x - transform.position.x,
        mousePosition.y - transform.position.y
        );
        transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     if(collision.transform.tag == "Rainbow")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Rainbow());
        }
       else if (collision.transform.tag == "Speed")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Speed());
        }
        else if (collision.transform.tag == "FireRate")
        {
            Destroy(collision.gameObject);
            StartCoroutine(FireRate());
        }
    }

    IEnumerator Rainbow()
    {        // Start function WaitAndPrint as a coroutine
        rainbow = 1;
        yield return new WaitForSeconds(15);
        rainbow = 0;
        color = 0;
    }
    IEnumerator Speed()
    {        // Start function WaitAndPrint as a coroutine
        moveSpeed = 25f;
        yield return new WaitForSeconds(15);
        moveSpeed = 11f;
    }
    IEnumerator FireRate()
    {        // Start function WaitAndPrint as a coroutine
        shotCoolDown = 0;
        yield return new WaitForSeconds(15);
        shotCoolDown = .01f;
    }

}
