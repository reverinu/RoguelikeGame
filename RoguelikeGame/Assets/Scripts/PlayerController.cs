using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

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

    private GameObject model;
    //private bool isMoveRunning = false;
    //private bool isActionRunning = false;
    private int playerDirection = 0;

    PlayerForwardChecker playerForwardCheker;
    PlayerScript playerScript;

    void Start()
    {
        playerForwardCheker = GameObject.Find("Game Manager").GetComponent<PlayerForwardChecker>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        model = GameObject.FindGameObjectWithTag("ModelTag");
    }
	
	// Update is called once per frame
	void Update () {
        
        MovePlayer();
        RotatePlayer();
        ActPlayer();
	}

    private void MovePlayer()
    {
        if (!(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            StartCoroutine("iContinueMoveing");
        }
    }

    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                SetPlayerRotation(KeyCode.E);
            }
            else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                SetPlayerRotation(KeyCode.Q);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                SetPlayerRotation(KeyCode.C);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                SetPlayerRotation(KeyCode.Z);
            }
            else if (Input.GetKey(KeyCode.UpArrow) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
            {
                SetPlayerRotation(KeyCode.UpArrow);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
            {
                SetPlayerRotation(KeyCode.DownArrow);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
            {
                SetPlayerRotation(KeyCode.LeftArrow);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
            {
                SetPlayerRotation(KeyCode.RightArrow);
            }
        }
    }

    private void ActPlayer()
    {
        if (!(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                
                // エネミーだったら攻撃、アイテムだったら取得処理を実行
                GameObject enemy = playerForwardCheker.GetComponent<PlayerForwardChecker>().getEnemyObj(playerDirection);
                GameObject item = null;
                if (enemy != null && !playerScript.GetComponent<PlayerScript>().isActRunning)
                {
                    StartCoroutine("iAction");
                    // ここにダメージ計算処理を入れる
                    enemy.GetComponent<EnemyInfo>().hp--;
                }
                else if(item != null)
                {

                }
                else
                {
                    StartCoroutine("iAction");
                }
            }
        }
    }

    private IEnumerator iAction()
    {
        yield return new WaitForSeconds(0.001f);
        if (playerScript.GetComponent<PlayerScript>().isActRunning)
            yield break;
        playerScript.GetComponent<PlayerScript>().isActRunning = true;
        float pingpong = 0;
        float playerPosRow = transform.position.x;
        float playerPosColumn = transform.position.z;

        if(playerDirection == DIRECTIONCHECK.UP)
        {
            for (int i = 0; i <= 10; i++)
            {
                pingpong = (float)i * 0.1f;
                float nowPlayerPosRow = playerPosRow + Mathf.PingPong(pingpong, 0.5f) * 1f;
                float nowPlayerPosColumn = playerPosColumn;
                model.transform.position = new Vector3(nowPlayerPosRow, model.transform.position.y, nowPlayerPosColumn);
                yield return new WaitForSeconds(0.001f);
            }
        }
        else if (playerDirection == DIRECTIONCHECK.DOWN)
        {
            for (int i = 0; i <= 10; i++)
            {
                pingpong = (float)i * 0.1f;
                float nowPlayerPosRow = playerPosRow + Mathf.PingPong(pingpong, 0.5f) * (-1f);
                float nowPlayerPosColumn = playerPosColumn;
                model.transform.position = new Vector3(nowPlayerPosRow, model.transform.position.y, nowPlayerPosColumn);
                yield return new WaitForSeconds(0.001f);
            }
        }
        else if (playerDirection == DIRECTIONCHECK.LEFT)
        {
            for (int i = 0; i <= 10; i++)
            {
                pingpong = (float)i * 0.1f;
                float nowPlayerPosRow = playerPosRow;
                float nowPlayerPosColumn = playerPosColumn + Mathf.PingPong(pingpong, 0.5f) * 1f;
                model.transform.position = new Vector3(nowPlayerPosRow, model.transform.position.y, nowPlayerPosColumn);
                yield return new WaitForSeconds(0.001f);
            }
        }
        else if (playerDirection == DIRECTIONCHECK.RIGHT)
        {
            for (int i = 0; i <= 10; i++)
            {
                pingpong = (float)i * 0.1f;
                float nowPlayerPosRow = playerPosRow;
                float nowPlayerPosColumn = playerPosColumn + Mathf.PingPong(pingpong, 0.5f) * (-1f);
                model.transform.position = new Vector3(nowPlayerPosRow, model.transform.position.y, nowPlayerPosColumn);
                yield return new WaitForSeconds(0.001f);
            }
        }
        else if (playerDirection == DIRECTIONCHECK.UPLEFT)
        {
            for (int i = 0; i <= 10; i++)
            {
                pingpong = (float)i * 0.1f;
                float nowPlayerPosRow = playerPosRow + Mathf.PingPong(pingpong, 0.5f) * 1f;
                float nowPlayerPosColumn = playerPosColumn + Mathf.PingPong(pingpong, 0.5f) * 1f;
                model.transform.position = new Vector3(nowPlayerPosRow, model.transform.position.y, nowPlayerPosColumn);
                yield return new WaitForSeconds(0.001f);
            }
        }
        else if (playerDirection == DIRECTIONCHECK.UPRIGHT)
        {
            for (int i = 0; i <= 10; i++)
            {
                pingpong = (float)i * 0.1f;
                float nowPlayerPosRow = playerPosRow + Mathf.PingPong(pingpong, 0.5f) * 1f;
                float nowPlayerPosColumn = playerPosColumn + Mathf.PingPong(pingpong, 0.5f) * (-1f);
                model.transform.position = new Vector3(nowPlayerPosRow, model.transform.position.y, nowPlayerPosColumn);
                yield return new WaitForSeconds(0.001f);
            }
        }
        else if (playerDirection == DIRECTIONCHECK.DOWNLEFT)
        {
            for (int i = 0; i <= 10; i++)
            {
                pingpong = (float)i * 0.1f;
                float nowPlayerPosRow = playerPosRow + Mathf.PingPong(pingpong, 0.5f) * (-1f);
                float nowPlayerPosColumn = playerPosColumn + Mathf.PingPong(pingpong, 0.5f) * 1f;
                model.transform.position = new Vector3(nowPlayerPosRow, model.transform.position.y, nowPlayerPosColumn);
                yield return new WaitForSeconds(0.001f);
            }
        }
        else if (playerDirection == DIRECTIONCHECK.DOWNRIGHT)
        {
            for (int i = 0; i <= 10; i++)
            {
                pingpong = (float)i * 0.1f;
                float nowPlayerPosRow = playerPosRow + Mathf.PingPong(pingpong, 0.5f) * (-1f);
                float nowPlayerPosColumn = playerPosColumn + Mathf.PingPong(pingpong, 0.5f) * (-1f);
                model.transform.position = new Vector3(nowPlayerPosRow, model.transform.position.y, nowPlayerPosColumn);
                yield return new WaitForSeconds(0.001f);
            }
        }
        playerScript.GetComponent<PlayerScript>().isActRunning = false;
    }
    
    private IEnumerator iContinueMoveing()
    {
        if (playerScript.GetComponent<PlayerScript>().isMoveRunning)
            yield break;
        playerScript.GetComponent<PlayerScript>().isMoveRunning = true;

        // 斜め移動、上下左右移動
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasAll())
            {
                /* 滑らかな動きをやってみたくて実装予定（斜め移動だとなんか後半カクッってなるため調整中）
                float playerPosRow = transform.position.x;
                float playerPosColumn = transform.position.z;

                for (int theta = 0; theta <= 10; theta++) 
                {
                    float nowPlayerPosRow = playerPosRow + Mathf.Lerp(0, 1f, theta * 0.1f);
                    float nowPlayerPosColumn = playerPosColumn + Mathf.Lerp(0, -1f, theta * 0.1f);
                    transform.position = new Vector3(nowPlayerPosRow, transform.position.y, nowPlayerPosColumn);
                    yield return new WaitForSeconds(0.0000001f);
                }*/

                if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasAll())
                {
                    transform.position += MOVE.UP;
                    transform.position += MOVE.RIGHT;
                }
            }
            SetPlayerRotation(KeyCode.E);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasAll())
            {
                transform.position += MOVE.UP;
                transform.position += MOVE.LEFT;
            }
            SetPlayerRotation(KeyCode.Q);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasAll())
            {
                transform.position += MOVE.DOWN;
                transform.position += MOVE.LEFT;
            }
            SetPlayerRotation(KeyCode.Z);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasAll())
            {
                transform.position += MOVE.DOWN;
                transform.position += MOVE.RIGHT;
            }
            SetPlayerRotation(KeyCode.C);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasAll())
            {
                transform.position += MOVE.UP;
            }
            SetPlayerRotation(KeyCode.UpArrow);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasAll())
            {
                transform.position += MOVE.DOWN;
            }
            SetPlayerRotation(KeyCode.DownArrow);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasAll())
            {
                transform.position += MOVE.LEFT;
            }
            SetPlayerRotation(KeyCode.LeftArrow);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasAll())
            {
                transform.position += MOVE.RIGHT;
            }
            SetPlayerRotation(KeyCode.RightArrow);
        }
        Rename();
        yield return new WaitForSeconds(0.2f);

        playerScript.GetComponent<PlayerScript>().isMoveRunning = false;
    }
    
    

    private void SetPlayerRotation(KeyCode key)
    {
        if (key == KeyCode.Q)
        {
            model.transform.rotation = Quaternion.Euler(0, 315f, 0);
            playerDirection = DIRECTIONCHECK.UPLEFT;
        }
        else if (key == KeyCode.E)
        {
            model.transform.rotation = Quaternion.Euler(0, 45f, 0);
            playerDirection = DIRECTIONCHECK.UPRIGHT;
        }
        else if (key == KeyCode.Z)
        {
            model.transform.rotation = Quaternion.Euler(0, 225f, 0);
            playerDirection = DIRECTIONCHECK.DOWNLEFT;
        }
        else if (key == KeyCode.C)
        {
            model.transform.rotation = Quaternion.Euler(0, 135f, 0);
            playerDirection = DIRECTIONCHECK.DOWNRIGHT;
        }
        else if (key == KeyCode.UpArrow)
        {
            model.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerDirection = DIRECTIONCHECK.UP;

        }
        else if (key == KeyCode.DownArrow)
        {
            model.transform.rotation = Quaternion.Euler(0, 180f, 0);
            playerDirection = DIRECTIONCHECK.DOWN;
        }
        else if (key == KeyCode.LeftArrow)
        {
            model.transform.rotation = Quaternion.Euler(0, 270f, 0);
            playerDirection = DIRECTIONCHECK.LEFT;
        }
        else if (key == KeyCode.RightArrow)
        {
            model.transform.rotation = Quaternion.Euler(0, 90f, 0);
            playerDirection = DIRECTIONCHECK.RIGHT;
        }
        
    }

    // 移動するたびに名前を変える
    private void Rename()
    {
        int row = (int)this.transform.position.x;
        int column = (int)this.transform.position.z;
        this.name = "Player" + row +"," + column;
    }
    

}
