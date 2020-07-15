using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2 : MonoBehaviour{
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
    public float speed;
    public float interval;
    public int life = 10;
    private float timer;
    private SpriteRenderer spRenderer;
    private Animator anim;
    private Rigidbody2D rb2d;
    private int checkLR = -1;
    private bool _isRendered = false;
    private bool on_damage;

    // Start is called before the first frame update
    void Start(){
        spRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        if (_isRendered) {
            transform.position += new Vector3(speed * checkLR * 0.01f, 0, 0);
            if(timer > interval){                                                               //animation切り替え
                anim.SetTrigger("attack");
                timer = 0.0f;
            } else {
                timer += Time.deltaTime;
            }
            if (life <= 0) {                                                                    //ライフ
                Destroy (gameObject);	
            }
        }
        if(on_damage){                                                                          // ダメージフラグがtrueで有れば点滅させる
            float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
            spRenderer.color = new Color(1f,1f,1f,level);
        }
        /*if (mutekiteimer > 2.3) {
            isMuteki = false;
            mutekiteimer = 0;
        }*/
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
    void OnDamageEffect(){                                                           //　ダメージを受けた際の動き
        on_damage = true;                                                           // ダメージフラグON
        //isMuteki = true;                                                            //無敵開始
        Vector2 huttobiVec = new Vector2( x_huutobi * checkLR * -1, y_huttobi );    // 吹っ飛びベクトルの作成
        rb2d.AddForce(huttobiVec * Time.deltaTime , ForceMode2D.Impulse);             // プレイヤーの位置を後ろに飛ばす
        StartCoroutine("WaitForIt");                                                // コルーチン開始
    }
    IEnumerator WaitForIt(){
        yield return new WaitForSeconds(mutekitime);                                     // 1秒間処理を止める
        on_damage = false;                                                      // １秒後ダメージフラグをfalseにして点滅を戻す
        spRenderer.color = new Color(1f,1f,1f,1f);
        //mutekiteimer += Time.deltaTime;
    }

}