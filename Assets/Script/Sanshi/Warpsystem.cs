using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warpsystem : MonoBehaviour
{
    private GameObject obj;
    public Warpsystem transObj;
    private Vector3 transVec;
 
    void Start()
    {
        //移動先(円柱B)の座標を取得する
        transVec = transObj.transform.position;
    }
 
    //物体と重なる瞬間呼ばれる
    void OnTriggerEnter(Collider other)
    {
        //重なったオブジェクトを取得
        obj = GameObject.Find(other.name);
        //移動先(円柱B)の座標に移動する
        obj.transform.position = transVec;
    }
    }