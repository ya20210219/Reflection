using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalParticleController : MonoBehaviour
{
    [SerializeField] float deleteSpeed;

    ParticleSystem ps;
    ParticleSystem.EmissionModule em;
    ParticleSystem.MinMaxCurve mc;

    //[SerializeField] GameObject Door;
    //[SerializeField] float doorPos_x;       // ドア出現位置X
    //[SerializeField] float doorPos_y;       // ドア出現位置Y
    //[SerializeField] float doorPos_z;       // ドア出現位置Z
    //private bool isUse = false;

    //private float time;

    // Start is called before the first frame update
    void Start()
    {
        ps = this.gameObject.GetComponent<ParticleSystem>();
        em = ps.emission;
        mc = em.rateOverTime;
    }

    // Update is called once per frame
    void Update()
    {
        mc.constant -= deleteSpeed;
        em.rateOverTime = mc;

        //time += Time.deltaTime;
        //Debug.Log(time + "秒");

        //if(time > 5.0f)
        //{
        //    if (!isUse)
        //    { 
        //        Instantiate(Door, new Vector3(doorPos_x, doorPos_y, doorPos_z), Quaternion.identity);
        //        isUse = true;
        //    }

        //}
    }
}
