using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoorController : MonoBehaviour
{
    private Animator anim = null; //アニメーターの格納
    [HideInInspector] public bool NextFadeDoor_Open; //trueになったらドアが開く
    [HideInInspector] public bool NextFadeDoor_Wait; //false中はドアを待機させる

    [HideInInspector] public bool NextStageFade; //trueになるとフェードインを開始する
    [HideInInspector] public bool NextDoorSwitch;   //trueになると扉を開く準備をする
    
    private float time; //ドアのタイミングを時間で制御する

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //アニメーターのコンポーネント取得
        NextFadeDoor_Open = false;
        NextFadeDoor_Wait = true;

        NextStageFade = false;
        NextDoorSwitch = false;

        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (NextDoorSwitch == true)
        {
            time += Time.deltaTime;

            if (2.0f <= time)
            {
                //ドアを表示
                NextFadeDoor_Open = true;
                NextFadeDoor_Wait = false;
            }
            if (5.0f <= time)
            {
                ////次のシーンへ遷移
                NextStageFade = true;
            }
        }

        //アニメーションの切り替え
        anim.SetBool("NextFadeDoor_Open", NextFadeDoor_Open);
        anim.SetBool("NextFadeDoor_Wait", NextFadeDoor_Wait);
    }

    public void SetNextDoorSwitchTrue()
    {
        NextDoorSwitch = true;
    }
}
