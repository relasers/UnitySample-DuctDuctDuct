using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCaster : MonoBehaviour {

    public Transform BarrelTransform;
    public Transform AutoTarget;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, transform.forward * 200.0f, Color.red);

        RaycastHit target;

        // 카메라 정면방향으로 레이캐스팅
        if (Physics.Raycast(transform.position, transform.forward, out target, 1000.0f))
        {
            // 충돌지점 정면으로 방향을 바꾸자.
            BarrelTransform.LookAt(target.point);
            Debug.DrawRay(BarrelTransform.position, BarrelTransform.forward * 1000.0f, Color.cyan);
        }

        if (Input.GetMouseButton(0))
        {
            Ray RayToScreen;
            RaycastHit Ray_target;
            RayToScreen = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(RayToScreen, out Ray_target))
            {
                if (Ray_target.transform.tag == "Enemy")
                {
                    if (AutoTarget)
                    {
                        AutoTarget.GetComponent<EnemyStat>().AutoTargeted = false;
                        AutoTarget = null;
                    }

                    AutoTarget = Ray_target.transform;

                    if (AutoTarget)
                    {
                        EnemyStat stat = AutoTarget.GetComponent<EnemyStat>();
                        if(stat)
                            stat.AutoTargeted = true;
                    }
                }
            }

        }

        if (AutoTarget)
        {

            // 충돌지점 정면으로 방향을 바꾸자.
            BarrelTransform.LookAt(AutoTarget.transform.position);
            Debug.DrawRay(BarrelTransform.position, BarrelTransform.forward * 1000.0f, Color.cyan);

            EnemyStat stat = AutoTarget.GetComponent<EnemyStat>();

            if(stat)
            if (!stat.AutoTargeted) AutoTarget = null;

        }


    }



}
