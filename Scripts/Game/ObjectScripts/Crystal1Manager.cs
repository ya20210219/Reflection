//---------------------------------------------------------------------------------
//                      【クリスタル１管理オブジェクト】
//
// Author    :ariyoshi, Kureha
// Team     :Okemi
//---------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal1Manager : MonoBehaviour
{
    bool Used; //クリスタルに反射させるかどうか

    private Transform LightGenerator1;   //光生成を行う子オブジェクト1
    private Transform LightGenerator2;   //光生成を行う子オブジェクト2

    void Start()
    {
        Used = false;
        LightGenerator1 = transform.GetChild(2);
        LightGenerator2 = transform.GetChild(3);
    }

    void Update()
    {
        if(Used)
        {
            LightGenerator1.GetComponent<CrystalLightGenerator>().SetUsedTrue();
            LightGenerator2.GetComponent<CrystalLightGenerator>().SetUsedTrue();
        }
        else if (!Used)
        {
            LightGenerator1.GetComponent<CrystalLightGenerator>().SetUsedFalse();
            LightGenerator2.GetComponent<CrystalLightGenerator>().SetUsedFalse();
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
