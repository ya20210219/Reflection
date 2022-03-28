using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomHue : MonoBehaviour
{
    private GameObject stageMgr;        //ステージマネージャーのオブジェクト
    private StageManager StageMgrSc;

    private Image img;
    [SerializeField] Color32 Night   = new Color32(255, 255, 255, 10);
    [SerializeField] Color32 Morning = new Color32(255, 255, 255, 10);
    [SerializeField] Color32 Noon    = new Color32(255, 255, 255, 10);
    [SerializeField] Color32 Evening = new Color32(255, 255, 255, 10);

    private GameObject roomhue;
    

    // Start is called before the first frame update
    void Start()
    {
        stageMgr = GameObject.Find("StageManager");
        StageMgrSc = stageMgr.GetComponent<StageManager>();
        img = transform.GetComponent<Image>();

        img.color = Night;
    }

    // Update is called once per frame
    void Update()
    {
        float CT = StageMgrSc.GetChangeTime();
        float Time = CT - StageMgrSc.GetTimer();
        float value;
        //Debug.Log(Time);

        switch (StageMgrSc.GetNowTimeZone())
        {
            case StageManager.TIME_ZONE.NIGHT:
                img.color = Night;
                break;

            case StageManager.TIME_ZONE.MORNING:
                if(Time < CT / 2)
                {
                    // Night To Morning
                    value = Time / (CT / 2);

                    img.color = Color.Lerp(Night, Morning, Easing.QuartOut(value));
                }
                else if(Time > CT / 2)
                {
                    // To Noon
                    value = (Time - (CT / 2)) / CT;

                    img.color = Color.Lerp(Morning, Noon, Easing.QuartInOut(value));
                }
                break;

            case StageManager.TIME_ZONE.NOON:
                if (Time < CT / 2)
                {
                    // To Noon
                    value = (Time + (CT / 2)) / CT;

                    img.color = Color.Lerp(Morning, Noon, Easing.QuartInOut(value));
                }
                else if (Time > CT / 2)
                {
                    // To Evening
                    value = (Time - (CT / 2)) / CT;

                    img.color = Color.Lerp(Noon, Evening, Easing.QuartInOut(value));
                }

                break;

            case StageManager.TIME_ZONE.EVENING:
                if (Time < CT / 2)
                {
                    // To Evening
                    value = (Time + (CT / 2)) / CT;

                    img.color = Color.Lerp(Noon, Evening, Easing.QuartInOut(value));
                }
                else if (Time > CT / 2)
                {
                    // Evening To Night
                    value = (Time - (CT / 2)) / (CT / 2);

                    img.color = Color.Lerp(Evening, Night, Easing.QuartOut(value));
                }

                break;

            default:
                break;
        }
    }
}
