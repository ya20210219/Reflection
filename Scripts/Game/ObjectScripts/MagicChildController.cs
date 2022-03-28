//---------------------------------------------------------------------------------
//                      【子接続魔法陣管理】
//
// Author   :ohnari, Kureha
// Team     :Okemi
//---------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicChildController : MonoBehaviour
{
    /* ステージマネージャーのあれこれ */
    GameObject sMgr;
    StageManager sMgrSc;

    // 起動に必要な当てる時間用変数
    [SerializeField] private float LightHitTime = 1.5f;
    /* 透過・時間 */
    private float alpha = 0.0f;
    private float FadeMorningTime = 2.0f;
    [SerializeField] private float morningTime;
    [SerializeField] private float noonTime;
    [SerializeField] private float eveningTime;

    /* スプライトのあれこれ */
    private SpriteRenderer Spr;
    [SerializeField] Sprite ZeroMag;
    [SerializeField] Sprite Mor_Mag;
    [SerializeField] Sprite Mor_Noo_Mag;
    [SerializeField] Sprite Mor_Eve_Mag;
    [SerializeField] Sprite Noo_Mag;
    [SerializeField] Sprite Noo_Eve_Mag;
    [SerializeField] Sprite Eve_Mag;
    [SerializeField] Sprite FullMag;

    private float Rotate = 10.0f;　  // 魔法陣の回転
    private Vector2 MagicPos;               // 魔法陣位置
    [SerializeField] float MoveRange_x;     // 魔法陣可動範囲X
    [SerializeField] float MoveRange_y;     // 魔法陣可動範囲Y

    private bool IsAll = false;        // 完成フラグ
    private bool IsMorning = false;    // 朝に光が当てられたか
    private bool IsNoon = false;       // 朝に光が当てられたか
    private bool IsEvening = false;    // 朝に光が当てられたか


    /* パーティクル設定用 */
    [SerializeField] GameObject ParticleMagicNight;
    private GameObject ParticleMagicNight_Clone;

    [SerializeField] GameObject ParticleMagicLevel_1;
    [SerializeField] GameObject ParticleMagicLevel_2;
    [SerializeField] GameObject ParticleMagicLevel_Max;
    private int Level = 0;

    [SerializeField] GameObject HitParticle_Mor;
    [SerializeField] GameObject HitParticle_Noon;
    [SerializeField] GameObject HitParticle_Eve;
    private GameObject HitCloneParticle_Mor;
    private GameObject HitCloneParticle_Noon;
    private GameObject HitCloneParticle_Eve;
    // パーティクル生成フラグ
    private bool IsParticleMorning = false;
    private bool IsParticleNoon = false;
    private bool IsParticleEvening = false;

    // 光
    private bool IsHit = false;

    // SE
    private GameObject SEObject = null;
    private SEManager SEScript = null;

    // 比較用
    private StageManager.TIME_ZONE OldTimeZone;
    private bool MagicReset = false;


    void Start()
    {
        // 初期透明化
        Spr = GetComponent<SpriteRenderer>();
        var color = Spr.color;
        color.a = 0.0f;
        Spr.color = color;
        Spr.sprite = ZeroMag;

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
        if (HitCloneParticle_Mor)
        {
            HitCloneParticle_Mor.transform.position = transform.position;
        }
        if (HitCloneParticle_Noon)
        {
            HitCloneParticle_Noon.transform.position = transform.position;
        }
        if (HitCloneParticle_Eve)
        {
            HitCloneParticle_Eve.transform.position = transform.position;
        }

        if (sMgrSc.GetClear()) return;

        // 完成チェック
        if (IsMorning && IsNoon && IsEvening)
        {
            if (!IsAll)
            {
                Instantiate(ParticleMagicLevel_Max, this.transform.position, Quaternion.identity);
                IsAll = true;
            }
            this.tag = "MagicChildComp";
        }

        switch (sMgrSc.GetNowTimeZone())
        {
            /* 夜の魔法陣処理 */
            case StageManager.TIME_ZONE.NIGHT:
                // 時間が切り替わったタイミングのみ
                if (sMgrSc.GetNowTimeZone() != OldTimeZone)
                {
                    MagicReset = false;
                    Debug.Log("False = MagicReset");
                }

                // リセット後魔法陣フェードイン
                if (alpha <= 1.0f && MagicReset)
                {
                    var color = Spr.color;
                    alpha += Time.deltaTime / (LightHitTime * 3);
                    color.a = alpha;
                    Spr.color = color;
                }
                // 魔法陣リセット、フェードアウト
                if (!MagicReset)
                {
                    var color = Spr.color;
                    alpha -= Time.deltaTime / (LightHitTime * 3);
                    if (alpha <= 0.0f)
                    {
                        alpha = 0.0f;

                        // フラグ関係リセット
                        IsAll = IsMorning = IsNoon = IsEvening = false;
                        morningTime = noonTime = eveningTime = 0.0f;
                        Level = 0;
                        this.tag = "MagicChild";
                        if (!ParticleMagicNight_Clone)
                            ParticleMagicNight_Clone = Instantiate(ParticleMagicNight, this.transform.position, Quaternion.identity);

                        MagicReset = true;
                    }
                    color.a = alpha;
                    Spr.color = color;
                }
                break;

            /* 朝の魔法陣処理 */
            case StageManager.TIME_ZONE.MORNING:
                // 朝になった瞬間だけ透明度をリセットする
                if (sMgrSc.GetNowTimeZone() != OldTimeZone)
                {
                    //alpha = 0.0f;
                    //var color = Spr.color;
                    //color.a = alpha;
                    //Spr.color = color;
                    FadeMorningTime = 2.0f;
                    if (ParticleMagicNight_Clone)
                    {
                        ParticleMagicNight_Clone.GetComponent<ParticleSystem>().Stop();
                    }
                }
                if (FadeMorningTime >= 0.0f)
                {
                    FadeMorningTime -= Time.deltaTime;

                    var color = Spr.color;
                    alpha -= Time.deltaTime / 2.0f;
                    color.a = alpha;
                    Spr.color = color;
                }

                if (IsHit)
                {
                    morningTime += Time.deltaTime;

                    // 光が当たっている時、収束パーティクルを出現。起動パーティクルがでたら出現しなくなる
                    if (!IsMorning)
                    {
                        // 透明度加算
                        alpha += Time.deltaTime / (LightHitTime * 3);
                        var color = Spr.color;
                        color.a = alpha;
                        Spr.color = color;

                        if (!IsParticleMorning)
                        {
                            HitCloneParticle_Mor = Instantiate(HitParticle_Mor, this.transform.position, Quaternion.identity);
                            IsParticleMorning = true;
                        }
                        SEScript.SEPlay(SEScript.SEMagicCharge);
                        SEScript.SETimeCountReset();
                    }

                    if (morningTime > LightHitTime)
                    {
                        if (!IsMorning)
                        {
                            // 魔法陣起動
                            Level++;
                            CreateMagicParticle();

                            // 起動パーティクルが呼ばれたので、収束を消す。
                            if (HitCloneParticle_Mor)
                            {
                                Destroy(HitCloneParticle_Mor);
                                IsParticleMorning = false;
                            }

                            SEScript.SEForcedPlay(SEScript.SEMagicFirst);
                        }
                        IsMorning = true;
                    }
                }
                else
                {
                    if (HitCloneParticle_Mor)
                    {
                        Destroy(HitCloneParticle_Mor);
                        IsParticleMorning = false;
                    }
                    SEScript.SEForcedStop(SEScript.SEMagicCharge);
                }
                break;

            /* 昼の魔法陣処理 */
            case StageManager.TIME_ZONE.NOON:
                if (IsHit)
                {
                    noonTime += Time.deltaTime;


                    // 光が当たっている時、収束パーティクルを出現。起動パーティクルがでたら出現しなくなる
                    if (!IsNoon)
                    {
                        // 透明度加算
                        alpha += Time.deltaTime / (LightHitTime * 3);
                        var color = Spr.color;
                        color.a = alpha;
                        Spr.color = color;

                        if (!IsParticleNoon)
                        {
                            HitCloneParticle_Noon = Instantiate(HitParticle_Noon, this.transform.position, Quaternion.identity);
                            IsParticleNoon = true;
                        }
                        SEScript.SEPlay(SEScript.SEMagicCharge);
                        SEScript.SETimeCountReset();
                    }

                    if (noonTime > LightHitTime)
                    {
                        if (!IsNoon)
                        {
                            // 魔法陣起動
                            Level++;
                            CreateMagicParticle();

                            // 起動パーティクルが呼ばれたので、収束を消す。
                            if (HitCloneParticle_Noon)
                            {
                                Destroy(HitCloneParticle_Noon);
                                IsParticleNoon = false;
                            }

                            SEScript.SEForcedPlay(SEScript.SEMagicSecond);
                        }
                        IsNoon = true;
                    }
                }
                else
                {
                    if (HitCloneParticle_Noon)
                    {
                        Destroy(HitCloneParticle_Noon);
                        IsParticleNoon = false;
                    }
                    SEScript.SEForcedStop(SEScript.SEMagicCharge);
                }
                break;

            /* 夕方の魔法陣処理 */
            case StageManager.TIME_ZONE.EVENING:
                if (IsHit)
                {
                    eveningTime += Time.deltaTime;


                    // 光が当たっている時、収束パーティクルを出現。起動パーティクルがでたら出現しなくなる
                    if (!IsEvening)
                    {
                        // 透明度加算
                        alpha += Time.deltaTime / (LightHitTime * 3);
                        var color = Spr.color;
                        color.a = alpha;
                        Spr.color = color;

                        if (!IsParticleEvening)
                        {
                            HitCloneParticle_Eve = Instantiate(HitParticle_Eve, this.transform.position, Quaternion.identity);
                            IsParticleEvening = true;
                        }
                        SEScript.SEPlay(SEScript.SEMagicCharge);
                        SEScript.SETimeCountReset();
                    }

                    if (eveningTime > LightHitTime)
                    {
                        if (!IsEvening)
                        {
                            // 魔法陣起動
                            Level++;
                            CreateMagicParticle();

                            // 起動パーティクルが呼ばれたので、収束を消す。
                            if (HitCloneParticle_Eve)
                            {
                                Destroy(HitCloneParticle_Eve);
                                IsParticleEvening = false;
                            }

                            SEScript.SEForcedPlay(SEScript.SEMagicFinish);
                        }
                        IsEvening = true;
                    }
                }
                else
                {
                    if (HitCloneParticle_Eve)
                    {
                        Destroy(HitCloneParticle_Eve);
                        IsParticleEvening = false;
                    }
                    SEScript.SEForcedStop(SEScript.SEMagicCharge);
                }
                break;
        }

        // ごり押し条件スプライト設定
        if (IsMorning)
        {
            Spr.sprite = Mor_Mag;
            if (IsNoon)
            {
                Spr.sprite = Mor_Noo_Mag;
                if (IsEvening)
                {
                    Spr.sprite = FullMag;
                    IsAll = true;
                }
            }
            else if (IsEvening)
            {
                Spr.sprite = Mor_Eve_Mag;
            }
        }
        else if (IsNoon)
        {
            Spr.sprite = Noo_Mag;
            if (IsEvening)
            {
                Spr.sprite = Noo_Eve_Mag;
            }
        }
        else if (IsEvening)
        {
            Spr.sprite = Eve_Mag;
        }
        else
        {
            Spr.sprite = ZeroMag;
        }


        if (sMgrSc.GetNowTimeZone() != StageManager.TIME_ZONE.NIGHT &&
            FadeMorningTime <= 0.0f &&
            ParticleMagicNight_Clone)
        {
            Destroy(ParticleMagicNight_Clone);
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

    private void CreateMagicParticle()
    {
        if(Level == 1)
        {
            Instantiate(ParticleMagicLevel_1, this.transform.position, Quaternion.identity);
        }
        else if(Level == 2)
        {
            Instantiate(ParticleMagicLevel_2, this.transform.position, Quaternion.identity);
        }
        else if (Level == 3)
        {
            Instantiate(ParticleMagicLevel_Max, this.transform.position, Quaternion.identity);
        }
    }
}
