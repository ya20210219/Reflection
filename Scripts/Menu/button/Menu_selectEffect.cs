using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_selectEffect : MonoBehaviour
{
    private const int menu_Game = 0;
    private const int menu_StageSelect = 1;
    private const int menu_End = 2;
    private const int menu_Secret = 3;
    private const int menu_Num = 3;

    private const int scene_teamLogo = 0;
    private const int scene_title = 1;
    private const int scene_menu = 2;

    private float alfa;
    private bool fadeOutIn;

    GameObject FstMovieObject;
    FirstMovie FstMovieScript;
    GameObject MenuObject;
    Menu MenuScript;
    GameObject MenuPressBObject;
    Menu_pressB MenuPressBScript;
    GameObject MenuStartObject;
    Menu_start MenuStartScript;
    GameObject MenuStageSelectObject;
    Menu_stageSelect MenuStageSelectScript;
    GameObject MenuEndObject;
    Menu_end MenuEndScript;

    void Start()
    {
        FstMovieObject = GameObject.Find("FirstMovie");
        FstMovieScript = FstMovieObject.GetComponent<FirstMovie>();
        MenuObject = GameObject.Find("menu");
        MenuScript = MenuObject.GetComponent<Menu>();
        MenuPressBObject = GameObject.Find("pressB");
        MenuPressBScript = MenuPressBObject.GetComponent<Menu_pressB>();
        MenuStartObject = GameObject.Find("start");
        MenuStartScript = MenuStartObject.GetComponent<Menu_start>();
        MenuStageSelectObject = GameObject.Find("stage select");
        MenuStageSelectScript = MenuStageSelectObject.GetComponent<Menu_stageSelect>();
        MenuEndObject = GameObject.Find("end");
        MenuEndScript = MenuEndObject.GetComponent<Menu_end>();


        transform.position = MenuEndScript.MenuEndPos;

        GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alfa);

        fadeOutIn = false;
    }

    void FadeIn()
    {
        GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alfa);
        alfa += Time.deltaTime * 0.2f;

        if (alfa > 0.2f) alfa = 0.2f;
    }

    void FadeOut()
    {
        GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alfa);
        alfa -= Time.deltaTime * 0.2f;

        if (alfa < -0.01f)
        {
            alfa = 0.0f;
        }
    }

    void FadeOutIn()
    {
        GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alfa);
        

        if(alfa < 0)
        {
            fadeOutIn = true;
        }

        if (fadeOutIn == true)
        {
            alfa += Time.deltaTime * 0.2f;
        }
        else
        {
            alfa -= Time.deltaTime * 0.2f;
        }
    }

    void Update()
    {
        switch (MenuScript.nowScene)
        {
            case scene_teamLogo:
                break;

            case scene_title:
                if(FstMovieScript.titleFinish == true)
                {
                    FadeIn();
                }
                transform.position = MenuPressBScript.MenuPressBPos;
                break;

            case scene_menu:
                FadeOutIn();
                if (alfa > 0 && fadeOutIn == false)
                {
                    transform.position = MenuPressBScript.MenuPressBPos;
                }
                else
                {
                    switch (MenuScript.nowSelect)
                    {
                        case menu_Game:
                            transform.position = MenuStartScript.MenuStartPos;
                            break;

                        case menu_StageSelect:
                            transform.position = MenuStageSelectScript.MenuStageSelectPos;
                            break;

                        case menu_End:
                            transform.position = MenuEndScript.MenuEndPos;
                            break;
                    }
                }
                break;
        }
    }
}
