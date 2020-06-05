using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float speed = 30;
    public float jumpforce = 600f;
    private Rigidbody2D rb2d;
    private SpriteRenderer spRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();        
        this.spRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        if ( hori < 0 ){
            spRenderer.flipX = true;
        }else if ( hori > 0 ){
            spRenderer.flipX = false;
        }

        if ( Input.GetButtonDown("Jump") ){

            rb2d.AddForce( Vector2.up * jumpforce );

        }

        rb2d.AddForce( Vector2.right * speed * hori );
    }
}
