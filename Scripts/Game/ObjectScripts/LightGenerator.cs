using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGenerator : MonoBehaviour
{
    public GameObject PlayerMirror;     // PlayerMirrorそのものが入る変数
    public GameObject lightMgr;         // LightManegerScript取得用
    LightManager LightMgrSc;

    public float LineWidth;             //光の幅(太さ)

    void Start()
    {
        PlayerMirror = GameObject.Find("PlayerMirror");
        lightMgr = GameObject.Find("LightManagerObj");
        LightMgrSc = lightMgr.GetComponent<LightManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collison)
    {
        if (collison.gameObject.tag == "PlayerMirror")
        {
            Debug.Log("InLantern");

            // 光源からPlayerへ反射角の計算
            Vector3 InDirection = PlayerMirror.transform.position - transform.position;
            Vector3 HitObjReflectVec = new Vector3(0, 0, 0);
            if (Physics.Raycast(transform.position, InDirection, out RaycastHit Hit, Mathf.Infinity))
            {
                HitObjReflectVec = Vector3.Reflect(InDirection, Hit.normal);
            }

            PlayerMirror.GetComponent<LightTopManager>().SetToVec(HitObjReflectVec);
            PlayerMirror.GetComponent<LightTopManager>().SetHitPoint(PlayerMirror.transform.position);
            PlayerMirror.GetComponent<LightTopManager>().HitIsHit();
            LightMgrSc.GetComponent<LightManager>().AddReflectCnt(1);
        }
    }

    private void OnTriggerStay(Collider collison)
    {
        if (collison.gameObject.tag == "PlayerMirror")
        {
            // 光源からPlayerへ反射角の計算
            Vector3 InDirection = PlayerMirror.transform.position - transform.position;
            Vector3 HitObjReflectVec = new Vector3(0, 0, 0);
            if (Physics.Raycast(transform.position, InDirection, out RaycastHit Hit, Mathf.Infinity))
            {
                HitObjReflectVec = Vector3.Reflect(InDirection, Hit.normal);
            }

            PlayerMirror.GetComponent<LightTopManager>().SetToVec(HitObjReflectVec);
            PlayerMirror.GetComponent<LightTopManager>().SetHitPoint(PlayerMirror.transform.position);
            PlayerMirror.GetComponent<LightTopManager>().HitIsHit();
        }
    }

    private void OnTriggerExit(Collider collison)
    {
        if (collison.gameObject.tag == "PlayerMirror")
        {
            Debug.Log("OutLantern");
            PlayerMirror.GetComponent<LightTopManager>().OutIsHit();
            LightMgrSc.GetComponent<LightManager>().AddReflectCnt(-1);
        }
    }
}