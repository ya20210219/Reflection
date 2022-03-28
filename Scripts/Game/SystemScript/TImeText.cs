using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // text追加

public class TImeText : MonoBehaviour
{
    public GameObject score_object = null; // Textオブジェクト
    public GameObject stageObject;
    private StageManager StageScript = null;
    private float time_num = 0.0f; // time変数
    

    // Start is called before the first frame update
    void Start()
    {
        stageObject = GameObject.Find("StageManager");
        StageScript = stageObject.GetComponent<StageManager>(); // ステージスクリプト取得

    }

    // Update is called once per frame
    void Update()
    {
        
        time_num = StageScript.GetTimer(); // 経過時間取得
        

        // オブジェクトから Text コンポーネントを取得
        Text time_text = score_object.GetComponent<Text>();
        // テキストの表示を入れ替える
        time_text.text = "残り時間 : " +  (int)time_num + "秒";
    }
}
