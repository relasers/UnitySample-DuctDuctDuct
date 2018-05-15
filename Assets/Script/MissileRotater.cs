using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileRotater : MonoBehaviour {

    public float RotateSpeed = 50.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(0,1,0),RotateSpeed);

	}
    private void OnDestroy()
    {
        TrailRenderer Trail = GetComponent<TrailRenderer>();

        Trail.transform.parent = null;
        Trail.autodestruct = true;
        //Trail = null;

    }
}
