using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    public GameObject fire;             //ファイアを指定
    public GameObject rock;             //ロックを指定
    public GameObject thunderbolt;      //サンダーを指定
    public float speed = 30;            //歩く速さ
    public float jumpforce = 600f;      //ジャンプ力
    public LayerMask groundLayer;       //レイヤー保存用変数
    public float logLR = 1;             //fire.csに渡す用プレイヤーの左右向き
    public float minstagelocate;        //一番左端の座標
    public float fallposision = -10;    //落ちる時のy座標
    private float checkLR = 1;          //プレイヤーの左右向き
    private bool isGround;              //接地フラグ
    private bool isAttack;              //攻撃フラグ
    private int life = 5;               //ライフ
    private Rigidbody2D rb2d;           //ゲット用の変数
    private SpriteRenderer spRenderer;  //ゲット用の変数

    // Start is called before the first frame update
    void Start(){
        Debug.Log(life);
        this.rb2d = GetComponent<Rigidbody2D>();
        this.spRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        float hori = Input.GetAxisRaw("Horizontal");                       //グラフィック左右反転処理
        if ( hori < 0 ){
            spRenderer.flipX = true;
            checkLR = -1;
        }else if ( hori > 0 ){
            spRenderer.flipX = false;
            checkLR = 1;
        }

        rb2d.AddForce( Vector2.right * speed * hori * Time.deltaTime );     //左右移動処理

        float velX = rb2d.velocity.x;                                       //最大速度調整
        float velY = rb2d.velocity.y;
        if( transform.position.x < minstagelocate ){
            transform.position = new Vector2( minstagelocate, transform.position.y );
            rb2d.velocity = new Vector2( 0f, velY );
        }
        if( Mathf.Abs(velX) > 8 ){
            if( velX > 8.0f ){
                rb2d.velocity = new Vector2( 8.0f, velY );
            }
            if( velX < -8.0f ){
                rb2d.velocity = new Vector2( -8.0f, velY );
            }
        }

        if ( Input.GetButtonDown("Jump") && isGround ){                     //ジャンプ処理
            rb2d.AddForce( Vector2.up * jumpforce , ForceMode2D.Impulse);
        }
        if ( !Input.GetButton("Jump") && !isGround ){
            rb2d.AddForce( Vector2.down * 9);
        }

        if( !( GameObject.Find("fire(Clone)") ) && !( GameObject.Find("rock(Clone)") ) && !( GameObject.Find("thunder(Clone)") ) ){       //攻撃中でない場合フラグを降ろす
            isAttack = false;
        }

        if ( Input.GetKeyDown("x") && !( Input.GetKey("up") ) && !( Input.GetKey("down") ) && !isAttack ){      //火球の生成処理
            isAttack = true;
            logLR = checkLR;
            //火球の複製
			GameObject bullet;
            //複製した火球を生成
			bullet = Instantiate (fire);
            //火球の位置をplayerの位置に設定
			bullet.transform.position = transform.position + new Vector3(0f, -0.7f, 0f);
        }

        if ( Input.GetKeyDown("x") && !( Input.GetKey("up") ) && Input.GetKey("down") && !isAttack && isGround ){   //岩の生成処理
            isAttack = true;
            //岩の複製
			GameObject rock_0;
			GameObject rock_1;
            //複製した弾丸を生成
			rock_0 = Instantiate (rock);
			rock_1 = Instantiate (rock);
            //弾丸の位置をplayerの前に設定
			rock_0.transform.position = transform.position + new Vector3(2f, 0.5f, 0f);
			rock_1.transform.position = transform.position + new Vector3(-2f, 0.5f, 0f);
        }

        if ( Input.GetKeyDown("x") && Input.GetKey("up") && !( Input.GetKey("down") ) && !isAttack ){      //雷の生成処理
            isAttack = true;
            //落ちるy座標の取得
            Vector2 thunOrigin = new Vector2( transform.position.x + 7f * checkLR, 6.1f );
            RaycastHit2D thunHit = Physics2D.Raycast( thunOrigin, Vector2.down, 12.1f );
            if ( thunHit.point.y < transform.position.y - 8f ){
                thunHit.point = new Vector2(thunHit.point.x, transform.position.y - 8f);
            }
            //雷の複製
			GameObject thunder;
            //複製した雷を生成
			thunder = Instantiate (thunderbolt);
            //雷の位置をplayerの位置に設定
			thunder.transform.position = new Vector3(transform.position.x + 7f * checkLR, thunHit.point.y, 0f);
        }
        if ( life <= 0 && GameObject.Find("Hero") ){
            Destroy(gameObject);
            SceneManager.LoadScene("ActionScene");
        }
        if ( transform.position.y < fallposision ){
            Destroy(gameObject);
            SceneManager.LoadScene("ActionScene");
        }
    }

    void FixedUpdate(){

        isGround = false;
        Vector2 groundPos =
        new Vector2 (
            transform.position.x,
            transform.position.y
        );
        Vector2 groundArea = new Vector2( 0.125f, 0.5f );
        Vector2 chousei = new Vector2( 0, 1.5f );
        isGround =
            Physics2D.OverlapArea(
                groundPos + groundArea - chousei, 
                groundPos - groundArea - chousei,
                groundLayer
            );

    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Enemy1"){
            life -= 1;
            Debug.Log(life);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "Enemy1"){
            life -= 1;
            Debug.Log(life);
        }
    }
}