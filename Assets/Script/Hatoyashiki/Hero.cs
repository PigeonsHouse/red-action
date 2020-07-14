using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour{
    public static bool bossmaeChecker = false;  //ボス前フラグ
    public GameObject fire;                     //ファイアを指定
    public GameObject rock;                     //ロックを指定
    public GameObject thunderbolt;              //サンダーを指定
    public Camera camera;                       //カメラ保存用変数
    public LayerMask groundLayer;               //レイヤー保存用変数
    public float speed;                         //歩く速さ
    public float jumpforce;                     //ジャンプ力
    public float logLR = 1;                     //fire.csに渡す用プレイヤーの左右向き
    public float minstagelocate;                //一番左端の座標
    public float fallposision;                  //落ちる時のy座標
    public int maxlife;                         //上限ライフ
    private int life;                           //ライフ
    private int count_hit = 0;                  //無敵時間のカウンタ
    private int count_boss = -1;                //ボス部屋侵入用カウンタ
    private int count_clear = -1;               //クリア後待機時間のカウンタ
    private float checkLR = 1;                  //プレイヤーの左右向き
    private float hori;                         //左右入力の格納変数
    private string moveFloorTag = "Movefloor";  //タグ名保存用変数
    private bool canMove = true;                //移動用命令を読むかのフラグ
    private bool isHit = false;                 //ヒットしたかのフラグ
    private bool isHit_anim = false;            //アニメ用のヒットフラグ
    private bool isGround;                      //接地フラグ
    private bool area1;                         //坂道上部フラグ
    private bool area2;                         //坂道下部フラグ
    private bool isSloping;                     //坂道フラグ
    private bool isAttack;                      //攻撃フラグ
    private bool isAttackFire;                  //攻撃フラグ
    private bool isAttackRock;                  //攻撃フラグ
    private bool isAttackThunder;               //攻撃フラグ
    private bool isDead = false;                //死亡フラグ
    private bool isDeadTri = false;             //死亡トリガー
    private bool inBossRoom = false;            //ボス部屋にいるかのフラグ
    private GameObject movFlo;                  //ロックを指定
    private Rigidbody2D rb2d;                   //ゲット用の変数
    private SpriteRenderer spRenderer;          //ゲット用の変数
    private Animator anim;                      //ゲット用の変数
    private Notslipground moveGra;              //動く床のスクリプト格納用変数
    private lifeUIcon lifescr;                  //ライフUIのスクリプト格納用変数


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
            hori = Input.GetAxisRaw("Horizontal");                       //グラフィック左右反転処理
            if (!isAttackRock && !isAttackThunder){
                if ( hori < 0 ){
                    spRenderer.flipX = true;
                    checkLR = -1;
                } else if ( hori > 0 ) {
                    spRenderer.flipX = false;
                    checkLR = 1;
                }
                if(isSloping){
                    gameObject.transform.Translate(0.1f * hori, 0f, 0f);
                    //Vector2 slope = new Vector2( 14f, 8f );
                    //rb2d.AddForce( slope * speed * hori * Time.deltaTime );     //左右移動処理
                }else{
                    rb2d.AddForce( Vector2.right * speed * hori * Time.deltaTime );     //左右移動処理
                }
            }

            if ( Input.GetKeyDown(KeyCode.K) && isGround && !isAttackRock ){                     //ジャンプ処理
                rb2d.AddForce( Vector2.up * jumpforce , ForceMode2D.Impulse);
            }

            if ( Input.GetKeyDown(KeyCode.J) && !( Input.GetKey(KeyCode.W) ) && !( Input.GetKey(KeyCode.S) ) && !isAttack ){      //火球の生成処理
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

        if ( life <= 0 ){
            if (!isDead)isDeadTri = true;
            isDead = true;
        }
        if ( transform.position.y < fallposision){
            isDead = true;
        }

        if (isHit && count_hit == 0) {
            rb2d.velocity = new Vector2( 0f, rb2d.velocity.y );
            rb2d.AddForce( new Vector2(checkLR * -1f, 1f) * 8f , ForceMode2D.Impulse);
            count_hit = 360; // 無敵時間をセット
            gameObject.layer = LayerMask.NameToLayer("PLdamage");
        }
        if(count_hit < (360 - 100)){
            isHit_anim = false;
        }
        if (count_hit > 0) {
            if (count_hit % 4 == 2 || count_hit % 4 == 3){
                spRenderer.color = new Color(1f, 1f, 1f, 0f);
            }else{
                spRenderer.color = new Color(1f, 1f, 1f, 255f);
            }
            --count_hit;
            if (count_hit <= 0) {
                // 無敵時間の終わり
                count_hit = 0;
                isHit = false;
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }

        anim.SetFloat("Speed", Mathf.Abs(hori * speed));
        anim.SetBool("isGround", isGround );
        anim.SetBool("isHit", isHit_anim );
        anim.SetBool("isAttackFire", isAttackFire );
        anim.SetBool("isAttackRock", isAttackRock );
        anim.SetBool("isAttackThunder", isAttackThunder );
        anim.SetBool("isDead", isDead );

        if (SceneManager.GetActiveScene().name == "Game"){
            if(inBossRoom == true){
                minstagelocate = 339.4f;
            }
            if (transform.position.x >= 338 && inBossRoom == false){
                canMove = false;
                if (count_boss < 0){
                    rb2d.velocity = new Vector2( 0f, velY );
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
                    count_clear = 800;
                }
                if(count_clear > 0){
                    --count_clear;
                }
                if(count_clear == 0){
                    SceneManager.LoadScene("Gamesecond");
                }
            }            
            if(isDead){
                rb2d.velocity = new Vector2( 0f, rb2d.velocity.y );
                rb2d.AddForce( Vector2.down * 5f);
                lifescr.SetPlayerLifeUI(0);
                if(isDeadTri){
                    rb2d.velocity = new Vector2( 0f, rb2d.velocity.y );
                    rb2d.AddForce( Vector2.up * 20f , ForceMode2D.Impulse);
                    Destroy(GetComponent<CapsuleCollider2D>());
                    isDeadTri = false;
                }
                if (transform.position.y < -110f ){
                    SceneManager.LoadScene("Game");
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "Gamesecond"){
/*            if(inBossRoom == true){
                minstagelocate = 339.4f;
            }
/*            if (transform.position.x >= 338 && inBossRoom == false){
                canMove = false;
                if (count_boss < 0){
                    rb2d.velocity = new Vector2( 0f, velY );
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
            if (GameObject.Find("boss02") == null){
                canMove = false;
                if(count_clear < 0){
                    GameObject.Find("BGM").GetComponent<ChangeMusic>().Change(3);
                    count_clear = 800;
                }
                if(count_clear > 0){
                    --count_clear;
                }
                if(count_clear == 0){
                    SceneManager.LoadScene("Gamesecond");
                }
            }*/
            if(isDead){
                rb2d.velocity = new Vector2( 0f, rb2d.velocity.y );
                rb2d.AddForce( Vector2.down * 5f);
                lifescr.SetPlayerLifeUI(0);
                if(isDeadTri){
                    rb2d.velocity = new Vector2( 0f, rb2d.velocity.y );
                    rb2d.AddForce( Vector2.up * 20f , ForceMode2D.Impulse);
                    Destroy(GetComponent<CapsuleCollider2D>());
                    isDeadTri = false;
                }
                if (transform.position.y < -110f ){
                    SceneManager.LoadScene("Gamesecond");
                }
            }
        }
    }

    void FixedUpdate(){
        isGround = false;
        isSloping = false;
        Vector2 groundPos = new Vector2 ( transform.position.x, transform.position.y - 1.5f );
        Vector2 groundArea = new Vector2( 0.14f, 0.5f );
        Vector2 wallArea1 = new Vector2 ( 0.3f * hori, 1.0f );
        Vector2 wallArea2 = new Vector2 ( 0.8f * hori, 2.5f );
        Vector2 wallArea3 = new Vector2 ( 0.6f * hori, 0.2f );
        Vector2 wallArea4 = new Vector2 ( 1.1f * hori, 1.0f );
        area1 = Physics2D.OverlapArea( groundPos + wallArea1, groundPos + wallArea2, groundLayer );
        area2 = Physics2D.OverlapArea( groundPos + wallArea3, groundPos + wallArea4, groundLayer );
        isGround = Physics2D.OverlapArea( groundPos + groundArea, groundPos - groundArea, groundLayer );
        if (!area1 && area2){
            isSloping = true;
        }else{
            isSloping = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if (canMove){
            if (col.gameObject.tag == "Enemy1" && count_hit <= 0){
                life -= 1;
                lifescr.SetPlayerLifeUI(life);
                isHit = true;
                isHit_anim = true;
            }else if(col.collider.tag == "Movefloor"){
                movFlo = col.gameObject;
            }
        }
    }

    void OnCollisionExit2D(Collision2D col){
        movFlo = null;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(canMove){
            if (collider.gameObject.tag == "Enemy1" && count_hit <= 0){
                life -= 1;
                lifescr.SetPlayerLifeUI(life);
                isHit = true;
                isHit_anim = true;
            }
            if (collider.gameObject.tag == "Food"){
                if (life < maxlife) life += 1;
                lifescr.SetPlayerLifeUI(life);
            }
        }
    }
}