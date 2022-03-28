using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageChangeTex : MonoBehaviour
{
    public Image stageNumberImage;
    public Sprite[] stageNumber = new Sprite[16];
    public Image stageImage;
    public Sprite[] stageImageEx = new Sprite[16];

    GameObject stageSelectObject;
    stageSelect stageSelectScript;

    void Start()
    {
        stageSelectObject = GameObject.Find("stageSelect");
        stageSelectScript = stageSelectObject.GetComponent<stageSelect>();
    }

    void Update()
    {
        stageNumberImage.sprite = stageNumber[stageSelectScript.doorNumber];
        stageImage.sprite       = stageImageEx[stageSelectScript.doorNumber];
    }
}
