using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongInCorridor : MonoBehaviour {

    public Vector3 minPosition;
    public Vector3 maxPosition;
    public Vector3 delta;

    // Use this for initialization
    void Start () {

        // 초기 최소 최대 위치는 객체의 초기 위치로 초기화
        minPosition = transform.position;
        maxPosition = transform.position;

        delta =
            new Vector3(
               Mathf.InverseLerp(minPosition.x, maxPosition.x, transform.position.x),
               Mathf.InverseLerp(minPosition.y, maxPosition.y, transform.position.y),
               Mathf.InverseLerp(minPosition.z, maxPosition.z, transform.position.z)
             );

        // 외부로부터 게임 매니저 정보를 받아온다.
        GameObject GameManager = GameObject.Find("GameManager");

    }
	
	// Update is called once per frame
	void Update () {

        delta =
            new Vector3(
                Mathf.PingPong(Time.time*0.5f, 1),
                Mathf.PingPong(Time.time * 0.5f, 1),
                Mathf.PingPong(Time.time * 0.5f, 1)
             );

        

        transform.position = new Vector3(
             Mathf.SmoothStep(minPosition.x, maxPosition.x, delta.x),
             Mathf.SmoothStep(minPosition.y, maxPosition.y, delta.y),
             Mathf.SmoothStep(minPosition.z, maxPosition.z, delta.z)
        );
    }
}
