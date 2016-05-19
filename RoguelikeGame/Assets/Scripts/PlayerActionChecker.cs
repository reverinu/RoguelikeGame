using UnityEngine;
using System.Collections;
/*

    Playerが行動をした瞬間を判定するスクリプト

*/
public class PlayerActionChecker : MonoBehaviour {
    
    PlayerScript playerScript;
    private Vector3 nowPlayerPos;
    private int playerLine = 0;// 移動後と移動前の直線距離
    private static readonly int MOVEDISTANCE = 1;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        nowPlayerPos = transform.position;
    }
	// Update is called once per frame
	void Update () {
        CheckActing();
	}

    private void CheckActing()
    {
        playerLine = (int)Mathf.Sqrt((transform.position.x - nowPlayerPos.x) * (transform.position.x - nowPlayerPos.x) + (transform.position.z - nowPlayerPos.z) * (transform.position.z - nowPlayerPos.z));
        if (playerLine >= MOVEDISTANCE || (Input.GetKeyDown(KeyCode.A) && !playerScript.GetComponent<PlayerScript>().isActRunning))
        {
            playerScript.GetComponent<PlayerScript>().isPlayerAction = true;
            nowPlayerPos = transform.position;
            //Debug.Log(playerScript.GetComponent<PlayerScript>().isPlayerAction);
        }
        else
        {
            playerScript.GetComponent<PlayerScript>().isPlayerAction = false;
        }
    }

}
