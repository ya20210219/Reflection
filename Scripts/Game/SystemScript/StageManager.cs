//---------------------------------------------------------------------------------
//                      【昼夜切り替えなど】
//
// Author    :ariyoshi, Kureha, ohnari
// Team     :Okemi
//---------------------------------------------------------------------------------
#pragma warning disable 0414

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public enum TIME_ZONE    //現在の時間帯
    {
        NONE = -1,

        NIGHT,      //夜
        MORNING,    //朝
        NOON,       //昼
        EVENING,    //夕方

        TIME_ZONE_MAX,
    }

    public Vector3 StartPos;        //スタートの座標
    public Vector3 GoalPos;         //ゴールの座標
    private Vector3 GoalRot = new Vector3(0.0f, 180.0f, 0.0f);
    [SerializeField] GameObject Door;
    [SerializeField] GameObject GoalParticle;

    GameObject lightMgr;
    LightManager LightMgrSc;
    GameObject MagicMgr;
    MagicManager MagicMgrSc;
    
    [SerializeField] float Timer;                    //時間帯を変えるための時間
    [SerializeField] float ChangeTime = 30.0f;       //時間帯が変わる制限時間
    
    TIME_ZONE NowTimeZone;   //今が昼かどうか

    // クリア用
    private bool ClearOnceTime = false;
    private bool Clear_All = false;

    private GameObject   ScoreObject = null;
    private ScoreManager ScoreScript = null;

    private GameObject      BGMObject = null;
    private BgmCriAtomSouce BGMScript = null;

    private GameObject SEObject = null;
    private SEManager  SEScript = null;

    [SerializeField] private int StageNomber = 0;

    private GameObject TimeObj;
    private TimeImage  TimeScript;

    private static string SceneName = null;

    void Start()
    {
        Timer = ChangeTime;
        ClearOnceTime = false;   //クリアフラグ

        SceneName = SceneManager.GetActiveScene().name;
        Debug.Log(SceneName);

        NowTimeZone = TIME_ZONE.NIGHT; //初手は夜(仮)

        lightMgr = GameObject.Find("LightManagerObj");
        LightMgrSc = lightMgr.GetComponent<LightManager>();
        MagicMgr = GameObject.Find("MagicParent");
        MagicMgrSc = MagicMgr.GetComponent<MagicManager>();

        ScoreObject = GameObject.Find("Score");
        if (ScoreObject != null)
        {
            ScoreScript = ScoreObject.GetComponent<ScoreManager>();
        }

        BGMObject = GameObject.Find("BGMManager");
        if(BGMObject != null)
        {
            BGMScript = BGMObject.GetComponent<BgmCriAtomSouce>();
        }

        SEObject = GameObject.Find("SEManager");
        if (SEObject != null)
        {
            SEScript = SEObject.GetComponent<SEManager>();
        }

        TimeObj = GameObject.Find("TimeManager");
        if (TimeObj != null)
        {
            TimeScript = TimeObj.GetComponent<TimeImage>();
        }

        ScoreScript.ScoreCountReset();
        ScoreManager.LoadScoreData();
    }

    void Update()
    {
        if (Clear_All) return;

        if (NowTimeZone != TIME_ZONE.NIGHT)
        {
            ToNight("FakeMagicOn");
        }
        
        //ドア確認用に設置
        if (Input.GetKeyDown(KeyCode.K))
        {
            ClearOnceTime = true;
            Debug.Log("Kが押された");
        }
        // クリア処理
        if (ClearOnceTime)
        {
            Clear();
            ClearOnceTime = false;
        }

        // ※応急+デバッグ
        // 時間帯更新処理
        // 夜であればコントローラー入力で次の時間へ
        if ((Timer <= 0 ||
             (NowTimeZone == TIME_ZONE.NIGHT && Input.GetKeyDown("joystick button 3"))) &&
            !Clear_All)
        {
            StopBgm();

            if (NowTimeZone == TIME_ZONE.EVENING)
            {
                ScoreScript.ScoreLoopsAdd();
                ScoreScript.SetScoreParticle();
            }

            Debug.Log("ChangeTime");
            NowTimeZone = (TIME_ZONE)(((int)NowTimeZone + 1) % (int)TIME_ZONE.TIME_ZONE_MAX);

            if (NowTimeZone == TIME_ZONE.MORNING)
            {
                SEScript.SEPlay(SEScript.SETimeLoop);
                
                TimeScript.TimeBoolAllfalse();
            }
            
            Timer = ChangeTime;
        }

        // 夜以外での時間更新処理
        if (NowTimeZone != TIME_ZONE.NIGHT)
        {
            Timer -= Time.deltaTime;
        }
    }
    private void ToNight(string tagname)
    {
        GameObject[] tagObjects = GameObject.FindGameObjectsWithTag(tagname);

        if ((tagObjects.Length >= 1))
        {
            StopBgm();
            ScoreScript.ScoreLoopsAdd();
            ScoreScript.SetScoreParticle();

            NowTimeZone = TIME_ZONE.NIGHT;

            Timer = ChangeTime;
        }
    }
    private void Clear()
    {
        if (!Clear_All)
        {
            NowTimeZone = TIME_ZONE.NIGHT;
            ScoreScript.SetScoreCountArray(StageNomber);
            ScoreScript.SetScoreLoopsArray(StageNomber);
            StopBgm();
            SEScript.SEPlay(SEScript.SEDoorPop);
            Instantiate(GoalParticle, new Vector3(GoalPos.x, GoalPos.y + 0.1f, GoalPos.z), Quaternion.Euler(80f, 0f, 0f));
            Invoke("DoorSpawn", 5);

            Clear_All = true;
        }
    }
    private void DoorSpawn()
    {
        Instantiate(Door, GoalPos, Quaternion.Euler(GoalRot));
    }

    public void SetClearTrue()
    {
        ClearOnceTime = true;
    }
    public bool GetClear()
    {
        return ClearOnceTime;
    }

    public bool GetClearFlg()
    {
        return ClearOnceTime;
    }

    public void SetNowTimeZone(TIME_ZONE time)
    {
        NowTimeZone = time;
        Timer = ChangeTime;
    }
    public TIME_ZONE GetNowTimeZone()
    {
        return NowTimeZone;
    }

    public float GetTimer()
    {
        return Timer;
    }
    
    public float GetChangeTime()
    {
        return ChangeTime;
    }

    public string GetSceneName()
    {
        return SceneName;
    }

    public int GetStageNomber()
    {
        return StageNomber;
    }

    public void StopBgm()
    {
        BGMScript.SetPlayMusicFlag(false);
    }
}