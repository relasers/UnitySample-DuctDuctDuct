using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour {

    public int hp = 1;
    public GameObject Particle;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // 체력이 0이 되면 사망
        if (hp <= 0)
        {

            for (int i = 0; i < 100; ++i)
            {
                GameObject NewParticle = Instantiate(Particle,
                // 플레이어 전방 100미터
                // 복도 내부 랜덤한 위치에서 생성
                transform.position,
                Quaternion.identity
               );
                transform.localScale = new Vector3(2, 2, 2);
                // 본인 색깔의 파티클로.
                MeshRenderer particles_meshRenderer = NewParticle.GetComponentInChildren<MeshRenderer>();

                if(particles_meshRenderer)
                    particles_meshRenderer.material.color = transform.GetComponent<MeshRenderer>().material.color;

            }

            // 만약 내가 붉은 색이라면?
            if (GetComponent<EnemyCubeBehavior>().ColorType == CubeColorType.RED)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                SpaceShipController controller;
                if (player)
                {
                    controller = player.GetComponent<SpaceShipController>();

                    if (controller)
                        controller.missile_catridge++;
                }

                

            }

            Destroy(gameObject);
        }

	}


}
