using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatusUIManager : MonoBehaviour {

    [SerializeField]
    Text hpText;
    GameObject player;
    private bool isStarting = true;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(isStarting)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            isStarting = false;
        }
        UpdateHPText();
	}

    private void UpdateHPText()
    {
        if (hpText.GetComponent<Text>().text != "HP:" + player.GetComponent<PlayerInfo>().hp)
        {
            hpText.GetComponent<Text>().text = "HP:" + player.GetComponent<PlayerInfo>().hp;
        }
    }
}
