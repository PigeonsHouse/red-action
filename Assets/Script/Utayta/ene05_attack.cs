using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ene05_attack : MonoBehaviour
{
    public float speed = 3.0f;
    public float maxDistance = 100.0f;

    private Rigidbody2D rb;
    private Vector2 defaultPos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(rb == null) {
            Debug.Log("設定が足りません");
            Destroy(this.gameObject);
        }
        defaultPos = transform.position;
    }
    void FixedUpdate()
    {
        float d = Vector3.Distance(transform.position,defaultPos);

        //最大移動距離を超えている
        if (d > maxDistance) {
            Destroy(this.gameObject);
        } else {
            rb.MovePosition(transform.position += Vector3.down * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
