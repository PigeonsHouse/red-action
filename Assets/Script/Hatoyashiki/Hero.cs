using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    public GameObject fire;                     //ファイアを指定
    public GameObject rock;                     //ロックを指定
    public GameObject thunderbolt;              //サンダーを指定
    public float speed = 30;                    //歩く速さ
    public float jumpforce = 600f;              //ジャンプ力
    public LayerMask groundLayer;               //レイヤー保存用変数
    public string moveFloorTag = "Movefloor";   //レイヤー保存用変数
    public float logLR = 1;                     //fire.csに渡す用プレイヤーの左右向き
    public float minstagelocate;                //一番左端の座標
    public float fallposision = -10;            //落ちる時のy座標
    public int maxlife = 5;                     //上限ライフ
    private int life;                           //ライフ
    private int count = 0;                      //無敵時間のカウンタ
    private float checkLR = 1;                  //プレイヤーの左右向き
    private bool isHit = false;                 //ヒットしたかのフラグ
    private bool isGround;                      //接地フラグ
    private bool isAttack;                      //攻撃フラグ
    private bool isAttackFire;                  //攻撃フラグ
    private bool isAttackRock;                  //攻撃フラグ
    private bool isAttackThunder;               //攻撃フラグ
    private bool isDead = false;                //死亡フラグ
    private bool isDeadTri = false;             //死亡トリガー
    private Rigidbody2D rb2d;                   //ゲット用の変数
    private SpriteRenderer spRenderer;          //ゲット用の変数
    private Animator anim;                      //ゲット用の変数
    private Notslipground moveGra;              //動く床のスクリプト格納用変数
    private lifeUIcon lifescr;                  //ライフUIのスクリプト格納用変数

    public Camera camera;
    private int count_boss = -1;
    private int count_clear = -1;
    private bool canMove = true;
    private bool inBossRoom = false;
    private float hori;

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        lifescr = GetComponent<lifeUIcon>();
        life = maxlife;
        lifescr.SetPlayerLifeUI(life);
    }
    
    void Update(){
        if (!isDead && canMove){
            hori = Input.GetAxisRaw("Horizontal");                       //グラフィック左右反転処理//////////////////////////////////////
            if (!isAttackRock && !isAttackThunder){
                if ( hori < 0 ){
                    spRenderer.flipX = true;
                    checkLR = -1;
                }else if ( hori > 0 ){
                    spRenderer.flipX = false;
                    checkLR = 1;
                }
                rb2d.AddForce( Vector2.right * speed * hori * Time.deltaTime );     //左右移動処理
            }

            if ( Input.GetKeyDown(KeyCode.K) && isGround && !isAttackRock ){                     //ジャンプ処理////////////////////////////////////////
                rb2d.AddForce( Vector2.up * jumpforce , ForceMode2D.Impulse);
            }

            if ( Input.GetKeyDown(KeyCode.J) && !( Input.GetKey(KeyCode.W) ) && !( Input.GetKey(KeyCode.S) ) && !isAttack ){      //火球の生成処理/////////////
                isAttack = true;
                isAttackFire = true;
                logLR = checkLR;
                //火球の複製
                GameObject bullet;
                //複製した火球を生成
                bullet = Instantiate (fire);
                //火球の位置をplayerの位置に設定
                bullet.transform.position = transform.position + new Vector3(0f, -0.7f, 0f);
            }
            if ( Input.GetKeyDown(KeyCode.J) && !( Input.GetKey(KeyCode.W) ) && Input.GetKey(KeyCode.S) && !isAttack && isGround ){   //岩の生成処理
                isAttack = true;
                isAttackRock = true;
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
            if ( Input.GetKeyDown(KeyCode.J) && Input.GetKey(KeyCode.W) && !( Input.GetKey(KeyCode.S) ) && !isAttack ){      //雷の生成処理
                isAttack = true;
                isAttackThunder = true;
                //落ちるy座標の取得
                Vector2 thunOrigin = new Vector2( transform.position.x + 7f * checkLR, 6.1f );
                RaycastHit2D thunHit = Physics2D.Raycast( thunOrigin, Vector2.down, 130.1f );
                /*if ( thunHit.point.y < transform.position.y - 8f ){
                    thunHit.point = new Vector2(thunHit.point.x, transform.position.y - 8f);
                }*/
                if ( thunHit.collider == null ){
                    thunHit.point = new Vector2(thunHit.point.x, -8.032941f);
                } 
                //雷の複製
                GameObject thunder;
                //複製した雷を生成
                thunder = Instantiate (thunderbolt);
                //雷の位置をplayerの位置に設定
                thunder.transform.position = new Vector3(transform.position.x + 7f * checkLR, thunHit.point.y, 0f);
            }
        }
        float velX = rb2d.velocity.x;
        float velY = rb2d.velocity.y;
            
                
        if( transform.position.x < minstagelocate ){
            transform.position = new Vector2( minstagelocate, transform.position.y );
            rb2d.velocity = new Vector2( 0f, velY );
        }

        if (isAttackRock || isAttackThunder){
            rb2d.velocity = new Vector2( 0f, velY );
        }

        if( Mathf.Abs(velX) > 8 ){                                       //最大速度調整
            if( velX > 8.0f ){
                rb2d.velocity = new Vector2( 8.0f, velY );
            }
            if( velX < -8.0f ){
                rb2d.velocity = new Vector2( -8.0f, velY );
            }
        }

        if ( !Input.GetKey(KeyCode.K) && velY > 0 ){
            rb2d.AddForce( Vector2.down * 20f);
        }

        if( !( GameObject.Find("fire(Clone)") ) && !( GameObject.Find("rock(Clone)") ) && !( GameObject.Find("thunder(Clone)") ) ){       //攻撃中でない場合フラグを降ろす
            isAttackThunder = isAttackRock = isAttackFire = isAttack = false;
        }

        if ( life <= 0 && GameObject.Find("Hero") ){
            isDead = true;
            isDeadTri = true;
        }
        if ( transform.position.y < fallposision){
            isDead = true;
        }

        if (isHit && count == 0) {
            rb2d.velocity = new Vector2( 0f, rb2d.velocity.y );
            rb2d.AddForce( new Vector2(checkLR * -1f, 1f) * 8f , ForceMode2D.Impulse);
            count = 360; // 無敵時間をセット
            gameObject.layer = LayerMask.NameToLayer("PLdamage");
        }
        if (count > 0) {
            if (count % 4 == 2 || count % 4 == 3){
                spRenderer.color = new Color(1f, 1f, 1f, 0f);
            }else{
                spRenderer.color = new Color(1f, 1f, 1f, 255f);
            }
            --count;
            if (count <= 0) {
                // 無敵時間の終わり
                count = 0;
                isHit = false;
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }

        anim.SetFloat("Speed", Mathf.Abs(hori * speed));
        anim.SetBool("isGround", isGround );            
        anim.SetBool("isAttackFire", isAttackFire );
        anim.SetBool("isAttackRock", isAttackRock );
        anim.SetBool("isAttackThunder", isAttackThunder );


        if (SceneManager.GetActiveScene().name == "Game"){
            if(inBossRoom == true){
                minstagelocate = 339.4f;
            }
            if (transform.position.x >= 338 && inBossRoom == false){
                canMove = false;
                if (count_boss < 0){
                    rb2d.velocity = new Vector2( 0f, velY );
                    hori = 0f;
                    count_boss = 500;
                }else if(count_boss >= 1){
                    --count_boss;
                }
                if (count_boss == 400){
                    GameObject.Find("BGM").GetComponent<ChangeMusic>().Change(2);
                }
                if(count_boss < 200){
                    camera.orthographicSize += 1f / 200f;
                    rb2d.AddForce( Vector2.right * speed * 1.5f * Time.deltaTime );
                }
                if(count_boss == 0){
                    canMove = true;
                    inBossRoom = true;
                }
            }
            if (GameObject.Find("boss01") == null){
                canMove = false;
                if(count_clear < 0){
                    GameObject.Find("BGM").GetComponent<ChangeMusic>().Change(3);
                    count_clear++;
                }
            }
        }


        anim.SetBool("isDead", isDead );
        if(isDead){
            rb2d.velocity = new Vector2( 0f, rb2d.velocity.y );
            rb2d.AddForce( Vector2.down * 5f);
            lifescr.SetPlayerLifeUI(0);
            if(isDeadTri){
                rb2d.velocity = new Vector2( 0f, rb2d.velocity.y );
                rb2d.AddForce( Vector2.up * 11f , ForceMode2D.Impulse);
                Destroy(GetComponent<CapsuleCollider2D>());
                isDeadTri = false;
            }
            if (transform.position.y < -110f ){
                SceneManager.LoadScene("Game");
            }
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
        if (canMove){
            if (col.gameObject.tag == "Enemy1" && count <= 0){
                life -= 1;
                lifescr.SetPlayerLifeUI(life);
                isHit = true;
            }else if(col.collider.tag == "Movefloor"){
                moveGra = col.gameObject.GetComponent<Notslipground>();
            }
        }
    }

    void OnCollisionExit2D(Collision2D col){
        moveGra = null;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(canMove){
            if (collider.gameObject.tag == "Enemy1" && count <= 0){
                life -= 1;
                lifescr.SetPlayerLifeUI(life);
                isHit = true;
            }
            if (collider.gameObject.tag == "Food"){
                if (life < maxlife) life += 1;
                lifescr.SetPlayerLifeUI(life);
            }
        }
    }
}