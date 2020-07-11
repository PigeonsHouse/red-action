using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire4 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 defaultPos;
    public float speed = 3.0f;
    public float maxDistance = 50.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultPos = transform.position;
    }
    void FixedUpdate()
    {
        float d = Vector3.Distance(transform.position,defaultPos);
        if (d > maxDistance) {
            Destroy(this.gameObject);
        } else {
            rb.MovePosition(transform.position += new Vector3(1, 2, 0) * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Hero" || collision.name == "fire(Clone)" || collision.name == "rock(Clone)" || collision.name == "thunder(Clone)" ) {
            Destroy(this.gameObject);
        }
    }
}
