using UnityEngine;
using System.Collections;
/*
 * 
 *　プレイヤーの前方方向に何があるか見て結果を返す機能を保持するスクリプト 
 * 
 */
public class PlayerForwardChecker : MonoBehaviour {

    private struct DIRECTION
    {
        public static readonly int UP = 1;
        public static readonly int DOWN = -1;
        public static readonly int LEFT = 1;
        public static readonly int RIGHT = -1;
    }

    private GameObject player;
    private GameObject trout;

    public bool hasWall()
    {
        player = GameObject.Find("Player");
        bool hasWall = false;
        int nowPlayerPosRow = (int)player.transform.position.x;
        int nowPlayerPosColumn = (int)player.transform.position.z;
        
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            trout = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.UP) + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if(!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.UP) + "," + nowPlayerPosColumn);
            if(!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            trout = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.UP) + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.UP) + "," + nowPlayerPosColumn);
            if(!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            trout = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + nowPlayerPosColumn);
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            trout = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + nowPlayerPosColumn);
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            trout = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.UP) + "," + nowPlayerPosColumn);
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            trout = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + nowPlayerPosColumn);
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            trout = GameObject.Find("Trout" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            trout = GameObject.Find("Trout" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        return hasWall;
    }
}
