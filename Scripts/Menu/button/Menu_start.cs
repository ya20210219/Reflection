using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_start : MonoBehaviour
{
    private float alfa;
    float red, green, blue;

    public Vector3 MenuStartPos;
    public bool FadeInFinish;
    GameObject MenuObject;
    Menu MenuScript;

    void Start()
    {
        MenuStartPos = transform.position;
        FadeInFinish = false;

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

        if (alfa > 1)
        {
            alfa = 1;
        }
    }

    void FadeOut()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa -= Time.deltaTime;

        if (alfa < 0) alfa = 0;
    }


    void Update()
    {
        if (MenuScript.start == false)
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
