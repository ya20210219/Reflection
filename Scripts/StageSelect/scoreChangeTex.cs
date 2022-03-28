using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreChangeTex : MonoBehaviour
{
    [SerializeField] GameObject StageSelect;
    private stageSelect stageSelectSc;
    [SerializeField] private GameObject ScoreTextObject = null;
    private Text ScoreText = null;

    public Image scoreChangeImage;
    public Sprite[] score = new Sprite[4];
    
    private int scorePoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.LoadScoreData();

        if (StageSelect)
        {
            stageSelectSc = StageSelect.GetComponent<stageSelect>();
            scorePoint = ScoreManager.GetScoreCountArray(stageSelectSc.GetDoorNum());
        }
        if (ScoreTextObject)
        {
            ScoreText = ScoreTextObject.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StageSelect)
        {
            scorePoint = ScoreManager.GetScoreCountArray(stageSelectSc.GetDoorNum());
        }
        scoreChangeImage.sprite = score[scorePoint];

        if (ScoreManager.GetScoreLoopsArray(stageSelectSc.GetDoorNum()) == 99)
        {
            ScoreText.text = "?/" + ScoreManager.NormalScore[stageSelectSc.GetDoorNum()];
        }
        else
        {
            ScoreText.text = ScoreManager.GetScoreLoopsArray(stageSelectSc.GetDoorNum()) + "/" + ScoreManager.NormalScore[stageSelectSc.GetDoorNum()];
        }

        Debug.Log(scorePoint);
    }
}