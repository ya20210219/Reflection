using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    [Header("フェード")] public FadeImage fade;

    private bool firstPush = false;
    private bool goNextScene = false;

    //スタートボタンを押されたら呼ばれる


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Press Start!");
            if (!firstPush)
            {
                Debug.Log("Go Next Scene!");
                fade.StartFadeOut();
                firstPush = true;
            }
        }

        if (!goNextScene && fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene("NEWSampleScene_ohta");
            goNextScene = true;
        }
    }
}