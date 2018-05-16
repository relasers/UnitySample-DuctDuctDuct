using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject cube;
    public GameObject player;

    public float spawn_delay = 1.0f;
    public float spawn_red_delay = 10.0f;

    public int WallWidth = 50;
    public int WallHeight = 50;



    // 적 큐브를 생성하는 코루틴
    Coroutine coroutine_enemySpawn;
    // 붉은 큐브를 10초마다 생성하는 코루틴
    Coroutine coroutine_enemyRedSpawn;

    // 벽을 생성하는 월 빌더 컴퍼넌트로부터 
    // 복도의 크기를 받아온다.
    CorriderWallBuilder WallBuilder;
    // 플레이어 리스포너 컴퍼넌트로 부터 플레이어의 정보를 받아온다.
    PlayerRespawner PlayerRespawner;

    IEnumerator SpawnEnemyCube(float delay)
    {

        while (true)
        {


            GameObject NewCube = Instantiate(cube,
                 // 플레이어 전방 100미터
                 // 복도 내부 랜덤한 위치에서 생성
                 new Vector3(Random.Range(-WallHeight * 0.4f, WallHeight * 0.4f),
                 Random.Range(WallHeight * 0.2f, WallHeight * 0.8f),
                 Mathf.Min(PlayerRespawner.Player.transform.position.z + 100, 490)
                 ),
                 Quaternion.identity
                );
            NewCube.transform.localScale *= Random.Range(0.8f,1.2f);

            yield return new WaitForSeconds(delay);
        }

    }

    IEnumerator SpawnEnemyRedCube(float delay)
    {

        while (true)
        {

            yield return new WaitForSeconds(delay);

            GameObject NewCube = Instantiate(cube,
                 // 플레이어 전방 100미터
                 // 복도 내부 랜덤한 위치에서 생성
                 new Vector3(Random.Range(-WallHeight * 0.4f, WallHeight * 0.4f),
                 Random.Range(WallHeight * 0.2f, WallHeight * 0.8f),
                 Mathf.Min(PlayerRespawner.Player.transform.position.z + 100, 490)

                 ),
                 Quaternion.identity
                );


            NewCube.transform.localScale *= Random.Range(1.5f, 2.0f);
            NewCube.GetComponent<EnemyCubeBehavior>().ColorType = CubeColorType.RED;
            NewCube.GetComponent<EnemyCubeBehavior>().SetCubeColor();
            
        }

    }

    // Use this for initialization
    void Start () {

        WallBuilder = transform.GetComponent<CorriderWallBuilder>();
        PlayerRespawner = transform.GetComponent<PlayerRespawner>();


        if (WallBuilder)
        {
            WallWidth = WallBuilder.WallWidth;
            WallHeight = WallBuilder.WallHeight;
        }

        coroutine_enemySpawn = StartCoroutine(SpawnEnemyCube(spawn_delay));
        coroutine_enemyRedSpawn = StartCoroutine(SpawnEnemyRedCube(spawn_red_delay));
    }

    // Update is called once per frame
    void Update () {
		

	}
}


//private GameObject[] gameobjectz;
//gameobjectz = GameObject.FindGameObjectsWithTag("Light");
//Debug.Log("Light Num :: " + gameobjectz.Length);