using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField]  private GameObject DoorLeftObject;
    [SerializeField]  private GameObject DoorRightObject;
    private Transform DoorLeftTrans = null;
    private Transform DoorRightTrans = null;
   

    private float DoorRotateLeft = 0.0f;
    private float DoorRotateRight = 0.0f;
    [SerializeField] private float AddRotate = 0.0f;
    private bool DoorFlag = false;
    private bool IsOnceSe = false;

    private GameObject SEObject = null;
    private SEManager SEScript = null;

    // Start is called before the first frame update
    void Start()
    {
        //DoorCollider = DoorObject.GetComponent<BoxCollider>();
        DoorLeftTrans  = DoorLeftObject.GetComponent<Transform>();
        DoorRightTrans = DoorRightObject.GetComponent<Transform>();
        SEObject = GameObject.Find("SEManager");       // SE のオブジェクト取得
        SEScript = SEObject.GetComponent<SEManager>(); // SE のスクリプト取得
    }

    // Update is called once per frame
    void Update()
    {
        if(DoorFlag == true)
        {
            if (DoorRotateLeft <= 90.0f && DoorRotateRight >= -90.0f)
            {
                if (!IsOnceSe)
                {
                    SEScript.SEPlay(SEScript.SEDoorOpen);
                    IsOnceSe = true;
                }
                DoorRotateLeft  += AddRotate;
                DoorRotateRight -= AddRotate;
                DoorLeftTrans.transform.Rotate(0.0f, AddRotate, 0.0f);
                DoorRightTrans.transform.Rotate(0.0f, -AddRotate, 0.0f);
            }
            else
            {
                
                DoorFlag = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("ドアが押された");
                DoorFlag = true;
            }
        }
    }
}
