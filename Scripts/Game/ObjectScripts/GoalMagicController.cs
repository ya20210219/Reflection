using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0414

public class GoalMagicController : MonoBehaviour
{
    private float Rotate = 10.0f;　 // 魔法陣の回転
    private float scale = 0.1f;
    private float time;

    [SerializeField] GameObject Jet;
    [SerializeField] GameObject Fall;
    [SerializeField] GameObject Inside;
    [SerializeField] GameObject Outside;

    private bool isUse = false;
    private bool isUse2 = false;
    private bool isUse3 = false;


    void Start()
    {
    }

    void Update()
    {
        time += Time.deltaTime;

        float angle = Rotate * Time.deltaTime;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + angle);

        // 収束パーティクル
        if(!isUse3)
        {
            Instantiate(Inside, this.transform.position, Quaternion.identity);
            isUse3 = true;
        }

        // 放出・噴水パーティクル
        if (scale > 4.0f)
        {
            scale = 4.0f;
            if (!isUse)
            {
                Instantiate(Jet, this.transform.position, Quaternion.identity);
                Instantiate(Outside, this.transform.position, Quaternion.identity);
                isUse = true;
            }
            if (!isUse2)
            {
                Instantiate(Fall, new Vector3(10.5f, 0.0f, 0.0f), Quaternion.identity);
                isUse2 = true;
            }
        }
        // ウェーブパーティクル
        //this.transform.localScale = new Vector2(time, scale);
        scale += Time.deltaTime;
    }
}
