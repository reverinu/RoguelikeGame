using UnityEngine;
using System.Collections;

public class EnemyAlgorithm : MonoBehaviour {
    private struct MOVE
    {
        public static readonly Vector3 UP = new Vector3(1, 0, 0);
        public static readonly Vector3 DOWN = new Vector3(-1, 0, 0);
        public static readonly Vector3 LEFT = new Vector3(0, 0, 1);
        public static readonly Vector3 RIGHT = new Vector3(0, 0, -1);
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


    GameObject player;
    [SerializeField]
    PlayerScript playerScript;
    ActionPatternInfo actionPatternInfo;
    EnemyForwardChecker enemyForwardChecker;

    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = GameObject.Find("Setting").GetComponent<PlayerScript>();
        actionPatternInfo = this.GetComponent<EnemyInfo>().actionPattern.GetComponent<ActionPatternInfo>();
        enemyForwardChecker = this.GetComponent<EnemyForwardChecker>();
    }
	// Update is called once per frame
	void Update () {
        EnemyAction();
	}

    private void EnemyAction(){
        if(playerScript.GetComponent<PlayerScript>().isPlayerAction)
        {
            if(MeasurePlayerDistance() <= actionPatternInfo.GetComponent<ActionPatternInfo>().distanceNotice)
            {
                Debug.Log("近い！！");
            }
            else
            {
                RandomAction();
            }
        }
    }

    private int MeasurePlayerDistance()
    {
        float row, column;
        row = player.transform.position.x - this.transform.position.x;
        column = player.transform.position.z - this.transform.position.z;
        int distance = (int)Mathf.Sqrt(row*row + column*column);
        return distance;
    }

    private void RandomAction()
    {
        int rand = Random.Range(0, 8);

        if (!(enemyForwardChecker.GetComponent<EnemyForwardChecker>().hasWall(rand) || enemyForwardChecker.GetComponent<EnemyForwardChecker>().hasOtherEnemy(rand)))
        {
            if (rand == DIRECTIONCHECK.UP)
            {
                transform.position += MOVE.UP;
            }
            else if (rand == DIRECTIONCHECK.DOWN)
            {
                transform.position += MOVE.DOWN;
            }
            else if (rand == DIRECTIONCHECK.LEFT)
            {
                transform.position += MOVE.LEFT;
            }
            else if (rand == DIRECTIONCHECK.RIGHT)
            {
                transform.position += MOVE.RIGHT;
            }
            else if (rand == DIRECTIONCHECK.UPLEFT)
            {
                transform.position += MOVE.UP;
                transform.position += MOVE.LEFT;
            }
            else if (rand == DIRECTIONCHECK.UPRIGHT)
            {
                transform.position += MOVE.UP;
                transform.position += MOVE.RIGHT;
            }
            else if (rand == DIRECTIONCHECK.DOWNLEFT)
            {
                transform.position += MOVE.DOWN;
                transform.position += MOVE.LEFT;
            }
            else if (rand == DIRECTIONCHECK.DOWNRIGHT)
            {
                transform.position += MOVE.DOWN;
                transform.position += MOVE.RIGHT;
            }
            Rename();
        }
    }

    private void AttackAction()
    {

    }

    private void HuntAction()
    {

    }

    // 名前で位置を取得するため移動するたびに変える
    private void Rename()
    {
        int row = (int)this.transform.position.x;
        int column = (int)this.transform.position.z;
        this.name = "Enemy" + row + "," + column;
    }
}
