using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerTrigger : MonoBehaviour
{
    private GameObject Drawer;
    private DrawerController DrawerCon;
    private Shading2Manager ShadingMgr;

    private GameObject stageMgr;                    //ステージマネージャーのオブジェクト
    private StageManager StageMgrSc;

    //階段のキー入力後の計測
    private float ElapsedTime = 0.0f;
    //ボタンの連射を阻止
    private bool Counter_flag = false;
    [SerializeField] float RecastTime = 0.3f;
    //private bool KyeOn = false;
    

    // Start is called before the first frame update
    void Start()
    {
        Drawer = transform.parent.parent.gameObject;

        if (Drawer.tag == "Drawer")
        {
            DrawerCon = Drawer.GetComponent<DrawerController>();
            if (DrawerCon)
            {
                DrawerCon.SetOutlineFalse();
                Debug.Log("DrawerCon.SetOutlineFalse");
            }
        }
        else if (Drawer.tag == "Closet")
        {
            ShadingMgr = Drawer.GetComponent<Shading2Manager>();
            if (ShadingMgr)
            {
                ShadingMgr.SetOutlineFalse();
                Debug.Log("ShadingCon.SetOutlineFalse");
            }
        }

        stageMgr = GameObject.Find("StageManager");
        StageMgrSc = stageMgr.GetComponent<StageManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider collision)
    {
        //if (StageMgrSc.GetNowTimeZone() != StageManager.TIME_ZONE.NIGHT) return;

        if (collision.gameObject.tag == "Player")
        {
            if (Drawer.tag == "Drawer")
            {
                DrawerCon.SetOutlineTrue();
            }
            else if (Drawer.tag == "Closet")
            {
                ShadingMgr.SetOutlineTrue();
            }

            // 入力処理
            if ((Input.GetKey(KeyCode.C) || Input.GetKey("joystick button 1")) &&
                !Counter_flag)
            {
                Counter_flag = true;

                if (Drawer.tag == "Drawer")
                {
                    DrawerCon.ChangeInout();
                }
                else if (Drawer.tag == "Closet")
                {
                    ShadingMgr.ChangeInout();
                }
            }

            // ディレイ
            if (Counter_flag)
            {
                ElapsedTime += Time.deltaTime;

                if (ElapsedTime >= RecastTime)
                {
                    Counter_flag = false;
                    ElapsedTime = 0.0f;
                }
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        //if (StageMgrSc.GetNowTimeZone() != StageManager.TIME_ZONE.NIGHT) return;

        if (collision.gameObject.tag == "Player")
        {
            if (Drawer.tag == "Drawer")
            {
                DrawerCon.SetOutlineFalse();
            }
            else if (Drawer.tag == "Closet")
            {
                ShadingMgr.SetOutlineFalse();
            }
        }
    }
}
