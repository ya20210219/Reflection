//---------------------------------------------------------------------------------
//                      【光線の発射オブジェクト】
//
// Actor    :ariyoshi, Kureha
// Team     :Okemi
//---------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Color MorningColor = new Color(255, 0, 0, 255);
    [SerializeField] private Color NoonColor    = new Color(0, 255, 0, 255);
    [SerializeField] private Color EveningColor = new Color(0, 0, 255, 255);

    StageManager StageMgr;

    int ReflectLim = 256;         //反射上限
    private int ReflectCnt = 0;         //反射回数
    bool night = false;

    void Start()
    {
        StageMgr = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    void Update()
    {

    }

    // アタッチしたオブジェクトから光線を飛ばしているように見せたり隠したりする関数
    // 
    // OriginPos    ：Lightを飛ばしてるように見せる原点
    // TargetVec    ：飛ばす方向
    // RayLine      ：アタッチしたオブジェクトのLineRenderder
    // width        ：光線幅
    // Used         ：アタッチしたオブジェクトに光線照射フラグが立ってるかどうか
    // 
    public void LightCheck(Vector3 OriginPos, Vector3 TargetVec,ref LineRenderer RayLine, float width, bool Used = false)
    {
        if (night) return;

        if (RayLine == null)
        {
            throw new System.ArgumentNullException(nameof(RayLine));
        }

        // 光線の設定
        RayLine.SetPosition(0, OriginPos);
        RayLine.SetPosition(1, OriginPos); // 光の終着点
        RayLine.startWidth = width;
        RayLine.endWidth = width;
        switch (StageMgr.GetNowTimeZone())
        {
            case StageManager.TIME_ZONE.MORNING:
                RayLine.startColor = MorningColor;
                RayLine.endColor = MorningColor;
                break;
            case StageManager.TIME_ZONE.NOON:
                RayLine.startColor = NoonColor;
                RayLine.endColor = NoonColor;
                break;
            case StageManager.TIME_ZONE.EVENING:
                RayLine.startColor = EveningColor;
                RayLine.endColor = EveningColor;
                break;
        }
        
        // 使用中の場合は終点を更新
        if (Used)
        {
            // Rayの作成
            Ray RayToTarget = new Ray(OriginPos, TargetVec);

            // LineRenderer終点の更新
            RayLine.SetPosition(1, TargetVec * 50);

            // Ray衝突時に衝突オジェクトに終点を更新
            int LayerObj = LayerMask.NameToLayer("Object");
            int LayerMaskObj = 1 << LayerObj;
            if (Physics.Raycast(RayToTarget, out RaycastHit hit, Mathf.Infinity, LayerMaskObj))
            {
                RayLine.SetPosition(1, hit.point);
            }
        }
    }

    public int GetReflectLim()
    {
        return ReflectLim;
    }
    public void SetReflectLim(int num)
    {
        ReflectLim = num;
    }
    public void SetNightTrue()
    {
        night = true;
    }
    public void SetNightFalse()
    {
        night = false;
    }

    public int GetReflectCnt()
    {
        return ReflectCnt;
    }
    public void AddReflectCnt(int Add)
    {
        ReflectCnt += Add;
        Debug.Log(ReflectCnt);
    }
}
