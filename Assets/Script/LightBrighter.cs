using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBrighter : MonoBehaviour {

    Light light;
    public float MaxRange;

    public float InitIntensity = 1.0f;
    public float IntensityFlickRangeValue = 1.0f;

    public float Brightdelay = 1.0f;

    float delta = 0.0f;

    Coroutine Coroutine_LightSmoothRangeExpender;

    // 점차적으로 조명의 intensity 값을 증가시키는 코루틴
    IEnumerator LightSmoothRangeExpender(float delay)
    {
        // 한번에 얼마나 델타값이 증가하면 되는지 미리 계산
        float deltaStepValue = 1 / MaxRange;

        // MaxBright 단계에 걸쳐 조명 범위를 넓어지게 만들 것
        // 이때 델타값을 사용하자.
        for (int i = 0; i < MaxRange; ++i )
        {

            delta += deltaStepValue;

            light.range = Mathf.SmoothStep(0.0f, MaxRange, delta);
            yield return new WaitForSeconds(delay);

        }


    }


	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();

        //코루틴 시작!
        Coroutine_LightSmoothRangeExpender = StartCoroutine(LightSmoothRangeExpender(Brightdelay));


    }
	
	// Update is called once per frame
	void Update () {

        // 조명이 은은하게 밝아졌다 줄어들었다 할 수 있도록
        // 이 때 x 값에 비례해서 주기를 변경하자. ( 복도 길이에 따라 다른 주기로 깜빡이도록 )
        light.intensity = InitIntensity + ( ( 0.5f +Mathf.Sin (Time.time + transform.position.z*10*Mathf.PI ) *0.5f ) * IntensityFlickRangeValue );

	}
}
