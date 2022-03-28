using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFadeDoor : MonoBehaviour
{
    [Header("フェードを入れる")] public FadeImage fade;
    [Header("次のステージのシーン")] public SceneObject m_nextScene;

    GameObject OpenDoorController;  //OpenDoorControllerそのものが入る変数
    OpenDoorController OpenDoorConSpt;  //OpenDoorControllerが入る変数
    GameObject MainCamera;

    private bool NextFade; //trueになるとフェードインを開始する
    private bool firstPush = false; //trueになるとフェード処理を開始する
    private bool goNextScene = false;   //trueになると次のシーンへ移動する

    //スタートボタンを押されたら呼ばれる

    private void Start()
    {
        OpenDoorController = GameObject.Find("OpenDoorController");
        OpenDoorConSpt = OpenDoorController.GetComponent<OpenDoorController>();
        MainCamera = GameObject.Find("Main Camera");
        if (MainCamera)
        {
            GetComponent<Canvas>().worldCamera = MainCamera.GetComponent<Camera>();
        }
    }


    private void Update()
    {
        NextFade = OpenDoorConSpt.NextStageFade;
        
        if (NextFade == true)
        {
            if (!firstPush)
            {
                Debug.Log("Go Next Scene!");
                fade.StartFadeOut();
                firstPush = true;
            }
        }

        if (!goNextScene && fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene(m_nextScene);
            goNextScene = true;
        }
    }
}
