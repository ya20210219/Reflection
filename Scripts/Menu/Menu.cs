using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("フェード")] public FadeImage fade;

    private const int menu_Game = 0;
    private const int menu_StageSelect = 1;
    private const int menu_End = 2;
    private const int menu_Secret = 3;
    private const int menu_Num = 3;
    public int nowSelect = 0;

    private const int scene_teamLogo = 0;
    private const int scene_title = 1;
    private const int scene_menu = 2;
    public int nowScene = 0;

    private bool firstPush = false;
    private int fadeSelect = 0;

    public bool pressB;
    public bool stageSelect;
    public bool start;
    public bool end;

    public float menuCountTime;

    private float InputInterval;            //入力のインターバルカウント
    private bool InputIntervalFlg;          //入力のインターバルが有効かどうか
    [SerializeField] float IntervalSeconds = 0.2f; // 長押しインターバル

    private GameObject SEObject = null;
    private SEManager SEScript = null;


    GameObject PressBObject;
    Menu_pressB PressBScript;
    GameObject MenuStartObject;
    Menu_start MenuStartScript;
    GameObject FirstMovieObject;
    FirstMovie FirstMovieScript;

    private void Awake()
    {
        nowSelect = 0;
    }

    private void Start()
    {
        InputInterval = 0;
        InputIntervalFlg = false;

        nowScene = scene_title;
        fadeSelect = 0;

        pressB = false;
        stageSelect = true;
        start = true;
        end = true;
        firstPush = false;

        PressBObject = GameObject.Find("pressB");
        PressBScript = PressBObject.GetComponent<Menu_pressB>();
        MenuStartObject = GameObject.Find("start");
        MenuStartScript = MenuStartObject.GetComponent<Menu_start>();
        FirstMovieObject = GameObject.Find("FirstMovie");
        FirstMovieScript = FirstMovieObject.GetComponent<FirstMovie>();

        SEObject = GameObject.Find("SEManager");
        if (SEObject != null)
        {
            SEScript = SEObject.GetComponent<SEManager>();
        }
    }

    private void Update()
    {
        //十字
        float dph = Input.GetAxis("D_Pad_H");
        float dpv = Input.GetAxis("D_Pad_V");

        //スティック
        float lsh = Input.GetAxisRaw("L_Stick_H");
        float lsv = Input.GetAxisRaw("L_Stick_V");

        switch (nowScene)
        {
            case scene_teamLogo:
                break;

            case scene_title:
                if (!FirstMovieScript.titleFinish) break;

                if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown("joystick button 1"))
                {
                    pressB = true;
                    nowScene++;
                    SEScript.SEForcedPlay(SEScript.SEStageSelect);
                }

                break;

            case scene_menu:

                if (PressBScript.FadeFinish == true)
                {
                    start = false;
                    stageSelect = false;
                    end = false;
                }

                if (fade.IsFadeOutComplete())
                {
                    switch (fadeSelect)
                    {
                        case menu_Game:
                            SceneManager.LoadScene("OpMovie");
                            break;

                        case menu_StageSelect:
                            SceneManager.LoadScene("StageSelect");
                            break;

                        case menu_End:
                            #if UNITY_EDITOR
                            UnityEditor.EditorApplication.isPlaying = false;
                            #elif UNITY_STANDALONE
                            UnityEngine.Application.Quit();
                            #endif
                            break;
                    }
                }

                if (!InputIntervalFlg)
                {
                    // 選択移動
                    if (Input.GetKeyDown(KeyCode.W) || dpv == 1 || lsv == 1)
                    {
                        nowSelect = (nowSelect + (menu_Num - 1)) % menu_Num;
                        InputIntervalFlg = true;
                        SEScript.SEForcedPlay(SEScript.SEStageMove);
                    }
                    else if (Input.GetKeyDown(KeyCode.S) || dpv == -1 || lsv == -1)
                    {
                        nowSelect = (nowSelect + 1) % menu_Num;
                        InputIntervalFlg = true;
                        SEScript.SEForcedPlay(SEScript.SEStageMove);
                    }
                }
                else if (InputIntervalFlg)
                {
                    InputInterval += Time.deltaTime;
                    Debug.Log(InputInterval);
                    if (InputInterval > IntervalSeconds)
                    {
                        InputIntervalFlg = false;
                        InputInterval = 0;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1"))
                {
                    switch (nowSelect)
                    {
                        case menu_Game:
                            start = true;
                            if (!firstPush)
                            {
                                fade.StartFadeOut();
                                firstPush = true;
                                fadeSelect = menu_Game;
                                SEScript.SEForcedPlay(SEScript.SEStageSelect);
                            }
                            break;

                        case menu_StageSelect:
                            stageSelect = true;
                            if (!firstPush)
                            {
                                fade.StartFadeOut();
                                firstPush = true;
                                fadeSelect = menu_StageSelect;
                                SEScript.SEForcedPlay(SEScript.SEStageSelect);
                            }
                            break;

                        case menu_End:
                            end = true;
                            if (!firstPush)
                            {
                                fade.StartFadeOut();
                                firstPush = true;
                                fadeSelect = menu_End;
                                SEScript.SEForcedPlay(SEScript.SEStageSelect);
                            }
                            break;
                    }
                }
                break;
        }
    }
}
