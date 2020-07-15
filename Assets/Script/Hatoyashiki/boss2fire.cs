using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2fire : MonoBehaviour{
    private Rigidbody2D rb2d;           // ゲット用関数
    // Start is called before the first frame update
    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        float checkLR;
        if (transform.localScale.y > 0){
            checkLR = 1;
        }else{
            checkLR = -1;
        }
        float theta = (rb2d.rotation + 90f) * Mathf.PI / 180f;
        float vx = Mathf.Cos(theta);
        float vy = Mathf.Sin(theta);
        rb2d.MovePosition(transform.position -= new Vector3( vx, vy, 0f ) * checkLR * Time.deltaTime * 10f);
        Destroy( this.gameObject, 2f );
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.name == "Hero" || col.name == "fire(Clone)" || col.name == "rock(Clone)" || col.name == "thunder(Clone)" ) {
            Destroy( this.gameObject );
        }
    }

}
