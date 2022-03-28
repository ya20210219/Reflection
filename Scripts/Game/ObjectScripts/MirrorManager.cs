//---------------------------------------------------------------------------------
//                      【鏡オブジェクト】
//
// Actor    :ariyoshi, Kureha
// Team     :Okemi
//---------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    private GameObject lightMgr;        // LightManegerScript取得用
    private LightManager LightMgrSc;
    private LightTopManager LightTopMgrSc;// LightTopManager取得用
    private LineRenderer MyLineRend;    // このオブジェクトが反射する光線の詳細
    
    private Vector3 ToVec;              // 光の反射角
    private Vector3 HitPoint;           // 他のオブジェクトから光が当てられた箇所

    public float lineWidth;             // 光の幅(太さ)
    public bool IsHit = false;      // 光が当たってるかどうか

    void Start()
    {
        lightMgr = GameObject.Find("LightManagerObj");
        LightMgrSc = lightMgr.GetComponent<LightManager>();
        LightTopMgrSc = GetComponentInChildren<LightTopManager>();

        MyLineRend = this.gameObject.GetComponent<LineRenderer>();

        IsHit = false;
    }

    void Update()
    {
        // このギミックがオンかどうか
        //if (IsHit)
        //{
        //    ToVec = LightMgrSc.GetComponent<LightManager>().LightCheck(HitPoint, ToVec, ref MyLineRend, lineWidth, true);
        //}
        //else if (!IsHit)
        //{
        //    ToVec = LightMgrSc.GetComponent<LightManager>().LightCheck(HitPoint, ToVec, ref MyLineRend, 0, false);
        //}
    }

    //public void SetHitLight_True()
    //{
    //    IsHit = true;
    //}
    //public void SetHitLight_False()
    //{
    //    IsHit = false;
    //}

    public void SetReflectVec(Vector3 Vec)
    {
        ToVec = Vec;
    }
    public Vector3 GetReflectVec()
    {
        return ToVec;
    }

    public void SetHitPoint(Vector3 Vec)
    {
        HitPoint = Vec;
    }
    public Vector3 GetHitPoint()
    {
        return HitPoint;
    }
}
