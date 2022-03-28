using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    [System.NonSerialized]
    public bool inout;      //出し入れの切り替え
    public bool OnPull = true;
    private bool State;     //引き出しているか・引き出されていないか
    private float oldposz;

    // Start is called before the first frame update
    void Start()
    {
        inout = false;
        State = OnPull;
        Vector3 pos = this.gameObject.transform.position;

        if (State)
        {
            this.gameObject.transform.position = new Vector3(pos.x, pos.y, 0);
        }
        else if (!State)
        {
            this.gameObject.transform.position = new Vector3(pos.x, pos.y, 1.5f);
        }

        oldposz = pos.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.gameObject.transform.position;

        if (inout)
        {
            if (State)
            {
                this.gameObject.transform.position = new Vector3(pos.x, pos.y, 1.5f);
                State = !State;

                ChangeInout();
            }
            else if (!State)
            {
                this.gameObject.transform.position = new Vector3(pos.x, pos.y, 0.0f);
                State = !State;

                ChangeInout();
            }
        }
    }

    public void ChangeInout()
    {
        if (inout == true)
        {
            inout = false;

        }
        else if (inout == false)
        {
            inout = true;
        }
    }
}