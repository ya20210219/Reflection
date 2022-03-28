using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class Shading2Manager : MonoBehaviour
{
    private Transform GateTransform;
    private Vector3 DefRot;
    private Vector3 DefPos;

    [SerializeField] private bool OnPull = true;  //出し入れの切り替え
    private bool State;   //引き出しているか・引き出されていないか

    private List<Outline> ChildOutline = new List<Outline>();

    private GameObject SEObject = null; // SE のオブジェクト
    private SEManager SEScript = null; // SE のスクリプト
    private bool SEFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        GateTransform = transform.GetChild(1);
        //Debug.Log(GateTransform.position.ToString("F6"));

        DefRot = GateTransform.transform.localEulerAngles;
        DefPos = GateTransform.position;
        State = OnPull;

        GetChildren(transform, ref ChildOutline);

        SEObject = GameObject.Find("SEManager");       // SE のオブジェクト取得
        SEScript = SEObject.GetComponent<SEManager>(); // SE のスクリプト取得

        if (State)
        {
            GateTransform.localEulerAngles = new Vector3(DefRot.x, -90, DefRot.z);
            GateTransform.position = new Vector3(DefPos.x, DefPos.y, 1.15f);
        }
        else if (!State)
        {
            GateTransform.localEulerAngles = new Vector3(DefRot.x, DefRot.y, DefRot.z);
            GateTransform.position = new Vector3(DefPos.x, DefPos.y, DefPos.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (State)
        {
            GateTransform.localEulerAngles = new Vector3(DefRot.x, -90, DefRot.z);
            GateTransform.position = new Vector3(DefPos.x, DefPos.y, 1.15f);

            if (SEFlag == false)
            {
                SEScript.SEStop(SEScript.SEBookPush);
                SEScript.SEPlay(SEScript.SEBookPull);
                SEFlag = true;
            }
        }
        else if (!State)
        {
            GateTransform.localEulerAngles = new Vector3(DefRot.x, DefRot.y, DefRot.z);
            GateTransform.position = new Vector3(DefPos.x, DefPos.y, DefPos.z);

            if (SEFlag == false)
            {
                SEScript.SEStop(SEScript.SEBookPull);
                SEScript.SEPlay(SEScript.SEBookPush);
                SEFlag = true;
            }
        }
    }

    public void ChangeInout()
    {
        if (State)
        {
            State = false;
        }
        else if (!State)
        {
            State = true;
        }
        SEFlag = false;
    }
    public void SetOutlineTrue()
    {
        foreach (Outline obj in ChildOutline)
        {
            obj.eraseRenderer = false;
        }
    }
    public void SetOutlineFalse()
    {
        foreach (Outline obj in ChildOutline)
        {
            obj.eraseRenderer = true;
        }
    }

    // 先入れ式入れ子全捜査
    static void GetChildren(Transform obj, ref List<Outline> AllObj)
    {
        if (obj.childCount <= 0) return;

        foreach (Transform Child in obj)
        {
            if (Child.GetComponent<Outline>())
            {
                AllObj.Add(Child.GetComponent<Outline>());
            }
            GetChildren(Child, ref AllObj);
        }
    }
}
