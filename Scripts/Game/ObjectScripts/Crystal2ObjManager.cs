//---------------------------------------------------------------------------------
//                      【クリスタル２の角の管理オブジェクト】
//
// Author    :ariyoshi, Kureha
// Team     :Okemi
//---------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal2ObjManager : MonoBehaviour
{
    enum Vectol
    {
        NONE = -1,

        UP,
        LEFTDOWN,
        DOWN,
        RIGHTDOWM,
        VARIABLE,
        MAX
    };
    Vector3[] Angle;

    [SerializeField] private Vectol ThisVectol = Vectol.VARIABLE;
    Vector3 ToVec;

    GameObject CrystalRootMgr;

    bool Used = false;

    void Start()
    {
        CrystalRootMgr = transform.parent.gameObject;

        Angle = new Vector3[(int)Vectol.MAX]
        {
        new Vector3(Mathf.Cos((CrystalRootMgr.transform.eulerAngles.z +  90) * Mathf.Deg2Rad), Mathf.Sin((CrystalRootMgr.transform.eulerAngles.z +  90) * Mathf.Deg2Rad), 0.0f),
        new Vector3(Mathf.Cos((CrystalRootMgr.transform.eulerAngles.z + 210) * Mathf.Deg2Rad), Mathf.Sin((CrystalRootMgr.transform.eulerAngles.z + 210) * Mathf.Deg2Rad), 0.0f),
        new Vector3(Mathf.Cos((CrystalRootMgr.transform.eulerAngles.z + 270) * Mathf.Deg2Rad), Mathf.Sin((CrystalRootMgr.transform.eulerAngles.z + 270) * Mathf.Deg2Rad), 0.0f),
        new Vector3(Mathf.Cos((CrystalRootMgr.transform.eulerAngles.z + 330) * Mathf.Deg2Rad), Mathf.Sin((CrystalRootMgr.transform.eulerAngles.z + 330) * Mathf.Deg2Rad), 0.0f),
        new Vector3(0.0f, 0.0f, 0.0f),
        };

        ToVec = Angle[(int)ThisVectol];

    }

    // Update is called once per frame
    void Update()
    {
        if (Used)
        {
            GetComponent<LightTopManager>().SetHitPoint(CrystalRootMgr.transform.position);
            GetComponent<LightTopManager>().HitIsHit();
            GetComponent<LightTopManager>().SetToVec(ToVec);
        }
        else if (!Used)
        {
            GetComponent<LightTopManager>().OutIsHit();
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
