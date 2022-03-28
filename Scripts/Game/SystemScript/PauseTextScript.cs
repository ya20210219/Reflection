//---------------------------------------------------------------------------------
//                      【ポーズ画面中のテキスト】
//
// Actor    :ariyoshi
// Team     :Okemi
//---------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseTextScript : MonoBehaviour
{
    public Text text;

    private GameObject StageMgr;
    PauseScript PauseMgrSc;
    // Start is called before the first frame update
    void Start()
    {
        StageMgr = GameObject.Find("StageManager");
        PauseMgrSc = StageMgr.GetComponent<PauseScript>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (StageMgr.GetComponent<PauseScript>().GetPauseNum())
        {
            case PauseScript.PauseMenuList.ReNight:
                text.text = "～時間を夜に飛ばします。ループ回数は一つ加算されます～";
                break;
            case PauseScript.PauseMenuList.BackGame:
                text.text = "～ゲームに戻ります～";
                break;
            case PauseScript.PauseMenuList.StageSelect:
                text.text = "～ステージセレクト画面に戻ります～";
                break;
            case PauseScript.PauseMenuList.GameEnd:
                text.text = "～タイトルに戻ります～";
                break;
        }
    }
}
