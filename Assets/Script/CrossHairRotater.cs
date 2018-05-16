using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairRotater : MonoBehaviour {

    RectTransform rect_transform;

	// Use this for initialization
	void Start () {
        rect_transform = GetComponent<RectTransform>();

    }
	
	// Update is called once per frame
	void Update () {
        rect_transform.Rotate(new Vector3(0,0,1), 64*Time.deltaTime);
	}
}
