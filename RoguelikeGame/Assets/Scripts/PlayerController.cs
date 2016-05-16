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

    private GameObject model;
    private bool isRunning = false;

    PlayerForwardChecker playerForwardCheker;

    void Start()
    {
        playerForwardCheker = GameObject.Find("Game Manager").GetComponent<PlayerForwardChecker>();
        model = GameObject.FindGameObjectWithTag("ModelTag");
    }
	
	// Update is called once per frame
	void Update () {
        MovePlayer();
        RotatePlayer();
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
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                SetPlayerRotation(KeyCode.Z);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                SetPlayerRotation(KeyCode.C);
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

    

    private IEnumerator iContinueMoveing()
    {
        if (isRunning)
            yield break;
        isRunning = true;

        // 斜め移動、上下左右移動
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            if(!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasWall())
            {
                transform.position += MOVE.UP;
                transform.position += MOVE.RIGHT;
            }
            SetPlayerRotation(KeyCode.E);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasWall())
            {
                transform.position += MOVE.UP;
                transform.position += MOVE.LEFT;
            }
            SetPlayerRotation(KeyCode.Q);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasWall())
            {
                transform.position += MOVE.DOWN;
                transform.position += MOVE.LEFT;
            }
            SetPlayerRotation(KeyCode.Z);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasWall())
            {
                transform.position += MOVE.DOWN;
                transform.position += MOVE.RIGHT;
            }
            SetPlayerRotation(KeyCode.C);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasWall())
            {
                transform.position += MOVE.UP;
            }
            SetPlayerRotation(KeyCode.UpArrow);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasWall())
            {
                transform.position += MOVE.DOWN;
            }
            SetPlayerRotation(KeyCode.DownArrow);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasWall())
            {
                transform.position += MOVE.LEFT;
            }
            SetPlayerRotation(KeyCode.LeftArrow);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            if (!playerForwardCheker.GetComponent<PlayerForwardChecker>().hasWall())
            {
                transform.position += MOVE.RIGHT;
            }
            SetPlayerRotation(KeyCode.RightArrow);
        }
        yield return new WaitForSeconds(0.2f);

        isRunning = false;
    }

    private void SetPlayerRotation(KeyCode key)
    {
        if (key == KeyCode.UpArrow)
        {
            model.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (key == KeyCode.DownArrow)
        {
            model.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (key == KeyCode.LeftArrow)
        {
            model.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (key == KeyCode.RightArrow)
        {
            model.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if(key == KeyCode.Q)
        {
            model.transform.rotation = Quaternion.Euler(0, 315, 0);
        }
        else if (key == KeyCode.E)
        {
            model.transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else if (key == KeyCode.Z)
        {
            model.transform.rotation = Quaternion.Euler(0, 225, 0);
        }
        else if (key == KeyCode.C)
        {
            model.transform.rotation = Quaternion.Euler(0, 135, 0);
        }
    }

    

}
