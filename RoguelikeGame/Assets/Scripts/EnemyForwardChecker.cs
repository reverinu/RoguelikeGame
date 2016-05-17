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
    private GameObject trout;
    private GameObject otherEnemy;
    

    public bool hasWall(int direction)
    {
        bool hasWall = false;
        nowEnemyPosRow = (int)this.transform.position.x;
        nowEnemyPosColumn = (int)this.transform.position.z;


        if (direction == DIRECTIONCHECK.UPRIGHT)
        {
            trout = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.UP) + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.UPLEFT)
        {
            trout = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.UP) + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWNLEFT)
        {
            trout = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWNRIGHT)
        {
            trout = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
            trout = GameObject.Find("Trout" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.UP)
        {
            trout = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWN)
        {
            trout = GameObject.Find("Trout" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.LEFT)
        {
            trout = GameObject.Find("Trout" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
            {
                hasWall = true;
            }
        }
        else if (direction == DIRECTIONCHECK.RIGHT)
        {
            trout = GameObject.Find("Trout" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (!trout.GetComponent<TroutInfo>().isTraffic)
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
            otherEnemy = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.UP) + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
            otherEnemy = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
            otherEnemy = GameObject.Find("Enemy" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.UPLEFT)
        {
            otherEnemy = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.UP) + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
            otherEnemy = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
            otherEnemy = GameObject.Find("Enemy" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWNLEFT)
        {
            otherEnemy = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
            otherEnemy = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
            otherEnemy = GameObject.Find("Enemy" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWNRIGHT)
        {
            otherEnemy = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
            otherEnemy = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
            otherEnemy = GameObject.Find("Enemy" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.UP)
        {
            otherEnemy = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.UP) + "," + nowEnemyPosColumn);
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.DOWN)
        {
            otherEnemy = GameObject.Find("Enemy" + (nowEnemyPosRow + DIRECTION.DOWN) + "," + nowEnemyPosColumn);
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.LEFT)
        {
            otherEnemy = GameObject.Find("Enemy" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.LEFT));
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
        }
        else if (direction == DIRECTIONCHECK.RIGHT)
        {
            otherEnemy = GameObject.Find("Enemy" + nowEnemyPosRow + "," + (nowEnemyPosColumn + DIRECTION.RIGHT));
            if (otherEnemy != null)
            {
                hasOtherEnemy = true;
            }
        }


        return hasOtherEnemy;
    }
}
