using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    //public enum SE_NAME
    //{
    //    DOOR_APPER,
    //    PLAYER_STEP,
    //    PLAYER_JUMP,
    //    PLAYER_LANDING,
    //    BOOK_PULL,
    //    BOOK_PUSH,
    //    DOOR_OPEN,
    //    MAGIC_CIRCLE_CHARGE,
    //    MAGIC_CIRCLE_FIRST,
    //    MAGIC_CIRCLE_SECOND,
    //    MAGIC_CIRCLE_FINISH,
    //    FAKE_MAGIC_CIRCLE_CHARGE,
    //    FAKE_MAGIC_CIRCLE_FIRST,
    //    FAKE_MAGIC_CIRCLE_SECOND,
    //    FAKE_MAGIC_CIRCLE_FINISH,
    //    MIRROR,
    //    PLAYER_MIRROR,
    //    TIME_LOOP,
    //    SE_NAME_MAX

    //}
    
    public CriAtomSource SEPlayerStep;
    public CriAtomSource SEPlayerJump;
    public CriAtomSource SEPlayerLanding;
    public CriAtomSource SEPlayerMirror;
    public CriAtomSource SEMirror;
    public CriAtomSource SEBookPull;
    public CriAtomSource SEBookPush;
    public CriAtomSource SETimeLoop;
    public CriAtomSource SEMagicCharge;
    public CriAtomSource SEMagicFirst;
    public CriAtomSource SEMagicSecond;
    public CriAtomSource SEMagicFinish;
    public CriAtomSource SEFakeMagicCharge;
    public CriAtomSource SEFakeMagicFirst;
    public CriAtomSource SEFakeMagicSecond;
    public CriAtomSource SEFakeMagicFinish;
    public CriAtomSource SEDoorPop;
    public CriAtomSource SEDoorOpen;
    public CriAtomSource SEStageSelect;
    public CriAtomSource SEStageMove;
    public CriAtomSource SEStageSelectMove;
    public CriAtomSource SEMagicChargePause;
    public CriAtomSource SEFakeMagicChargePause;

    private int TimeCount = 0;

    private float halfVolume = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        //SEPlayerStep.SetVolume(halfVolume);
        //Debug.Log("halfVolume:" + GetVolume);
        //SEPlayerJump.SetVolume(halfVolume);
        //SEPlayerLanding.SetVolume(halfVolume);
        //SEPlayerMirror.SetVolume(halfVolume);
        //SEMirror.SetVolume(halfVolume);
        //SEBookPull.SetVolume(halfVolume);
        //SEBookPush.SetVolume(halfVolume);
        //SETimeLoop.SetVolume(halfVolume);
        //SEMagicCharge.SetVolume(halfVolume);
        //SEMagicFirst.SetVolume(halfVolume);
        //SEMagicSecond.SetVolume(halfVolume);
        //SEMagicFinish.SetVolume(halfVolume);
        //SEFakeMagicCharge.SetVolume(halfVolume);
        //SEFakeMagicFirst.SetVolume(halfVolume);
        //SEFakeMagicSecond.SetVolume(halfVolume);
        //SEFakeMagicFinish.SetVolume(halfVolume);
        //SEDoorPop.SetVolume(halfVolume);
        //SEDoorOpen.SetVolume(halfVolume);
        //SEStageSelect.SetVolume(halfVolume);
        //SEStageMove.SetVolume(halfVolume);
        //SEStageSelectMove.SetVolume(halfVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SEPlay(CriAtomSource SEName)
    {
        if (SEName.status == CriAtomSource.Status.Stop || 
            SEName.status == CriAtomSource.Status.PlayEnd)
        {
            SEName.Play();
        }
        
    }

    public void SEStop(CriAtomSource SEName)
    {
        if (SEName.status == CriAtomSource.Status.Prep ||
            SEName.status == CriAtomSource.Status.Playing)
        {
            SEName.Stop();
        }
    }

    public void SEForcedPlay(CriAtomSource SEName)
    {
        SEName.Play();
    }

    public void SEForcedStop(CriAtomSource SEName)
    {
        SEName.Stop();
    }

    public void SETimePlay(CriAtomSource SEName, int time) // よくないから使わないで
    {
        TimeCount++;

        if (TimeCount >= time)
        {
            SEName.Play();
            TimeCount = 0;
        }
    }

    public void SETimeStop(CriAtomSource SEName, int time)
    {
        TimeCount++;

        if (TimeCount >= time)
        {
            SEName.Stop();
            TimeCount = 0;
        }
    }

    public void SETimePause(CriAtomSource SEName, int time, bool flag)
    {
        TimeCount++;

        if (TimeCount >= time)
        {
            SEName.Pause(flag);
            TimeCount = 0;
        }
    }

    public void SETimeCountReset()
    {
        TimeCount = 0;
    }

    
}
