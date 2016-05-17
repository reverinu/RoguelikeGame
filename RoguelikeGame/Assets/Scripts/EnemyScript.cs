using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour {
    
    public struct ENEMY
    {
        public static readonly int BOXSLIME = 0;
    }

    public List<GameObject> enemyList;
    public int enemyNum;
    public int[] provisionalEnemyPos;
    public Vector3[] enemyPos;

    [SerializeField]
    PlaceScript placeScript;

    private static readonly Vector3 DEFAULTPOS = new Vector3(0 , 1, 0);

    public void SetEnemyPos()
    {
        enemyNum = placeScript.GetComponent<PlaceScript>().ROOMNUM;
        provisionalEnemyPos = new int[enemyNum];
        enemyPos = new Vector3[enemyNum];
        int enemyPosTemp = placeScript.GetComponent<PlaceScript>().roomTroutCount / enemyNum;
        int enemyPosTempTemp = enemyPosTemp;
        for (int i = 0; i < enemyNum; i++)
        {
            provisionalEnemyPos[i] = enemyPosTemp;
            enemyPosTemp += enemyPosTempTemp;
        }


        int k = 0;// エネミーの番号
        enemyPosTemp = 0;
        for (int row = 0; row < PlaceScript.MAX.ROW; row++)
        {
            for (int column = 0; column < PlaceScript.MAX.COLUMN; column++)
            {
                if (placeScript.GetComponent<PlaceScript>().place[row, column] == PlaceScript.TROUT.ROOM)
                {
                    enemyPosTemp++;
                    if(provisionalEnemyPos[k] == enemyPosTemp)
                    {
                        enemyPos[k] = placeScript.GetComponent<PlaceScript>().placePos[row, column] + DEFAULTPOS;
                        k++;
                    }
                    if(k == enemyNum - 1)
                    {
                        row = PlaceScript.MAX.ROW;
                        break;
                    }
                }
            }
        }
    }

    public void SetEnemyObj()
    {
        for (int i = 0; i < enemyNum; i++)
        {
            GameObject enemyObj = (GameObject)Instantiate(enemyList[ENEMY.BOXSLIME], enemyPos[i], Quaternion.identity);
            enemyObj.name = "Enemy" + i;
            enemyObj.transform.parent = this.transform;
        }
    }
}
