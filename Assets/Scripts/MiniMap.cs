using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;
    public GameObject mapPlayer;
    float playerX;
    float playerZ;
    float mapPlayerX;
    float mapPlayerY;

    public Player playerScr;
    void Awake()
    {
        //playerScr = GetComponent<Player>();
    }

    void Update()
    {
        //playerScr.finalAngle
        //if (playerScr.moveVec != Vector3.zero)
        //{
        //    if (playerScr.finalAngle != 0)
        //        mapPlayer.transform.Rotate(Vector3.forward, playerScr.finalAngle);
        //}
        playerX = playerScr.finalMoveVecX;
        playerZ = playerScr.finalMoveVecZ;
        mapPlayerX = mapPlayer.GetComponent<RectTransform>().anchoredPosition.x;
        mapPlayerY = mapPlayer.GetComponent<RectTransform>().anchoredPosition.y;
        mapPlayerX += playerX * 150 / 286;
        mapPlayerY += playerZ * 222 / 414;
        //if (playerScr.finalAngle != 0)
        //{
            //mapPlayer.transform.Rotate(Vector3.forward, playerScr.finalAngle);
            //Quaternion.Euler(0, 0, playerScr.finalAngle)
            //mapPlayerX += playerX * 150 / 286;
            //mapPlayerY += playerZ * 222 / 414;
        //}
        mapPlayer.GetComponent<RectTransform>().anchoredPosition = new Vector3(mapPlayerX, mapPlayerY);
        
    }
}
