using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInfo : CharaInfo {

    public int level;
    public int totalExperienceValue;
    public List<GameObject> itemList;
    public GameObject protector;
    public GameObject weapon;
    public int money;
	// 経験値に関しては別スクリプトで管理
}
