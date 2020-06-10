using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamContorol : MonoBehaviour
{
    public GameObject hero;
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

        if (this.transform.position.x < 0){
            transform.position = new Vector3(
                0,
                this.transform.position.y,
                this.transform.position.z
            );
        }

        if (this.transform.position.y < 0){
            transform.position = new Vector3(
                this.transform.position.x,
                0,
                this.transform.position.z
            );
        }

    }
}
