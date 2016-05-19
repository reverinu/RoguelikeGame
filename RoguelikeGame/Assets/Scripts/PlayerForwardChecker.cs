using UnityEngine;
using System.Collections;
/*
 * 
 *　プレイヤーの前方方向に何があるか見て結果を返す機能を保持するスクリプト 
 * 
 */
public class PlayerForwardChecker : BaseForwardChecker {
    private GameObject player;
    private GameObject obj;

    public bool hasAll()
    {
        bool hasAll = false;
        if (hasWall() || hasEnemy())
        {
            hasAll = true;
        }
        return hasAll;
    }

    public bool hasWall()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bool hasWall = false;
        int nowPlayerPosRow = (int)player.transform.position.x;
        int nowPlayerPosColumn = (int)player.transform.position.z;
        
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            obj = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.UP) + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if(!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.UP) + "," + nowPlayerPosColumn);
            if(!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            obj = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.UP) + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.UP) + "," + nowPlayerPosColumn);
            if(!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            obj = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + nowPlayerPosColumn);
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            obj = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + nowPlayerPosColumn);
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            obj = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.UP) + "," + nowPlayerPosColumn);
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            obj = GameObject.Find("Trout" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + nowPlayerPosColumn);
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            obj = GameObject.Find("Trout" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            obj = GameObject.Find("Trout" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        return hasWall;
    }
    public bool hasEnemy()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bool hasEnemy = false;
        int nowPlayerPosRow = (int)player.transform.position.x;
        int nowPlayerPosColumn = (int)player.transform.position.z;

        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            obj = GameObject.Find("Enemy" + (nowPlayerPosRow + DIRECTION.UP) + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasEnemy = true;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            obj = GameObject.Find("Enemy" + (nowPlayerPosRow + DIRECTION.UP) + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasEnemy = true;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            obj = GameObject.Find("Enemy" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasEnemy = true;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            obj = GameObject.Find("Enemy" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasEnemy = true;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            obj = GameObject.Find("Enemy" + (nowPlayerPosRow + DIRECTION.UP) + "," + nowPlayerPosColumn);
            if (obj != null)
            {
                hasEnemy = true;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            obj = GameObject.Find("Enemy" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + nowPlayerPosColumn);
            if (obj != null)
            {
                hasEnemy = true;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            obj = GameObject.Find("Enemy" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasEnemy = true;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            obj = GameObject.Find("Enemy" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasEnemy = true;
            }
        }
        return hasEnemy;
    }

    public GameObject getEnemyObj(int direction)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        int nowPlayerPosRow = (int)player.transform.position.x;
        int nowPlayerPosColumn = (int)player.transform.position.z;

        if (direction == DIRECTIONCHECK.UPRIGHT)
        {
            obj = GameObject.Find("Enemy" + (nowPlayerPosRow + DIRECTION.UP) + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
        }
        else if (direction == DIRECTIONCHECK.UPLEFT)
        {
            obj = GameObject.Find("Enemy" + (nowPlayerPosRow + DIRECTION.UP) + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
        }
        else if (direction == DIRECTIONCHECK.DOWNRIGHT)
        {
            obj = GameObject.Find("Enemy" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
        }
        else if (direction == DIRECTIONCHECK.DOWNLEFT)
        {
            obj = GameObject.Find("Enemy" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
        }
        else if (direction == DIRECTIONCHECK.UP)
        {
            obj = GameObject.Find("Enemy" + (nowPlayerPosRow + DIRECTION.UP) + "," + nowPlayerPosColumn);
        }
        else if (direction == DIRECTIONCHECK.DOWN)
        {
            obj = GameObject.Find("Enemy" + (nowPlayerPosRow + DIRECTION.DOWN) + "," + nowPlayerPosColumn);
        }
        else if (direction == DIRECTIONCHECK.LEFT)
        {
            obj = GameObject.Find("Enemy" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.LEFT));
        }
        else if (direction == DIRECTIONCHECK.RIGHT)
        {
            obj = GameObject.Find("Enemy" + nowPlayerPosRow + "," + (nowPlayerPosColumn + DIRECTION.RIGHT));
        }
        return obj;
    }
}
