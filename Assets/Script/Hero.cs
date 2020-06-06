using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float speed = 30;            //歩く速さ
    public float jumpforce = 600f;      //ジャンプ力
    public LayerMask groundLayer;       //レイヤー保存用変数
    private bool isGround;              //接地フラグ
    private Rigidbody2D rb2d;           //ゲット用の変数
    private SpriteRenderer spRenderer;  //ゲット用の変数

    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.spRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxisRaw("Horizontal");    //グラフィック左右反転処理
        if ( hori < 0 ){
            spRenderer.flipX = true;
        }else if ( hori > 0 ){
            spRenderer.flipX = false;
        }

        if ( Input.GetButtonDown("Jump") && isGround ){             //ジャンプ処理
            rb2d.AddForce( Vector2.up * jumpforce );
        }

        rb2d.AddForce( Vector2.right * speed * hori * Time.deltaTime );             //左右移動処理
    }

    void FixedUpdate(){

        isGround = false;

        Vector2 groundPos =
        new Vector2 (
            transform.position.x,
            transform.position.y
        );

        Vector2 groundArea = new Vector2( 0.5f, 0.5f );
        
        Vector2 chousei = new Vector2( 0, 1.5f );

        isGround =
            Physics2D.OverlapArea(
                groundPos + groundArea - chousei, 
                groundPos - groundArea - chousei,
                groundLayer
            );
        
    }
}