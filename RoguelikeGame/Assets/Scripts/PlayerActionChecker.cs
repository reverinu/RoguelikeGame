using UnityEngine;
using System.Collections;
/*

    Player移動をした瞬間を判定するスクリプト

*/
public class PlayerActionChecker : MonoBehaviour {
    
    GameObject playerScript;
    private Vector3 nowPlayerPos;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Setting");
        nowPlayerPos = transform.position;
    }
	// Update is called once per frame
	void Update () {
        CheckMoving();
	}

    private void CheckMoving()
    {
        if (nowPlayerPos != transform.position)
        {
            playerScript.GetComponent<PlayerScript>().isPlayerAction = true;
            nowPlayerPos = transform.position;
            Debug.Log(playerScript.GetComponent<PlayerScript>().isPlayerAction);
        }
        else
        {
            playerScript.GetComponent<PlayerScript>().isPlayerAction = false;
        }
    }
}
