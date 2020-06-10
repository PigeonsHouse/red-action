using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class ene03 : MonoBehaviour
 {
     #region//インスペクターで設定する
     [Header("移動速度")]public float speed;
     [Header("重力")]public float gravity;
     [Header("画面外でも行動する")] public bool nonVisibleAct;
     #endregion
 
     #region//プライベート変数
     private Rigidbody2D rb = null;
     private SpriteRenderer sr = null;
     /*private bool rightTleftF = false;*/
     #endregion
     public Vector2 jumpforce;
     private float timer = 0;
     public float jumpinterval;
     private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
     private bool _isRendered = false;
 
     // Start is called before the first frame update
     void Start()
     {
         rb = GetComponent<Rigidbody2D>();
         sr = GetComponent<SpriteRenderer>();
     }
 
     void FixedUpdate()
     {
         
		
         if(_isRendered){

         
         if (timer>jumpinterval){
         rb.AddForce(jumpforce,ForceMode2D.Impulse);
         timer=0;
         }

         }
         timer+=Time.fixedDeltaTime;


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
     void OnWillRenderObject()
	{
    //メインカメラに映った時だけ_isRenderedをtrue
		if(Camera.current.tag == MAIN_CAMERA_TAG_NAME){
		_isRendered = true;
		}
	}
 }