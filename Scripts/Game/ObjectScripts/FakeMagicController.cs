using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMagicController : MonoBehaviour
{
    GameObject sMgr;
    StageManager sMgrSc;
    
    [SerializeField] float On = 5.0f;       // 偽魔法陣発動

    /* スプライトのあれこれ */
    private SpriteRenderer sr;
    [SerializeField] Sprite MagicOn;
    [SerializeField] Sprite MagicOff;

    /* 魔法陣移動のあれこれ */
    private Vector2 MagicPos;               // 魔法陣位置
    [SerializeField] float MoveRange_x;     // 魔法陣可動範囲X
    [SerializeField] float MoveRange_y;     // 魔法陣可動範囲Y
    float Rotate = 10.0f;   // 魔法陣の回転

    private bool IsFake = false;            // 一度のみ実行フラグ

    /* 透過・時間 */
    private float alpha;
    private float FakeTime = 0.0f;
    private float FadeMorningTime = 2.0f;
    private bool IsHit = false;
    private bool MagicReset = false;

    /* パーティクル設定用 */
    [SerializeField] GameObject ParticleMagicNight;
    private GameObject ParticleMagicNight_Clone;

    /* パーティクル設定用 */
    [SerializeField] GameObject FakeMagicParticle;
    [SerializeField] GameObject HitParticle;
    private GameObject HitParticle_Clone;
    private bool IsParticle = false;        // 一度のみ実行フラグ

    private StageManager.TIME_ZONE OldTimeZone;

    private GameObject SEObject = null;
    private SEManager SEScript = null;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        MagicPos = transform.position;

        sMgr = GameObject.Find("StageManager");
        sMgrSc = sMgr.GetComponent<StageManager>();

        IsHit = false;

        SEObject = GameObject.Find("SEManager");
        if (SEObject != null)
        {
            SEScript = SEObject.GetComponent<SEManager>();
        }

        OldTimeZone = StageManager.TIME_ZONE.EVENING;
    }

    void Update()
    {
        // Mathf.Sinで-1～1の値を取り、可動範囲を乗算して動かす
        transform.position = new Vector2(Mathf.Sin(Time.time) * MoveRange_x + MagicPos.x, Mathf.Sin(Time.time) * MoveRange_y + MagicPos.y);

        float angle = Rotate * Time.deltaTime;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + angle);

        // パーティクル追従
        if (ParticleMagicNight_Clone)
        {
            ParticleMagicNight_Clone.transform.position = transform.position;
        }

        if (sMgrSc.GetClear()) return;

        if (sMgrSc.GetNowTimeZone() == StageManager.TIME_ZONE.NIGHT)
        {
            /* 夜の魔法陣処理 */

            // 時間が切り替わったタイミングのみ
            if (sMgrSc.GetNowTimeZone() != OldTimeZone)
            {
                MagicReset = false;
                ParticleMagicNight_Clone = Instantiate(ParticleMagicNight, this.transform.position, Quaternion.identity);
            }

            // リセット後魔法陣フェードイン
            if (alpha <= 1.0f && MagicReset)
            {
                alpha += Time.deltaTime / On;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            }
            // 魔法陣リセット、フェードアウト
            if (!MagicReset)
            {
                alpha -= Time.deltaTime / On;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

                if (alpha <= 0.0f)
                {
                    alpha = 0.0f;

                    // フラグ関係リセット
                    IsFake = IsParticle = false;
                    FakeTime = 0.0f;
                    this.tag = "FakeMagic";

                    MagicReset = true;
                }
            }

            SEScript.SEForcedStop(SEScript.SEFakeMagicCharge);
        }
        else
        {
            // 夜以外
            // 朝になったときのみ
            if (sMgrSc.GetNowTimeZone() == StageManager.TIME_ZONE.MORNING)
            {
                // 朝になった瞬間だけ透明度をリセットする
                if (sMgrSc.GetNowTimeZone() != OldTimeZone)
                {
                    FadeMorningTime = 2.0f;
                    if (ParticleMagicNight_Clone)
                    {
                        ParticleMagicNight_Clone.GetComponent<ParticleSystem>().Stop();
                    }
                }
                if (FadeMorningTime >= 0.0f)
                {
                    FadeMorningTime -= Time.deltaTime;
                    
                    alpha -= Time.deltaTime / 2.0f;
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

                    if (FadeMorningTime <= 0.0f && ParticleMagicNight_Clone)
                    {
                        Destroy(ParticleMagicNight_Clone);
                    }
                }
            }

            if (IsHit)
            {
                FakeTime += Time.deltaTime;

                if (FakeTime < On) // 第一段階になったら変化させる
                {
                    sr.sprite = MagicOff;
                    alpha += Time.deltaTime / On;
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

                    if (!IsParticle)
                    {
                        HitParticle_Clone = Instantiate(HitParticle, this.transform.position, Quaternion.identity); // パーティクル発動
                        IsParticle = true;
                    }

                    //SEScript.SEForcedPlay(SEScript.SEFakeMagicFirst);
                    SEScript.SEPlay(SEScript.SEFakeMagicCharge);
                    SEScript.SETimeCountReset();
                }
                else if (FakeTime >= On) // 第二段階になったら変化させる
                {
                    if (!IsFake)
                    {
                        sr.sprite = MagicOn;
                        Instantiate(FakeMagicParticle, this.transform.position, Quaternion.identity);
                        this.tag = "FakeMagicOn";
                        Debug.Log("偽魔法陣起動");

                        if (HitParticle_Clone)
                        {
                            Destroy(HitParticle_Clone);
                            IsParticle = false;
                        }

                        SEScript.SEForcedPlay(SEScript.SEFakeMagicSecond);
                    }
                    IsFake = true;
                }
            }
            else
            {
                if (HitParticle_Clone)
                {
                    Destroy(HitParticle_Clone);
                    IsParticle = false;
                }
                SEScript.SEForcedStop(SEScript.SEFakeMagicCharge);
            }
        }
        
        OldTimeZone = sMgrSc.GetNowTimeZone();
    }

    public void HitLight()
    {
        IsHit = true;
    }
    public void OutLight()
    {
        IsHit = false;
    }
}
