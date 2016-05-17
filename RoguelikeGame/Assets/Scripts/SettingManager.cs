using UnityEngine;
using System.Collections;
/*

    ゲーム開始時にモノを配置する機能を保持するスクリプト

*/
public class SettingManager : MonoBehaviour {

    [SerializeField]
    PlaceScript placeScript;
    [SerializeField]
    PlayerScript playerScript;
    [SerializeField]
    EnemyScript enemyScript;

	// Use this for initialization
	void Start () {
	}

    public void SetAll() {
        SetTrout();
        SetPlayer();
        SetEnemy();
    }

	
	public void SetTrout()
    {
        placeScript.GetComponent<PlaceScript>().SetTroutPos();
        placeScript.GetComponent<PlaceScript>().SetTroutObj();
    }

    public void SetPlayer()
    {
        playerScript.GetComponent<PlayerScript>().SetPlayerPos();
        playerScript.GetComponent<PlayerScript>().SetPlayerObj();
    }

    public void SetEnemy()
    {
        enemyScript.GetComponent<EnemyScript>().SetEnemyPos();
        enemyScript.GetComponent<EnemyScript>().SetEnemyObj();
    }
}
