using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMStageSelectManager : MonoBehaviour
{
    [SerializeField] private CriAtomSource BgmStageSelect;
    private bool PlayMusicFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayMusicFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayMusicFlag)
        {
            BgmStageSelect.Play();
            SetPlayMusicFlag(true);
        }
    }

    public void SetPlayMusicFlag(bool flag)
    {
        PlayMusicFlag = flag;
    }

    public void SetValueToBGMAISAC(CriAtomSource BGMName, float aisacValue)
    {
        BGMName.SetAisacControl("BgmMixControl", aisacValue);
    }
}
