using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*

Placeの情報を保持するスクリプト

*/
public class PlaceScript: MonoBehaviour {

    public struct MAX
    {
        public static readonly int ROW = 100;
        public static readonly int COLUMN = 100;
        public static readonly int ROOMROW = 20;
        public static readonly int ROOMCOLUMN = 20;
        public static readonly int PASSAGENUM = (ROW * COLUMN) / ((ROOMROW + 1) * (ROOMCOLUMN + 1)) * 4;

    }
    public struct MIN
    {
        public static readonly int ROOMROW = 5;
        public static readonly int ROOMCOLUMN = 5;
    }
    public struct TROUT
    {
        public static readonly int WALL = 0;
        public static readonly int ROOM = 1;
        public static readonly int ROOMAREA = 2;
        public static readonly int PASSAGE = 3;
        public static readonly int PASSAGEAREA = 4;
    }
    
    public int[,] place = new int[MAX.ROW, MAX.COLUMN];
    public Vector3[,] placePos = new Vector3[MAX.ROW, MAX.COLUMN];
    public int ROOMNUM = (MAX.ROW * MAX.COLUMN) / ((MAX.ROOMROW + 1) * (MAX.ROOMCOLUMN + 1));
    int row;
    int column;
    int roomRowStart, roomColumnStart;// Room自動生成の時に使う。Room左上の座標
    int roomRowEnd, roomColumnEnd;// Room右下の座標
    int[] passageRow = new int[MAX.PASSAGENUM];// Passage開始地点候補のRow
    int[] passageColumn = new int[MAX.PASSAGENUM];// Passage開始地点候補のColumn
    int passageNum = 0;

    public int roomTroutCount = 0;

    [SerializeField]
    public List<GameObject> troutList;

    void Awake () {
        SetPos();
	}
    
    // 各マスの座標セット
    private void SetPos()
    {
        for (row = 0; row < MAX.ROW; row++)
        {
            for (column = 0; column < MAX.COLUMN; column++)
            {
                placePos[row, column] = new Vector3(row, 0, column);
            }
        }
    }
    
    public void SetTroutPos()
    {
        for (row = 0; row < MAX.ROW; row++)
        {
            for (column = 0; column < MAX.COLUMN; column++)
            {
                place[row, column] = TROUT.WALL;
            }
        }
        DesideRoom();
        DesidePassage();
        Debug.Log("a");
    }

    public void SetTroutObj()
    {
        for (row = 0; row < MAX.ROW; row++)
        {
            for (column = 0; column < MAX.COLUMN; column++)
            {
                if (place[row, column] == TROUT.WALL || place[row, column] == TROUT.ROOMAREA)
                {
                    // 管理しやすくするために全てに名前を付ける
                    GameObject troutObj = (GameObject)Instantiate(troutList[TROUT.WALL], placePos[row, column], Quaternion.identity);
                    troutObj.name = "Trout" + row + "," + column;
                    troutObj.transform.parent = this.transform;
                }
                else if (place[row, column] == TROUT.ROOM)
                {
                    roomTroutCount++;// EnemyやItem設置などに使う
                    GameObject troutObj = (GameObject)Instantiate(troutList[TROUT.ROOM], placePos[row, column], Quaternion.identity);
                    troutObj.name = "Trout" + row + "," + column;
                    troutObj.transform.parent = this.transform;
                }
                else if (place[row, column] == TROUT.PASSAGE)
                {
                    GameObject troutObj = (GameObject)Instantiate(troutList[TROUT.PASSAGE], placePos[row, column], Quaternion.identity);
                    troutObj.name = "Trout" + row + "," + column;
                    troutObj.transform.parent = this.transform;
                }
                else if (place[row, column] == TROUT.PASSAGEAREA)
                {
                    GameObject troutObj = (GameObject)Instantiate(troutList[TROUT.PASSAGEAREA], placePos[row, column], Quaternion.identity);
                    troutObj.name = "Trout" + row + "," + column;
                    troutObj.transform.parent = this.transform;
                }


            }
        }
    }

    private void DesideRoom()
    {
        int roomRowSize = 0;
        int roomColumnSize = 0;
        int edge = 4;// マップよりも内側にRoomを生成するためのやーつ
        int distance = 3;// Room間の距離
        

        for (int i = 0; i < ROOMNUM; i++)
        {
            while (true)
            {
                int overlapCheck = 0;
                switch (overlapCheck)
                {
                    case 0:
                        roomRowSize = Random.Range(MIN.ROOMROW, MAX.ROOMROW + 1);
                        roomColumnSize = Random.Range(MIN.ROOMCOLUMN, MAX.ROOMCOLUMN + 1);

                        roomRowStart = Random.Range(edge, MAX.ROW - roomRowSize - edge);
                        roomColumnStart = Random.Range(edge, MAX.COLUMN - roomColumnSize - edge);
                        roomRowEnd = roomRowStart + roomRowSize;
                        roomColumnEnd = roomColumnStart + roomColumnSize;
                        goto case 1;
                    case 1:
                        for (row = roomRowStart - distance; row < roomRowEnd + distance; row++)
                        {
                            for (column = roomColumnStart - distance; column < roomColumnEnd + distance; column++)
                            {
                                if (place[row, column] == TROUT.ROOM)
                                {
                                    goto case 0;
                                }
                            }
                        }
                        goto case 2;
                    case 2: break;
                }
                break;
            }

            // Roomの外枠はRoomAreaとして記録
            for (row = roomRowStart - distance + 2 ; row < roomRowEnd + distance - 2; row++)
            {
                for (column = roomColumnStart - distance + 2; column < roomColumnEnd + distance - 2; column++)
                {
                    place[row, column] = TROUT.ROOMAREA;
                }
            }

            for (row = roomRowStart; row < roomRowEnd; row++)
            {
                for (column = roomColumnStart; column < roomColumnEnd; column++)
                {
                    place[row, column] = TROUT.ROOM;
                }
            }

            // Roomの4辺のほぼ中央地点を通路候補として記録する
            for (row = roomRowStart; row < roomRowEnd; row++)
            {
                for (column = roomColumnStart; column < roomColumnEnd; column++)
                {
                    if (row == roomRowStart + ((roomRowEnd - roomRowStart) / 2) && column == roomColumnStart)
                    {
                        passageRow[passageNum] = row;
                        passageColumn[passageNum] = column;
                        passageNum++;
                        
                    }
                    else if (row == roomRowStart + ((roomRowEnd - roomRowStart) / 2) && column == (roomColumnEnd - 1))
                    {
                        passageRow[passageNum] = row;
                        passageColumn[passageNum] = column;
                        passageNum++;
                        
                    }
                    else if (column == roomColumnStart + ((roomColumnEnd - roomColumnStart) / 2) && row == roomRowStart)
                    {
                        passageRow[passageNum] = row;
                        passageColumn[passageNum] = column;
                        passageNum++;
                    }
                    else if (column == roomColumnStart + ((roomColumnEnd - roomColumnStart) / 2) && (row == roomRowEnd - 1))
                    {
                        passageRow[passageNum] = row;
                        passageColumn[passageNum] = column;
                        passageNum++;
                    }
                }
            }
        }
    }

    private void DesidePassage()
    {
        bool[] hasPassage = new bool[MAX.PASSAGENUM];
        int passageCount = 0;

        // 初期化
        for (int i = 0; i < MAX.PASSAGENUM; i++)
        {
            hasPassage[i] = false;
        }


        int passage1 = 0;
        int passage2 = 0;
        
        for(passageCount = 0; passageCount < (MAX.PASSAGENUM / 2); passageCount++)
        {
            // Passage候補二つをランダムに決定する
            for (int i = 0; i < 100; i++)
            {
                
                // 90週してもどうにもならない場合強制的に決める
                if (i >= 90)
                {
                    for (int k = 0; k < MAX.PASSAGENUM / 2; k++)
                    {
                        if (!hasPassage[k])
                        {
                            passage1 = k;
                            hasPassage[passage1] = true;
                            break;
                        }
                    }

                    for (int k = MAX.PASSAGENUM / 2; k < MAX.PASSAGENUM; k++)
                    {
                        if (!hasPassage[k])
                        {
                            passage2 = k;
                            hasPassage[passage2] = true;
                            break;
                        }
                    }

                    break;
                }
                else if(i < 90)
                {
                    passage1 = Random.Range(0, MAX.PASSAGENUM / 2);
                    passage2 = Random.Range(MAX.PASSAGENUM / 2, MAX.PASSAGENUM);
                    if (!(hasPassage[passage1] || hasPassage[passage2]) && passage1 != passage2)
                    {
                        hasPassage[passage1] = true;
                        hasPassage[passage2] = true;
                        break;
                    }
                }
            }

            // RoomAreaを脱出する程度にPassageを伸ばしておく
            switch (passage1 % 4) {
                case 0:
                    for (row = passageRow[passage1] - 1; row > passageRow[passage1] - 2; row--)
                    {
                        place[row, passageColumn[passage1]] = TROUT.PASSAGEAREA;
                    }
                    passageRow[passage1] = row;
                    break;
                case 1:
                    for (column = passageColumn[passage1] - 1; column > passageColumn[passage1] - 2; column--)
                    {
                        place[passageRow[passage1], column] = TROUT.PASSAGEAREA;
                    }
                    passageColumn[passage1] = column;
                    break;
                case 2:
                    for (column = passageColumn[passage1] + 1; column < passageColumn[passage1] + 2; column++)
                    {
                        place[passageRow[passage1], column] = TROUT.PASSAGEAREA;
                    }
                    passageColumn[passage1] = column;
                    break;
                case 3:
                    for (row = passageRow[passage1] + 1; row < passageRow[passage1] + 2; row++)
                    {
                        place[row, passageColumn[passage1]] = TROUT.PASSAGEAREA;
                    }
                    passageRow[passage1] = row;
                    break;
            }
            switch (passage2 % 4)
            {
                case 0:
                    for (row = passageRow[passage2] - 1; row > passageRow[passage2] - 2; row--)
                    {
                        place[row, passageColumn[passage2]] = TROUT.PASSAGEAREA;
                    }
                    passageRow[passage2] = row;
                    break;
                case 1:
                    for (column = passageColumn[passage2] - 1; column > passageColumn[passage2] - 2; column--)
                    {
                        place[passageRow[passage2], column] = TROUT.PASSAGEAREA;
                    }
                    passageColumn[passage2] = column;
                    break;
                case 2:
                    for (column = passageColumn[passage2] + 1; column < passageColumn[passage2] + 2; column++)
                    {
                        place[passageRow[passage2], column] = TROUT.PASSAGEAREA;
                    }
                    passageColumn[passage2] = column;
                    break;
                case 3:
                    for (row = passageRow[passage2] + 1; row < passageRow[passage2] + 2; row++)
                    {
                        place[row, passageColumn[passage2]] = TROUT.PASSAGEAREA;
                    }
                    passageRow[passage2] = row;
                    break;
            }

            place[passageRow[passage1], passageColumn[passage1]] = TROUT.PASSAGE;
            place[passageRow[passage2], passageColumn[passage2]] = TROUT.PASSAGE;

            // Passage1とPassage2を繋げる
            int rowTemp = passageRow[passage1] - passageRow[passage2];
            int columnTemp = passageColumn[passage1] - passageColumn[passage2];
            int order = 0;// 最初に設置していくのは縦か横かどっちかを決める変数
            int roopCheck = 0;
            while (rowTemp != 0 || columnTemp != 0)
            {
                for (int k = 0; k <= 20; k++)
                {
                    switch (order)
                    {
                        case 0:
                            if (rowTemp > 0)
                            {
                                for (int i = rowTemp; i > 0; i--)
                                {
                                    // もしROOMAREAだったりPASSAGEAREAだったりしたら一時中断
                                    if (place[passageRow[passage1] - 1, passageColumn[passage1]] == TROUT.ROOMAREA || place[passageRow[passage1] - 1, passageColumn[passage1]] == TROUT.PASSAGEAREA)
                                    {
                                        break;
                                    }
                                    // Rowをずらして、ずらした先でTROUT.PASSAGEを設置
                                    passageRow[passage1]--;
                                    place[passageRow[passage1], passageColumn[passage1]] = TROUT.PASSAGE;
                                    rowTemp--;
                                }
                            }
                            else if (rowTemp < 0)
                            {
                                for (int i = rowTemp; i < 0; i++)
                                {
                                    if (place[passageRow[passage1] + 1, passageColumn[passage1]] == TROUT.ROOMAREA || place[passageRow[passage1] + 1, passageColumn[passage1]] == TROUT.PASSAGEAREA)
                                    {
                                        break;
                                    }
                                    passageRow[passage1]++;
                                    place[passageRow[passage1], passageColumn[passage1]] = TROUT.PASSAGE;
                                    rowTemp++;
                                }
                            }
                            order = 1;
                            break;
                        case 1:
                            if (columnTemp > 0)
                            {
                                for (int i = columnTemp; i > 0; i--)
                                {
                                    // もしROOMAREAだったりPASSAGEAREAだったりしたら一時中断
                                    if (place[passageRow[passage1], passageColumn[passage1] - 1] == TROUT.ROOMAREA || place[passageRow[passage1], passageColumn[passage1] - 1] == TROUT.PASSAGEAREA)
                                    {
                                        break;
                                    }
                                    // Columnをずらして、ずらした先でTROUT.PASSAGEを設置
                                    passageColumn[passage1]--;
                                    place[passageRow[passage1], passageColumn[passage1]] = TROUT.PASSAGE;
                                    columnTemp--;
                                }
                            }
                            else if (columnTemp < 0)
                            {
                                for (int i = columnTemp; i < 0; i++)
                                {
                                    if (place[passageRow[passage1], passageColumn[passage1] + 1] == TROUT.ROOMAREA || place[passageRow[passage1], passageColumn[passage1] + 1] == TROUT.PASSAGEAREA)
                                    {
                                        break;
                                    }
                                    passageColumn[passage1]++;
                                    place[passageRow[passage1], passageColumn[passage1]] = TROUT.PASSAGE;
                                    columnTemp++;
                                }
                            }
                            order = 0;
                            break;
                    }



                }

                rowTemp = passageRow[passage1] - passageRow[passage2];
                columnTemp = passageColumn[passage1] - passageColumn[passage2];

                // 最短距離移動が不可になった時、TROUT.PASSAGEをROOMAREAを避けて設置する処理
                if (rowTemp != 0 || columnTemp != 0)
                {
                    for (int k = 0; k <= 30; k++)
                    {
                        if (place[passageRow[passage1] - 1, passageColumn[passage1]] == TROUT.ROOMAREA || place[passageRow[passage1] - 1, passageColumn[passage1]] == TROUT.PASSAGEAREA)
                        {
                            passageColumn[passage1]++;
                            place[passageRow[passage1], passageColumn[passage1]] = TROUT.PASSAGE;
                        }
                        else if (place[passageRow[passage1] + 1, passageColumn[passage1]] == TROUT.ROOMAREA || place[passageRow[passage1] + 1, passageColumn[passage1]] == TROUT.PASSAGEAREA)
                        {
                            passageColumn[passage1]--;
                            place[passageRow[passage1], passageColumn[passage1]] = TROUT.PASSAGE;
                        }
                        else if (place[passageRow[passage1], passageColumn[passage1] - 1] == TROUT.ROOMAREA || place[passageRow[passage1], passageColumn[passage1] - 1] == TROUT.PASSAGEAREA)
                        {
                            passageRow[passage1]++;
                            place[passageRow[passage1], passageColumn[passage1]] = TROUT.PASSAGE;
                        }
                        else if (place[passageRow[passage1], passageColumn[passage1] + 1] == TROUT.ROOMAREA || place[passageRow[passage1], passageColumn[passage1] + 1] == TROUT.PASSAGEAREA)
                        {
                            passageRow[passage1]--;
                            place[passageRow[passage1], passageColumn[passage1]] = TROUT.PASSAGE;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                rowTemp = passageRow[passage1] - passageRow[passage2];
                columnTemp = passageColumn[passage1] - passageColumn[passage2];

                roopCheck++;

                if(roopCheck >= 50)
                {
                    break;
                }
            }
        }
    }
}
