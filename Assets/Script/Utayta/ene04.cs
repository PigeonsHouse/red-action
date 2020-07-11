using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class ene04 : MonoBehaviour {
	
	Rigidbody2D rigidbody2D;
	private SpriteRenderer spRenderer;
	private Animator anim;
	public bool on_damage = false;       //ダメージフラグ
	private int checkLR = -1;
	public int speed = -3;
	private int right = 1;
	
	
//********** 開始 **********//
	//メインカメラのタグ名　constは定数(絶対に変わらない値)
	private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
	//カメラに映っているかの判定
	private bool _isRendered = false;
//********** 終了 **********//
	
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		spRenderer = GetComponent<SpriteRenderer>();	
		anim = GetComponent<Animator>();
	}
	
	void Update ()
	{
		if (!on_damage) {
			if (_isRendered) {
				rigidbody2D.velocity = new Vector2 (speed * right, rigidbody2D.velocity.y);
			}
		}
		if (checkLR == -1) {
			spRenderer.flipX = false;
		} else {
			spRenderer.flipX = true;
		}
		if(on_damage){                                                                          // ダメージフラグがtrueで有れば点滅させる
            float level = Mathf.Abs(Mathf.Sin(Time.time * 50));
            spRenderer.color = new Color(1f,1f,1f,level);
        }
	}
	public void Dead () {
        Destroy (gameObject);	
    }
	void OnCollisionEnter2D (Collision2D col)
	{
		
	}
	void OnWillRenderObject()
	{
		if(Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
		_isRendered = true;
		}
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Thunder" || col.gameObject.tag == "Rock"){
			anim.SetTrigger("dead");	
			on_damage = true;                                                           // ダメージフラグON
			speed = 0;
			Destroy (rigidbody2D);
			Destroy (GetComponent<BoxCollider2D>());
        }
		if (col.gameObject.layer == 8 || col.gameObject.tag == "Enemy1" ){
			right = right * -1;
			checkLR = checkLR * -1;
		}
	}
}