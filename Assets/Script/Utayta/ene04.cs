using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class ene04 : MonoBehaviour {
	
	Rigidbody2D rigidbody2D;
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
		
	}
	
	void Update ()
	{
		if (_isRendered)
         {
			rigidbody2D.velocity = new Vector2 (speed * right, rigidbody2D.velocity.y);
		 }
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
			Destroy (gameObject);	
        }
		if (col.gameObject.layer == 8 || col.gameObject.tag == "Enemy1" ){
			right = right * -1;
		}
	}
}