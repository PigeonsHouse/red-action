using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class ene02 : MonoBehaviour
 {
    [Header("攻撃オブジェクト")] public GameObject attackObj;
    [Header("攻撃間隔")] public float interval;
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
	private bool _isRendered = false;
    Rigidbody2D rigidbody2D;

    private Animator anim;
    private float timer;
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null || attackObj == null)
        {
            Debug.Log("設定が足りません");
            Destroy(this.gameObject);
        }
        else
        {
            attackObj.SetActive(false);
        }
         
        rigidbody2D = GetComponent<Rigidbody2D>();
     }

     // Update is called once per frame
     void FixedUpdate()
     {
          AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);

          //通常の状態
          /*if (currentState.IsName("idle"))
          {*/
              if(timer > interval)
              {
                  anim.SetTrigger("attack");
                  timer = 0.0f;
              }
              else
              {
                  timer += Time.deltaTime;
              }
          /*}*/

          /* if(timer > interval)
          {
              anim.SetBool("Attack", true);
              timer = 0.0f;
          }
         /* else
          {
              timer += Time.deltaTime;
          }*/
     }
         
          public void Attack()
   {
       if(_isRendered)
       {
     GameObject g = Instantiate(attackObj);
     g.transform.SetParent(transform);
     g.transform.position = attackObj.transform.position;
     g.SetActive(true);   
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
			Destroy (gameObject);	
        }
	}
 }
