using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ene01 : MonoBehaviour {
	
	Rigidbody2D rigidbody2D;
	public int speed = -3;
	public GameObject explosion;
	private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
	private bool _isRendered = false;	
	private int right = 1;
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();	
	}
	
	void Update ()
	{
		if (_isRendered){
			rigidbody2D.velocity = new Vector2 (speed * right, rigidbody2D.velocity.y);
		}
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Fire" || col.gameObject.tag == "Thunder" || col.gameObject.tag == "Rock"){
			Destroy (gameObject);	
        }
		if (col.gameObject.layer == 8 || col.gameObject.tag == "Enemy1" ){
			right = right * -1;
		}
	}
	

	void OnCollisionEnter2D (Collision2D col)
	{
		
	}
	void OnWillRenderObject()
	{
		if(Camera.current.tag == MAIN_CAMERA_TAG_NAME) {
			_isRendered = true;
		}
	}
}