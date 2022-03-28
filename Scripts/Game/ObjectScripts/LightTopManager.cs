//---------------------------------------------------------------------------------
//                      【光線の発射管理オブジェクト】
//
// Actor    :ariyoshi, Kureha
// Team     :Okemi
//---------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTopManager : MonoBehaviour
{
    private GameObject lightMgr;        // LightManegerScript取得用
    private LightManager MyLightMgrSc;

    private GameObject Crystal1Mgr;        // LightManegerScript取得用
    private Crystal1Manager Crystal1MgrSc;
    

    private LineRenderer MyLineRend;    // このオブジェクトが反射する光線の詳細

    private Vector3 ToVec;              // このオブジェクトから飛ばすRay角
    private Vector3 HitObjReflectVec;   // このオブジェクトから飛ばしたRayの反射角
    private Vector3 HitPoint;           // 他のオブジェクトから光が当てられた箇所

    [SerializeField] private float lineWidth = 1;         // 光の幅(太さ)
    [SerializeField] private bool IsPlayer = false;       // プレイヤーからの光線か否か
    private bool IsHit;                 // アタッチしたオブジェクトに光が当たってるか


    // 前フレームとの比較用
    private GameObject OldHitObject;
    private List<GameObject> OldHitMagic = new List<GameObject>();
    private List<GameObject> OldHitFakeMagic = new List<GameObject>();

    private GameObject SEObject = null;
    private SEManager SEScript = null;


    void Start()
    {
        ToVec = HitObjReflectVec = new Vector3(0, 0, 0);

        lightMgr = GameObject.Find("LightManagerObj");
        MyLightMgrSc = lightMgr.GetComponent<LightManager>();

        Crystal1Mgr = GameObject.Find("Crystal1MgrObj");
        if(Crystal1Mgr != null)
        {
            Crystal1MgrSc = Crystal1Mgr.GetComponent<Crystal1Manager>();
        }

        SEObject = GameObject.Find("SEManager");
        if (SEObject != null)
        {
            SEScript = SEObject.GetComponent<SEManager>();
        }

        MyLineRend = gameObject.GetComponent<LineRenderer>();
        
        IsHit = false;
    }

    void Update()
    {
        ToVec = new Vector3(ToVec.x, ToVec.y, 0);
        Ray ray = new Ray(HitPoint, ToVec);

        UpdateObjRay(ray);
        if (IsPlayer)
        {
            UpdateMagRay(ray);
        }
        UpdateFakeMagRay(ray);

        // ヒットチェックとLineRenderer更新
        if (IsHit)
        {
            MyLightMgrSc.LightCheck(HitPoint, ToVec, ref MyLineRend, lineWidth, true);
        }
        else if (!IsHit)
        {
            ToVec = new Vector3(0, 0, 0);
            MyLightMgrSc.LightCheck(HitPoint, ToVec, ref MyLineRend, 0, false);
        }
        Debug.DrawLine(HitPoint, HitPoint + ToVec * 100.0f, Color.red);
        //Debug.Log("ToVec : " + ToVec);
        //Debug.Log("HitObjReflectVec : " + HitObjReflectVec);
        //Debug.Log("HitPoint : " + HitPoint);
    }

    //------------------------------------------------------
    // Getter,Setter
    //------------------------------------------------------
    public void SetToVec(Vector3 Vec)
    {
        ToVec = Vec;
    }
    public Vector3 GetToVec()
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

    public void HitIsHit()
    {
        IsHit = true;
    }
    public void OutIsHit()
    {
        IsHit = false;
    }

    private void UpdateObjRay(Ray ray)
    {
        GameObject HitObject;

        // 実質OntriggerStay
        // 最新のRay衝突対象のオブジェクト取得と反射管理
        int LayerObj = LayerMask.NameToLayer("Object");
        int LayerMaskObj = 1 << LayerObj;
        if (Physics.Raycast(ray, out RaycastHit DefHitPos, 10000, LayerMaskObj))
        {
            HitObject = DefHitPos.collider.gameObject;
            
            // このオブジェクトから飛ばしたRayのヒット箇所の反射角
            Vector3 InDirection = DefHitPos.point - transform.position;
            HitObjReflectVec = Vector3.Reflect(InDirection, DefHitPos.normal);
            DefHitPos.point = new Vector3(DefHitPos.point.x, DefHitPos.point.y, 0.0f);

            // タグによる分岐
            if ((HitObject.tag == "PlayerMirror") ||
                (HitObject.tag == "MirrorFloor") ||
                (HitObject.tag == "Mirror"))
            {
                // Hit対象のToVecにHitObjReflectVecを代入してRayのVectolにする
                // HIt位置代入してHitObjのRayの始点にする
                // Hit対象のフラグを立てる
                HitObject.transform.gameObject.GetComponent<LightTopManager>().SetToVec(HitObjReflectVec);
                HitObject.transform.gameObject.GetComponent<LightTopManager>().SetHitPoint(DefHitPos.point);
                HitObject.transform.gameObject.GetComponent<LightTopManager>().HitIsHit();

                // 実質OntriggerEnter
                // 前フレームとHit対象が異なるときは反射回数加算
                if (OldHitObject != HitObject)
                {
                    // Debug用じゃなくなりました
                    switch (HitObject.tag)
                    {
                        case "PlayerMirror":
                            Debug.Log("OnTriggerEnter.PlayerMirror");
                            SEScript.SEForcedPlay(SEScript.SEPlayerMirror);
                            break;
                        case "Mirror":
                            Debug.Log("OnTriggerEnter.Mirror");
                            SEScript.SEForcedPlay(SEScript.SEMirror);
                            break;
                    }
                }
            }
            else if (HitObject.tag == "Crystal1")
            {
                Debug.Log("OnTriggerEnter.Crystal1");
                HitObject.transform.parent.GetComponent<Crystal1Manager>().SetUsedTrue();
            }
            else if (HitObject.tag == "Crystal2")
            {
                Debug.Log("OnTriggerEnter.Crystal2");
                HitObject.GetComponent<Crystal2Manager>().SetUsedTrue();
            }
            else
            {
                if (OldHitObject != HitObject)
                {
                    Debug.Log("OnTriggerEnter.NotReflectObj");

                }
            }
        }
        else
        {
            HitObject = null;
        }

        // OnTriggerExitの代替、やってること一緒
        // 今のフレームでRayがオブジェクトから外れた時
        if (OldHitObject != HitObject && OldHitObject != null)
        {
            // タグによる分岐
            if ((OldHitObject.tag == "PlayerMirror") ||
                (OldHitObject.tag == "Mirror") ||
                (OldHitObject.tag == "MirrorFloor"))
            {
                Debug.Log(OldHitObject.tag);

                // 反射OFF
                OldHitObject.transform.gameObject.GetComponent<LightTopManager>().OutIsHit();

                // Debug用
                switch (OldHitObject.tag)
                {
                    case "PlayerMirror":
                        Debug.Log("OnTriggerExit.PlayerMirror");
                        
                        break;
                    case "Mirror":
                        Debug.Log("OnTriggerExit.Mirror");
                        
                        break;
                }
            }
            else if ((OldHitObject.tag == "Crystal1"))
            {
                Debug.Log("OnTriggerExit.Crystal1");
                OldHitObject.transform.parent.GetComponent<Crystal1Manager>().SetUsedFalse();
            }
            else if ((OldHitObject.tag == "Crystal2"))
            {
                Debug.Log("OnTriggerExit.Crystal2");
                OldHitObject.GetComponent<Crystal2Manager>().SetUsedFalse();
            }
            else
            {
                Debug.Log("OnTriggerExit.NotReflectObj");
            }
        }

        // Old更新
        OldHitObject = HitObject;
    }

    private void UpdateMagRay(Ray ray)
    {
        List<GameObject> HitMagic = new List<GameObject>();

        // OnTriggerStay(魔法陣)専用
        // 魔法陣貫通処理
        int LayerObj = LayerMask.NameToLayer("Object");
        int LayerMagic = LayerMask.NameToLayer("Magic");
        int LayerMaskMagic = 1 << LayerObj | 1 << LayerMagic;
        foreach (RaycastHit MagHitPos in Physics.RaycastAll(ray, 10000, LayerMaskMagic))
        {
            // オブジェクトに当たったら停止
            if (MagHitPos.collider.gameObject.layer == LayerObj) break;
            //Debug.Log("OnTriggerEnter.MagicChild");

            // 魔法陣のヒット処理
            if ((MagHitPos.collider.gameObject.tag == "MagicChild") ||
                (MagHitPos.collider.gameObject.tag == "MagicChildComp"))
            {
                Debug.Log("OnTriggerEnter.MagicChild");
                HitMagic.Add(MagHitPos.collider.gameObject);
                MagHitPos.collider.gameObject.GetComponent<MagicChildController>().HitLight();
            }
        }

        // OnTriggerExitの代替、やってること一緒
        // 今のフレームでRayが魔法陣から外れた時
        if (!OldHitMagic.Equals(HitMagic) && OldHitMagic.Count > 0)
        {
            if (HitMagic.Count > 0)
            {
                for (int i = 0; i < OldHitMagic.Count; i++)
                {
                    if (!HitMagic.Contains(OldHitMagic[i]))
                    {
                        Debug.Log("OnTriggerExit.MagicChild");
                        OldHitMagic[i].GetComponent<MagicChildController>().OutLight();
                    }
                }
            }
            else
            {
                for (int i = 0; i < OldHitMagic.Count; i++)
                {
                    Debug.Log("OnTriggerExit.MagicChild");
                    OldHitMagic[i].GetComponent<MagicChildController>().OutLight();
                }
            }
        }

        // Old更新
        OldHitMagic.Clear();
        OldHitMagic = HitMagic;
    }


    // 偽魔法陣用のやつ（上のそのまま丸パクリ）
    private void UpdateFakeMagRay(Ray ray)
    {
        List<GameObject> HitFakeMagic = new List<GameObject>();

        // OnTriggerStay(魔法陣)専用
        // 魔法陣貫通処理
        int LayerObj = LayerMask.NameToLayer("Object");
        int LayerFakeMagic = LayerMask.NameToLayer("Fake");
        int LayerMaskFakeMagic = 1 << LayerObj | 1 << LayerFakeMagic;
        foreach (RaycastHit FakeMagHitPos in Physics.RaycastAll(ray, 10000, LayerMaskFakeMagic))
        {
            // オブジェクトに当たったら停止
            if ((FakeMagHitPos.collider.gameObject.layer == LayerObj)) break;

            // 魔法陣のヒット処理
            if (FakeMagHitPos.collider.gameObject.tag == "FakeMagic")
            {
                //Debug.Log("OnTriggerEnter.FakeMagic");
                HitFakeMagic.Add(FakeMagHitPos.collider.gameObject);
                FakeMagHitPos.collider.gameObject.GetComponent<FakeMagicController>().HitLight();
            }
        }

        // OnTriggerExitの代替、やってること一緒
        // 今のフレームでRayが魔法陣から外れた時
        if (!OldHitFakeMagic.Equals(HitFakeMagic) && OldHitFakeMagic.Count > 0)
        {
            if (HitFakeMagic.Count > 0)
            {
                for (int i = 0; i < OldHitFakeMagic.Count; i++)
                {
                    if (!HitFakeMagic.Contains(OldHitFakeMagic[i]))
                    {
                        Debug.Log("OnTriggerExit.FakeMagic");
                        OldHitFakeMagic[i].GetComponent<FakeMagicController>().OutLight();

                    }
                }
            }
            else
            {
                for (int i = 0; i < OldHitFakeMagic.Count; i++)
                {
                    Debug.Log("OnTriggerExit.FakeMagic");
                    OldHitFakeMagic[i].GetComponent<FakeMagicController>().OutLight();
                }
            }
        }

        // Old更新
        OldHitFakeMagic.Clear();
        OldHitFakeMagic = HitFakeMagic;
    }
}