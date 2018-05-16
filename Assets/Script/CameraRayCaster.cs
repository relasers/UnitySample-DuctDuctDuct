using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCaster : MonoBehaviour {

    public Transform BarrelTransform;
    public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Debug.DrawRay(transform.position, transform.forward * 200.0f, Color.red);

        RaycastHit target;

        // 카메라 정면방향으로 레이캐스팅
        if (Physics.Raycast(transform.position, transform.forward, out target, 1000.0f)) 
        {
            // 충돌지점 정면으로 방향을 바꾸자.
            BarrelTransform.LookAt(target.point);
            Debug.DrawRay(BarrelTransform.position, BarrelTransform.forward * 1000.0f, Color.cyan); 
        }


    }
}
