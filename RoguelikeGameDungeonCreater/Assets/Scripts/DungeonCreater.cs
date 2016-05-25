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
    bool[,] perfectArea;// 通路付きの部屋で末端の部屋ではないかどうか
    int[,] areaCenterPosX;// 通路の中継地点
    int[,] areaCenterPosZ;

    int[,] group;// すべての部屋が繋がらない場合があって、その場合はグループ分けをする

    int maxRoomHeight;
    int maxRoomWidth;
    int minRoomHeight;
    int minRoomWidth;
    int edge;
    int maxRoomNum;
    int minRoomNum;
    int roomNum;
    bool[,] isRoomUp;// 上に通路を伸ばすことが出来る
    bool[,] isRoomDown;
    bool[,] isRoomRight;
    bool[,] isRoomLeft;
    int[,] roomCenterPosX;// 部屋の中央地点から通路が伸びる
    int[,] roomCenterPosZ;

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
        SetAreaCenterPos();
    }
	// Use this for initialization
	void Start () {
        SetTroutPos();
        SetTroutObj();
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
        perfectArea = new bool[maxXAreaNum, maxZAreaNum];
        areaCenterPosX = new int[maxXAreaNum, maxZAreaNum];
        areaCenterPosZ = new int[maxXAreaNum, maxZAreaNum];
        group = new int[maxXAreaNum, maxZAreaNum];
        maxRoomHeight = areaHeight - 4;
        maxRoomWidth = areaWidth - 4;
        minRoomHeight = 5;
        minRoomWidth = 5;
        edge = 2;// maxRoomHeightWidthの-4の半分の値でなければならない
        maxRoomNum = maxXAreaNum * maxZAreaNum * 3 / 4;// エリア数の４分の３
        minRoomNum = maxXAreaNum * maxZAreaNum / 2;// エリア数の半分
        roomNum = Random.Range(minRoomNum, maxRoomNum);
        isRoomUp = new bool[maxXAreaNum, maxZAreaNum];
        isRoomDown = new bool[maxXAreaNum, maxZAreaNum];
        isRoomRight = new bool[maxXAreaNum, maxZAreaNum];
        isRoomLeft = new bool[maxXAreaNum, maxZAreaNum];
        roomCenterPosX = new int[maxXAreaNum, maxZAreaNum];
        roomCenterPosZ = new int[maxXAreaNum, maxZAreaNum];
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

    private void SetAreaCenterPos()
    {
        for (int zAreaNum = 0; zAreaNum < maxZAreaNum; zAreaNum++)
        {
            for (int xAreaNum = 0; xAreaNum < maxXAreaNum; xAreaNum++)
            {
                areaCenterPosX[xAreaNum, zAreaNum] = areaWidth * xAreaNum + areaWidth / 2;
                areaCenterPosZ[xAreaNum, zAreaNum] = areaHeight * zAreaNum + areaHeight / 2;
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
                if(area[xAreaNum, zAreaNum] != TROUT.ROOM)
                {
                    Debug.Log("area[" + xAreaNum + ", " + zAreaNum + "]");
                    area[xAreaNum, zAreaNum] = TROUT.ROOM;
                    SetRoomType(xAreaNum, zAreaNum);
                    break;
                }
                giveUpCount++;
                if(giveUpCount >= 100)
                {
                    Debug.Log("give up!");
                    break;
                }
            }
        }
    }

    public void SetRoomType(int xAreaNum,int zAreaNum)
    {
        if(xAreaNum == 0 && zAreaNum == 0)
        {
            isRoomUp[xAreaNum, zAreaNum] = true;
            isRoomRight[xAreaNum, zAreaNum] = true;
        }
        else if (xAreaNum == maxXAreaNum - 1 && zAreaNum == 0)
        {
            isRoomUp[xAreaNum, zAreaNum] = true;
            isRoomLeft[xAreaNum, zAreaNum] = true;
        }
        else if (xAreaNum == 0 && zAreaNum == maxZAreaNum - 1)
        {
            isRoomDown[xAreaNum, zAreaNum] = true;
            isRoomRight[xAreaNum, zAreaNum] = true;
        }
        else if (xAreaNum == maxXAreaNum - 1 && zAreaNum == maxZAreaNum - 1)
        {
            isRoomDown[xAreaNum, zAreaNum] = true;
            isRoomLeft[xAreaNum, zAreaNum] = true;
        }
        else if (!(xAreaNum == 0 || xAreaNum == maxXAreaNum - 1) && zAreaNum == 0)
        {
            isRoomUp[xAreaNum, zAreaNum] = true;
            isRoomRight[xAreaNum, zAreaNum] = true;
            isRoomLeft[xAreaNum, zAreaNum] = true;
        }
        else if (!(xAreaNum == 0 || xAreaNum == maxXAreaNum - 1) && zAreaNum == maxZAreaNum - 1)
        {
            isRoomDown[xAreaNum, zAreaNum] = true;
            isRoomRight[xAreaNum, zAreaNum] = true;
            isRoomLeft[xAreaNum, zAreaNum] = true;
        }
        else if (xAreaNum == 0 && !(zAreaNum == 0 || zAreaNum == maxZAreaNum - 1))
        {
            isRoomUp[xAreaNum, zAreaNum] = true;
            isRoomDown[xAreaNum, zAreaNum] = true;
            isRoomRight[xAreaNum, zAreaNum] = true;
        }
        else if (xAreaNum == maxXAreaNum - 1 && !(zAreaNum == 0 || zAreaNum == maxZAreaNum - 1))
        {
            isRoomUp[xAreaNum, zAreaNum] = true;
            isRoomDown[xAreaNum, zAreaNum] = true;
            isRoomLeft[xAreaNum, zAreaNum] = true;
        }
        else
        {
            isRoomUp[xAreaNum, zAreaNum] = true;
            isRoomDown[xAreaNum, zAreaNum] = true;
            isRoomRight[xAreaNum, zAreaNum] = true;
            isRoomLeft[xAreaNum, zAreaNum] = true;
        }
    }

    public void SetRoom()
    {
        for (int zAreaNum = 0; zAreaNum < maxZAreaNum; zAreaNum++)
        {
            for (int xAreaNum = 0; xAreaNum < maxXAreaNum; xAreaNum++)
            {
                if(area[xAreaNum, zAreaNum] == TROUT.ROOM)
                {
                    int roomWidth = Random.Range(minRoomWidth, maxRoomWidth);
                    int roomHeight = Random.Range(minRoomHeight, maxRoomHeight);
                    int roomStartPosX = Random.Range((xAreaNum * areaWidth) + edge, (xAreaNum * areaWidth) + areaWidth - roomWidth - edge);
                    int roomStartPosZ = Random.Range((zAreaNum * areaHeight) + edge, (zAreaNum * areaHeight) + areaHeight - roomHeight - edge);
                    int roomEndPosX = roomStartPosX + roomWidth;
                    int roomEndPosZ = roomStartPosZ + roomHeight;
                    SetRoomCenterPos(xAreaNum, zAreaNum, roomStartPosX, roomStartPosZ, roomWidth, roomHeight);
                    for (z = roomStartPosZ; z < roomEndPosZ; z++)
                    {
                        for (x = roomStartPosX; x < roomEndPosX; x++)
                        {
                            place[x, z] = TROUT.ROOM;
                        }
                    }
                }
            }
        }
    }

    public void SetRoomCenterPos(int xAreaNum, int zAreaNum, int roomStartPosX, int roomStartPosZ, int roomWidth, int roomHeight)
    {
        roomCenterPosX[xAreaNum, zAreaNum] = roomStartPosX + roomWidth / 2;
        roomCenterPosZ[xAreaNum, zAreaNum] = roomStartPosZ + roomHeight / 2;
    }

    public void SetPassage()
    {
        int groupNum = 0;

        for (int zAreaNum = 0; zAreaNum < maxZAreaNum; zAreaNum++)
        {
            for (int xAreaNum = 0; xAreaNum < maxXAreaNum; xAreaNum++)
            {
                if(area[xAreaNum, zAreaNum] == TROUT.ROOM && !perfectArea[xAreaNum, zAreaNum])
                {
                    ConnectRoom(xAreaNum, zAreaNum, groupNum);
                    groupNum++;
                }
            }
        }

        int groupNumTemp = groupNum;
        Debug.Log("groupNum= " + groupNum);
        // ただしくgroup分けできていない

        /*
        for (groupNum = 0; groupNum < groupNumTemp - 1; groupNum++)
        {
            ConnectGroup(groupNum);
        }
         * */
        ConnectGroup(0);
        
        
    }

    public void ConnectGroup(int groupNum)
    {
        int xAreaNum1 = -1;// 最初に＋１するため。１回目は０でfor文回したい
        int zAreaNum1 = 0;
        int xAreaNum2 = 0;
        int zAreaNum2 = 0;
        int xAreaNum1Decision = 0;
        int zAreaNum1Decision = 0;
        int xAreaNum2Decision = 0;
        int zAreaNum2Decision = 0;
        float groupsLength = 100;// 大きめの数字を入れておく。０だと比べるときにこれが優先されてしまう
        float groupsLengthDecision = 100;
        float previousGroupsLength = 101;

        for (int i = 0; i < maxXAreaNum * maxZAreaNum; i++)
        {
            for (int zAreaNum = zAreaNum1; zAreaNum < maxZAreaNum; zAreaNum++)
            {
                for (int xAreaNum = xAreaNum1 + 1; xAreaNum < maxXAreaNum; xAreaNum++)
                {
                    if (area[xAreaNum, zAreaNum] == TROUT.ROOM && group[xAreaNum, zAreaNum] == groupNum)
                    {
                        if (isRoomUp[xAreaNum, zAreaNum] || isRoomDown[xAreaNum, zAreaNum] || isRoomRight[xAreaNum, zAreaNum] || isRoomLeft[xAreaNum, zAreaNum])
                        {
                            xAreaNum1 = xAreaNum;
                            zAreaNum1 = zAreaNum;
                            zAreaNum = maxZAreaNum;
                            break;
                        }
                    }
                }
            }
            Debug.Log("ルームはこちら！" + xAreaNum1 + "," + zAreaNum1);

            for (int zAreaNum = 0; zAreaNum < maxZAreaNum; zAreaNum++)
            {
                for (int xAreaNum = 0; xAreaNum < maxXAreaNum; xAreaNum++)
                {
                    if (area[xAreaNum, zAreaNum] == TROUT.ROOM && group[xAreaNum, zAreaNum] == groupNum + 1)
                    {
                        if (isRoomUp[xAreaNum, zAreaNum] || isRoomDown[xAreaNum, zAreaNum] || isRoomRight[xAreaNum, zAreaNum] || isRoomLeft[xAreaNum, zAreaNum])
                        {
                            xAreaNum2 = xAreaNum;
                            zAreaNum2 = zAreaNum;

                            // groupNum番目のgroupと＋１番目のgroupの一番近いやつを検索
                            float temp = groupsLengthDecision;
                            groupsLength = MeasureGroupsLength(xAreaNum1, zAreaNum1, xAreaNum2, zAreaNum2);

                            if (groupsLength < temp)
                            {
                                groupsLengthDecision = groupsLength;
                                xAreaNum2Decision = xAreaNum2;
                                zAreaNum2Decision = zAreaNum2;
                            }
                        }
                    }
                }
            }
            Debug.Log("ルームに一番近いのはこちら！！！" + xAreaNum2Decision + "," + zAreaNum2Decision);


            if (groupsLengthDecision < previousGroupsLength)
            {
                xAreaNum1Decision = xAreaNum1;
                zAreaNum1Decision = zAreaNum1;
                Debug.Log("ルームはこちらその２！！" + xAreaNum1Decision + "," + zAreaNum1Decision);
            }
            previousGroupsLength = groupsLengthDecision;

            // 次のループでxAreaNumが最大値を超えそうになった時の処理
            if(xAreaNum1 == maxXAreaNum - 1){
                xAreaNum1 = -1;
                zAreaNum1++;
            }
        }

        
        Debug.Log("真ルームはこちら！" + xAreaNum1Decision + "," + zAreaNum1Decision);
        Debug.Log("真ルームに一番近いのはこちら！！！" + xAreaNum2Decision + "," + zAreaNum2Decision);
        

        for (int zAreaNum = zAreaNum1Decision; zAreaNum != zAreaNum2Decision;)
        {
            if(zAreaNum < zAreaNum2Decision)
            {
                zAreaNum++;
            }
            else if(zAreaNum > zAreaNum2Decision)
            {
                zAreaNum--;
            }
            if (area[xAreaNum1Decision, zAreaNum] == TROUT.ROOM)
            {
                // 道中で
                Debug.Log("部屋発見してしまった！！！ｚ");
            }
            
        }

        for (int xAreaNum = xAreaNum1Decision; xAreaNum != xAreaNum2Decision; )
        {
            if (xAreaNum < xAreaNum2Decision)
            {
                xAreaNum++;
            }
            else if (xAreaNum > xAreaNum2Decision)
            {
                xAreaNum--;
            }
            if (area[xAreaNum, zAreaNum2Decision] == TROUT.ROOM)
            {
                // 道中で
                Debug.Log("部屋発見してしまった！！！ｘ");
            }
            
        }
    }

    public float MeasureGroupsLength(int xAreaNum1, int zAreaNum1, int xAreaNum2, int zAreaNum2)
    {
        return Mathf.Sqrt((xAreaNum1 - xAreaNum2) * (xAreaNum1 - xAreaNum2) + (zAreaNum1 - zAreaNum2) * (zAreaNum1 - zAreaNum2));
    }

    public void ConnectRoom(int xAreaNum, int zAreaNum, int groupNum)
    {
        group[xAreaNum, zAreaNum] = groupNum;
        
        Debug.Log("group:" + groupNum);
        Debug.Log("xAreaNum:" + xAreaNum);
        Debug.Log("zAreaNum:" + zAreaNum);
        Debug.Log("isRoomUp:" + isRoomUp[xAreaNum, zAreaNum]);
        Debug.Log("isRoomDown:" + isRoomDown[xAreaNum, zAreaNum]);
        Debug.Log("isRoomRight:" + isRoomRight[xAreaNum, zAreaNum]);
        Debug.Log("isRoomLeft:" + isRoomLeft[xAreaNum, zAreaNum]);

        if(isRoomUp[xAreaNum, zAreaNum])
        {
            if (area[xAreaNum, zAreaNum + 1] == TROUT.ROOM)//UP
            {
                ExtendPassageUp(xAreaNum, zAreaNum);
                isRoomUp[xAreaNum, zAreaNum] = false;
                isRoomDown[xAreaNum, zAreaNum + 1] = false;
                ConnectRoom(xAreaNum, zAreaNum + 1, groupNum);
            }
        }
        if(isRoomDown[xAreaNum, zAreaNum])
        {
            if (area[xAreaNum, zAreaNum - 1] == TROUT.ROOM)//DOWN
            {
                ExtendPassageDown(xAreaNum, zAreaNum);
                isRoomDown[xAreaNum, zAreaNum] = false;
                isRoomUp[xAreaNum, zAreaNum - 1] = false;
                ConnectRoom(xAreaNum, zAreaNum - 1, groupNum);
            }
        }
        if(isRoomRight[xAreaNum, zAreaNum])
        {
            if (area[xAreaNum + 1, zAreaNum] == TROUT.ROOM)//RIGHT
            {
                ExtendPassageRight(xAreaNum, zAreaNum);
                isRoomRight[xAreaNum, zAreaNum] = false;
                isRoomLeft[xAreaNum + 1, zAreaNum] = false;
                ConnectRoom(xAreaNum + 1, zAreaNum, groupNum);
            }
        }
        if(isRoomLeft[xAreaNum, zAreaNum])
        {
            if (area[xAreaNum - 1, zAreaNum] == TROUT.ROOM)//LEFT
            {
                ExtendPassageLeft(xAreaNum, zAreaNum);
                isRoomLeft[xAreaNum, zAreaNum] = false;
                isRoomRight[xAreaNum - 1, zAreaNum] = false;
                ConnectRoom(xAreaNum - 1, zAreaNum, groupNum);
            }
        }

        perfectArea[xAreaNum, zAreaNum] = true;

    }
    public void ExtendPassageUp(int xAreaNum, int zAreaNum)// 上に伸ばしていく
    {
        int passage1X = roomCenterPosX[xAreaNum, zAreaNum];
        int passage1Z = roomCenterPosZ[xAreaNum, zAreaNum];
        int passage2X = roomCenterPosX[xAreaNum, zAreaNum + 1];
        int passage2Z = roomCenterPosZ[xAreaNum, zAreaNum + 1];
        int centerPosX = passage2X;
        int centerPosZ = (passage2Z - passage1Z) / 2 + passage1Z;

        int posDifferenceX = passage1X - centerPosX;
        int posDifferenceZ = passage1Z - centerPosZ;
        if(posDifferenceZ >= 0)
        {
            for (z = passage1Z; z >= centerPosZ; z--)
            {
                place[passage1X, z] = TROUT.PASSAGE;
            }
            passage1Z = centerPosZ;
        }
        if (posDifferenceZ < 0)
        {
            for (z = passage1Z; z <= centerPosZ; z++)
            {
                place[passage1X, z] = TROUT.PASSAGE;
            }
            passage1Z = centerPosZ;
        }
        if(posDifferenceX >= 0)
        {
            for (x = passage1X; x >= centerPosX; x--)
            {
                place[x, passage1Z] = TROUT.PASSAGE;
            }
            passage1X = centerPosX;
        }
        if(posDifferenceX < 0)
        {
            for (x = passage1X; x <= centerPosX; x++)
            {
                place[x, passage1Z] = TROUT.PASSAGE;
            }
            passage1X = centerPosX;
        }

        posDifferenceX = passage2X - centerPosX;
        posDifferenceZ = passage2Z - centerPosZ;

        if (posDifferenceZ >= 0)
        {
            for (z = passage2Z; z >= centerPosZ; z--)
            {
                place[passage2X, z] = TROUT.PASSAGE;
            }
            passage2Z = centerPosZ;
        }
        if (posDifferenceZ < 0)
        {
            for (z = passage2Z; z <= centerPosZ; z++)
            {
                place[passage2X, z] = TROUT.PASSAGE;
            }
            passage2Z = centerPosZ;
        }
        if (posDifferenceX >= 0)
        {
            for (x = passage2X; x >= centerPosX; x--)
            {
                place[x, passage2Z] = TROUT.PASSAGE;
            }
            passage2X = centerPosX;
        }
        if (posDifferenceX < 0)
        {
            for (x = passage2X; x <= centerPosX; x++)
            {
                place[x, passage2Z] = TROUT.PASSAGE;
            }
            passage2X = centerPosX;
        }
        

    }
    public void ExtendPassageDown(int xAreaNum, int zAreaNum)
    {
        int passage1X = roomCenterPosX[xAreaNum, zAreaNum];
        int passage1Z = roomCenterPosZ[xAreaNum, zAreaNum];
        int passage2X = roomCenterPosX[xAreaNum, zAreaNum - 1];
        int passage2Z = roomCenterPosZ[xAreaNum, zAreaNum - 1];
        int centerPosX = passage2X;
        int centerPosZ = (passage1Z - passage2Z) / 2 + passage2Z;

        int posDifferenceX = passage1X - centerPosX;
        int posDifferenceZ = passage1Z - centerPosZ;
        if (posDifferenceZ >= 0)
        {
            for (z = passage1Z; z >= centerPosZ; z--)
            {
                place[passage1X, z] = TROUT.PASSAGE;
            }
            passage1Z = centerPosZ;
        }
        if (posDifferenceZ < 0)
        {
            for (z = passage1Z; z <= centerPosZ; z++)
            {
                place[passage1X, z] = TROUT.PASSAGE;
            }
            passage1Z = centerPosZ;
        }
        if (posDifferenceX >= 0)
        {
            for (x = passage1X; x >= centerPosX; x--)
            {
                place[x, passage1Z] = TROUT.PASSAGE;
            }
            passage1X = centerPosX;
        }
        if (posDifferenceX < 0)
        {
            for (x = passage1X; x <= centerPosX; x++)
            {
                place[x, passage1Z] = TROUT.PASSAGE;
            }
            passage1X = centerPosX;
        }

        posDifferenceX = passage2X - centerPosX;
        posDifferenceZ = passage2Z - centerPosZ;

        if (posDifferenceZ >= 0)
        {
            for (z = passage2Z; z >= centerPosZ; z--)
            {
                place[passage2X, z] = TROUT.PASSAGE;
            }
            passage2Z = centerPosZ;
        }
        if (posDifferenceZ < 0)
        {
            for (z = passage2Z; z <= centerPosZ; z++)
            {
                place[passage2X, z] = TROUT.PASSAGE;
            }
            passage2Z = centerPosZ;
        }
        if (posDifferenceX >= 0)
        {
            for (x = passage2X; x >= centerPosX; x--)
            {
                place[x, passage2Z] = TROUT.PASSAGE;
            }
            passage2X = centerPosX;
        }
        if (posDifferenceX < 0)
        {
            for (x = passage2X; x <= centerPosX; x++)
            {
                place[x, passage2Z] = TROUT.PASSAGE;
            }
            passage2X = centerPosX;
        }
    }
    public void ExtendPassageRight(int xAreaNum, int zAreaNum)
    {
        int passage1X = roomCenterPosX[xAreaNum, zAreaNum];
        int passage1Z = roomCenterPosZ[xAreaNum, zAreaNum];
        int passage2X = roomCenterPosX[xAreaNum + 1, zAreaNum];
        int passage2Z = roomCenterPosZ[xAreaNum + 1, zAreaNum];
        int centerPosX = (passage2X - passage1X) / 2 + passage1X;
        int centerPosZ = passage2Z;

        int posDifferenceX = passage1X - centerPosX;
        int posDifferenceZ = passage1Z - centerPosZ;
        if (posDifferenceX >= 0)
        {
            for (x = passage1X; x >= centerPosX; x--)
            {
                place[x, passage1Z] = TROUT.PASSAGE;
            }
            passage1X = centerPosX;
        }
        if (posDifferenceX < 0)
        {
            for (x = passage1X; x <= centerPosX; x++)
            {
                place[x, passage1Z] = TROUT.PASSAGE;
            }
            passage1X = centerPosX;
        }
        if (posDifferenceZ >= 0)
        {
            for (z = passage1Z; z >= centerPosZ; z--)
            {
                place[passage1X, z] = TROUT.PASSAGE;
            }
            passage1Z = centerPosZ;
        }
        if (posDifferenceZ < 0)
        {
            for (z = passage1Z; z <= centerPosZ; z++)
            {
                place[passage1X, z] = TROUT.PASSAGE;
            }
            passage1Z = centerPosZ;
        }


        posDifferenceX = passage2X - centerPosX;
        posDifferenceZ = passage2Z - centerPosZ;
        if (posDifferenceX >= 0)
        {
            for (x = passage2X; x >= centerPosX; x--)
            {
                place[x, passage2Z] = TROUT.PASSAGE;
            }
            passage2X = centerPosX;
        }
        if (posDifferenceX < 0)
        {
            for (x = passage2X; x <= centerPosX; x++)
            {
                place[x, passage2Z] = TROUT.PASSAGE;
            }
            passage2X = centerPosX;
        }
        if (posDifferenceZ >= 0)
        {
            for (z = passage2Z; z >= centerPosZ; z--)
            {
                place[passage2X, z] = TROUT.PASSAGE;
            }
            passage2Z = centerPosZ;
        }
        if (posDifferenceZ < 0)
        {
            for (z = passage2Z; z <= centerPosZ; z++)
            {
                place[passage2X, z] = TROUT.PASSAGE;
            }
            passage2Z = centerPosZ;
        }
    }
    public void ExtendPassageLeft(int xAreaNum, int zAreaNum)
    {
        int passage1X = roomCenterPosX[xAreaNum, zAreaNum];
        int passage1Z = roomCenterPosZ[xAreaNum, zAreaNum];
        int passage2X = roomCenterPosX[xAreaNum - 1, zAreaNum];
        int passage2Z = roomCenterPosZ[xAreaNum - 1, zAreaNum];
        int centerPosX = (passage1X - passage2X) / 2 + passage2X;
        int centerPosZ = passage2Z;

        int posDifferenceX = passage1X - centerPosX;
        int posDifferenceZ = passage1Z - centerPosZ;
        if (posDifferenceX >= 0)
        {
            for (x = passage1X; x >= centerPosX; x--)
            {
                place[x, passage1Z] = TROUT.PASSAGE;
            }
            passage1X = centerPosX;
        }
        if (posDifferenceX < 0)
        {
            for (x = passage1X; x <= centerPosX; x++)
            {
                place[x, passage1Z] = TROUT.PASSAGE;
            }
            passage1X = centerPosX;
        }
        if (posDifferenceZ >= 0)
        {
            for (z = passage1Z; z >= centerPosZ; z--)
            {
                place[passage1X, z] = TROUT.PASSAGE;
            }
            passage1Z = centerPosZ;
        }
        if (posDifferenceZ < 0)
        {
            for (z = passage1Z; z <= centerPosZ; z++)
            {
                place[passage1X, z] = TROUT.PASSAGE;
            }
            passage1Z = centerPosZ;
        }
        

        posDifferenceX = passage2X - centerPosX;
        posDifferenceZ = passage2Z - centerPosZ;
        if (posDifferenceX >= 0)
        {
            for (x = passage2X; x >= centerPosX; x--)
            {
                place[x, passage2Z] = TROUT.PASSAGE;
            }
            passage2X = centerPosX;
        }
        if (posDifferenceX < 0)
        {
            for (x = passage2X; x <= centerPosX; x++)
            {
                place[x, passage2Z] = TROUT.PASSAGE;
            }
            passage2X = centerPosX;
        }
        if (posDifferenceZ >= 0)
        {
            for (z = passage2Z; z >= centerPosZ; z--)
            {
                place[passage2X, z] = TROUT.PASSAGE;
            }
            passage2Z = centerPosZ;
        }
        if (posDifferenceZ < 0)
        {
            for (z = passage2Z; z <= centerPosZ; z++)
            {
                place[passage2X, z] = TROUT.PASSAGE;
            }
            passage2Z = centerPosZ;
        }
        
    }

    public void SetTroutObj()
    {
        for (z = 0; z < dungeonHeight; z++)
        {
            for (x = 0; x < dungeonWidth; x++)
            {
                if(place[x, z] == TROUT.WALL)
                {
                    GameObject obj = (GameObject)Instantiate(wall, placePos[x, z], Quaternion.identity);
                    obj.transform.parent = transform;
                }
                else if(place[x, z] == TROUT.ROOM)
                {
                    GameObject obj = (GameObject)Instantiate(room, placePos[x, z], Quaternion.identity);
                    obj.transform.parent = transform;
                }
                else if (place[x, z] == TROUT.PASSAGE)
                {
                    GameObject obj = (GameObject)Instantiate(passage, placePos[x, z], Quaternion.identity);
                    obj.transform.parent = transform;
                }
            }
        }
    }
}
