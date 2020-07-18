using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2 : MonoBehaviour{
    public GameObject fire;
    public GameObject hero;
    public float interval;
    public float speed;
    public int life = 15;
    public int mutekitime;
    public float x_huutobi;
    public float y_huttobi;
    public float x_jump;
    public float y_jump;
    public float ret_x1;
    public float ret_x2;
    public float ret_x3;
    public float ret_x4;
    public float jmp_x1;
    public float jmp_x2;
    public float jmp_x3;
    public float jmp_x4;
    public float pos_y1;
    public float pos_y2;
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
    private float minstagelocate = 1265.4f;
    private float timer;
    private SpriteRenderer spRenderer;
    private Animator anim;
    private Rigidbody2D rb2d;
    private int checkLR = -1;
    private bool _isRendered = false;
    private bool on_damage;
    private bool haigo;
    private Vector2 ret1;
    private Vector2 ret2;
    private Vector2 ret3;
    private Vector2 ret4;
    private Vector2 jmp1;
    private Vector2 jmp2;
    private Vector2 jmp3;
    private Vector2 jmp4;

    // Start is called before the first frame update
    void Start(){
        ret1 = new Vector2( ret_x1, pos_y1 );
        ret2 = new Vector2( ret_x2, pos_y2 );
        ret3 = new Vector2( ret_x3, pos_y2 );
        ret4 = new Vector2( ret_x4, pos_y1 );
        jmp1 = new Vector2( jmp_x1, pos_y2 );
        jmp2 = new Vector2( jmp_x2, pos_y1 );
        jmp3 = new Vector2( jmp_x3, pos_y1 );
        jmp4 = new Vector2( jmp_x4, pos_y2 );
        spRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        if (_isRendered) {
            transform.position += new Vector3(speed * checkLR * 0.01f * Time.deltaTime, 0, 0);
            if(timer > interval){                                                               //animation切り替え
                anim.SetTrigger("attack");
                timer = 0.0f;
            } else {
                timer += Time.deltaTime;
            }
            if (life <= 0) {                                                                    //ライフ
                Destroy (gameObject);	
            }
            if(checkLR == -1){
                TurnCheck(ret1);
                TurnCheck(ret2);
                JumpCheck(jmp2);
                JumpCheck(jmp4);
                if(transform.position.x < ret_x1){
                    checkLR *= -1;
                    spRenderer.flipX = !spRenderer.flipX;
                }
            }
            if(checkLR == 1){
                TurnCheck(ret3);
                TurnCheck(ret4);
                JumpCheck(jmp1);
                JumpCheck(jmp3);
                if(transform.position.x > ret_x4){
                    checkLR *= -1;
                    spRenderer.flipX = !spRenderer.flipX;
                }
            }
        }
        if(on_damage){                                                                          // ダメージフラグがtrueで有れば点滅させる
            float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
            spRenderer.color = new Color(1f,1f,1f,level);
        }
        if( (hero.transform.position.x > transform.position.x && checkLR == -1) || (hero.transform.position.x < transform.position.x && checkLR == 1)){
            haigo = true;
        }else{
            haigo = false;
        }
        float velX = rb2d.velocity.x;
        float velY = rb2d.velocity.y;
        if( transform.position.x < minstagelocate ){
            transform.position = new Vector2( minstagelocate, transform.position.y );
            rb2d.velocity = new Vector2( 0f, velY );
        }
    }
    
    void Attack(){
        if (life < 8){
            speed = 900f;
            interval = 3;
            GameObject fires1 = Instantiate(fire);
            GameObject fires2 = Instantiate(fire);
            GameObject fires3 = Instantiate(fire);
            GameObject fires4 = Instantiate(fire);
            GameObject fires5 = Instantiate(fire);
            fires1.transform.position = transform.position + new Vector3(2.7f * checkLR, 0f, 0f);
            fires2.transform.position = transform.position + new Vector3(1.8f * checkLR, 1.3f, 0f);
            fires3.transform.position = transform.position + new Vector3(1.8f * checkLR, -1.3f, 0f);
            fires4.transform.position = transform.position + new Vector3(0.6f * checkLR, 2.4f, 0f);
            fires5.transform.position = transform.position + new Vector3(0.6f * checkLR, -2.4f, 0f);
            fires1.transform.localScale = new Vector3(2f, -2f * checkLR, 1f);
            fires2.transform.localScale = new Vector3(2f, -2f * checkLR, 1f);
            fires3.transform.localScale = new Vector3(2f, -2f * checkLR, 1f);
            fires4.transform.localScale = new Vector3(2f, -2f * checkLR, 1f);
            fires5.transform.localScale = new Vector3(2f, -2f * checkLR, 1f);
            fires2.transform.Rotate(0f, 0f, 30f * checkLR);
            fires3.transform.Rotate(0f, 0f, -30f * checkLR);
            fires4.transform.Rotate(0f, 0f, 60f * checkLR);
            fires5.transform.Rotate(0f, 0f, -60f * checkLR);
        }else{
            GameObject fires1 = Instantiate(fire);
            GameObject fires2 = Instantiate(fire);
            GameObject fires3 = Instantiate(fire);
            fires1.transform.position = transform.position + new Vector3(2.7f * checkLR, 0f, 0f);
            fires2.transform.position = transform.position + new Vector3(1.8f * checkLR, 1.3f, 0f);
            fires3.transform.position = transform.position + new Vector3(1.8f * checkLR, -1.3f, 0f);
            fires1.transform.localScale = new Vector3(2f, -2f * checkLR, 1f);
            fires2.transform.localScale = new Vector3(2f, -2f * checkLR, 1f);
            fires3.transform.localScale = new Vector3(2f, -2f * checkLR, 1f);
            fires2.transform.Rotate(0f, 0f, 30f * checkLR);
            fires3.transform.Rotate(0f, 0f, -30f * checkLR);
        }
    }

    void JumpCheck( Vector2 checkPos ){
        if( Vector2.Distance( transform.position, checkPos ) < 0.25f ){
            Vector2 jumpVec = new Vector2( x_jump * checkLR, y_jump );
            rb2d.AddForce(jumpVec * Time.deltaTime , ForceMode2D.Impulse);
        }
    }

    void TurnCheck( Vector2 checkPos ){
        if( Vector2.Distance( transform.position, checkPos ) < 0.25f ){
            checkLR *= -1;
            spRenderer.flipX = !spRenderer.flipX;
        }
    }

    void OnWillRenderObject(){
    //メインカメラに映った時だけ_isRenderedをtrue
        if(Camera.current.tag == MAIN_CAMERA_TAG_NAME){
            _isRendered = true;
        }
    }
    void OnTriggerEnter2D (Collider2D col){
        if(on_damage == false) {
            if (col.gameObject.tag == "Fire" || col.gameObject.tag == "Thunder" || col.gameObject.tag == "Rock"){
                life--;
                anim.SetTrigger("knock");
                OnDamageEffect();
            }
        }
    }
    void OnDamageEffect(){                                                          //　ダメージを受けた際の動き
        on_damage = true;                                                           // ダメージフラグON
        Vector2 huttobiVec = new Vector2( -x_huutobi * checkLR, y_huttobi );        // 吹っ飛びベクトルの作成
        rb2d.AddForce(huttobiVec * Time.deltaTime, ForceMode2D.Impulse);            // プレイヤーの位置を後ろに飛ばす
        StartCoroutine("WaitForIt");                                                // コルーチン開始
    }
    IEnumerator WaitForIt(){
        yield return new WaitForSeconds(mutekitime);                                     // 1秒間処理を止める
        on_damage = false;                                                      // １秒後ダメージフラグをfalseにして点滅を戻す
        spRenderer.color = new Color(1f,1f,1f,1f);
    }
}