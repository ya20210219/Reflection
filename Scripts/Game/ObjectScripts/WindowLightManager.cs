using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowLightManager : MonoBehaviour
{
    public enum WindowType
    {
        NONE = 0,
   
        RIGHT,  //右側についている
        UPPER,  //上側(天井)についている
        FRONT,  //正面についている
        LEFT,   //左側についている
    }


    public float lineWidth;
    public bool IsHit = false;      // 光が当たってるかどうか

    public Vector3 SetIrradiationVec1;  //照射角度(右側)
    public Vector3 SetIrradiationVec2;  //照射角度(左側)
    public Vector3 SetIrradiationVec3;  //照射角度(正面)
    private Vector3 ToVec;              //照射角度
    
    private GameObject stageMgr;        //ステージマネージャーのオブジェクト
    private StageManager StageMgrSc;
    private LineRenderer MyLineRend;
    private Vector3 HitPoint;           // 他のオブジェクトから光が当てられた箇所(無いんで自分のposを入れてる)

    private Vector3 ReflectVec;
    public WindowType WindowTypeNum;
    

    void Start()
    {
        SetIrradiationVec1 = new Vector3(-135.0f, -90.0f, 0);
        SetIrradiationVec2 = new Vector3(0.0f, -90.0f, 0.0f);
        SetIrradiationVec3 = new Vector3(135.0f, -90.0f, 0.0f);
        ToVec = new Vector3(135.0f, -90.0f, 0);
        
        stageMgr = GameObject.Find("StageManager");
        StageMgrSc = stageMgr.GetComponent<StageManager>();
        
        MyLineRend = gameObject.GetComponent<LineRenderer>();

        HitPoint = this.transform.position;

        //窓の設置タイプで光の角度を決めている
        switch (this.WindowTypeNum)
        {
            case WindowType.RIGHT:
                this.ToVec = SetIrradiationVec1;
                break;
            case WindowType.UPPER:
                this.ToVec = SetIrradiationVec2;
                break;
            case WindowType.FRONT:
                this.ToVec = SetIrradiationVec2;
                break;
            case WindowType.LEFT:
                this.ToVec = SetIrradiationVec3;
                break;
        }

        IsHit = false; //窓から光が差し込まない
    }

    void Update()
    {
        MyLineRend.startWidth = lineWidth;

        //Debug.DrawLine(transform.position, ToVec * 20.0f, Color.blue);

        //現在時刻によって光が差し込む窓を指定する
        switch (StageMgrSc.GetNowTimeZone())
        {
            case StageManager.TIME_ZONE.NIGHT: //夜の場合
                this.IsHit = false;
                this.GetComponent<LightTopManager>().OutIsHit();
                break;

            case StageManager.TIME_ZONE.MORNING: //朝だった場合
                if(this.WindowTypeNum == WindowType.FRONT || this.WindowTypeNum == WindowType.LEFT)
                {
                    if(this.WindowTypeNum == WindowType.FRONT)
                    {
                        this.ToVec = SetIrradiationVec3;
                    }
                    this.IsHit = true;
                    this.GetComponent<LightTopManager>().SetHitPoint(this.transform.position);
                    this.GetComponent<LightTopManager>().HitIsHit();
                    this.GetComponent<LightTopManager>().SetToVec(ToVec);
                }
                if (this.WindowTypeNum == WindowType.RIGHT || this.WindowTypeNum == WindowType.UPPER)
                {
                    this.IsHit = false;
                    this.GetComponent<LightTopManager>().OutIsHit();
                }
                break;

            case StageManager.TIME_ZONE.NOON: //昼だった場合
                if (this.WindowTypeNum == WindowType.UPPER || this.WindowTypeNum == WindowType.FRONT)
                {
                    if (this.WindowTypeNum == WindowType.FRONT)
                    {
                        this.ToVec = SetIrradiationVec2;
                    }
                    this.IsHit = true;
                    this.GetComponent<LightTopManager>().SetHitPoint(this.transform.position);
                    this.GetComponent<LightTopManager>().HitIsHit();
                    this.GetComponent<LightTopManager>().SetToVec(ToVec);
                }
                if (this.WindowTypeNum == WindowType.RIGHT || this.WindowTypeNum == WindowType.LEFT)
                {
                    this.IsHit = false;
                    this.GetComponent<LightTopManager>().OutIsHit();
                }
                break;

            case StageManager.TIME_ZONE.EVENING: //夕方だった場合
                if (this.WindowTypeNum == WindowType.RIGHT || this.WindowTypeNum == WindowType.FRONT)
                {
                    if (this.WindowTypeNum == WindowType.FRONT)
                    {
                        this.ToVec = SetIrradiationVec1;
                    }
                    this.IsHit = true;
                    this.GetComponent<LightTopManager>().SetHitPoint(this.transform.position);
                    this.GetComponent<LightTopManager>().HitIsHit();
                    this.GetComponent<LightTopManager>().SetToVec(ToVec);
                }
                if (this.WindowTypeNum == WindowType.UPPER || this.WindowTypeNum == WindowType.LEFT)
                {
                    this.IsHit = false;
                    this.GetComponent<LightTopManager>().OutIsHit();
                }
                break;

            default:
                this.IsHit = false;
                break;
        }

        //Debug.Log(this.IsHit);
        //Debug.Log(this.ToVec);
    }
}
