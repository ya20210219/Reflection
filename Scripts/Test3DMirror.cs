using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//private Vector3 addRotate =(30, 0, 0)

public class Test3DMirror : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            transform.Rotate(new Vector3(5, 0, 0));
        }

        if (Input.GetKey(KeyCode.F2))
        {
            transform.Rotate(new Vector3(10, 0, 0));
        }
        if(Input.GetKey(KeyCode.F3))
        {
            transform.Rotate(new Vector3(20, 0, 0));
        }
    }
}
