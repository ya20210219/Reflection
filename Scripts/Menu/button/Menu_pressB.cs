using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_pressB : MonoBehaviour
{
    private float alfa;
    private float speed = 0.004f;
    float red, green, blue;

    public bool FadeFinish;
    public Vector3 MenuPressBPos;

    GameObject MenuObject;
    Menu MenuScript;
    GameObject FstMovieObject;
    FirstMovie FstMovieScript;

    void Start()
    {
        FadeFinish = false;
        alfa = 0.0f;
        MenuPressBPos = transform.position;

        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;

        MenuObject = GameObject.Find("menu");
        MenuScript = MenuObject.GetComponent<Menu>();
        FstMovieObject = GameObject.Find("FirstMovie");
        FstMovieScript = FstMovieObject.GetComponent<FirstMovie>();

        GetComponent<Image>().color = new Color(red, green, blue, alfa);
    }

    void FadeIn()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed;

        if (alfa > 1) alfa = 1;
    }

    void FadeOut()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa -= speed;

        if (alfa < -0.01f)
        {
            alfa = 0.0f;
            FadeFinish = true;
        }
    }


    void Update()
    {
        if (FstMovieScript.titleFinish == true)
        {
            if (MenuScript.pressB == false)
            {
                FadeIn();
            }
            else
            {
                FadeOut();
            }
        }
    }
}
