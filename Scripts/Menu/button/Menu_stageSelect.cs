using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_stageSelect : MonoBehaviour
{
    private float alfa;
    float red, green, blue;
    public Vector3 MenuStageSelectPos;
    GameObject MenuObject;
    Menu MenuScript;

    void Start()
    {
        MenuStageSelectPos = transform.position;

        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;

        MenuObject = GameObject.Find("menu");
        MenuScript = MenuObject.GetComponent<Menu>();
    }

    void FadeIn()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += Time.deltaTime;

        if (alfa > 1) alfa = 1;
    }

    void FadeOut()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa -= Time.deltaTime;

        if (alfa < 0) alfa = 0;
    }


    void Update()
    {
        if (MenuScript.stageSelect == false)
        {
            FadeIn();
            //Debug.Log("FadeIn");
        }
        else
        {
            FadeOut();
            //Debug.Log("FadeOut");
        }
    }
}
