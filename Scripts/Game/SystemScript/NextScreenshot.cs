using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextScreenshot : MonoBehaviour
{
    private SpriteRenderer sr = null; //スプライトレンダラーの格納

    GameObject OpenDoorController;   //OpenDoorControllerそのものが入る変数
    OpenDoorController script;  //OpenDoorControllerが入る変数

    private bool NextFadeDoor_Open; //trueになったらドアが開く

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;

        OpenDoorController = GameObject.Find("OpenDoorController");
        script = OpenDoorController.GetComponent<OpenDoorController>();

        NextFadeDoor_Open = false;
    }

    // Update is called once per frame
    void Update()
    {
        NextFadeDoor_Open = script.NextFadeDoor_Open;

        //ドアが表示されたらスクリーンショットを表示
        if (NextFadeDoor_Open == true)
        {
            sr.enabled = true;
        }
    }
}
