using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notuse2 : MonoBehaviour
{
    private float timer = 0;
    private Vector3 enepos;
    // Start is called before the first frame update
    void Start()
    {
        enepos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      /*  transform.position = new Vector3(enepos.x, Mathf.Sin(Time.time) * 0.5f + enepos.y, enepos.z);*/

      if(timer < 0.5)
         {
          this.transform.position += new Vector3(-0.05f, 0.02f, 0);
         }
         else
         {
          this.transform.position += new Vector3(-0.05f, -0.02f, 0);
         }

         timer += Time.fixedDeltaTime;

         if(timer > 1)
         {
             timer = 0;
         }
    }
}
