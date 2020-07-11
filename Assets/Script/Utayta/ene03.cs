using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class ene03 : MonoBehaviour
 {
     #region//インスペクターで設定する
     private Animator anim;
     [Header("移動速度")]public float speed;
     [Header("重力")]public float gravity;
     [Header("画面外でも行動する")] public bool nonVisibleAct;
     #endregion
 
     #region//プライベート変数
     private Rigidbody2D rb;
     private SpriteRenderer sr;
     /*private bool rightTleftF = false;*/
     #endregion
     public Vector2 jumpforce;
     public bool on_damage = false;       //ダメージフラグ
     private float timer = 0;
     public float jumpinterval;
     private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
     private bool _isRendered = false;
 
     // Start is called before the first frame update
     void Start()
     {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
     }
 
     void FixedUpdate()
     {
         if(_isRendered){
            if (!on_damage) {
                if (timer>jumpinterval){
                    anim.SetTrigger("jump");
                    rb.AddForce(jumpforce,ForceMode2D.Impulse);
                    timer=0;
                } else {
                    timer+=Time.fixedDeltaTime;
                }
            }
        }
        if(on_damage){                                                                          // ダメージフラグがtrueで有れば点滅させる
            float level = Mathf.Abs(Mathf.Sin(Time.time * 50));
            sr.color = new Color(1f,1f,1f,level);
        }


         /*if (sr.isVisible || nonVisibleAct)
         {
             int xVector = -1;
             if (rightTleftF)
             {
                 xVector = 1;
                 transform.localScale = new Vector3(-1, 1, 1);
             }
             else
             {
                 transform.localScale = new Vector3(1, 1, 1);
             }
             rb.velocity = new Vector2(xVector * speed, -gravity);
         }*/
     }
    public void Dead () {
        Destroy (gameObject);	
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
		if (col.gameObject.tag == "Fire"){
            anim.SetTrigger("dead");
			on_damage = true;                                                           // ダメージフラグON
			Destroy (rb);
			Destroy (GetComponent<EdgeCollider2D>());
        }
	}
 }