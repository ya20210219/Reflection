//---------------------------------------------------------------------------------
//                      【ポーズ画面の選択肢が光ってるあれ】
//
// Actor    :ariyoshi
// Team     :Okemi
//---------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceScript : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform Choice;
    private GameObject StageMgr;
    PauseScript PauseMgrSc;

    GameObject ReNightS;
    GameObject GameS;
    GameObject StageSelectS;
    GameObject EndS;

    void Start()
    {
        StageMgr = GameObject.Find("StageManager");
        PauseMgrSc = StageMgr.GetComponent<PauseScript>();

        ReNightS = GameObject.Find("ReNightSpr");
        GameS = GameObject.Find("GameSpr");
        StageSelectS = GameObject.Find("StageSelectSpr");
        EndS = GameObject.Find("EndSpr");
    }
    void Update()
    {
        //Debug.Log(Choice.position);

        switch (StageMgr.GetComponent<PauseScript>().GetPauseNum())
        {
            case PauseScript.PauseMenuList.ReNight:
                Choice.position = ReNightS.transform.position;
                break;
            case PauseScript.PauseMenuList.BackGame:
                Choice.position = GameS.transform.position;
                break;
            case PauseScript.PauseMenuList.StageSelect:
                Choice.position = StageSelectS.transform.position;
                break;
            case PauseScript.PauseMenuList.GameEnd:
                Choice.position = EndS.transform.position;
                break;
        }
    }
}
