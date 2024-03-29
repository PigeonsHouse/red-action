﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class ene02 : MonoBehaviour
 {
    //public GameObject attackObj;
    public GameObject ene_fire;
    public float interval;
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
	private bool _isRendered = false;
    public bool on_damage = false;       //ダメージフラグ
    Rigidbody2D rigidbody2D;
    private SpriteRenderer spRenderer;

    private Animator anim;
    public float timer;
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null || ene_fire == null)
        {
            Debug.Log("設定が足りません");
            Destroy(this.gameObject);
        }
        else
        {
            //attackObj.SetActive(false);
        }
         
        rigidbody2D = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
     }

     // Update is called once per frame
     void FixedUpdate()
     {
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        if (_isRendered) {
            if(timer > interval) {
                anim.SetTrigger("attack");
                timer = 0.0f;
            } else {
                timer += Time.deltaTime;
            }
        }
        if(on_damage){                                                                          // ダメージフラグがtrueで有れば点滅させる
            float level = Mathf.Abs(Mathf.Sin(Time.time * 50));
            spRenderer.color = new Color(1f,1f,1f,level);
        }
    }
         
    public void Attack()
    {
        if(_isRendered) {
            GameObject attackObj;  //火球の複製
            attackObj = Instantiate (ene_fire);    //複製した火球を生成
            attackObj.transform.position = transform.position + new Vector3(-1.5f, -0.7f, 0f);    //火球の位置をplayerの位置に設定
        }
    }
    public void Dead () {
        Destroy (gameObject);	
    }

   void OnWillRenderObject()
	{
		if(Camera.current.tag == MAIN_CAMERA_TAG_NAME){             //メインカメラに映った時だけ_isRenderedをtrue
		_isRendered = true;
		}
	}
    void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Fire" || col.gameObject.tag == "Thunder" || col.gameObject.tag == "Rock"){
			anim.SetTrigger("dead");	
            on_damage = true;                                                           // ダメージフラグON
			Destroy (rigidbody2D);
			Destroy (GetComponent<EdgeCollider2D>());
        }
	}
 }
