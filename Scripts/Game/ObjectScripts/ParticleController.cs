using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // ParticleSystem
    ParticleSystem particle;


    // ScaleUp用の経過時間
    private float elapsedScaleUpTime = 0.0f;

    // Scaleを大きくする間隔時間
    [SerializeField] float scaleUpTime = 0.03f;

    // ScaleUpする割合
    [SerializeField] float scaleUpParam = 0.1f;

    // パーティクル削除用の経過時間
    private float elapsedDeleteTime = 0f;

    // パーティクルを削除するまでの時間
    [SerializeField] float deleteTime = 0.1f;


    void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //if(particle.isStopped)
        //{
        //    Destroy(this.gameObject);
        //}

        elapsedScaleUpTime += Time.deltaTime;
        elapsedDeleteTime += Time.deltaTime;

        if (elapsedDeleteTime >= deleteTime)
        {
            Destroy(gameObject);
        }

        if (elapsedScaleUpTime > scaleUpTime)
        {
            transform.localScale += new Vector3(scaleUpParam, scaleUpParam, scaleUpParam);
            elapsedScaleUpTime = 0f;
        }

    }
}
