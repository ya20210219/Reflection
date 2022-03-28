using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_end : MonoBehaviour
{
    private float alfa;
    float red, green, blue;
    GameObject MenuObject;
    Menu MenuScript;

    public Vector3 MenuEndPos;

    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;

        MenuEndPos = transform.position;

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
        if (MenuScript.end == false)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }
}
