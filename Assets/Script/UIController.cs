using System.Collections;
using System.Collections.Generic;
// TEXT 컴퍼넌트에 접근하기 위해 필요
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour {

    Text UItext;
    public GameObject player;
	// Use this for initialization
	void Start () {
        UItext = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        SpaceShipController controller = player.GetComponent<SpaceShipController>();

        if (controller)
        {
            UItext.text = "Ammo :: " + controller.ammo + "\n" + "Special :: " + controller.missile_catridge;

        }


    }
}
