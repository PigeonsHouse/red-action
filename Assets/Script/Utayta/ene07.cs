﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ene07 : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    private SpriteRenderer spRenderer;
    private Animator anim;
    private float timer = 0;
    private float timer2 = 0;
    public float chargetime;
    public float dashtime;
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
    private bool _isRendered = false;
    public bool on_damage = false;       //ダメージフラグ
    public float sp;
    int a = 0; // 1回目のダッシュまでに力を加えるのを防ぐために用いた変数
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (_isRendered) {
            if (!on_damage) {
                if (timer >= chargetime && timer <= chargetime + dashtime && a == 0){
                    a++;
                }
                if (a == 1){
                    if (timer >= chargetime && timer <= chargetime + dashtime){
                        GetComponent<Rigidbody2D>().AddForce(Vector2.left * sp, ForceMode2D.Impulse);
                    } else if (timer <= dashtime) {
                        GetComponent<Rigidbody2D>().AddForce(Vector2.right * sp , ForceMode2D.Impulse);
                    }
                }
                if (timer > chargetime + dashtime) {
                    timer = 0;
                }
            }
        }
        timer += Time.fixedDeltaTime;
        if(on_damage){                                                                          // ダメージフラグがtrueで有れば点滅させる
            float level = Mathf.Abs(Mathf.Sin(Time.time * 50));
            spRenderer.color = new Color(1f,1f,1f,level);
        }
    }
    public void Dead () {
        Destroy (gameObject);	
    }
    void OnWillRenderObject()
	{
    //メインカメラに映った時だけ_isRenderedをtrue
		if(Camera.current.tag == MAIN_CAMERA_TAG_NAME) {
		_isRendered = true;
		}
	}
    void OnTriggerEnter2D (Collider2D col)
	{
        if (col.gameObject.tag == "Fire" || col.gameObject.tag == "Thunder" || col.gameObject.tag == "Rock"){
            anim.SetTrigger("dead");
            on_damage = true;                                                           // ダメージフラグON
			Destroy (rigidbody2D);
			Destroy (GetComponent<CapsuleCollider2D>());
        }
    }
}
