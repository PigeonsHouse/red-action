using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamContorol : MonoBehaviour
{
    public GameObject hero;
    public float camcenter_x;
    public float camcenter_y;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            hero.transform.position.x,
            hero.transform.position.y - 2f,
            this.transform.position.z
        );

        if (this.transform.position.x < camcenter_x){
            transform.position = new Vector3(
                camcenter_x,
                this.transform.position.y,
                this.transform.position.z
            );
        }

        if (this.transform.position.y < camcenter_y){
            transform.position = new Vector3(
                this.transform.position.x,
                camcenter_y,
                this.transform.position.z
            );
        }

    }
}
