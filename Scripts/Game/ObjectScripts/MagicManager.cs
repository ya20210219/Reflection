//---------------------------------------------------------------------------------
//                      【魔法陣管理】
//
// Author   :ohnari, Kureha
// Team     :Okemi
//---------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    GameObject[] tagObjects;
    GameObject stageMgr;                    //ステージマネージャーのオブジェクト
    StageManager StageMgrSc;

    [SerializeField] int MaxMagic = 2;

    private float timer = 0.0f;
    private float interval = 2.0f;

    private bool isCalledOnce = false;      // 一度のみ実行フラグ

    void Start()
    {
        stageMgr = GameObject.Find("StageManager");
        StageMgrSc = stageMgr.GetComponent<StageManager>();
    }

    void Update()
    {
        if (isCalledOnce) return;

        timer += Time.deltaTime;
        if(timer >interval)
        {
            Check("MagicChildComp");
            timer = 0;
        }
    }

    private void Check(string tagname)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(tagname);

        if((tagObjects.Length >= MaxMagic || Input.GetKeyDown(KeyCode.K)) && !isCalledOnce)
        {
            isCalledOnce = true;
            Debug.Log(tagname + "起動しました");
            StageMgrSc.GetComponent<StageManager>().SetClearTrue();
            StageMgrSc.SetNowTimeZone(StageManager.TIME_ZONE.NIGHT);
        }
    }
}