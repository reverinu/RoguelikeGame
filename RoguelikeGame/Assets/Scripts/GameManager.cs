using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    SettingManager settingManager;

	// Use this for initialization
	void Start () {
        // Trout配置
        //settingManager.GetComponent<SettingManager>().SetTrout();
        // アイテム配置
        // トラップ配置
        // モンスター配置
        // 階段配置
        // キャラ配置

        settingManager.GetComponent<SettingManager>().SetAll();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
