using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //------------------------------------------------------------
    //アニメーション変数
    //------------------------------------------------------------
    private Animator anim = null; //アニメーターの格納
    private bool JumpAni;
    private bool RunAni; //ランが有効かどうかのフラグ
    private bool WaitAni; //ウェイトが有効化どうか
    private bool KazasuAni; //かざすが有効化どうか
    private bool AnimeFlag;

    //------------------------------------------------------------
    //プレイヤー変数
    //------------------------------------------------------------
    private float MoveSpeed = 5;          // プレイヤーに加えるスピード用変数
    private float Gravity = 4.3f;
    private float JumpForce = 1250;          // プレイヤーのジャンプ力用変数
    private Rigidbody RigidBody;
    private bool CanJump;                   //ジャンプが可能かどうかのフラグ
    // zに0.6めも
    public Text CntTex;

    private GameObject SEObject = null;
    private SEManager  SEScript = null;
    private int PlayerWalk = 0;
    private const int PlayerWalkTime = 150;


    void Start()
    {
        //StageManager = GameObject.Find("StageManager");
        //script = StageManager.GetComponent<StageManager>();
        //transform.position = script.StartPos;
        CanJump = false;       //初期は一応false

        anim = GetComponent<Animator>(); //アニメーターのコンポーネント取得
        JumpAni = false;
        RunAni = false;
        WaitAni = false;
        KazasuAni = false;

        RigidBody = transform.GetComponent<Rigidbody>();

        SEObject = GameObject.Find("SEManager");
        if (SEObject != null)
        {
            SEScript = SEObject.GetComponent<SEManager>();
        }
        
    }

    void Update()
    {
        //LRスティックの角度を取得
        float lsh = Input.GetAxisRaw("L_Stick_H");
        float lsv = Input.GetAxisRaw("L_Stick_V");
        
        // 移動
        // ワールド座標を基準に、回転を取得
        Vector3 worldAngle = transform.eulerAngles;
        if (Input.GetKey(KeyCode.D) || lsh >= 0.1)   //Dキーが押されたかどうかを判定している
        {
            RigidBody.velocity = new Vector3(MoveSpeed, RigidBody.velocity.y, 0.0f);
            worldAngle.y = 0.0f;

            if(CanJump == true)
            {
                PlayerWalk++;

                if (PlayerWalk >= PlayerWalkTime)
                {
                    
                    SEScript.SEForcedPlay(SEScript.SEPlayerStep);
                    PlayerWalk = 0;
                }
            }
            
            RunAni = true;
            WaitAni = false;
        }
        else if (Input.GetKey(KeyCode.A) || lsh <= -0.1)
        {
            RigidBody.velocity = new Vector3(-MoveSpeed, RigidBody.velocity.y, 0.0f);
            worldAngle.y = 180.0f;

            if (CanJump == true)
            {
                PlayerWalk++;

                if (PlayerWalk >= PlayerWalkTime)
                {
                    SEScript.SEForcedPlay(SEScript.SEPlayerStep);
                    PlayerWalk = 0;
                }
            }

            RunAni = true;
            WaitAni = false;
        }
        else
        {
            RigidBody.velocity = new Vector3(0.0f, RigidBody.velocity.y, 0.0f);
            WaitAni = true;
            RunAni = false;
        }
        // 回転角度を設定
        transform.eulerAngles = worldAngle;

        // ジャンプ
        if (CanJump && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Space)))
        {
            RigidBody.AddForce(transform.up * JumpForce);
            JumpAni = true;
            WaitAni = false;
            SEScript.SEPlay(SEScript.SEPlayerJump);
        }
        else
        {
            JumpAni = false;
            WaitAni = true;
        }

        //かざす
        if (Input.GetKey(KeyCode.F))
        {
            KazasuAni = true;
            WaitAni = false;
        }
        else
        {
            KazasuAni = false;
            WaitAni = true;
        }

        RigidBody.velocity = new Vector3(RigidBody.velocity.x, RigidBody.velocity.y +(-9.81f * Gravity * Time.deltaTime), 0.0f);

        SetAnimation(); // アニメーション反映
    }

    private void OnCollisionStay(Collision Collision)   //オブジェクトと当たっている時だけ(Stay)
    {
        if ((Collision.gameObject.tag == "floor" ||
            Collision.gameObject.tag == "MirrorFloor" ||
            Collision.gameObject.tag == "Drawer" ||
            Collision.gameObject.tag == "Stairs"))
        {
            Ray ray = new Ray(transform.position, -transform.up);
            int LayerObj = LayerMask.NameToLayer("Object");
            int LayerIgnore = LayerMask.NameToLayer("Ignore Raycast");
            int LayerMaskObj = 1 << LayerObj | 1 << LayerIgnore;
            if (Physics.Raycast(ray, out RaycastHit DefHitPos, 1.2f, LayerMaskObj))
            {
                if ((DefHitPos.transform.gameObject.tag == "floor" ||
                     DefHitPos.transform.gameObject.tag == "MirrorFloor" ||
                     DefHitPos.transform.gameObject.tag == "Drawer" ||
                     DefHitPos.transform.gameObject.tag == "Stairs"))
                {
                    if (!CanJump)
                    {
                        SEScript.SEPlay(SEScript.SEPlayerLanding);
                        JumpAni = false;
                    }
                    CanJump = true;   //ジャンプ有効
                }
            }
        }
    }
    private void OnCollisionExit(Collision Collision)   //オブジェクトと当たっていない時だけ(Exit)
    {
        //if (Collision.gameObject.tag == "floor" ||
        //    Collision.gameObject.tag == "MirrorFloor" ||
        //    Collision.gameObject.tag == "Drawer" ||
        //    Collision.gameObject.tag == "Stairs")
        if (RigidBody.velocity.y != 0.0f)
        {
            CanJump = false;   //ジャンプ無効
        }
    }

    private void SetAnimation()
    {
        anim.SetBool("Jump", JumpAni);
        anim.SetBool("Wait", WaitAni);
        anim.SetBool("Run", RunAni);
        anim.SetBool("Kazasu", KazasuAni);
    }
}
