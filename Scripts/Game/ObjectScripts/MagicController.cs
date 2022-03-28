using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{

    [SerializeField] float First = 1.5f;
    [SerializeField] float Second = 3.0f;
    [SerializeField] float Third = 5.0f;

    private SpriteRenderer sr;
    [SerializeField] Sprite FirstMagic;
    [SerializeField] Sprite SecondMagic;
    [SerializeField] Sprite ThirdMagic;

    private Vector2 MagicPos;               // 魔法陣位置
    [SerializeField] float MoveRange_x;     // 魔法陣可動範囲X
    [SerializeField] float MoveRange_y;     // 魔法陣可動範囲Y

    private bool isCalledOnce = false;      // 一度のみ実行フラグ
    private bool isCalledOnce1 = false;      // 一度のみ実行フラグ
    private bool isCalledOnce2 = false;      // 一度のみ実行フラグ
    private bool isCalledOnce3 = false;      // 一度のみ実行フラグ

    [SerializeField] float alpha;
    private float time;

    public GameObject Door;                 // ドアを生成する準備
    [SerializeField] float doorPos_x;       // ドア出現位置X
    [SerializeField] float doorPos_y;       // ドア出現位置Y


    [SerializeField] GameObject particleObject;
    [SerializeField] GameObject particleObject2;
    [SerializeField] GameObject particleObject3;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        MagicPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector2(Mathf.Sin(Time.time) * MoveRange_x + MagicPos.x, Mathf.Sin(Time.time) * MoveRange_y + MagicPos.y);
        transform.Rotate(new Vector3(0, 0, 1));
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "LightTop")
        {
            time += Time.deltaTime;
            if (time < First) // 第一段階になったら変化させる
            {
                if (!isCalledOnce1)
                {
                    Instantiate(particleObject, this.transform.position, Quaternion.identity);
                    isCalledOnce1 = true;
                }
                sr.sprite = FirstMagic;
                alpha = 0.01f + time / Third;
                sr.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            }
            else if(time > First && time < Second) // 第二段階になったら変化させる
            {
                if (!isCalledOnce2)
                {
                    Instantiate(particleObject2, this.transform.position, Quaternion.identity);
                    isCalledOnce2 = true;
                }
                sr.sprite = SecondMagic;
                alpha = 0.01f + time / Third;
                sr.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            }
            else if(time > Second && time < Third) // 第三段階になったら変化させる
            {
                if (!isCalledOnce3)
                {
                    Debug.Log("呼び出されてます");
                    Instantiate(particleObject3, this.transform.position, Quaternion.identity);
                    isCalledOnce3 = true;
                }
                sr.sprite = ThirdMagic;
                alpha = 0.01f + time / Third;
                sr.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            }
            else if(!isCalledOnce)
            { 
                Instantiate(Door, new Vector3(doorPos_x, doorPos_y, 0.0f), Quaternion.identity);
                isCalledOnce = true;
            }
            
        }
    }
}
