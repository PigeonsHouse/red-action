using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warpsystem : MonoBehaviour
{
    private GameObject obj;
    public Warpsystem transObj;
    private Vector2 transVec;
    private bool moveStatus;
 
    void Start()
    {
        
        transVec = transObj.transform.position;
        //初期では移動可能なためTrue
        moveStatus = true;
    }
 
    void Update (){
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        obj = GameObject.Find(other.name);
        //自分が移動可能なとき移動する。
        if (moveStatus)
        {
            //移動先は直後移動できないようにする
            transObj.moveStatus = false;
            obj.transform.position = transVec;
        }
    }
    //物体と離れた直後呼ばれる
    void OnTriggerExit2D(Collider2D other)
    {
        //移動可能にする。
        moveStatus = true;
    }
}