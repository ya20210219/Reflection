using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Image Img = null;
    public Sprite sprite_1 = null;
    public Sprite sprite_2 = null;
    public Sprite sprite_3 = null;

    [SerializeField] private GameObject ScoreParticle = null;
    [SerializeField] private GameObject ScoreTextObject = null;
    private Text ScoreText = null;

    private bool GoodCall = false;
    private bool NormalCall = false;
    private bool BadCall = false;

    private int StageNum = 0;
    private int ScoreCount = 0;
    private int ScoreLoops = 0;
    private const int MaxStage = 16;
    private static int[] HighScoreCountArray = new int[MaxStage];
    private static int[] HighScoreLoopsArray = new int[MaxStage];
    public static int[] GoodScore =
    {
        3, 3, 3,
        3, 3, 3,
        2, 2, 2,
        1, 1, 1, 1,
        1, 1, 1
    };
    public static int[] NormalScore =
    {
        6, 6, 6,
        5, 5, 5,
        4, 4, 4,
        3, 3, 3, 3,
        2, 2, 2
    };

    //private static List<int> ScoreCountArray = new List<int>;

    private GameObject BgmObject = null;
    private BgmCriAtomSouce BgmScript = null;

    private GameObject StageObject = null;
    private StageManager StageScript = null;
    // Start is called before the first frame update
    void Start()
    {

        StageObject = GameObject.Find("StageManager");
        if (StageObject != null)
        {
            StageScript = StageObject.GetComponent<StageManager>();
            StageNum = StageScript.GetStageNomber();

        }

        BgmObject = GameObject.Find("BGMManager");
        if (BgmObject != null)
        {
            BgmScript = BgmObject.GetComponent<BgmCriAtomSouce>();
        }
        // ChangeSprite(sprite_1);
        GoodCall = true;
        ScoreText = ScoreTextObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = ScoreLoops + "/" + NormalScore[StageNum];

        //Debug.Log("ScoreCount:" + ScoreCount);
        if (ScoreLoops <= GoodScore[StageNum])
        {
            ScoreCount = 0;
            if (GoodCall)
            {
                ChangeSprite(sprite_1);

                GoodCall = false;
                NormalCall = true;
            }
        }
        else if (ScoreLoops <= NormalScore[StageNum])
        {
            ScoreCount = 1;
            if (NormalCall)
            {
                ChangeSprite(sprite_2);
                BgmScript.BgmMixControlAdd();

                NormalCall = false;
                BadCall = true;
            }
        }
        if (ScoreLoops > NormalScore[StageNum])
        {
            ScoreCount = 2;
            if (BadCall)
            {
                ScoreText.color = Color.red;
                ChangeSprite(sprite_3);
                BgmScript.BgmMixControlAdd();

                BadCall = false;
            }
        }
    }

    private void ChangeSprite(Sprite sprite)
    {
        Img = GetComponent<Image>();
        Img.sprite = sprite;
    }

    public void ScoreLoopsAdd()
    {
        ScoreLoops++;
    }

    public void ScoreCountReset()
    {
        ScoreCount = 0;
    }

    public int GetScoreCount()
    {
        return ScoreCount;
    }

    // スコアロード
    public static void LoadScoreData()
    {
        HighScoreCountArray = new int[MaxStage];
        HighScoreLoopsArray = new int[MaxStage];
        for (int i = 0; i < MaxStage; i++)
        {
            HighScoreCountArray[i] = PlayerPrefs.GetInt("HighScoreCountStage" + (i + 1), 3);
            HighScoreLoopsArray[i] = PlayerPrefs.GetInt("HighScoreLoopsStage" + (i + 1), 99);
        }
    }

    // ハイスコアゲッター＆ハイスコアセットセーブ
    public static int GetScoreCountArray(int index)
    {
        // NULL除け
        if (index < MaxStage && index >= 0)
            return HighScoreCountArray[index];
        else
            return 3;
    }
    public void SetScoreCountArray(int index)
    {
        // NULL除け
        if (index < MaxStage && index >= 0)
        {
            if (HighScoreCountArray[index] > ScoreCount)
            {
                HighScoreCountArray[index] = ScoreCount;
                PlayerPrefs.SetInt("HighScoreCountStage" + (index + 1), HighScoreCountArray[index]);
            }
        }
    }
    public static int GetScoreLoopsArray(int stageNum)
    {
        // NULL除け
        if (stageNum < MaxStage && stageNum >= 0)
            return HighScoreLoopsArray[stageNum];
        else
            return 99;
    }
    public void SetScoreLoopsArray(int index)
    {
        // NULL除け
        if (index < MaxStage && index >= 0)
        {
            if (HighScoreLoopsArray[index] > ScoreLoops)
            {
                HighScoreLoopsArray[index] = ScoreLoops;
                PlayerPrefs.SetInt("HighScoreLoopsStage" + (index + 1), HighScoreLoopsArray[index]);
            }
        }
    }

    // パーティクル
    public void SetScoreParticle()
    {
        Instantiate(ScoreParticle, this.transform.position, Quaternion.identity);
    }
}
