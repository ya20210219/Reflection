using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeImage : MonoBehaviour
{
    private GameObject StageObject = null;
    private StageManager StageScript = null;

    [SerializeField] private Image TimeBackNight = null;
    [SerializeField] private Image TimeBack = null;
    [SerializeField] private Image TimeSecondHand = null;
    [SerializeField] private Image TimeTome = null;


    private bool TimeChangeMorning;
    private bool TimeChangeNoon;
    private bool TimeChangeAfterNoon;

    private float ChangeTransformRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 60;

        StageObject = GameObject.Find("StageManager");
        if (StageObject != null)
        {
            StageScript = StageObject.GetComponent<StageManager>();
        }

        ChangeTransformRotation = -120.0f / StageScript.GetChangeTime();

        TimeBackNight.enabled = true;
        TimeBack.enabled = false;
        TimeSecondHand.enabled = false;
        TimeTome.enabled = false;

        TimeBoolAllfalse();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ChangeTransformRotation);

        if (StageScript.GetNowTimeZone() != StageManager.TIME_ZONE.NIGHT)
        {
            TimeBackNight.enabled = false;
            TimeBack.enabled = true;
            TimeSecondHand.enabled = true;
            TimeTome.enabled = true;

            if (StageScript.GetNowTimeZone() == StageManager.TIME_ZONE.MORNING && !TimeChangeMorning)
            {

                //TimeSecondHand.rectTransform.Rotate(0, 0, (int)ChangeTransformRotation);
                TimeSecondHand.rectTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 315.0f);
                TimeChangeMorning = true;

            }
            if (StageScript.GetNowTimeZone() == StageManager.TIME_ZONE.NOON && !TimeChangeNoon)
            {
                TimeSecondHand.rectTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 200.0f);
                TimeChangeNoon = true;
            }
            if (StageScript.GetNowTimeZone() == StageManager.TIME_ZONE.EVENING && !TimeChangeAfterNoon)
            {
                //TimeSecondHand.rectTransform.Rotate(0, 0, 75);
                TimeSecondHand.rectTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 75.0f);
                TimeChangeAfterNoon = true;
            }

            TimeSecondHand.rectTransform.Rotate(0, 0, (ChangeTransformRotation * Time.deltaTime));
        }
        else
        {
            TimeBackNight.enabled = true;
            TimeBack.enabled = false;
            TimeSecondHand.enabled = false;
            TimeTome.enabled = false;
        }

    }
    public void TimeBoolAllfalse()
    {
        TimeChangeMorning = false;
        TimeChangeNoon = false;
        TimeChangeAfterNoon = false;
    }
}
