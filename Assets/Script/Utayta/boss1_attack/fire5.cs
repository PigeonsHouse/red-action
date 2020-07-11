﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire5 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 defaultPos;
    public float speed = 3.0f;
    public float maxDistance = 100.0f;
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
            rb.MovePosition(transform.position += new Vector3(2, 1, 0) * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(this.gameObject);
    }
}