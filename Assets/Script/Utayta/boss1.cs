﻿using System.Collections;
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
    private float checkLR = 1;
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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Animator animator = GetComponent<Animator>();
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        if(timer > interval){                                                               //animation切り替え
            anim.SetTrigger("attack");
            timer = 0.0f;
        } else {
            timer += Time.deltaTime;
        }
        /*if (speed > 0){                                                                     //向き反転
            spRenderer.flipX = false;
            checkLR = 1;
        } else {
            spRenderer.flipX = true;
            checkLR = -1;
        }*/
        if (life <= 0) {                                                                    //ライフ
            Destroy (gameObject);	
        }
        
    }
    public void Attack()
    {
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