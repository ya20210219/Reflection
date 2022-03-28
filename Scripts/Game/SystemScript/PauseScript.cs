//---------------------------------------------------------------------------------
//                      【ポーズ画面】
//
// Actor    :ariyoshi
// Team     :Okemi
//---------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    //　ポーズした時に表示するUIのプレハブ
    private GameObject PauseUIPrefab;
    //　ポーズUIのインスタンス
    private GameObject pauseUIInstance;

    private ScoreManager ScoreScript = null;

    private float InputInterval;          //入力のインターバルカウント
    private bool InputIntervalFlg;      //入力のインターバルが有効かどうか
    public float IntervalSeconds = 0.2f;
    
    StageManager StageMgrSc;

    //GameObject ReNightS;
    //GameObject GameS;
    //GameObject StageSelectS;
    //GameObject EndS;
    
    public enum PauseMenuList
    {
        NONE = 0,

        ReNight,         //夜へ
        BackGame,        //ゲームに戻る
        StageSelect,     //ステージセレクトに行く
        GameEnd,         //ゲームを終わる

        MenuListMax,
    }

    public PauseMenuList MenuListNum;   //ポーズメニューで今何を選んでいるか

    void Start()
    {
        InputInterval = 0;
        InputIntervalFlg = false;
        StageMgrSc = GetComponent<StageManager>();

        ScoreScript = GameObject.Find("Score").GetComponent<ScoreManager>();

        MenuListNum = PauseMenuList.ReNight;    //一応最初の選択肢は一番上のReNightに設定しておきます

        MenuListNum = (PauseMenuList)1;    //一応最初の選択肢は一番上のReNightに設定しておきます

        //RectTransform rectTransform = GetComponent<RectTransform>();

        //ReNightS = GameObject.Find("ReNightSpr");
        //GameS = GameObject.Find("GameSpr");
        //StageSelectS = GameObject.Find("StageSelectSpr");
        //EndS = GameObject.Find("EndSpr");


    }

    // Update is called once per frame
    void Update()
    {
        //十字
        float dph = Input.GetAxis("D_Pad_H");
        float dpv = Input.GetAxis("D_Pad_V");

        //スティック
        float lsh = Input.GetAxisRaw("L_Stick_H");
        float lsv = Input.GetAxisRaw("L_Stick_V");

        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown("joystick button 7"))
        {
            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(PauseUIPrefab) as GameObject;
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
            }
        }

        //ポーズ画面なら
        if (Time.timeScale == 0f)
        {
            if(!InputIntervalFlg)
            {
                if (dpv == -1 || lsv == -1)
                {
                    Debug.Log("次");
                    MenuListNum = (PauseMenuList)((int)MenuListNum + 1);
                    if (MenuListNum == PauseMenuList.MenuListMax)
                    {
                        MenuListNum = PauseMenuList.ReNight;
                    }
                    InputIntervalFlg = true;
                }

                if (dpv == 1 || lsv == 1)
                {
                    Debug.Log("前");
                    MenuListNum = (PauseMenuList)((int)MenuListNum - 1);
                    if (MenuListNum == PauseMenuList.NONE)
                    {
                        MenuListNum = PauseMenuList.GameEnd;
                    }
                    InputIntervalFlg = true;
                }
            }
            else if (InputIntervalFlg)
            {
                InputInterval += Time.unscaledDeltaTime;
                //Debug.Log(InputInterval);
                if(InputInterval > IntervalSeconds)
                {
                    InputIntervalFlg = false;
                    InputInterval = 0;
                }
            }

            // Aボタンでも戻る
            if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 0"))
            {
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
            }

            switch (MenuListNum)
            {
                case PauseMenuList.ReNight:

                    //Choice.localPosition = new Vector3(0.0f, 18.0f, 0.0f);
                    if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown("joystick button 1"))
                    {
                        Debug.Log("夜にしたい");
                        Time.timeScale = 1f;
                        StageMgrSc.GetComponent<StageManager>().SetNowTimeZone(StageManager.TIME_ZONE.NIGHT);
                        StageMgrSc.StopBgm();
                        GameObject.Find("PlayerPawn").transform.position = new Vector3(0.0f, 1.0f, -2.0f);

                        if (ScoreScript)
                        {
                            ScoreScript.ScoreLoopsAdd();
                            ScoreScript.SetScoreParticle();
                        }

                        Destroy(pauseUIInstance);
                    }
                    break;
                case PauseMenuList.BackGame:
                    //Choice.localPosition = new Vector3(0.0f, -10.0f);
                    if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown("joystick button 1"))
                    {
                        Destroy(pauseUIInstance);
                        Time.timeScale = 1f;
                    }
                    break;
                case PauseMenuList.StageSelect:
                    //Choice.localPosition = new Vector3(0.0f, -38.0f);
                    if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown("joystick button 1"))
                    {
                        //ステージセレクトシーンに移動？
                        Time.timeScale = 1f;
                        SceneManager.LoadScene("StageSelect");
                    }
                    break;
                case PauseMenuList.GameEnd:
                    //Choice.localPosition = new Vector3(0.0f, -60.0f);
                    if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown("joystick button 1"))
                    {
                        //タイトルに移動？
                        Time.timeScale = 1f;
                        SceneManager.LoadScene("Menu");
                    }
                    break;
            }
        }
    }

    public PauseMenuList GetPauseNum()
    {
        return MenuListNum;
    }
}
