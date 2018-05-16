using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject boss;
    bool boss_killed_switch = false;
    Coroutine coroutine_GameEnd;
    Coroutine coroutine_GameRestart;

    public float DelayTime = 5.0f;

    IEnumerator GameExit(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }

    IEnumerator GameRestart(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.LoadLevel(Application.loadedLevel);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // 보스 없으면 게임 종료 루틴 실행
        if (!boss && !boss_killed_switch)
        {
            boss_killed_switch = true;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }

            GameEnd();
        }

    }

    public void GameRestart()
    {
        coroutine_GameRestart = StartCoroutine(GameRestart(DelayTime));
    }

    public void GameEnd()
    {
        coroutine_GameEnd = StartCoroutine(GameExit(DelayTime));
    }

}
