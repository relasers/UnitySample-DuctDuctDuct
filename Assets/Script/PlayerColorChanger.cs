using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChanger : MonoBehaviour {

    Transform player_spaceships_transform;
    Light[] lights;

    bool Danger_is_Near;
    bool Collide_On_Wall;
    // Use this for initialization
    void Start () {
        player_spaceships_transform = GetComponentInParent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        // 적들의 정보를 받아온다.
         GameObject[] Enemies;

        // 형제 컴퍼넌트의 라이트를 받기위해 이 코드를 작성하였다.
        // 더 나은 코드 응답, 응답바람. ★
        lights = transform.parent.GetComponentsInChildren<Light>();

        Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Danger_is_Near = false;
        // 적 큐브가 내 뒤에 있으면 분홍색으로
        foreach (GameObject enemy in Enemies)
        {
            if (player_spaceships_transform.position.z > enemy.transform.position.z)
            {
                Danger_is_Near = true;
                
                break;
            }
        }
        MeshRenderer mesh_renderer = GetComponentInParent<MeshRenderer>();
        if (Danger_is_Near)
        {
            // 분홍
            mesh_renderer.material.color = new Color(0.8f, 0.35f, 0.39f, 1.0f);
            ChangeLightColorToMaterialColor();

        }
        else
        {
            mesh_renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            ChangeLightColorToMaterialColor();
        }

        if (Collide_On_Wall)
        {
            mesh_renderer.material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            ChangeLightColorToMaterialColor();
        }


    }



    private void OnTriggerEnter(Collider other)
    {
        // 벽에 부딫힌 상태면 색상을 변경하자.
        if (other.gameObject.tag == "SolidBlock")
        {
            Collide_On_Wall = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "SolidBlock")
        {
            Collide_On_Wall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SolidBlock")
        {
            Collide_On_Wall = false;
        }
    }

    void ChangeLightColorToMaterialColor()
    {
        MeshRenderer mesh_renderer = GetComponentInParent<MeshRenderer>();
        foreach (Light light in lights)
        {
            light.color = mesh_renderer.material.color;
        }
    }
}
