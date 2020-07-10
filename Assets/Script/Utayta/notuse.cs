using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notuse : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public Vector2 jumpforce;

     private float timer = 0;
     private float timer1 = 0;
     public float jumpinterval;
   public Shot m_shotPrefab; // 弾のプレハブ
public float m_shotSpeed; // 弾の移動の速さ
public float m_shotAngleRange; // 複数の弾を発射する時の角度
public float m_shotTimer; // 弾の発射タイミングを管理するタイマー
public int m_shotCount; // 弾の発射数
public float m_shotInterval; // 弾の発射間隔（秒）
private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
     private bool _isRendered = false;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // 毎フレーム呼び出される関数
    private void Update()
    {     
// プレイヤーのスクリーン座標を計算する
var screenPos = Camera.main.WorldToScreenPoint( transform.position );

// プレイヤーから見たマウスカーソルの方向を計算する
var direction = Input.mousePosition - screenPos;

// マウスカーソルが存在する方向の角度を取得する
var angle = Utils.GetAngle( Vector3.zero, direction );


// 弾の発射タイミングを管理するタイマーを更新する
m_shotTimer += Time.deltaTime;

// まだ弾の発射タイミングではない場合は、ここで処理を終える
/*if ( m_shotTimer < m_shotInterval ) return;*/
/*if(m_shotTimer>m_shotInterval) m_shotTimer = 0;*/

// 弾の発射タイミングを管理するタイマーをリセットする
/*m_shotTimer = 0;*/

// 弾を発射する

ShootNWay( angle, m_shotAngleRange, m_shotSpeed, m_shotCount );


    if(_isRendered){
         if (timer1>jumpinterval)
         {
         rigidbody2D.AddForce(jumpforce,ForceMode2D.Impulse);
         timer1=0;
         }
          timer1 += Time.fixedDeltaTime;
          timer += Time.fixedDeltaTime;

         if(timer < 4)
         {
          this.transform.position += new Vector3(-0.1f, 0, 0);
         }
         else
         {
          this.transform.position += new Vector3(0.1f, 0, 0);
         }

         if(timer > 8)
         {
             timer = 0;
         }
    }
    }
    private void ShootNWay( 
    float angleBase, float angleRange, float speed, int count )
{
    if(_isRendered)
    {
    var pos = transform.localPosition; // プレイヤーの位置
    var rot = transform.localRotation; // プレイヤーの向き

if(m_shotTimer > m_shotInterval)
{
    for ( int i = 0; i < count; ++i )
        {
            // 弾の発射角度を計算する
            var angle = angleBase + 
                angleRange * ( ( float )i / ( count - 1 ) - 0.5f );

            // 発射する弾を生成する
            var shot = Instantiate( m_shotPrefab, pos, rot );

            // 弾を発射する方向と速さを設定する
            shot.Init( angle, speed );
        }
    m_shotTimer = 0;
}
}

}
void OnWillRenderObject()
	{
    //メインカメラに映った時だけ_isRenderedをtrue
		if(Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
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
