using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public float fireSpeed = 2000f;     // 炎のスピード
    private GameObject hero;            // オブジェクト用変数
    private Hero heroScr;         // スクリプト用変数
    private Rigidbody2D rb2d;           // ゲット用関数
    private SpriteRenderer spRenderer;  // ゲット用関数
    private float heroLR = 1;
    private Vector3 defalutScale;//playerの向きを格納する配列
    // Start is called before the first frame update
    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        hero = GameObject.Find( "Hero" );
        heroScr = hero.GetComponent<Hero>();
        heroLR = heroScr.logLR;
        defalutScale = transform.localScale;
    }

    // Update is called once per frame
    void Update(){
        float velFirex = rb2d.velocity.x;
        float velFirey = rb2d.velocity.y;

        if( heroLR < 0 ){
            transform.localScale = -defalutScale;
        }
        Vector2 Movement = new Vector2 (fireSpeed * heroLR, 0);
        rb2d.AddForce(Movement * 2, ForceMode2D.Impulse);
        if( Mathf.Abs(velFirex) > 20 ){
            if( velFirex > 20.0f ){
                rb2d.velocity = new Vector2( 20.0f, velFirey );
            }
            if( velFirex < -20.0f ){
                rb2d.velocity = new Vector2( -20.0f, velFirey );
            }
        }
        Destroy( gameObject, 0.5f );
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Enemy1" || col.gameObject.layer == 8 ){
            Destroy( gameObject );
        }
    }

}
