using UnityEngine;
using System.Collections;
/*

    Playerに関してのスクリプト

*/
public class PlayerScript : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    PlaceScript placeScript;
    [SerializeField]
    Vector3 playerPos;

    public bool isPlayerAction = false;

    private static readonly Vector3 DEFAULTPOS = new Vector3(0, 1, 0);
    private int firstPlayerPosRow;
    private int firstPlayerPosColumn;

    public void SetPlayerPos()
    {
        for (int row = 0; row < PlaceScript.MAX.ROW; row++)
        {
            for (int column = 0; column < PlaceScript.MAX.COLUMN; column++)
            {
                if(placeScript.GetComponent<PlaceScript>().place[row, column] == PlaceScript.TROUT.ROOM)
                {
                    playerPos = placeScript.GetComponent<PlaceScript>().placePos[row, column] + DEFAULTPOS;
                    row = PlaceScript.MAX.ROW;
                    break;
                }
            }
        }
        
    }

    public void SetPlayerObj()
    {
        GameObject playerObj = (GameObject)Instantiate(player, playerPos, Quaternion.identity);
        int row = (int)playerObj.transform.position.x;
        int column = (int)playerObj.transform.position.z;
        playerObj.name = "Player" + row +"," + column;
    }
}
