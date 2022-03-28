using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMirrorController : MonoBehaviour
{
    private float rotateSpeed_LR = 120;       // 鏡回転速度
    private float rotateSpeed_LtRt = 15.0f;       // 鏡回転速度
    private float MirrorRange = 1.0f;        // 鏡までの距離

    [System.NonSerialized]
    public bool bHitLight = false;          // 光線がこの鏡に当たっているかどうか
    
    private Vector3 MirrorPos;              // PlayerMirror座標初期化用
    private GameObject Player;              // PlayerContlolScript取得用
    private PlayerController PlayerCtrlSc;
    
    void Start()
    {
        // 鏡の位置を親のx+1に指定する
        MirrorPos = transform.parent.gameObject.transform.position;
        MirrorPos.x= gameObject.transform.position.x + MirrorRange;
        transform.position = MirrorPos;

        Player = GameObject.Find("Player");
        PlayerCtrlSc = Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        // 鏡の回転の中心点更新
        MirrorPos = transform.parent.gameObject.transform.position;
        MirrorPos = new Vector3(MirrorPos.x, MirrorPos.y, 0.0f);
        // 回転させる角度
        float angle_Bottun = rotateSpeed_LR * Time.deltaTime;
        float angle_Trigger = rotateSpeed_LtRt * Time.deltaTime;
        // LRトリガー
        float LTr = Input.GetAxisRaw("L_Trigger");
        float RTr = Input.GetAxisRaw("R_Trigger");

        // Lステ左回転
        if (Input.GetKey("joystick button 4") || Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(MirrorPos, Vector3.forward, angle_Bottun);
        }
        // Rステ右回転
        else if (Input.GetKey("joystick button 5") || Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(MirrorPos, Vector3.forward, -angle_Bottun);
        }
        // L左回転
        if (LTr >= 0.5 || Input.GetKey(KeyCode.Z))
        {
            transform.RotateAround(MirrorPos, Vector3.forward, angle_Trigger);
        }
        // R右回転
        else if (RTr >= 0.5 || Input.GetKey(KeyCode.C))
        {
            transform.RotateAround(MirrorPos, Vector3.forward, -angle_Trigger);
        }
    }

    public void SetHitLight_True()
    {
        bHitLight = true;
    }
    public void SetHitLight_False()
    {
        bHitLight = false;
    }
}
