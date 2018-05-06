using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSliderFowarder : MonoBehaviour {

    float Floor_Scale_x;
    MeshRenderer renderer;

    // 0에서서 1까지 
    float delta = 0;

    // Use this for initialization
    void Start () {
        Floor_Scale_x = Mathf.Abs(transform.position.x);

       //재질에 접근하기 위한 컴퍼넌트를 받아온다.
       renderer = GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        // 1 보다 클때까지 더하고, 
        if (delta < 1)
        {
            delta += Time.deltaTime*0.25f;

        }

        delta = Mathf.Min(delta, 1);

        // 0~1 ,,처음엔 급하게 오르다 1로갈수록 천천히 올라가게 된다.
        float calculatedvalue = Mathf.Sqrt(1 - (1 - delta) * (1 - delta));


        // 생성되는 순간은 초록색이다.
        renderer.material.color
           = new Color(
                 renderer.material.color.r,
                   renderer.material.color.g,
                    calculatedvalue,
                     renderer.material.color.a
               );

        // 위치를 재위치시킨다.

        transform.position =
            new Vector3
            (
                calculatedvalue * Floor_Scale_x-1000,
                transform.position.y,
                transform.position.z
                );
    }
}
