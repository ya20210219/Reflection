using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMirrorRoot : MonoBehaviour
{
    [SerializeField] private GameObject Player;      // PlayerContlolScript取得用

    void Start()
    {
        Player = GameObject.Find("PlayerPawn");
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 0f);
    }
    
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 0f);
    }
}
