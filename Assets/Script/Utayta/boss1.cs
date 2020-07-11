using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1 : MonoBehaviour
{
    private SpriteRenderer spRenderer;
    private Animator anim;
    Rigidbody2D rb;
    public GameObject fire1;             //ファイアを指定
    public GameObject fire2;             //ファイアを指定
    public GameObject fire3;             //ファイアを指定
    public GameObject fire4;             //ファイアを指定
    public GameObject fire5;             //ファイアを指定
    public GameObject fire6;             //ファイアを指定
    public GameObject fire7;             //ファイアを指定
    public GameObject fire8;             //ファイアを指定
    public GameObject fire9;             //ファイアを指定
    public GameObject fire10;             //ファイアを指定
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
	private bool _isRendered = false;
    public float timer;
    public float interval;
    private int life = 10;
    public float x1;    //fireの位置
    public float x2;
    public float x3;
    public float x4;
    public float x5;
    public float x6;
    public float x7;
    public float x8;
    public float x9;
    public float x10;
    public float y1;
    public float y2;
    public float y3;
    public float y4;
    public float y5;
    public float y6;
    public float y7;
    public float y8;
    public float y9;
    public float y10;
    public float turnPoint1;
    public float jumpPoint1;
    public float jumpPoint2;
    public float jumpPoint3;
    public float jumpPoint4;
    public float jumpPoint5;
    public float jumpPoint6;

    public float turnPoint2;
    public float y_highPos;
    public float y_midPos;
    public float y_lowPos;
    public float y_FPos;
    public float x_jump;
    public float y_jump;
    public float speed;
    private int checkLR = -1;
    Vector2 turnLU;
    Vector2 turnLD;
    Vector2 turnRU;
    Vector2 turnRD;
    Vector2 jumpLD;
    Vector2 jumpLM;
    Vector2 jumpRM;
    Vector2 jumpRD;
    Vector2 jumpFR;
    Vector2 jumpFL;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        turnLU = new Vector2( turnPoint1, y_highPos );
        turnLD = new Vector2( turnPoint1, y_lowPos  );
        turnRU = new Vector2( turnPoint2, y_highPos );
        turnRD = new Vector2( turnPoint2, y_lowPos  );
        jumpLD = new Vector2( jumpPoint1, y_lowPos  );
        jumpLM = new Vector2( jumpPoint2, y_midPos  );
        jumpRM = new Vector2( jumpPoint3, y_midPos  );
        jumpRD = new Vector2( jumpPoint4, y_lowPos  );
        jumpFR = new Vector2( jumpPoint5, y_FPos  );
        jumpFL = new Vector2( jumpPoint6, y_FPos  );
    }
    void Update()
    {
        Animator animator = GetComponent<Animator>();
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        if (_isRendered) {
            transform.position += new Vector3(speed * checkLR * 0.01f, 0, 0);
            if(checkLR ==  1){
                JumpCheck(jumpLD);
                JumpCheck(jumpRM);
                JumpCheck(jumpFR);
                TurnCheckL(turnRU);
                TurnCheckL(turnRD);
            }
            if(checkLR == -1){
                JumpCheck(jumpRD);
                JumpCheck(jumpLM);
                JumpCheck(jumpFL);
                TurnCheckR(turnLU);
                TurnCheckR(turnLD);
            }
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
        
    }
    public void Attack()
    {
        if (_isRendered) {
            GameObject bullet1;  //火球の複製
            GameObject bullet2;  //火球の複製
            GameObject bullet3;  //火球の複製
            GameObject bullet4;  //火球の複製
            GameObject bullet5;  //火球の複製
            GameObject bullet6; //火球の複製
            GameObject bullet7;  //火球の複製
            GameObject bullet8;  //火球の複製
            GameObject bullet9;  //火球の複製
            GameObject bullet10;  //火球の複製
            bullet1 = Instantiate (fire1);    //複製した火球を生成
            bullet2 = Instantiate (fire2);    //複製した火球を生成
            bullet3 = Instantiate (fire3);    //複製した火球を生成
            bullet4 = Instantiate (fire4);    //複製した火球を生成
            bullet5 = Instantiate (fire5);    //複製した火球を生成
            bullet6 = Instantiate (fire6);    //複製した火球を生成
            bullet7 = Instantiate (fire7);    //複製した火球を生成
            bullet8 = Instantiate (fire8);    //複製した火球を生成
            bullet9 = Instantiate (fire9);    //複製した火球を生成
            bullet10 = Instantiate (fire10);    //複製した火球を生成
            bullet1.transform.position = transform.position + new Vector3(x1, y1, 0f);    //火球の位置をplayerの位置に設定
            bullet2.transform.position = transform.position + new Vector3(x2, y2, 0f);    //火球の位置をplayerの位置に設定
            bullet3.transform.position = transform.position + new Vector3(x3, y3, 0f);    //火球の位置をplayerの位置に設定
            bullet4.transform.position = transform.position + new Vector3(x4, y4, 0f);    //火球の位置をplayerの位置に設定
            bullet5.transform.position = transform.position + new Vector3(x5, y5, 0f);    //火球の位置をplayerの位置に設定
            bullet6.transform.position = transform.position + new Vector3(x6, y6, 0f);    //火球の位置をplayerの位置に設定
            bullet7.transform.position = transform.position + new Vector3(x7, y7, 0f);    //火球の位置をplayerの位置に設定
            bullet8.transform.position = transform.position + new Vector3(x8, y8, 0f);    //火球の位置をplayerの位置に設定
            bullet9.transform.position = transform.position + new Vector3(x9, y9, 0f);    //火球の位置をplayerの位置に設定
            bullet10.transform.position = transform.position + new Vector3(x10, y10, 0f);    //火球の位置をplayerの位置に設定
        }
    }
    void JumpCheck( Vector2 checkPos ){
        if( Vector2.Distance( transform.position, checkPos ) < 0.25f ){
            Vector2 jumpVec = new Vector2( x_jump * checkLR, y_jump );
            rb.AddForce(jumpVec * Time.deltaTime , ForceMode2D.Impulse);
        }
    }

    void TurnCheckL( Vector2 checkPos ){
        if( Vector2.Distance( transform.position, checkPos ) < 0.25f ){
            checkLR *= -1;
            spRenderer.flipX = false;
        }
    }
    void TurnCheckR( Vector2 checkPos ){
        if( Vector2.Distance( transform.position, checkPos ) < 0.25f ){
            checkLR *= -1;
            spRenderer.flipX = true;
        }
    }
    void OnWillRenderObject()
	{
    //メインカメラに映った時だけ_isRenderedをtrue
		if(Camera.current.tag == MAIN_CAMERA_TAG_NAME){
		_isRendered = true;
		}
	}
    void OnTriggerEnter2D (Collider2D col)
	{
        if (col.gameObject.tag == "Fire" || col.gameObject.tag == "Thunder" || col.gameObject.tag == "Rock"){
            life--;
        }
    }
}
