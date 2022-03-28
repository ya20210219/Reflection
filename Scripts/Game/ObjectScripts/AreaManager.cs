using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    // 全ての子オブジェクト
    List<Transform> AllChildren = new List<Transform>();
    List<Transform> AllMaterialObj = new List<Transform>();
    List<Vector3> AllMaterialDefPos = new List<Vector3>();

    [SerializeField] private float VariableValue = 0.6f;

    const float threshold = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        // 棚内のエリア全子要素を取得
        GetChildren(transform, ref AllChildren);

        // 棚内のmaterial所持オブジェクトを全て取得
        foreach(Transform Obj in AllChildren)
        {
            if (Obj.GetComponent<MeshRenderer>())
            {
                // デフォルトのメッシュ付きオブジェクト
                AllMaterialDefPos.Add(Obj.position);

                // 初期z座標次第で明度を下げてから格納
                if (Obj.position.z > threshold)
                {
                    // 暗め
                    float H, S, V;
                    Color.RGBToHSV(Obj.gameObject.GetComponent<Renderer>().material.color, out H, out S, out V);

                    V = Mathf.Clamp(V - VariableValue, 0, 1);

                    Obj.gameObject.GetComponent<Renderer>().material.color = Color.HSVToRGB(H, S, V);
                    Debug.Log(Obj.gameObject + " = 暗いよ");
                    Debug.Log(Obj.gameObject.GetComponent<Renderer>().material.color);
                }
                AllMaterialObj.Add(Obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < AllMaterialObj.Count; i++)
        {
            // z座標次第でHSVに変換して明度を下げる
            if (AllMaterialObj[i].position.z > threshold)
            {
                // 暗め
                float H, S, V;
                Color.RGBToHSV(AllMaterialObj[i].gameObject.GetComponent<Renderer>().material.color, out H, out S, out V);
            
                V = Mathf.Clamp(1.0f - VariableValue, 0, 1);
            
                AllMaterialObj[i].gameObject.GetComponent<Renderer>().material.color = Color.HSVToRGB(H, S, V);
            }
            else if(AllMaterialObj[i].position.z <= threshold)
            {
                // 元
                float H, S, V;
                Color.RGBToHSV(AllMaterialObj[i].gameObject.GetComponent<Renderer>().material.color, out H, out S, out V);

                V = 1.0f;

                AllMaterialObj[i].gameObject.GetComponent<Renderer>().material.color = Color.HSVToRGB(H, S, V);
            }
        }
    }

    // 先入れ式入れ子全捜査
    void GetChildren(Transform obj, ref List<Transform> AllObj)
    {
        //Transform ChildTf = obj;

        if (obj.childCount <= 0) return;

        foreach (Transform Child in obj)
        {
            AllObj.Add(Child);
            GetChildren(Child, ref AllObj);
        }
    }
}
