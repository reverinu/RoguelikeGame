using UnityEngine;
using System.Collections;

public class EnemyForwardChecker : MonoBehaviour {

    private struct DIRECTION
    {
        public static readonly int UP = 1;
        public static readonly int DOWN = -1;
        public static readonly int LEFT = 1;
        public static readonly int RIGHT = -1;
    }

    private struct DIRECTIONCHECK
    {
        public static readonly int UP = 0;
        public static readonly int DOWN = 1;
        public static readonly int LEFT = 2;
        public static readonly int RIGHT = 3;
        public static readonly int UPLEFT = 4;
        public static readonly int UPRIGHT = 5;
        public static readonly int DOWNLEFT = 6;
        public static readonly int DOWNRIGHT = 7;
    }

    private int nowEnemyPosRow;
    private int nowEnemyPosColumn;
    private GameObject obj;

    public bool hasAll(int direction)
    {
        bool hasAll = false;
        if(hasWall(direction) || hasOtherEnemy(direction) || hasPlayer(direction))
        {
            hasAll = true;
        }
        return hasAll;
    }
    

    public bool hasWall(int direction)
    {
        bool hasWall = false;
        nowEnemyPosRow = (int)this.transform.position.x;
        nowEnemyPosColumn = (int)this.transform.position.z;


        if (direction == DIRECTIONCHECK.UPRIGHT)
        {
            obj = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.UP) + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.UPLEFT)
        {
            obj = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.UP) + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWNLEFT)
        {
            obj = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWNRIGHT)
        {
            obj = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            obj = GameObject.Find("Trout" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.UP)
        {
            obj = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWN)
        {
            obj = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.LEFT)
        {
            obj = GameObject.Find("Trout" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.RIGHT)
        {
            obj = GameObject.Find("Trout" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (!obj.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        return hasWall;
    }

    public bool hasOtherEnemy(int direction)
    {
        bool hasOtherEnemy = false;
        nowEnemyPosRow = (int)this.transform.position.x;
        nowEnemyPosColumn = (int)this.transform.position.z;

        if (direction == DIRECTIONCHECK.UPRIGHT)
        {
            obj = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.UP) + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
            obj = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
            obj = GameObject.Find("Enemy" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.UPLEFT)
        {
            obj = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.UP) + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
            obj = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
            obj = GameObject.Find("Enemy" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWNLEFT)
        {
            obj = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
            obj = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
            obj = GameObject.Find("Enemy" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWNRIGHT)
        {
            obj = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
            obj = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
            obj = GameObject.Find("Enemy" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.UP)
        {
            obj = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWN)
        {
            obj = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.LEFT)
        {
            obj = GameObject.Find("Enemy" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.RIGHT)
        {
            obj = GameObject.Find("Enemy" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasOtherEnemy = true;
            }
        }
        return hasOtherEnemy;
    }

    public bool hasPlayer(int direction)
    {
        bool hasPlayer = false;
        nowEnemyPosRow = (int)this.transform.position.x;
        nowEnemyPosColumn = (int)this.transform.position.z;

        if (direction == DIRECTIONCHECK.UPRIGHT)
        {
            obj = GameObject.Find("Player" + (nowEnemyPosRow + DIRECTION.UP) + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasPlayer = true;
            }
            obj = GameObject.Find("Player" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (obj != null)
            {
                hasPlayer = true;
            }
            obj = GameObject.Find("Player" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasPlayer = true;
            }
        }
        else if (direction == DIRECTIONCHECK.UPLEFT)
        {
            obj = GameObject.Find("Player" + (nowEnemyPosRow + DIRECTION.UP) + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasPlayer = true;
            }
            obj = GameObject.Find("Player" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (obj != null)
            {
                hasPlayer = true;
            }
            obj = GameObject.Find("Player" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasPlayer = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWNLEFT)
        {
            obj = GameObject.Find("Player" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasPlayer = true;
            }
            obj = GameObject.Find("Player" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (obj != null)
            {
                hasPlayer = true;
            }
            obj = GameObject.Find("Player" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasPlayer = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWNRIGHT)
        {
            obj = GameObject.Find("Player" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasPlayer = true;
            }
            obj = GameObject.Find("Player" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (obj != null)
            {
                hasPlayer = true;
            }
            obj = GameObject.Find("Player" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasPlayer = true;
            }
        }
        else if (direction == DIRECTIONCHECK.UP)
        {
            obj = GameObject.Find("Player" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (obj != null)
            {
                hasPlayer = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWN)
        {
            obj = GameObject.Find("Player" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (obj != null)
            {
                hasPlayer = true;
            }
        }
        else if (direction == DIRECTIONCHECK.LEFT)
        {
            obj = GameObject.Find("Player" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (obj != null)
            {
                hasPlayer = true;
            }
        }
        else if (direction == DIRECTIONCHECK.RIGHT)
        {
            obj = GameObject.Find("Player" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (obj != null)
            {
                hasPlayer = true;
            }
        }
        return hasPlayer;
    }
}
