using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bad : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public Vector2 jumpforce;

     private float timer = 0;
     private float timer1 = 0;
     public float jumpinterval;

     /*public float turninterval;*/

     /*public int speed = -3;*/
     private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
     private bool _isRendered = false;

   

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);*/
        
    if(_isRendered)
    {
         if (timer1>jumpinterval)
         {
         rigidbody2D.AddForce(jumpforce,ForceMode2D.Impulse);
         timer1=0;
         }
          timer1 += Time.fixedDeltaTime;
          timer += Time.fixedDeltaTime;

         if(timer < 2.5)
         {
          this.transform.position += new Vector3(-0.1f, 0, 0);
         }
         else
         {
          this.transform.position += new Vector3(0.1f, 0, 0);
         }

         if(timer > 5)
         {
             timer = 0;
         }
    }
     /*rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);*/

         
         /*rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);

         if (timer > turninterval)
         {
           rigidbody2D.velocity = new Vector2 (-speed, rigidbody2D.velocity.y);
         }*/
         

         /*if(timer > turninterval)
         {
             rigidbody2D.AddForce (movepower, ForceMode2D.Impulse);
         } 
         else
         {
             rigidbody2D.AddForce(-movepower, ForceMode2D.Impulse);
         }*/
    }
    void OnWillRenderObject()
	{
    //メインカメラに映った時だけ_isRenderedをtrue
		if(Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
		_isRendered = true;
		}
	}
}
