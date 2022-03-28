using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveIcon : MonoBehaviour
{
    private float red, green, blue;
    private float time;
    public GameObject[] door = new GameObject[16];

    GameObject StageSelectObject;
    stageSelect StageSelectScript;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;

        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;

        StageSelectObject = GameObject.Find("stageSelect");
        StageSelectScript = StageSelectObject.GetComponent<stageSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        GetComponent<Image>().color
            = new Color(red, green, blue, Mathf.PingPong(time / 2, 1.5f));

        transform.position = door[StageSelectScript.doorNumber].transform.position;
    }
}
