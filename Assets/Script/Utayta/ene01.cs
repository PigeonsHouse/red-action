using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class ene01 : MonoBehaviour {
	
	Rigidbody2D rigidbody2D;
	public int speed = -3;
	public GameObject explosion;
	
	
//********** 開始 **********//
	//メインカメラのタグ名　constは定数(絶対に変わらない値)
	private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
	//カメラに映っているかの判定
	private bool _isRendered = false;
//********** 終了 **********//
	
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		
	}
	
	void Update ()
	{
//********** 開始 **********//
		if (_isRendered)
         {
			rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
		 }
//********** 終了 **********//
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
//********** 開始 **********//
	if (col.tag == "Bullet") {
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
		}
		}
//********** 終了 **********//
	

	void OnCollisionEnter2D (Collision2D col)
	{
		
	}
 //********** 開始 **********//
	//Rendererがカメラに映ってる間に呼ばれ続ける
	void OnWillRenderObject()
	{
    //メインカメラに映った時だけ_isRenderedをtrue
		if(Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
		_isRendered = true;
		}
	}
//********** 終了 **********//
}