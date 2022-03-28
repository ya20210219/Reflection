using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class GoalManager : MonoBehaviour
{
    OpenDoorController OpenDoorConSpt;
    private List<Outline> ChildOutline = new List<Outline>();


    void Start()
    {
        OpenDoorConSpt = GameObject.Find("OpenDoorController").GetComponent<OpenDoorController>();

        GetChildren(transform, ref ChildOutline);
        SetOutlineFalse();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            //シーン移動
            if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.O))
            {
                // ドアオープン
                OpenDoorConSpt.SetNextDoorSwitchTrue();
            }
            SetOutlineTrue();
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            SetOutlineFalse();
        }
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
