using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;

    // Update is called once per frame
    void Update()
    {
        Object.Destroy(gameObject, 3.0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Object.Destroy(gameObject);
    }
    private void FixedUpdate()
    {
         //rockets?
        //gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, speed));
        gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.transform.up * speed;
    }
}
