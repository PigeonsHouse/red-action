using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ene05 : MonoBehaviour
{
    public float jumptimer = 0;
    public float timer = 0;
    public float at_time;
    public float interval;
    public float jump;
    GameObject Hero;
    public GameObject k_at;
    private Animator anim;
    Rigidbody2D rigidbody2;
    public Vector2 force1;
    public Vector2 jumpforce;
    private bool isAttack_K;  
    Vector2 moveVector;                  // 移動速度の入力
    public float moveForceMultiplier;     // 移動速度の入力に対する追従度
    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
    private bool _isRendered = false;
    void Start() 
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        this.Hero = GameObject.Find("Hero");
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        Vector2 HeroPos = Hero.transform.position;
        Vector2 st = transform.position;  
        if(jumptimer < jump) {
            jumptimer += Time.fixedDeltaTime;
        } else {
            GetComponent<Rigidbody2D>().AddForce(jumpforce,ForceMode2D.Impulse);
            jumptimer = 0;
        }
        if (_isRendered) {
            if(HeroPos.x < transform.position.x) {
                GetComponent<Rigidbody2D>().AddForce(-force1, ForceMode2D.Impulse); 
            } else {
                GetComponent<Rigidbody2D>().AddForce(force1, ForceMode2D.Impulse); 
            }
        }
        rigidbody2.AddForce(moveForceMultiplier * (moveVector - rigidbody2.velocity));
        Vector2 origin = new Vector2( transform.position.x, transform.position.y - 2f );
        RaycastHit2D hit = Physics2D.Raycast( origin , Vector2.down, 130.1f );
        if (hit.collider != null) {
            if (hit.collider.tag == "Hero"){
                if (timer > interval) {
                    isAttack_K = true;
                    GameObject attackObj;  //火球の複製
                    attackObj = Instantiate (k_at);    //複製した火球を生成
                    attackObj.transform.position = transform.position + new Vector3(0f, -1.6f, 0f);    //火球の位置をplayerの位置に設定
                    timer = 0;
                } else {
                    timer += Time.deltaTime;
                } 
            }
        } else {
            timer = 3;
        }
    }
    void OnTriggerEnter2D (Collider2D col)
	{
        if (col.gameObject.tag == "Fire" || col.gameObject.tag == "Thunder" || col.gameObject.tag == "Rock"){
        Destroy (gameObject);
        }
	}
    void OnWillRenderObject()
	{
    //メインカメラに映った時だけ_isRenderedをtrue
		if(Camera.current.tag == MAIN_CAMERA_TAG_NAME) {
		_isRendered = true;
		}
	}
}
