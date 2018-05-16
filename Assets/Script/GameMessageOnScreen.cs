using System.Collections;
using System.Collections.Generic;
// TEXT 컴퍼넌트에 접근하기 위해 필요
using UnityEngine.UI;
using UnityEngine;

public class GameMessageOnScreen : MonoBehaviour {
    Text UItext;
    public GameObject player;
    public GameObject boss;
    // Use this for initialization
    void Start () {
        UItext = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        SpaceShipController controller = player.GetComponent<SpaceShipController>();

        if (controller)
        {
            if(!controller.isAlive)
                UItext.text = "Wasted";
        }

        if (!boss)
        {
            UItext.text = "GameClear";
        }
    }
}
