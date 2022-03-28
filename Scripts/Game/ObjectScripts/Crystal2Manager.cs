//---------------------------------------------------------------------------------
//                      【クリスタル２大元の管理オブジェクト】
//
// Author    :ariyoshi, Kureha
// Team     :Okemi
//---------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal2Manager : MonoBehaviour
{
    private Transform Crystal2_1;      //光生成を行う子オブジェクト1
    private Transform Crystal2_2;      //光生成を行う子オブジェクト2
    private Transform Crystal2_3;      //光生成を行う子オブジェクト3

    private bool Used; //クリスタルに反射させるかどうか
    
    void Start()
    {
        //子(角)オブジェクト捜索
        Crystal2_1 = transform.GetChild(0);
        Crystal2_2 = transform.GetChild(1);
        Crystal2_3 = transform.GetChild(2);
    }

    void Update()
    {
        if (Used)
        {
            Crystal2_1.GetComponent<Crystal2ObjManager>().SetUsedTrue();
            Crystal2_2.GetComponent<Crystal2ObjManager>().SetUsedTrue();
            Crystal2_3.GetComponent<Crystal2ObjManager>().SetUsedTrue();
        }
        else if (!Used)
        {
            Crystal2_1.GetComponent<Crystal2ObjManager>().SetUsedFalse();
            Crystal2_2.GetComponent<Crystal2ObjManager>().SetUsedFalse();
            Crystal2_3.GetComponent<Crystal2ObjManager>().SetUsedFalse();
        }
    }
    public void SetUsedTrue()
    {
        Used = true;
    }
    public void SetUsedFalse()
    {
        Used = false;
    }
}
