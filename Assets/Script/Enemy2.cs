using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

 public class Enemy2 : MonoBehaviour
 {
     [Header("攻撃オブジェクト")] public GameObject attackObj;
     [Header("攻撃間隔")] public float interval;
      private SpriteRenderer sr = null; 
      private Rigidbody2D rb = null;

     private Animator anim;
     private float timer;

     // Start is called before the first frame update
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
          sr = GetComponent<SpriteRenderer>(); 
          rb = GetComponent<Rigidbody2D>();
     }

     // Update is called once per frame
     void FixedUpdate()
     {
         if (sr.isVisible)
         {
             Debug.Log("画面に見えている");
         } 
          AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);

          //通常の状態
          if (currentState.IsName("idle"))
          {
              if(timer > interval)
              {
                  anim.SetTrigger("attack");
                  timer = 0.0f;
              }
              else
              {
                  timer += Time.deltaTime;
              }
          }
     }
          public void Attack()
{
     GameObject g = Instantiate(attackObj);
     g.transform.SetParent(transform);
     g.transform.position = attackObj.transform.position;
     g.SetActive(true);
}
 }
