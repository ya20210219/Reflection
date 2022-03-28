using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorChangeTex : MonoBehaviour
{
    [SerializeField] GameObject StageSelect;
    [SerializeField] int ThisNum = 0;

    public Image doorChangeImage;
    public Sprite[] door = new Sprite[3];

    private int scorePoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (StageSelect)
        {
            scorePoint = ScoreManager.GetScoreCountArray(ThisNum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StageSelect)
        {
            scorePoint = ScoreManager.GetScoreCountArray(ThisNum);
            if (scorePoint > 2)
            {
                scorePoint = 2;
            }
        }
        doorChangeImage.sprite = door[scorePoint];
    }
}