using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : MonoBehaviour
{
     private const string MAIN_CAMERA_TAG_NAME = "MainCamera";
	private bool _isRendered = false;
      Rigidbody2D rigidbody2D;

     private Animator anim;
     private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
      void OnWillRenderObject()
	{
    //メインカメラに映った時だけ_isRenderedをtrue
		if(Camera.current.tag == MAIN_CAMERA_TAG_NAME){
		_isRendered = true;
		}
	}
}
