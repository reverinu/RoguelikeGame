using UnityEngine;
using System.Collections;

public class DungeonCreater : MonoBehaviour {

    public struct TROUT
    {
        public static readonly int WALL = 0;
        public static readonly int ROOM = 1;
        public static readonly int PASSAGE = 2;
    }

    public int[,] place;// TROUTの属性を決める
    Vector3[,] placePos;// 位置を記録

    public int dungeonHeight;
    public int dungeonWidth;
    [Header("高さ分割数（割り切れる値で）")]
    public int dungeonHeightSplit;
    [Header("幅分割数（割り切れる値で）")]
    public int dungeonWidthSplit;

    int areaHeight;
    int areaWidth;
    int maxXAreaNum;
    int maxZAreaNum;
    int[,] area;

    int maxRoomHeight;
    int maxRoomWidth;
    int minRoomHeight;
    int minRoomWidth;
    int maxRoomNum;
    int minRoomNum;
    int roomNum;

    int x;// 横
    int z;// 縦

    [SerializeField]
    GameObject wall;
    [SerializeField]
    GameObject room;
    [SerializeField]
    GameObject passage;

    void Awake()
    {
        SetVariety();
        CheckVariety();
        SetPlacePos();
    }
	// Use this for initialization
	void Start () {
        SetTroutPos();
	}
    private void SetVariety()
    {
        place = new int[dungeonHeight, dungeonWidth];
        placePos = new Vector3[dungeonHeight, dungeonWidth];
        areaHeight = dungeonHeight / dungeonHeightSplit;
        areaWidth = dungeonWidth / dungeonWidthSplit;
        maxXAreaNum = dungeonHeightSplit;
        maxZAreaNum = dungeonWidthSplit;
        area = new int[maxXAreaNum, maxZAreaNum];
        maxRoomHeight = areaHeight - 4;
        maxRoomWidth = areaWidth - 4;
        minRoomHeight = 5;
        minRoomWidth = 5;
        maxRoomNum = maxXAreaNum * maxZAreaNum / 2;// エリア数の半分
        minRoomNum = maxXAreaNum * maxZAreaNum / 4;// エリア数の４分の１
        roomNum = Random.Range(minRoomNum, maxRoomNum);
    }

    private void CheckVariety()
    {
        if (dungeonHeight % dungeonHeightSplit != 0)
        {
            Debug.Log("dungeonHeight % dungeonHeightSplit != 0");
        }
        if (dungeonWidth % dungeonWidthSplit != 0)
        {
            Debug.Log("dungeonWidth % dungeonWidthSplit != 0");
        }
        if(maxRoomHeight <= minRoomHeight)
        {
            Debug.Log("maxRoomHeight <= minRoomHeight");
        }
        if(maxRoomWidth <= minRoomWidth)
        {
            Debug.Log("maxRoomWidth <= minRoomWidth");
        }
    }

    private void SetPlacePos()
    {
        for (z = 0; z < dungeonHeight; z++)
        {
            for (x = 0; x < dungeonWidth; x++)
            {
                placePos[x, z] = new Vector3(x, 0, z);
            }
        }
    }

    public void SetTroutPos()
    {
        // まず全部WALLにする
        SetAllWall();
        // どのエリアに生成するか決める
        DesideRoomCreatingArea();
        // そこのランダムな位置に創るRoomの情報を設置する
        SetRoom();
        // 通路でつなげる
        SetPassage();
    }
    public void SetAllWall()
    {
        for (z = 0; z < dungeonHeight; z++)
        {
            for (x = 0; x < dungeonWidth; x++)
            {
                place[x, z] = TROUT.WALL;
            }
        }
    }

    public void DesideRoomCreatingArea()
    {
        for (int i = 0; i < roomNum; i++)
        {
            int giveUpCount = 0;
            while (true)
            {
                int xAreaNum = Random.Range(0,maxXAreaNum);
                int zAreaNum = Random.Range(0,maxZAreaNum);
                if()
                {

                }
                giveUpCount++;
                break;
            }
        }
    }
    public void SetRoom()
    {

    }
    public void SetPassage()
    {

    }
}
