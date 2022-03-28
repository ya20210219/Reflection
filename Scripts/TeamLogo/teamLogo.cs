using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teamLogo : MonoBehaviour
{
    [Header("フェード")] public FadeImage fade;

    private bool firstPush = false;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        firstPush = false;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0") || time > 30)
        {
            if (!firstPush)
            {
                fade.StartFadeOut();
                firstPush = true;
            }
        }

        if (fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
