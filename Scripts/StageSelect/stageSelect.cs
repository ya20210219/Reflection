using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stageSelect : MonoBehaviour
{
    [Header("フェード")] public FadeImage fade;

    [SerializeField] private SceneObject[] StageNum = new SceneObject[16];

    private const int backMenu = 17;

    public int doorNumber = 0;
    private const int doorNumberMax = 15;

    private bool firstPush = false;
    private int fadeSelect = 0;


    private GameObject SEObject = null;
    private SEManager SEScript = null;

    private float InputInterval;          //入力のインターバルカウント
    private bool InputIntervalFlg;      //入力のインターバルが有効かどうか
    [SerializeField] float IntervalFrame = 0.2f;



    void Start()
    {
        InputInterval = 0;
        InputIntervalFlg = false;

        firstPush = false;
        fadeSelect = 0;

        SEObject = GameObject.Find("SEManager");
        if (SEObject != null)
        {
            SEScript = SEObject.GetComponent<SEManager>();
        }
    }


    void Update()
    {
        //十字
        float dph = Input.GetAxis("D_Pad_H");
        float dpv = Input.GetAxis("D_Pad_V");

        //スティック
        float lsh = Input.GetAxisRaw("L_Stick_H");
        float lsv = Input.GetAxisRaw("L_Stick_V");

        if (!InputIntervalFlg)
        {
            if (Input.GetKeyDown(KeyCode.A) || lsh == -1.0 || dph == -1.0)
            {
                doorNumber--;
                InputIntervalFlg = true;
                if (doorNumber < 0) doorNumber = 0;
                SEScript.SEForcedPlay(SEScript.SEStageSelect);
            }
            else if (Input.GetKeyDown(KeyCode.D) || lsh == 1.0 || dph == 1.0)
            {
                doorNumber++;
                InputIntervalFlg = true;
                if (doorNumber > doorNumberMax) doorNumber = doorNumberMax;
                SEScript.SEForcedPlay(SEScript.SEStageSelect);
            }
        }
        else if (InputIntervalFlg)
        {
            InputInterval += Time.deltaTime;
            if (InputInterval > IntervalFrame)
            {
                InputIntervalFlg = false;
                InputInterval = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown("joystick button 7"))
        {
            if (!firstPush)
            {
                fade.StartFadeOut();
                firstPush = true;
                fadeSelect = backMenu;
                SEScript.SEForcedPlay(SEScript.SEStageMove);
            }
        }
        else if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 0"))
        {
            if (!firstPush)
            {
                fade.StartFadeOut();
                firstPush = true;
                fadeSelect = backMenu;
                SEScript.SEForcedPlay(SEScript.SEStageSelectMove);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1"))
        {
            if (fadeSelect >= 0 && doorNumber <= doorNumberMax)
            {
                fade.StartFadeOut();
                firstPush = true;
                fadeSelect = doorNumber;
                SEScript.SEForcedPlay(SEScript.SEStageSelectMove);
            }
        }

        if (fade.IsFadeOutComplete())
        {
            if (fadeSelect == backMenu)
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                if (fadeSelect >= 0 && fadeSelect <= doorNumberMax)
                {
                    SceneManager.LoadScene(StageNum[fadeSelect]);
                }
            }
        }
    }

    public int GetDoorNum()
    {
        return doorNumber;
    }
}
