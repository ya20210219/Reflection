using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTex : MonoBehaviour
{
    private float nowPos;
    private float time;

    void Start()
    {
        time = 0;
        nowPos = this.transform.position.y;
    }

    void Update()
    {
        time += Time.deltaTime;

        transform.position
            = new Vector3(transform.position.x,
            nowPos + Mathf.PingPong(time/40, 0.05f),
            transform.position.z);
    }
}