using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTrigger : MonoBehaviour
{
    GameObject stairs;
    StairsController StairsSc;

    //階段のキー入力後の計測
    private float elapsedTime = 0.0f;
    //ボタンの連射を阻止
    private bool counter_flag = false;
    //キーを離すまで本の出し入れはしない
    private bool keyon = false;

    // Start is called before the first frame update
    void Start()
    {
        stairs = transform.parent.gameObject;
        StairsSc = stairs.GetComponent<StairsController>();

        elapsedTime = 0.0f;
        counter_flag = false;
        keyon = false;
    }

    // Update is called once per frame
    void Update()
    {
        //キーを離したら本棚が出せるようになる
        if (Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp("joystick button 3"))
        {
            keyon = false;
        }
    }

    private void OnTriggerStay(Collider collision)   //オブジェクトと当たっている時だけ(Stay)
    {
        if (collision.gameObject.tag == "Player")   //floorというタグを持つオブジェクトと接触していたら
        {
            if ((Input.GetKey(KeyCode.C) || Input.GetKey("joystick button 3")) &&
                !counter_flag &&
                !keyon)
            {
                counter_flag = true;
                keyon = true;

                StairsSc.ChangeInout();
                //Debug.Log("C");
            }

            if (counter_flag == true)
            {
                elapsedTime += Time.deltaTime;

            }

            if (elapsedTime >= 0.1f && counter_flag == true)
            {
                counter_flag = false;
                elapsedTime = 0.0f;
            }
        }
    }
}

