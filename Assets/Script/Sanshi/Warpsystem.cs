using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warpsystem : MonoBehaviour
{   
    void OnControllerColliderHit(ControllerColliderHit hit){
        if (hit.gameObject.tag == "Warp") {
            GetComponent<Hero>().enabled = false;
            transform.position = new Vector3(20,-1,0);
            GetComponent<Hero>().enabled = true;
            Debug.Log(hit.gameObject);
        }

    }
}