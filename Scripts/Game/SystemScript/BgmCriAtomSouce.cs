using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmCriAtomSouce : MonoBehaviour
{
    [SerializeField] private CriAtomSource BgmNight;
    [SerializeField] private CriAtomSource BgmMorning;
    [SerializeField] private CriAtomSource BgmSunset;
    [SerializeField] private CriAtomSource BgmAfternoon;
    [SerializeField] private CriAtomSource BgmStageSelect;
    private GameObject StageMgr;
    private StageManager StageScript;
    private bool PlayMusicFlag = false;

    private GameObject ScoreObject;
    private ScoreManager ScoreScript;

    private float BgmMixControl = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {

        StageMgr = GameObject.Find("StageManager");
        if (StageMgr != null)
        {
            StageScript = StageMgr.GetComponent<StageManager>();
        }

        //SetValueToBGMAISAC(BgmNight, float.Epsilon);
        //SetValueToBGMAISAC(BgmMorning, float.Epsilon);
        //SetValueToBGMAISAC(BgmSunset, float.Epsilon);
        //SetValueToBGMAISAC(BgmAfternoon, float.Epsilon);
        //ScoreObject = GameObject.Find("ScoreManager");
        //if(ScoreObject != null)
        //{
        //    ScoreScript = ScoreObject.GetComponent<ScoreManager>();
        //}
        //CriAtomSource audio = (CriAtomSource)GetComponent("CriAtomSource");
        //audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(StageScript.GetNowTimeZone() == StageManager.TIME_ZONE.NIGHT)
        {
            
            if (PlayMusicFlag == false)
            {
                BgmAfternoon.Stop();
                //SetValueToBGMAISAC(BgmNight, BgmMixControl);
                //SetValueToBGMAISAC(BgmMorning, BgmMixControl);
                //SetValueToBGMAISAC(BgmSunset, BgmMixControl);
                //SetValueToBGMAISAC(BgmAfternoon, BgmMixControl);
                BgmNight.Play();
                SetPlayMusicFlag(true);
                
            }
            
        }
        if (StageScript.GetNowTimeZone() == StageManager.TIME_ZONE.MORNING)
        {
            
            if (PlayMusicFlag == false)
            {
                BgmNight.Stop();
                BgmMorning.Play();
                SetPlayMusicFlag(true);
            }
                
        }
        if (StageScript.GetNowTimeZone() == StageManager.TIME_ZONE.NOON)
        {
            
            if (PlayMusicFlag == false)
            {
                BgmMorning.Stop();
                BgmSunset.Play();
                SetPlayMusicFlag(true);
            }
            
        }
        if (StageScript.GetNowTimeZone() == StageManager.TIME_ZONE.EVENING)
        {
            
            if (PlayMusicFlag == false)
            {
                BgmSunset.Stop();
                BgmAfternoon.Play();
                SetPlayMusicFlag(true);
            }
                
        }

        

    }

    public void SetPlayMusicFlag(bool flag)
    {
        PlayMusicFlag = flag;
    }

    public void BgmMixControlAdd()
    {
        BgmMixControl += 0.2f;
    }

    public void SetValueToBGMAISAC(CriAtomSource BGMName, float aisacValue)
    {
        BGMName.SetAisacControl("BgmMixControl", aisacValue);
    }
}
